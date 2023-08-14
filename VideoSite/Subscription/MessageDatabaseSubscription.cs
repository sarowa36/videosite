using DataAccessLayer;
using EntityLayer.Models.Contents;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Security.Permissions;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using VideoSite.Hubs;
using VideoSite.Subscription.Base;
using EntityLayer.ViewModels.MessageHub;

namespace VideoSite.Subscription
{
    public class MessageDatabaseSubscription : IBaseDatabaseSubscription<Message>
    {
        public readonly IHubContext<MessageHub> hub;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private SqlTableDependency<Message> sqlTableDependency;
        public MessageDatabaseSubscription(IConfiguration configuration, IHubContext<MessageHub> hub, ILogger<MessageDatabaseSubscription> logger)
        {
            this.hub = hub;
            this.configuration = configuration;
            this.logger = logger;
        }
        public void Configure()
        {
            try
            {
                sqlTableDependency = new SqlTableDependency<Message>(configuration.GetConnectionString("Default"), "Message");
                sqlTableDependency.OnChanged += OnChanged;
                sqlTableDependency.OnError += OnError;
                sqlTableDependency.Start();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
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
