using EntityLayer.ViewModels.LikeController;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validators.ViewModels.LikeController
{
    public class LikeAddViewModelValidator:AbstractValidator<LikeAddViewModel>
    {
        public LikeAddViewModelValidator()
        {
            RuleFor(x => x.EpisodeId).Id();
        }
    }
}
