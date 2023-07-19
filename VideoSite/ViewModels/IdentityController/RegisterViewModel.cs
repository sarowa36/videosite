using EntityLayer.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace VideoSite.ViewModels.IdentityController
{
    public class RegisterViewModel
    {
        [MaxLength(16),MinLength(8),Required]
        public string UserName { get; set; }
        [MaxLength(16), MinLength(6),DataType(DataType.Password),Required]
        public string Password { get; set; }
        [Compare("Password")]
        public string Password2 { get; set; }
        [MaxLength(32), MinLength(8), DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }
        [Required]
        public IFormFile ProfileImage { get; set; }
        public List<int> Categories { get; set; }
    }
}
