using VideoSite.Subscription.Base;

namespace VideoSite.Subscription.Middleware
{
    public static class DatabaseSubscriptionMiddleware
    {
        public static void UseDatabaseSubscription<T>(this IApplicationBuilder app) /*where TEntity : class, new() where T : IBaseDatabaseSubscription<TEntity>*/ 
        {
            var service=app.ApplicationServices.GetService<T>();
            service.GetType().GetMethod("Configure").Invoke(service,null);
        }
    }
}
