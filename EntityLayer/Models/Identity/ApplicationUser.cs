using EntityLayer.Models.Contents;
using EntityLayer.Models.M2MRelationships;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string ImageLink { get; set; } = "/src/assets/profile.png";
        public List<UserM2MCategory> UserM2MCategory { get; set; }=new List<UserM2MCategory>();
    }
}
