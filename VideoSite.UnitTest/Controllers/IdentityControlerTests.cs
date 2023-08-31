using EntityLayer.ViewModels.IdentityController;
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
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)
       , DefaultPriority(0)]
    public class IdentityControlerTests:ControllerTestBase
    {
        private const string UserName = "foobarbaz";
        private const string Password = "123654Foo+";
        [Fact,Priority(-10)]
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
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            res = await Client.GetAsync("api/identity/getuser");
            Assert.Equal(HttpStatusCode.OK,res.StatusCode);
        }
    }
}
