using System.ComponentModel.DataAnnotations;

namespace VideoSite.ViewModels.IdentityController
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; }
    }
}
