using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Http;

namespace EntityLayer.ViewModels.IdentityController
{
    public class ProfileEditViewModel
    {
        public List<int> Categories { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ImageLink { get; set; }
    }
}
