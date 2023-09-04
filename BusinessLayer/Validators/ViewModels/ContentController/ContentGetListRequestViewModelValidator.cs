using EntityLayer.ViewModels.ContentController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.ContentController
{
    public class ContentGetListFilterViewModelValidator:AbstractValidator<ContentGetListFilterViewModel>
    {
        public ContentGetListFilterViewModelValidator()
        {
            RuleFor(x => x.LastId).GreaterThanOrEqualTo(0);
            RuleFor(x=>x.Release).GreaterThanOrEqualTo(1950).LessThanOrEqualTo(DateTime.Now.Year);
        }
    }
}
