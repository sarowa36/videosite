using EntityLayer.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Contents
{
    [Keyless]
    public class ContentWatchCounts
    {
        public string Ip { get; set; }
        public int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode Episode { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
