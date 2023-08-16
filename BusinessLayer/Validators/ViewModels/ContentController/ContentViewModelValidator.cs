using EntityLayer.Enums;
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
        public ContentViewModelValidator(CrudMethodEnum en=CrudMethodEnum.Create)
        {
            if (en == CrudMethodEnum.Update)
            {
                RuleFor(x=>x.Id).NotEmpty().Must(x=>x>0);
            }
            if(en == CrudMethodEnum.Create)
            {
                RuleFor(x => x.File).NotNull();
            }
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(2);
            RuleFor(x => x.EpisodeList).Must(x => x.Count > 0);
            RuleForEach(x => x.EpisodeList).SetValidator(x => new ContentViewModelEpisodeListValidator());
        }
    }
    internal class ContentViewModelEpisodeListValidator : AbstractValidator<Episode>
    {
        public ContentViewModelEpisodeListValidator(CrudMethodEnum en = CrudMethodEnum.Create)
        {
            if(en== CrudMethodEnum.Update)
            {
                RuleFor(x => x.Id).NotNull().Must(x => x > 0);
                RuleFor(x=>x.ContentId).NotNull().Must(x => x > 0);
            }
            RuleFor(x=>x.Name).NotNull().NotEmpty().MinimumLength(2);
            RuleForEach(x => x.SourceList).SetValidator(x => new ContentViewModelEpisodeListSourceListValidator());
        }
    }
    internal class ContentViewModelEpisodeListSourceListValidator : AbstractValidator<SourceOfIframe>
    {
        public ContentViewModelEpisodeListSourceListValidator(CrudMethodEnum en = CrudMethodEnum.Create)
        {
            if(en == CrudMethodEnum.Update)
            {
                RuleFor(x=>x.Id).NotNull().Must(x => x > 0);
                RuleFor(x=>x.EpisodeId).NotNull().Must(x => x > 0);
            }
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2);
            RuleFor(x => x.IframeCode).NotNull().NotEmpty().Url();
        }
    }
}
