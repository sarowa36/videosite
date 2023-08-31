using Azure.Core;
using DataAccessLayer.Helpers;
using DataAccessLayer.ParamaterPass;
using EntityLayer.Models.Contents;
using EntityLayer.Models.M2MRelationships;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ContentRepository : GenericBaseRepository<Content, ContentParamaterPass>
    {
        public ContentRepository(ADC db) : base(db)
        {
        }
        public override Content Get(int id)
        {
            return base.Get(id, db.Content.Include(x => x.ContentM2MCategories).ThenInclude(x => x.Category).Include(x => x.EpisodeList).ThenInclude(x => x.SourceList));
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
        public override string? Update(Content t, ContentParamaterPass param)
        {
            var beforeList = db.ContentM2MCategories.Where(x => x.ContentId == t.Id).Select(x => x.CategoryId).ToList();
            var newList = param.RequestCategoryIds;
            var removeList = beforeList.Except(newList);
            var addList = newList.Except(beforeList);
            if (addList != null && addList.Count() > 0)
            {
                addList.ToList().ForEach(x =>
                t.ContentM2MCategories.Add(new ContentM2MCategory() { CategoryId = x })
                );
            }
            if (removeList != null && removeList.Count() > 0)
            {
                db.ContentM2MCategories.RemoveRange(
                    removeList.Select(
                        x => new ContentM2MCategory() { CategoryId = x, ContentId = t.Id }));
                //removeList.ToList().ForEach(x =>
                // db.ContentM2MCategories.Remove(db.ContentM2MCategories.FirstOrDefault(y => y.ContentId == model.Id && y.CategoryId == x))
                // );
            }
            return base.Update(t, param);
        }
    }
}
