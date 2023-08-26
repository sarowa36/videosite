using DataAccessLayer.Repositories;
using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Priority;

namespace DataAccessLayer.UnitTest.Repositories
{
   
    public class ContentRepositoryTest:RepositoryTestBaseClass<Content,ContentRepository>
    {
        public override void TryToAddNewValidValue()
        {
            var val = new Content()
            {
                Description = "Foo",
                Name = "Bar",
                ImageLink = "/foo.png",
                ContentM2MCategories=new(),
                EpisodeList = new() {
                    new Episode() {
                        Name = "Bölüm 1",
                        SourceList = new() {
                            new SourceOfIframe() {
                                Name = "Youtube", IframeCode = "https://foo.com" } } } }
            };
            var result = repo.Create(val);
            Assert.True(string.IsNullOrWhiteSpace(result));
            
        }
    }
}
