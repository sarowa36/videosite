using EntityLayer.Models.Contents;
using EntityLayer.ViewModels.ContentController;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsLayer.Validation;

namespace BusinessLayer.Validators.ViewModels.ContentController
{
    public class ContentViewModelValidator:AbstractValidator<ContentViewModel>
    {
        public ContentViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
            RuleFor(x => x.EpisodeList).Must(x => x.Count > 0);
            RuleForEach(x => x.EpisodeList).SetValidator(x => new ContentViewModelEpisodeListValidator());
        }
        public override ValidationResult Validate(ValidationContext<ContentViewModel> context)
        {
            var returnVal=    base.Validate(context);
            return returnVal;
        }
    }
    internal class ContentViewModelEpisodeListValidator : AbstractValidator<Episode>
    {
        public ContentViewModelEpisodeListValidator()
        {
            RuleFor(x=>x.Name).NotNull().NotEmpty().MinimumLength(2);
            RuleForEach(x => x.SourceList).SetValidator(x => new ContentViewModelEpisodeListSourceListValidator());
        }
    }
    internal class ContentViewModelEpisodeListSourceListValidator : AbstractValidator<SourceOfIframe>
    {
        public ContentViewModelEpisodeListSourceListValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(x => x.IframeCode).NotNull().NotEmpty().Url();
        }
    }
}
