using EntityLayer.Base;
using EntityLayer.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Contents
{
    public class Message : AOfDefaultContent
    {
        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public string FromId { get; set; }
        [ForeignKey("FromId")]
        public ApplicationUser From { get; set; }
        public string ToId { get; set; }
        [ForeignKey("ToId")]
        public ApplicationUser To { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
    }
}
