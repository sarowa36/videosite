using EntityLayer.ViewModels.CommentController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.CommentController
{
    public class CommentCreateViewModelValidator:AbstractValidator<CommentCreateViewModel>
    {
        public CommentCreateViewModelValidator()
        {
            RuleFor(x => x.EpisodeId).GreaterThan(0).NotEmpty();
            RuleFor(x=>x.Text).NotNull().MinimumLength(3);
        }
    }
}
