using EntityLayer.Base;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Application DbContext
    /// </summary>
    public class ADC:DbContext
    {
        public ADC(DbContextOptions<ADC> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Content> Content { get; set; }
        public DbSet<Episode> Episode { get; set; }
        public DbSet<SourceOfIframe> SourceOfIframe { get; set; }
    }
}
