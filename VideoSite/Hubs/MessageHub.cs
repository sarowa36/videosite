using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using VideoSite.ViewModels.MessageHub;

namespace VideoSite.Hubs
{
    public class MessageHub:Hub
    {
        public static Dictionary<string,ApplicationUser> ClientIdApplicationUserList { get; }=new Dictionary<string,ApplicationUser>();
        private readonly ADC db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly MessageRepository MRepo;
        public MessageHub(ADC db,UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager= userManager;
            this.MRepo=new MessageRepository(db);
        }
        public async Task<object> GetAll(string userId)
        {
            var currentUser = await userManager.GetUserAsync(Context.User);
            var val= MRepo.GetAll(currentUser.Id,userId).Select(x => new MessageViewModel(x));
            var images = new {fromImage=currentUser.ImageLink, toImage=db.Users.Where(x=>x.Id==userId).Select(x=>x.ImageLink).FirstOrDefault() };
            return  new { messageList=val,images };
        }
        public async Task CreateMessage(string userId,string text)
        {
            if (text.Length > 0) { 
            var currentUser = await userManager.GetUserAsync(Context.User);
            db.Message.Add(new EntityLayer.Models.Contents.Message() { FromId = currentUser.Id, ToId = userId, Text = text });
            db.SaveChanges();
            }
        }
        public async Task<object> GetUsers()
        {
            var currentUser = await userManager.GetUserAsync(Context.User);
            var val = db.Users
                .Where(x => x.Id != currentUser.Id).Select(x => new {
                    x.Id,
                    x.ImageLink,
                    x.UserName,
                    lastMessage = db.Message.OrderBy(x=>x.Date).Where(y => y.FromId == x.Id && y.ToId == currentUser.Id || y.ToId == x.Id && y.FromId == currentUser.Id).Select(x => new {x.Text,x.Date}).LastOrDefault()
                }).OrderByDescending(x=>x.lastMessage.Date).ToList();
            return val ;
        }
        public async Task<IEnumerable<MessageViewModel>> GetOldMessages(int id,string toId)
        {
            var currentUser=await userManager.GetUserAsync(Context.User);
            return MRepo.GetAll(currentUser.Id, toId, id).Select(x=>new MessageViewModel(x));
        }
        public override async Task OnConnectedAsync()
        {
            ClientIdApplicationUserList.Add(Context.ConnectionId, await userManager.GetUserAsync(Context.User));
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientIdApplicationUserList.Remove(Context.ConnectionId);
        }
    }
}
