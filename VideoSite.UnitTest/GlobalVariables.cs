using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoSite.UnitTest
{
    public static class GlobalVariables
    {
        public static int DefaultFirstItemId { get { return 1; } }
        public const string UserName = "foobarbaz";
        public const string Password = "123654Foo+";
        public const string NewPassword = "123654Bar+";
        public static HttpClient Client { get {
                if (_Client == null)
                    _Client = Factory.CreateClient();// Server.CreateClient();
                return _Client;
            } }
        public static TestServer Server { get {
                if (_Server == null) {
                    _Server = new TestServer(new WebHostBuilder().UseStartup<VideoSite.Program>());
                }
                return _Server;
            } }
        public static WebApplicationFactory<VideoSite.Program> Factory { get
            {
                if (_Factory == null)
                    _Factory = new WebApplicationFactory<Program>();
                return _Factory;
            } }
        private static TestServer? _Server { get; set; }
        private static HttpClient? _Client { get; set; }
        private static WebApplicationFactory<VideoSite.Program> _Factory{get;set; } 
    }
}
