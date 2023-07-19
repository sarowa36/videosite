using DataAccessLayer.Repositories;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VideoSite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ADC db;
        private readonly CategoryRepository r;
        public CategoryController(ADC db)
        {
            this.db = db;
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
    }
}
