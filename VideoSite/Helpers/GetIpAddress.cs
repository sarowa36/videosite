namespace VideoSite.Helpers
{
    public static class GetIpAddress
    {
        public static string GetIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? "";
        }
    }
}
