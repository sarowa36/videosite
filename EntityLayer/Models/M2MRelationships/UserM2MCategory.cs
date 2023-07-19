using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;

namespace EntityLayer.Models.M2MRelationships
{
    public class UserM2MCategory
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User{ get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
