using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.IdentityController
{
    public class RefreshPasswordModelValidator:AbstractValidator<RefreshPasswordModel>
    {
        public RefreshPasswordModelValidator()
        {
            RuleFor(x => x.UserName).UserName();
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.PasswordConfirm).Compare(x => x.Password);
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
