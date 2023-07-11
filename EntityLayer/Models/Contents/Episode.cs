using EntityLayer.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer.Models.Contents
{
    public class Episode : AOfDefaultContent
    {
        public override int Id { get; set; }
        public override bool IsDeleted { get; set; }
        public string Name { get; set; }
        public List<SourceOfIframe> SourceList { get; set; }
        public int ContentId { get; set; }
        [Required, AllowNull]
        [ForeignKey("ContentId")]
        public Content? Content { get; set; }
    }
}
