using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.ViewModels.UserCommunicationController;

namespace VideoSite.Controllers
{
    public class UserCommunicationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserCommunicationController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        /// <summary>
        /// Action for inspect user.
        /// Example path: [domain]/user/[User Id]
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Returns wanted user for anonoymous users</returns>
        public async Task<IActionResult> GetUser(string id)
        {
            return Ok(new GetUserViewModel(await userManager.FindByNameAsync(id)));
        }
    }
}
