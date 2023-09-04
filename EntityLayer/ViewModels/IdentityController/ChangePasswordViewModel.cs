using System.ComponentModel.DataAnnotations;

namespace EntityLayer.ViewModels.IdentityController
{
    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}
