using EntityLayer.Models.Identity;

namespace VideoSite.ViewModels.VerifyEmailHub
{
    public class UserTokenViewModel
    {
        public ApplicationUser User { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
    }
}
