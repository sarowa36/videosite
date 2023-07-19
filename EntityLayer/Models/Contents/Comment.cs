using EntityLayer.Base;
using EntityLayer.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Contents
{
    public class Comment : AOfDefaultContent
    {
        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [MaxLength(500)]
        public string Text { get; set; }
        public int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode Episode { get; set; }
    }
}
