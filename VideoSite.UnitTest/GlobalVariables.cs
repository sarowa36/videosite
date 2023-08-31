using Microsoft.AspNetCore.Hosting;
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
        public static HttpClient Client { get {
                if (_Client == null)
                    _Client = Server.CreateClient();
                return _Client;
            } }
        public static TestServer Server{ get {
                if (_Server == null)
                    _Server = new TestServer(new WebHostBuilder().UseStartup<VideoSite.Program>());
                return _Server;
            } }
        private static TestServer? _Server { get; set;}
        private static HttpClient? _Client { get; set; }
    }
}
