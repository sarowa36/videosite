using EntityLayer.Enums;

namespace EntityLayer.ViewModels.LikeController
{
    public class LikeAddViewModel
    {
        public int EpisodeId { get; set; }
        public LikeDislikeEnum LikeOrDislike { get; set; }
    }
}
