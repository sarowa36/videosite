using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Models.Contents;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.Models.M2MRelationships
{
    public class ContentM2MCategory
    {
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public Content Content { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
