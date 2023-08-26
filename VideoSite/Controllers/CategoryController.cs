using DataAccessLayer.Repositories;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VideoSite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ADC _db;
        private readonly CategoryRepository r;
        public CategoryController(ADC db)
        {
            this._db = db;
            r = new CategoryRepository(db);
        }
        public async Task<IActionResult> GetList()
        {
            return Ok(r.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> Create(string CategoryName)
        {
            r.Create(new EntityLayer.Models.Contents.Category() { Name= CategoryName });
            return Ok();
        }
        public async Task<IActionResult> Update(int id)
        {
            return Ok(r.Get(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,string value)
        {
            var a = r.Get(id);
            a.Name= value;
            r.Update(a);
            return Ok();
        }
        public async Task<IActionResult> Delete(int id)
        {
            r.Delete(id);
            return Ok();
        }
    }
}
