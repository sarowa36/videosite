using DataAccessLayer;
using DataAccessLayer.Repositories;
using EntityLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using EntityLayer.ViewModels.MessageHub;

namespace VideoSite.Hubs
{
    public class MessageHub : Hub
    {
        public static Dictionary<string, ApplicationUser> ClientIdApplicationUserList { get; } = new Dictionary<string, ApplicationUser>();
        private readonly ADC _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MessageRepository MRepo;
        public MessageHub(ADC db, UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
            this.MRepo = new MessageRepository(db);
        }
        public async Task<object> GetAll(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            var val = MRepo.GetAll(currentUser.Id, userId).Select(x => new MessageViewModel(x));
            var images = new { fromImage = currentUser.ImageLink, toImage = MRepo.GetUserImage(userId) };
            return new { messageList = val, images };
        }
        public async Task CreateMessage(string userId, string text)
        {
            if (text.Length > 0)
            {
                var currentUser = await _userManager.GetUserAsync(Context.User);
                MRepo.Create(new EntityLayer.Models.Contents.Message() { FromId = currentUser.Id, ToId = userId, Text = text });
            }
        }
        public async Task<object> GetUsers()
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            return MRepo.GetUsersForMessaging(currentUser.Id);
        }
        public async Task<IEnumerable<MessageViewModel>> GetOldMessages(int id, string toId)
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            return MRepo.GetAll(currentUser.Id, toId, id).Select(x => new MessageViewModel(x));
        }
        public override async Task OnConnectedAsync()
        {
            ClientIdApplicationUserList.Add(Context.ConnectionId, await _userManager.GetUserAsync(Context.User));
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientIdApplicationUserList.Remove(Context.ConnectionId);
        }
    }
}
