using DataAccessLayer;
using EntityLayer.Enums;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using EntityLayer.ViewModels.LikeController;

namespace VideoSite.Controllers
{
    [Authorize(Roles ="USER")]
    public class LikeController : Controller
    {
        private readonly ADC db;
        private readonly UserManager<ApplicationUser> userManager;
        public LikeController(ADC db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Get(int episodeId)
        {
            var AppUser = await userManager.GetUserAsync(HttpContext.User);
            if (AppUser != null)
            {
                return Ok(new
                {
                    likeCount = db.UserM2MLike.Where(x => x.EpisodeId == episodeId && x.LikeDislike == LikeDislikeEnum.Like).Count(),
                    dislikeCount = db.UserM2MLike.Where(x => x.EpisodeId == episodeId && x.LikeDislike == LikeDislikeEnum.Dislike).Count(),
                    currentUserVal = db.UserM2MLike.FirstOrDefault(x => x.UserId == AppUser.Id && x.EpisodeId == episodeId)?.LikeDislike
                });
            }
            else
            {
                return Ok();
            }
        }
        /// <summary>
        /// Action for Create, Update, Delete operations
        /// </summary>
        /// <returns>Like Dislike Counts</returns>
        public async Task<IActionResult> CUD(LikeAddViewModel model)
        {
            var AppUser = await userManager.GetUserAsync(HttpContext.User);
            var val = db.UserM2MLike.FirstOrDefault(x => x.UserId == AppUser.Id && x.EpisodeId == model.EpisodeId);
            if (val != null)
            {
                if (val.LikeDislike != model.LikeOrDislike)
                    val.LikeDislike = model.LikeOrDislike;
                else
                    db.UserM2MLike.Remove(val);
            }
            else
            {
                db.UserM2MLike.Add(new EntityLayer.Models.M2MRelationships.UserM2MLike() { EpisodeId = model.EpisodeId, UserId = AppUser.Id, LikeDislike = model.LikeOrDislike });
            }
            db.SaveChanges();
            return await Get(model.EpisodeId);
        }
    }
}
