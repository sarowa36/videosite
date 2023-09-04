using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.CommentController
{
    public class CommentCreateViewModel
    {
        public int EpisodeId { get; set; }
        public string Text{ get; set; }
        public Comment AsComment(string userId)
        {
            return new Comment() {
                EpisodeId = EpisodeId,
                Text = Text,
                UserId=userId
            };
        }
    }
}
