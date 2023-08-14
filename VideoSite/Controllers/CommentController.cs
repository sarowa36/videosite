using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Models.Contents;
using EntityLayer.ViewModels.CommentController;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VideoSite.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ADC db;
        private readonly CommentRepository r;
        public CommentController(UserManager<ApplicationUser> usermanager,ADC db)
        {
            this.userManager = usermanager;
            this.db = db;
            r=new CommentRepository(db);
        }
        public async Task<IActionResult> GetList(int EpisodeId)
        {
            var allComments = r.GetAll(db.Comment.Include(x=>x.User).Where(x => x.EpisodeId == EpisodeId));
            var returnValue = allComments.Select(x => new CommentViewModel() {ImageLink=x.User.ImageLink, Text=x.Text,UserName=x.User.UserName });
            return Ok(returnValue);
        }
        [HttpPost]
        public async Task<IActionResult> Create([MaxLength(500)]string text,int episodeId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null&& !string.IsNullOrWhiteSpace(text) && text.Length<500 && episodeId >0)
            {
                r.Create(new Comment() { UserId= user.Id,Text=text,EpisodeId=episodeId });
                return Ok(new { succeeded = true });
            }
            return Ok(new { succeeded =false});
        }
    }
}
