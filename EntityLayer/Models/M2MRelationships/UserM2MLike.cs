using EntityLayer.Enums;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.M2MRelationships
{
    public class UserM2MLike
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode Episode { get; set; }
        public LikeDislikeEnum LikeDislike { get; set; }
    }
}
