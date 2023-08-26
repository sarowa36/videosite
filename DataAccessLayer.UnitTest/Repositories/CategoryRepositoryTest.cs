using DataAccessLayer.Repositories;
using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Priority;

namespace DataAccessLayer.UnitTest.Repositories
{
   
    public class CategoryRepositoryTest : RepositoryTestBaseClass<Category, CategoryRepository>
    {
        public override void TryToAddNewValidValue()
        {
            var val = new Category()
            {
                Name= "Test"
            };
           
            var result = repo.Create(val);
            Assert.True(string.IsNullOrWhiteSpace(result));
        }
    }
}
