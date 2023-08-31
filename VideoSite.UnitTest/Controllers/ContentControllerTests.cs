using BusinessLayer.Validators.ViewModels.ContentController;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitTest.Repositories;
using EntityLayer.Models.Contents;
using EntityLayer.ViewModels.ContentController;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using ToolsLayer.FormDataSerializer;
using ToolsLayer.Test;
using VideoSite.Controllers;
using Xunit.Priority;

namespace VideoSite.UnitTest.Controllers
{

    public class ContentControllerTests:ControllerTestBase
    {
        [Fact, Priority(-10)]
        public async void Create()
        {
            var data = new ContentViewModel()
            {
                Description = "Foo",
                Name = "Bar",
                ImageLink = "/foo.png",
                EpisodeList = new() {
                    new Episode() {
                        Name = "Bölüm 1",
                        SourceList = new() {
                            new SourceOfIframe() {
                                Name = "Youtube", IframeCode = "https://foo.com" } } } }
            };
            ByteArrayContent img = CreateImageContentForTest.Create();
            var postData = ObjectToMultipartFormDataConverter.Convert(data);
            postData.Add(img, "File", "test.png");

            var res = await Client.PostAsync("api/content/create", postData);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void GetListWithOutParameter()
        {
            var res =await Client.GetAsync("api/Content/GetList");
            var values =  JsonConvert.DeserializeObject<List<Content>>(await res.Content.ReadAsStringAsync());

            res.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK,res.StatusCode);
            Assert.Equal(1, values.Count);
        }
        [Fact]
        public async void Get()
        {
            var res = await Client.GetAsync("api/content/get/" + DefaultFirstItemId);
            var value=JsonConvert.DeserializeObject<Content>(await res.Content.ReadAsStringAsync());

            res.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK,res.StatusCode);
            Assert.True(value!=null);
        }
        [Fact]
        public async void Watch()
        {
            var res = await Client.GetAsync("api/content/watch/"+DefaultFirstItemId);
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void CheckUpdateActionReturnValidValue()
        {
            var res = await Client.GetAsync("api/content/update/" + DefaultFirstItemId);
            var data = JsonConvert.DeserializeObject<ContentViewModel>(await res.Content.ReadAsStringAsync());
            var validationresult = new ContentViewModelValidator(EntityLayer.Enums.CrudMethodEnum.Update).Validate(data);
            Assert.True(validationresult.IsValid);
        }
        [Fact]
        public async void UpdateGetAction()
        {
            var res = await Client.GetAsync("api/content/update/" + DefaultFirstItemId);
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async void UpdatePostAction()
        {
            var res = await Client.GetAsync("api/content/update/"+DefaultFirstItemId);
            var data = JsonConvert.DeserializeObject<ContentViewModel>(await res.Content.ReadAsStringAsync());
            res = await Client.PostAsync("api/content/update", ObjectToMultipartFormDataConverter.Convert(data));

            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact,Priority(10)]
        public async void Delete()
        {
             var res = await Client.GetAsync("api/content/delete/" + DefaultFirstItemId);
             Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
