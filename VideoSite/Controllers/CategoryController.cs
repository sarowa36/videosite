using DataAccessLayer.Repositories;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Models.Contents;
using EntityLayer.ViewModels.CategoryController;
using BusinessLayer.Validators.ViewModels.CategoryController;
using VideoSite.Helpers;
using System.ComponentModel.DataAnnotations;

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
            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                string? error = r.Create(new EntityLayer.Models.Contents.Category() { Name = CategoryName });
                if (string.IsNullOrWhiteSpace(error))
                    return Ok();
            }
            return BadRequest();
        }
        public async Task<IActionResult> Update([Range(0,int.MaxValue)]int id)
        {
            if (r.Get(id, out Category data))
                return Ok(new CategoryUpdateViewModel(data));
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            var validateResult = new CategoryUpdateViewModelValidator().Validate(model);
            if (validateResult.IsValid)
            {
                var error = r.Update(model.AsCategory());
                return error == null ? Ok() : BadRequest();
            }
            return BadRequest(validateResult.ListInvalidValueErrors());
        }

        public async Task<IActionResult> Delete(int id)
        {
           var error=  r.Delete(id);
            if(string.IsNullOrWhiteSpace(error))
            return Ok();
            else
                return BadRequest();
        }
    }
}
