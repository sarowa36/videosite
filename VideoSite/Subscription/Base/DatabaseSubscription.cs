using System.Runtime.Intrinsics.Arm;
using TableDependency.SqlClient.Base.EventArgs;
using TableDependency.SqlClient.Base;
using TableDependency.SqlClient;
using EntityLayer.Models.Contents;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VideoSite.Subscription.Base
{
    public interface IBaseDatabaseSubscription<T>  where T : class, new()
    {
         void Configure();
         void OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e);
         void OnChanged(object sender, RecordChangedEventArgs<T> e);

    }
}
