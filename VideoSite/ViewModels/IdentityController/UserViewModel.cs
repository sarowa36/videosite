using EntityLayer.Models.Identity;

namespace VideoSite.ViewModels.IdentityController
{
    public class UserViewModel
    {
        public UserViewModel(ApplicationUser user,List<string> roles)
        {
            this.Name = user.UserName;
            this.Roles=roles;
            this.ImageLink = user.ImageLink;
            this.Id=user.Id;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public List<string> Roles { get; set; }
    }
}
