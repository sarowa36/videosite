﻿using EntityLayer.Enums;
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
    public class ContentGetListFilterViewModel
    {
        [BindProperty(Name = "id", SupportsGet = true)]
        public int LastId { get; set; } = 0;
        public ContentOrderEnum Order { get; set; }
        public string? Keyword { get; set; }
        public List<string>? Categories{ get; set; }
        public int? Release { get; set; } 
    }
}
