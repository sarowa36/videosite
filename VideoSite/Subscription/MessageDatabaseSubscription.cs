using DataAccessLayer;
using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using VideoSite.Hubs;
using VideoSite.Subscription.Base;
using VideoSite.ViewModels.MessageHub;

namespace VideoSite.Subscription
{
    public class MessageDatabaseSubscription : IBaseDatabaseSubscription<Message>
    {
        public readonly IHubContext<MessageHub> hub;
        private readonly IConfiguration configuration;
        private SqlTableDependency<Message> sqlTableDependency;
        public MessageDatabaseSubscription(IConfiguration configuration, IHubContext<MessageHub> hub)
        {
            this.hub = hub;
            this.configuration = configuration;
        }
        public void Configure()
        {
            sqlTableDependency = new SqlTableDependency<Message>(configuration.GetConnectionString("Default"), "Message");
            sqlTableDependency.OnChanged += OnChanged;
            sqlTableDependency.OnError += OnError;
            sqlTableDependency.Start();
        }
        public void OnChanged(object sender, RecordChangedEventArgs<Message> e)
        {
            if (e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
            {
                var connectionsId = MessageHub.ClientIdApplicationUserList.Where(x => x.Value.Id == e.Entity.ToId || x.Value.Id == e.Entity.FromId).Select(x => x.Key);
                if (connectionsId != null && connectionsId.Count() > 0)
                {
                    hub.Clients.Clients(connectionsId).SendAsync("newMessage", new MessageViewModel(e.Entity));
                }
            }
        }
        public void OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
        }

        ~MessageDatabaseSubscription()
        {
            sqlTableDependency.Stop();
        }
    }
}
