using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Models.Contents;
using EntityLayer.ViewModels.CommentController;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.ParamaterPass;
using VideoSite.Helpers;
using BusinessLayer.Validators.ViewModels.CommentController;

namespace VideoSite.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ADC _db;
        private readonly CommentRepository r;
        public CommentController(UserManager<ApplicationUser> usermanager, ADC db)
        {
            this._userManager = usermanager;
            this._db = db;
            r = new CommentRepository(db);
        }
        public async Task<IActionResult> GetList([Range(0, int.MaxValue)] int EpisodeId)
        {
            if (ModelState.IsValid)
            {
                var allComments = r.GetAll(new CommentParameterPass() { EpisodeId = EpisodeId });
                var returnValue = allComments.Select(x => new CommentViewModel() { ImageLink = x.User.ImageLink, Text = x.Text, UserName = x.User.UserName });
                return Ok(returnValue);
            }
            return BadRequest(ModelState.ListInvalidValueErrors());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = new CommentCreateViewModelValidator().Validate(model);
            if (user != null && result.IsValid)
            {
                var error = r.Create(model.AsComment(user.Id));
                if (string.IsNullOrWhiteSpace(error))
                    return Ok();
            }
            return BadRequest(result.ListInvalidValueErrors());
        }
    }
}
