using DataAccessLayer;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using EntityLayer.Models.M2MRelationships;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.ViewModels.UserController;

namespace VideoSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ADC db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(ADC db,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.db= db;
            this.userManager= userManager;
            this.roleManager= roleManager;
        }
        public async Task<IActionResult> GetList()
        {
            var returnValue = new List<UserGetViewModel>();
            foreach (var item in userManager.Users.ToList())
            {
                returnValue.Add(new UserGetViewModel( item, (await userManager.GetRolesAsync(item)).ToList()));
            } 
            return Ok(returnValue);
        }
        public async Task<IActionResult> AllRoles() => Ok(roleManager.Roles.Select(x => x.Name).ToList());
        public async Task<IActionResult> GetUserRoles(string id) => Ok(await userManager.GetRolesAsync(await userManager.FindByIdAsync(id)));
        public async Task<IActionResult> ToggleLock(string id)
        {
            var user=await userManager.FindByIdAsync(id);
            if (!user.LockoutEnabled)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            else
            {
                user.LockoutEnd = null;
            }
            user.LockoutEnabled=!user.LockoutEnabled;
            await userManager.UpdateAsync(user);
            return Ok();
        }
        public async Task<IActionResult> UpdateUserRoles(string userId,List<string> roles)
        {
            var neforeroles = await userManager.GetRolesAsync(await userManager.FindByIdAsync(userId));
            var removeList = neforeroles.Except(roles);
            var addlist = roles.Except(neforeroles);
            var user= await userManager.FindByIdAsync(userId);
            if (addlist != null && addlist.Count() > 0)
            {
                await userManager.AddToRolesAsync(user, roles);
            }
            if (removeList != null && removeList.Count() > 0)
            {
                await userManager.RemoveFromRolesAsync(user, roles);
            }
            return Ok();
        }
    }
}
