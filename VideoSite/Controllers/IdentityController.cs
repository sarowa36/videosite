using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolsLayer.FileManagement;
using EntityLayer.ViewModels.IdentityController;
using EntityLayer.Models.M2MRelationships;
using VideoSite.Helpers;
using VideoSite.Hubs;
using Microsoft.AspNetCore.SignalR;
using VideoSite.InMemoryData;
using ToolsLayer.List;
using EntityLayer;
using BusinessLayer.Validators.ViewModels.IdentityController;

namespace VideoSite.Controllers
{
    public class IdentityController : Controller
    {
        public readonly SignInManager<ApplicationUser> signmanager;
        public readonly UserManager<ApplicationUser> userManager;
        public readonly RoleManager<IdentityRole> roleManager;
        public readonly ADC db;
        public readonly IHubContext<VerifyEmailHub> verifyEmailHub;
        public IdentityController(ADC db, SignInManager<ApplicationUser> signmanager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHubContext<VerifyEmailHub> verifyEmailHub)
        {
            this.signmanager = signmanager;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
            this.verifyEmailHub = verifyEmailHub;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var validationResult = new RegisterViewModelValidator().Validate(model);
            if (validationResult.IsValid)
            {
                var user = model.AsApplicationUser();
                user.ImageLink = await model.ProfileImage.SaveFileAsync(Path.Combine("content", "profile"));

                if (model.Categories != null && model.Categories.Count > 0)
                    user.UserM2MCategory.AddRange(model.Categories.Select(x => new UserM2MCategory() { CategoryId = x }));
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                    await signmanager.SignInAsync(user, true);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { modelOnly = result.Errors.Select(x => x.Description) });
                }
            }
            return BadRequest(validationResult.ListInvalidValueErrors());
        }
        /*
         For quick test 
         public async Task<IActionResult> d()
        {
            await signmanager.SignInAsync(await userManager.FindByNameAsync("navysoldier"), true);
            return Ok();
        }*/
        public async Task<IActionResult> GetUser()
        {

            var user = await GetCurrentUserAsync();
            if (user != null)
                return Ok(
                    new UserViewModel(
                        user,
                        (await userManager.GetRolesAsync(user)).ToList()
                      )
                    );
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var modelState = new LoginViewModelValidator().Validate(model);
            if (modelState.IsValid)
            {
                var result = await signmanager.PasswordSignInAsync(userName: model.UserName, password: model.Password, isPersistent: true, false);
                if (result.Succeeded)
                    return Ok(result);
                else
                    return BadRequest(new { modelOnly = new List<string>() { "Kullanıcı Adı Yada Şifre Geçersiz" } });
            }
            return BadRequest(modelState.ListInvalidValueErrors());
        }

        public async Task<IActionResult> Logout()
        {
            await signmanager.SignOutAsync();
            return Ok();
        }
        public async Task<IActionResult> ProfileEdit()
        {
            var user = db.Users.AsQueryable().Include(x => x.UserM2MCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.UserName == User.Identity.Name);
            return Ok(new ProfileEditViewModel() { Categories = user.UserM2MCategory.Select(x => x.CategoryId).ToList(), ImageLink = user.ImageLink });
        }
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileEditViewModel model)
        {
            var modelState = new ProfileEditViewModelValidator().Validate(model);
            if (modelState.IsValid)
            {
                var user = db.Users.AsQueryable().Include(x => x.UserM2MCategory).FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (model.ProfileImage != null)
                    user.ImageLink = await model.ProfileImage.SaveFileAsync(Path.Combine("content", "profile"));
                //Changin Favoruite Categories
                user.UserM2MCategory.Select(x => x.CategoryId).ToList().RemoveBeforeAddNew(model.Categories,
                    (IEnumerable<int> remove) =>
                    {
                        db.UserM2MCategories.RemoveRange(db.UserM2MCategories.Where(x => x.UserId == user.Id && remove.Contains(x.CategoryId)));
                    },
                    (IEnumerable<int> addlist) =>
                    {
                        db.UserM2MCategories.AddRange(addlist.Select(x => new UserM2MCategory() { CategoryId = x, UserId = user.Id }));
                    });

                db.SaveChanges();
                return Ok();
            }
            return BadRequest(modelState.ListInvalidValueErrors());
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var modelState = new ChangePasswordViewModelValidator().Validate(model);
            if (modelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new { Succeeded = true });
                }
                else
                {
                    return BadRequest(new { modelOnly = result.Errors.Select(x => x.Description).ToList() });
                }
            }
            return BadRequest(modelState.ListInvalidValueErrors());
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordToken(string token)
        {
            var userTokenSource = UserTokenSource.UserTokenViewModels.Find(x => x.Token == token);
            if (userTokenSource != null)
            {
                await verifyEmailHub.Clients.Client(userTokenSource.ClientId).SendAsync("emailVerified", userTokenSource.Token);
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> RefreshPassword(RefreshPasswordModel model)
        {
            var user = UserTokenSource.UserTokenViewModels.Find(x => x.Token == model.Token);
            var modelState = new RefreshPasswordModelValidator().Validate(model);
            if (modelState.IsValid && user != null)
            {
                var result = await userManager.ResetPasswordAsync(user.User, model.Token, model.Password);
                if (result.Succeeded)
                {
                    UserTokenSource.UserTokenViewModels.Remove(UserTokenSource.UserTokenViewModels.Find(x => x.User.Id == user.User.Id));
                    return Ok(new { succeeded = true });
                }
                else
                {
                    return BadRequest(new { modelOnly = result.Errors.Select(x => x.Description) });
                }
            }
            return BadRequest(modelState.ListInvalidValueErrors());
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string guid)
        {
            var val = EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Find(x => x.Guid == guid);
            if (val != null)
            {
                await verifyEmailHub.Clients.Client(val.ClientId).SendAsync("emailAccepted");
                EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Remove(val);
                return Ok();
            }
            else
                return BadRequest();
        }
        #region helpers
        [NonAction]
        private async Task<ApplicationUser?> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);
        #endregion
    }
}
