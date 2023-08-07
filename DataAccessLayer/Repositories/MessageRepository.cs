using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MessageRepository 
    {
        private ADC db;
        private readonly int DefaultCount = 40;
        public MessageRepository(ADC db) 
        {
            this.db = db;
        }
        public List<Message> GetAll(string currentUserId,string toUserId ,int? lastMessageId = null)
        {
            var query = db.Message.OrderByDescending(x => x.Date).AsQueryable();
            if (lastMessageId != null)
               query= query.Where(x => (x.ToId == currentUserId && x.FromId == toUserId || x.ToId == toUserId && x.FromId == currentUserId) && x.Id < lastMessageId);
            else
                query=query.Where(x => x.ToId == currentUserId && x.FromId == toUserId || x.ToId == toUserId && x.FromId == currentUserId);
            //var list = query.Take(DefaultCount).ToList();
            var val = query.Take(DefaultCount).ToList();
            return val;
        }
    }
}
