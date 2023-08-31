using EntityLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.ContentController
{
    public class ContentGetListRequestViewModel
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        [Range(0,int.MaxValue)]
        public int LastId { get; set; } = 0;
        public ContentOrderEnum Order { get; set; }
        public string? Keyword { get; set; }
        public List<string>? Categories{ get; set; }
        [Range(1900,2050)]
        public int? Release { get; set; } 
    }
}
