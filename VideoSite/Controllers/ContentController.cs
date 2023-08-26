using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToolsLayer.FileManagement;
using EntityLayer.ViewModels.ContentController;
using EntityLayer.Models.M2MRelationships;
using VideoSite.Helpers;
using BusinessLayer.Validators.ViewModels.ContentController;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using EntityLayer.Models.Identity;

namespace VideoSite.Controllers
{
    public class ContentController : Controller
    {
        public readonly ADC db;
        public readonly ContentRepository r;
        public readonly UserManager<ApplicationUser> userManager;
        public ContentController(ADC db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            r = new ContentRepository(this.db);
            this.userManager = userManager;
        }
        public async Task<IActionResult> GetList(ContentGetListRequestViewModel index)
        {
            return Json(r.GetAll());
        }
        public async Task<IActionResult> Get(int id)
        {
            return Json(new ContentGetViewModel(r.Get(id, db.Content.Include(x => x.ContentM2MCategories).ThenInclude(x => x.Category).Include(x => x.EpisodeList).ThenInclude(x => x.SourceList))));
        }
        public async Task<IActionResult> Watch(int id)
        {

            r.Counter(id,
                HttpContext.GetIp(),
                (await userManager.GetUserAsync(HttpContext.User))?.Id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContentViewModel model)
        {
            var v = new ContentViewModelValidator();
            var validateResult = v.Validate(model);
            if (validateResult.IsValid)
            {
                if (model.File != null && model.File.Length > 2)
                     model.ImageLink=await model.File.SaveFileAsync(Path.Combine("content", "poster"));
                var content = model.AsContent();
                model.Categories.ForEach(x => content.ContentM2MCategories.Add(new ContentM2MCategory() { CategoryId = x }));
                r.Create(content);
                return Ok(new { succeeded = true });
            }
            else
            {
                return Ok(validateResult.ListInvalidValueErrors());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return Json(new ContentViewModel(r.Get(id, db.Content.Include(x => x.ContentM2MCategories).Include(x => x.EpisodeList).ThenInclude(x => x.SourceList))));
        }
        [HttpPost]
        public async Task<IActionResult> Update(ContentViewModel model)
        {
            var validationResult = (new ContentViewModelValidator(EntityLayer.Enums.CrudMethodEnum.Update)).Validate(model);
            if (validationResult.IsValid)
            {
                if (model.File != null && model.File.Length > 2)
                     model.ImageLink= await model.File.SaveFileAsync(Path.Combine("content", "poster"));
                var beforeList = db.ContentM2MCategories.Where(x => x.ContentId == model.Id).Select(x => x.CategoryId).ToList();
                var newList = model.Categories;
                var removeList = beforeList.Except(newList);
                var addlist = newList.Except(beforeList);
                var content = model.AsContent();
                if (addlist != null && addlist.Count() > 0)
                {
                    addlist.ToList().ForEach(x =>
                    content.ContentM2MCategories.Add(new ContentM2MCategory() { CategoryId = x })
                    );
                }
                if (removeList != null && removeList.Count() > 0)
                {
                    removeList.ToList().ForEach(x =>
                   db.ContentM2MCategories.Remove(db.ContentM2MCategories.FirstOrDefault(y => y.ContentId == model.Id && y.CategoryId == x))
                    );
                }
                r.Update(content);
                return Json(new { succeeded = true });
            }
            else
            {
                return Ok(validationResult.ListInvalidValueErrors());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            r.Delete(id);
            return Ok();
        }
    }

}