using EntityLayer.Models.Identity;

namespace EntityLayer.ViewModels.UserController
{
    public class UserGetViewModel
    {
        public UserGetViewModel(ApplicationUser user,List<string> roles)
        {
            this.Id= user.Id;
            this.Roles = roles;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.LockOut = user.LockoutEnabled;
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool LockOut { get; set; }
        public List<string> Roles { get; set; }
    }
}
