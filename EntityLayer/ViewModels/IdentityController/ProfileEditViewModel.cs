using Microsoft.AspNetCore.Http;
using ToolsLayer.FileManagement;

namespace EntityLayer.ViewModels.IdentityController
{
    public class ProfileEditViewModel
    {
        public List<int> Categories { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string? ImageLink { get; set; }
    }
}
