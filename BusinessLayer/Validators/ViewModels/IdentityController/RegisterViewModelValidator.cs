using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.IdentityController
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.UserName).UserName();
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.Password2).Compare(x=>x.Password);
            RuleFor(x=>x.Email).EmailAddress().MinimumLength(8).MaximumLength(32);
            RuleFor(x => x.ProfileImage).NotNull();
        }
    }
}