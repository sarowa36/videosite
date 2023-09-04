using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.IdentityController
{
    public class ChangePasswordViewModelValidator:AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(x => x.OldPassword).Password();
            RuleFor(x=>x.NewPassword).Password();
            RuleFor(x=>x.NewPasswordConfirm).Compare(x=>x.NewPassword);
        }
    }
}
