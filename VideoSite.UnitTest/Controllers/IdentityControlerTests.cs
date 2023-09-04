using DataAccessLayer;
using EntityLayer.ViewModels.IdentityController;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToolsLayer.FormDataSerializer;
using ToolsLayer.Test;
using Xunit.Priority;

namespace VideoSite.UnitTest.Controllers
{
    public class IdentityControlerTests:ControllerTestBase
    {

        [Fact,Priority(-30)]
        public async void Register()
        {
            var data =ObjectToMultipartFormDataConverter.Convert( new RegisterViewModel(){
            Email="foobar123@gmail.com",
            Password=Password,
            Password2 = Password,
            UserName=UserName
            });
            data.Add(CreateImageContentForTest.Create(), "ProfileImage","test.png");
            
            var res = await Client.PostAsync("api/identity/register", data);
            Client.DefaultRequestHeaders.Add("Cookie", res.Headers.GetValues("Set-Cookie"));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            
        }
        [Fact]
        public async void GetUser()
        {
           var res = await Client.GetAsync("api/identity/getuser");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        
    }
}
