using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ViewModels.CategoryController
{
    public class CategoryUpdateViewModel
    {
        public CategoryUpdateViewModel(){ }
        public CategoryUpdateViewModel(Category c)
        {
                this.Id = c.Id;
                this.Name= c.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Category AsCategory()
        {
            return new Category { Id = Id, Name = Name };
        }
    }
}
