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
        public void Create(Message val)
        {
            db.Message.Add(val);
            db.SaveChanges();
        }
        public IEnumerable<object> GetUsersForMessaging(string CurrentUserId)
        {
            return db.Users
              .Where(x => x.Id != CurrentUserId).Select(x => new
              {
                  x.Id,
                  x.ImageLink,
                  x.UserName,
                  lastMessage = db.Message.OrderBy(x => x.Date).Where(y => y.FromId == x.Id && y.ToId == CurrentUserId || y.ToId == x.Id && y.FromId == CurrentUserId).Select(x => new { x.Text, x.Date }).LastOrDefault()
              }).OrderByDescending(x => x.lastMessage.Date).ToList();
        }
        public string GetUserImage(string id)=> db.Users.Where(x => x.Id == id).Select(x => x.ImageLink).FirstOrDefault();
        
    }
}
