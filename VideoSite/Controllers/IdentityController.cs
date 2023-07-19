using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToolsLayer.FileManagement;
using VideoSite.ViewModels.IdentityController;

namespace VideoSite.Controllers
{
    public class IdentityController : Controller
    {
        public readonly SignInManager<ApplicationUser> signmanager;
        public readonly UserManager<ApplicationUser> userManager;
        public readonly RoleManager<IdentityRole> roleManager;
        public readonly ADC db;
        public IdentityController(ADC db,SignInManager<ApplicationUser> signmanger,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.signmanager = signmanger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
        }
        /*
        UserName= "ss36",
        Email="ss@gmail.com",
        PhoneNumber="905511351858",
        password:"123Ss+"
         */
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
            return Ok(await signmanager.PasswordSignInAsync(userName: model.UserName, password: model.Password, isPersistent: true, false));
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new ApplicationUser() {
                Email= model.Email,
                UserName= model.UserName,
                ImageLink = await model.ProfileImage.SaveFileAsync(Path.Combine("content", "profile")),
                EmailConfirmed=true,
                };
                model.Categories.ForEach(x => user.UserM2MCategory.Add(new EntityLayer.Models.M2MRelationships.UserM2MCategory() { CategoryId = x }));
                var result=await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "USER");
                await signmanager.SignInAsync(user,true);
                return Ok(result);
            }
            return Ok(new{ succeeded = false });
        }
        public async Task<IActionResult> Logout()
        {
            await signmanager.SignOutAsync();
            return Ok();
        }
        #region helpers
        private async Task<ApplicationUser?> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);
        #endregion
    }
}
