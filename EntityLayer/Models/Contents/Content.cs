using EntityLayer.Base;
using EntityLayer.Models.M2MRelationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models.Contents
{
    public class Content : AOfDefaultContent
    {
        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public List<Episode> EpisodeList { get; set; }
        public List<ContentM2MCategory> ContentM2MCategories { get; set; }=new List<ContentM2MCategory>();
    }
}
