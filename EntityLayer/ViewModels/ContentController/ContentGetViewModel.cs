using EntityLayer.Models.Contents;

namespace EntityLayer.ViewModels.ContentController
{
    public class ContentGetViewModel
    {
        public ContentGetViewModel() { }
        public ContentGetViewModel(Content content)
        {
            this.Id = content.Id;
            this.Name = content.Name;
            this.Description = content.Description;
            this.EpisodeList = content.EpisodeList;
            this.ImageLink = content.ImageLink;
            content.ContentM2MCategories.ForEach(x => this.Categories.Add(x.CategoryId,x.Category.Name));
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<int,string> Categories { get; set; } = new Dictionary<int, string>();
        public List<Episode> EpisodeList { get; set; } = new List<Episode>();
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
