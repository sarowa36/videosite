using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ToolsLayer.FileManagement;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<IActionResult> Get(int id,int episodeId)
        {
            var query = db.Content.Include(x => x.EpisodeList).ThenInclude(x=>x.SourceList);
            return Json(r.Get(id, query));
        }
        [HttpPost]
        public async Task<IActionResult> Create(Content model, IFormFile file)
        {
            if (file != null && file.Length > 2)
                await model.SaveFileAsync(Path.Combine("content", "poster"), x => x.ImageLink, x => file);
            r.Create(model);
            return Json(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return Json(r.Get(id, db.Content.Include(x => x.EpisodeList).ThenInclude(x => x.SourceList)));
        }
        [HttpPost]
        public async Task<IActionResult> Update(Content model, IFormFile file)
        {
            if (file != null && file.Length > 2)
                await model.SaveFileAsync(Path.Combine("content", "poster"), x => x.ImageLink, x => file);
            r.Update(model);
            return Json(model);
        }
    }

}