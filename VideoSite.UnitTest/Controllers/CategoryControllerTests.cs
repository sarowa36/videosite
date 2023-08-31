using EntityLayer.ViewModels.CategoryController;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToolsLayer.FormDataSerializer;
using Xunit.Priority;

namespace VideoSite.UnitTest.Controllers
{

    public class CategoryControllerTests:ControllerTestBase
    {
        public CategoryControllerTests()
        {
            Environment.SetEnvironmentVariable("IsInUnitTest", "1");
        }
        [Fact, Priority(-10)]
        public async void Create()
        {
            var res = await Client.PostAsync("api/category/create", ObjectToMultipartFormDataConverter.Convert(new { categoryName = "Foo" }));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void GetList()
        {
            var res = await Client.GetAsync("api/category/getlist");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            Assert.Equal(1, JsonConvert.DeserializeObject<object[]>(await res.Content.ReadAsStringAsync()).Length);
        }
        [Fact]
        public async void UpdateGetAction()
        {
            var res = await Client.GetAsync("api/category/update/" + DefaultFirstItemId);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void UpdatePostAction()
        {
            var res = await Client.GetAsync("api/category/update/" + DefaultFirstItemId);
            var data = JsonConvert.DeserializeObject<CategoryUpdateViewModel>(await res.Content.ReadAsStringAsync());
             res = await Client.PostAsync("api/category/update", ObjectToMultipartFormDataConverter.Convert(data));
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact,Priority(10)]
        public async void Delete()
        {
            var res = await Client.GetAsync("api/category/delete/"+DefaultFirstItemId);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
