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
using DataAccessLayer.ParamaterPass;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace VideoSite.Controllers
{
    public class ContentController : Controller
    {
        public readonly ADC _db;
        public readonly ContentRepository r;
        public readonly UserManager<ApplicationUser> userManager;
        public ContentController(ADC db, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            r = new ContentRepository(this._db);
            this.userManager = userManager;
        }
        public async Task<IActionResult> GetList(ContentGetListFilterViewModel model)
        {
            var modelState = new ContentGetListFilterViewModelValidator().Validate(model);

            if (modelState.IsValid)
                return Ok(r.GetAll());
            else
                return BadRequest(modelState.ListInvalidValueErrors());
        }
        public async Task<IActionResult> Get([Range(0, int.MaxValue), Required] int? id)
        {
            if (ModelState.IsValid)
            {
                if (r.Get((int)id,out Content entity))
                {
                    return Json(new ContentGetViewModel(entity));
                }
                return NotFound();
            }
            else
                return BadRequest(ModelState);
        }

        /// <summary>
        /// Thats action add a view count to episode if not added
        /// </summary>
        /// <param name="id">Episode Id</param>
        /// <returns>Ok or BadRequest</returns>
        public async Task<IActionResult> Watch([Range(0, int.MaxValue), Required] int? id)
        {
            if (ModelState.IsValid)
            {
                var error = r.Counter((int)id,
                      HttpContext.GetIp(),
                      (await userManager.GetUserAsync(HttpContext.User))?.Id);
                if (error != null)
                    return BadRequest();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContentViewModel model)
        {
            var validateResult = new ContentViewModelValidator().Validate(model);
            if (validateResult.IsValid)
            {
                if (model.File != null && model.File.Length > 2)
                    model.ImageLink = await model.File.SaveFileAsync(Path.Combine("content", "poster"));
                var content = model.AsContent();
                model.Categories.ForEach(x => content.ContentM2MCategories.Add(new ContentM2MCategory() { CategoryId = x }));
                var error = r.Create(content);
                if (error != null)
                    return BadRequest();
                return Ok();
            }
            else
            {
                return BadRequest(validateResult.ListInvalidValueErrors());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update([Range(0, int.MaxValue), Required] int? id)
        {
            if (ModelState.IsValid)
            {
                if (r.Get((int)id,out Content val))
                    return Ok(new ContentViewModel(val));
                else
                    return NotFound();
            }
            else return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ContentViewModel model)
        {
            var validationResult = new ContentViewModelValidator(EntityLayer.Enums.CrudMethodEnum.Update).Validate(model);
            if (validationResult.IsValid)
            {
                if (model.File != null && model.File.Length > 2)
                    model.ImageLink = await model.File.SaveFileAsync(Path.Combine("content", "poster"));
                var error = r.Update(model.AsContent(), new ContentParamaterPass() { RequestCategoryIds = model.Categories });
                if (!string.IsNullOrWhiteSpace(error))
                    return BadRequest();
                return Ok();
            }
            else
            {
                return BadRequest(validationResult.ListInvalidValueErrors());
            }
        }
        public async Task<IActionResult> Delete([Range(0, int.MaxValue), Required] int? id)
        {
            if (ModelState.IsValid)
            {
                var error = r.Delete((int)id);
                if (error != null) return BadRequest();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }

}