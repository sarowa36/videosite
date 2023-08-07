using EntityLayer.Enums;

namespace VideoSite.ViewModels.LikeController
{
    public class LikeAddViewModel
    {
        public int EpisodeId { get; set; }
        public LikeDislikeEnum LikeOrDislike { get; set; }
    }
}
