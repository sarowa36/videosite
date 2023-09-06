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
        [Fact,Priority(-20)]
        public async void LogOut()
        {
            var res = await Client.GetAsync("api/identity/logout");
            Client.DefaultRequestHeaders.Add("Cookie", res.Headers.GetValues("Set-Cookie"));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact, Priority(-10)]
        public async void Login()
        {
            var res = await Client.PostAsync("api/identity/login",
                ObjectToMultipartFormDataConverter.Convert(new LoginViewModel() { Password=Password,UserName=UserName}));
            Client.DefaultRequestHeaders.Add("Cookie", res.Headers.GetValues("Set-Cookie"));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void GetUser()
        {
           var res = await Client.GetAsync("api/identity/getuser");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void ProfileEditGetAction()
        {
            var res = await Client.GetAsync("api/Identity/ProfileEdit");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void ProfileEditPostAction()
        {
            var res = await Client.GetAsync("api/Identity/ProfileEdit");
            var data = JsonConvert.DeserializeObject<ProfileEditViewModel>(
                await res.Content.ReadAsStringAsync());
            res = await Client.PostAsync("api/identity/ProfileEdit",
                ObjectToMultipartFormDataConverter.Convert(data));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void ProfileEditPostActionWithNewProfilePicture()
        {
            var res = await Client.GetAsync("api/Identity/ProfileEdit");
            var data = ObjectToMultipartFormDataConverter.Convert(
                JsonConvert.DeserializeObject<ProfileEditViewModel>(
                    await res.Content.ReadAsStringAsync()));
            data.Add(CreateImageContentForTest.Create(), "profileImage", "test.png");
            res = await Client.PostAsync("api/identity/ProfileEdit", data);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void ChangePassword()
        {
            var res = await Client.PostAsync("api/identity/changepassword", 
                ObjectToMultipartFormDataConverter.Convert(
                    new ChangePasswordViewModel() { 
                        OldPassword=Password,
                        NewPassword=NewPassword,
                        NewPasswordConfirm=NewPassword
                    }));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

    }
}
