using EntityLayer.ViewModels.CategoryController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.CategoryController
{
    public class CategoryUpdateViewModelValidator : AbstractValidator<CategoryUpdateViewModel>
    {
        public CategoryUpdateViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(3);
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
