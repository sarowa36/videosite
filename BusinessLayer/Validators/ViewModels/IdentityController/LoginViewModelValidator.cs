using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.IdentityController
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x=>x.UserName).UserName();
            RuleFor(x=>x.Password).Password();
        }
    }
}
