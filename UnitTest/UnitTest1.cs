using BusinessLayer.Validators.ViewModels.ContentController;
using EntityLayer.Models.Contents;
using EntityLayer.ViewModels.ContentController;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ContentViewModelValidator validator= new ContentViewModelValidator();
            ContentViewModel m = new()
            {
                Description = "Foo",
                Name = "Bar",
                ImageLink = "/foo.png",
                Categories = new() { 2 },
                EpisodeList = new() { 
                    new Episode() { 
                        Name = "Bölüm 1",
                        SourceList = new() { 
                            new SourceOfIframe() { 
                                Name = "Youtube", IframeCode = "https://foo.com" } } } }
            };
           var validationResult= validator.Validate(m);
            Console.Write(String.Join(" ++ ", validationResult.Errors.Select(x => x.ErrorMessage)));
            Assert.True(validationResult.IsValid);
        }
    }
}