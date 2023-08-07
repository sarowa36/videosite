using EntityLayer.Models.Identity;

namespace VideoSite.ViewModels.UserCommunicationController
{
    public class GetUserViewModel
    {
        public GetUserViewModel(ApplicationUser? user=null)
        {
            if (user != null)
            {
                this.Id= user.Id;
                this.UserName = user.UserName;
                this.ImageLink= user.ImageLink;
            }
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string ImageLink{ get; set; }
    }
}
