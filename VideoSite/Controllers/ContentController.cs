using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index()
        {
            return Json(new {deneme="true"});
        }
        [HttpPost]
        public async Task<IActionResult> Create(Content model)
        {
            if (ModelState.IsValid)
            {
                r.Create(model);
            }
            return Json(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Content model)
        {
            if (ModelState.IsValid)
            {
                r.Update(model);
            }
            return Json(model);
        }
    }
}
