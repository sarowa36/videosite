using EntityLayer.Base;
using EntityLayer.Models.Contents;
using EntityLayer.Models.Identity;
using EntityLayer.Models.M2MRelationships;
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
    public class ADC:IdentityDbContext<ApplicationUser>
    {
        public ADC(DbContextOptions<ADC> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserM2MCategory>().HasKey(x =>new{ x.CategoryId,x.UserId});
            builder.Entity<ContentM2MCategory>().HasKey(x => new { x.CategoryId, x.ContentId });
            builder.Entity<UserM2MLike>().HasKey(x => new { x.UserId, x.EpisodeId });
            builder.Entity<Message>().ToTable(x => x.HasTrigger("Message"));
            builder.ApplyGlobalFilters<AOfDefaultContent>(x => !x.IsDeleted);
            base.OnModelCreating(builder);
        }

        public DbSet<Content> Content { get; set; }
        public DbSet<Episode> Episode { get; set; }
        public DbSet<SourceOfIframe> SourceOfIframe { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContentM2MCategory> ContentM2MCategories { get; set;}
        public DbSet<UserM2MCategory> UserM2MCategories { get; set; }
        public DbSet<UserM2MLike> UserM2MLike { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}