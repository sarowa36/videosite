using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ParamaterPass
{
    public class CommentParameterPass:IParamaterPass<Comment>
    {
        public int EpisodeId { get; set; }
    }
}
