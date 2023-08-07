using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolsLayer.FileManagement;
using ToolsLayer.List;
using VideoSite.ViewModels.IdentityController;
using EntityLayer.Models.M2MRelationships;
using VideoSite.Helpers;
using VideoSite.Hubs;
using Microsoft.AspNetCore.SignalR;
using VideoSite.InMemoryData;

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
        /*
        UserName= "ss36",
        Email="ss@gmail.com",
        PhoneNumber="905511351858",
        password:"123Ss+"
         */
        public async Task<IActionResult> d()
        {
            await signmanager.SignInAsync(await userManager.FindByNameAsync("navysoldier"), true);
            return Ok();
        }
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
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signmanager.PasswordSignInAsync(userName: model.UserName, password: model.Password, isPersistent: true, false);
                if(result.Succeeded)
                return Ok(result);
                else
                    return Ok(new {modelOnly=new List<string>() {"Kullanıcı Adı Yada Şifre Geçersiz" } });
            }
            return Ok(ModelState.ListInvalidValueErrors());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    ImageLink = await model.ProfileImage.SaveFileAsync(Path.Combine("content", "profile")),
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };
                model.Categories.ForEach(x => user.UserM2MCategory.Add(new EntityLayer.Models.M2MRelationships.UserM2MCategory() { CategoryId = x }));
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) { 
                await userManager.AddToRoleAsync(user, "USER");
                await signmanager.SignInAsync(user, true);
                return Ok(result);
                }
                else
                {
                    return Ok(new { modelOnly = result.Errors.Select(x => x.Description) });
                }
            }
            return Ok(ModelState.ListInvalidValueErrors());
        }
        public async Task<IActionResult> Logout()
        {
            await signmanager.SignOutAsync();
            return Ok();
        }
        public async Task<IActionResult> ProfileEdit()
        {
            var a = User.Identity.Name;
            var user = db.Users.AsQueryable().Include(x => x.UserM2MCategory).ThenInclude(x => x.Category).FirstOrDefault(x => x.UserName == a);
            return Ok(new ProfileEditViewModel() { Categories = user.UserM2MCategory.Select(x => x.CategoryId).ToList(), ImageLink = user.ImageLink });
        }
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(ProfileEditViewModel model)
        {
            var a = User.Identity.Name;
            var user = db.Users.AsQueryable().Include(x => x.UserM2MCategory).FirstOrDefault(x => x.UserName == a);
            if (model.ProfileImage != null)
                user.ImageLink = await model.ProfileImage.SaveFileAsync(Path.Combine("content", "profile"));
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
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new { Succeeded = true });
                }
                else
                {
                    return Ok(new { modelOnly = result.Errors.Select(x => x.Description).ToList() });
                }
            }
            else
            {
                return Ok(ModelState.ListInvalidValueErrors());
            }
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordToken(string token)
        {
            var userTokenSource = UserTokenSource.UserTokenViewModels.Find(x => x.Token == token);
            if (userTokenSource != null)
            {
                await verifyEmailHub.Clients.Client(userTokenSource.ClientId).SendAsync("emailVerified",userTokenSource.Token);
            }
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> RefreshPassword(RefreshPasswordModel model)
        {
            var user = UserTokenSource.UserTokenViewModels.Find(x => x.Token == model.Token);
            if (ModelState.IsValid && user != null)
            {
                var result = await userManager.ResetPasswordAsync(user.User, model.Token, model.Password);
                if (result.Succeeded) {
                    UserTokenSource.UserTokenViewModels.Remove(UserTokenSource.UserTokenViewModels.Find(x => x.User.Id == user.User.Id));
                    return Ok(new { succeeded = true }); }
                else
                {
                    return Ok(new { modelOnly = result.Errors.Select(x => x.Description) });
                }
            }
            return Ok(ModelState.ListInvalidValueErrors());
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string guid)
        {
            var val= EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Find(x=>x.Guid== guid);
            if (val != null)
            {
                await verifyEmailHub.Clients.Client(val.ClientId).SendAsync("emailAccepted");
                EmailVertificationWaitingClientsSource.EmailVertificationWaitingClientsViewModel.Remove(val);
                return Ok(new { succeeded = true });
            }
            else
                return Ok(new {succeeded=false});
        }
        #region helpers
        private async Task<ApplicationUser?> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);
        #endregion
    }
}
