using EntityLayer.Models.Contents;

namespace VideoSite.ViewModels.ContentController
{
    public class ContentViewModel
    {
        public ContentViewModel(){}
        public ContentViewModel(Content content)
        {
            this.Id = content.Id;
            this.Name = content.Name;
            this.Description = content.Description;
            this.EpisodeList = content.EpisodeList;
            this.ImageLink = content.ImageLink;
            content.ContentM2MCategories.ForEach(x =>this.Categories.Add(x.CategoryId));
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> Categories { get; set; }=new List<int>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
        public IFormFile? File { get; set; }
        public string? ImageLink { get; set; }
        public Content AsContent()
        {
            var returnValue = new Content
            {
                Id = Id,
                Name = Name,
                Description = Description,
                EpisodeList = EpisodeList,
                ImageLink = ImageLink
            };
            return returnValue;
        }
    }
}
