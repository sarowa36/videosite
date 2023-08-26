using DataAccessLayer.ParamaterPass;
using EntityLayer.Models.Contents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CommentRepository:GenericBaseRepository<Comment,CommentParameterPass>
    {
        public CommentRepository(ADC db):base(db)
        {
        }
        public override List<Comment> GetAll(CommentParameterPass param, int? index = null, int? count = null)
        {
            return GetAll(db.Comment.Include(x => x.User).Where(x => x.EpisodeId == param.EpisodeId), count);
        }
    }
}
