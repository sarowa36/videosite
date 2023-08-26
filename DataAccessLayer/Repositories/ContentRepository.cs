using DataAccessLayer.Helpers;
using EntityLayer.Models.Contents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ContentRepository : GenericBaseRepository<Content>
    {
        public ContentRepository(ADC db) : base(db)
        {
        }
        public override Content Get(int id)
        {
            return base.Get(id);
        }
        public override Content Get(int id, IQueryable<Content> filter)
        {
            return base.Get(id, filter);
        }
        public string? Counter(int id, string ip, string? userId)
        {
            if (!string.IsNullOrWhiteSpace(ip) && id > 0)
            {
                var a = new ContentWatchCounts() { EpisodeId = id, Ip = ip, UserId = userId };
                db.ContentWatchCounts
                    .AddIfNotExists(a,
                    x => x.EpisodeId == a.EpisodeId && x.Ip == a.Ip && x.UserId == a.UserId);
            }
            return SaveChanges();
        }
    }
}
