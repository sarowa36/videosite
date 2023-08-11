using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToolsLayer.FileManagement;
using VideoSite.ViewModels.ContentController;
using EntityLayer.Models.M2MRelationships;
using ToolsLayer.List;
using VideoSite.Helpers;

namespace VideoSite.Controllers
{
    public class ContentController : Controller
    {
        public readonly ADC db;
        public readonly ContentRepository r;
        public ContentController(ADC db)
        {
            this.db = db;
            r = new ContentRepository(this.db);
        }
        public async Task<IActionResult> GetList(int? index)
        {
            return Json(r.GetAll(index));
        }
        public async Task<IActionResult> Get(int id, int episodeId)
        {
            return Json(new ContentGetViewModel(r.Get(id, db.Content.Include(x => x.ContentM2MCategories).ThenInclude(x => x.Category).Include(x => x.EpisodeList).ThenInclude(x => x.SourceList))));
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null && model.File.Length > 2)
                    await model.SaveFileAsync(Path.Combine("content", "poster"), x => x.ImageLink, x => model.File);
                var content = model.AsContent();
                model.Categories.ForEach(x => content.ContentM2MCategories.Add(new ContentM2MCategory() { CategoryId = x }));
                r.Create(model.AsContent());
                return Ok(new { succeeded = true });
            }
            else
            {
                return Ok(ModelState.ListInvalidValueErrors());
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
            if (ModelState.IsValid)
            {
                if (model.File != null && model.File.Length > 2)
                    await model.SaveFileAsync(Path.Combine("content", "poster"), x => x.ImageLink, x => model.File);
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
                return Json(model);
            }
            else
            {
                return Ok(ModelState.ListInvalidValueErrors());
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            r.Delete(id);
            return Ok();
        }
    }

}