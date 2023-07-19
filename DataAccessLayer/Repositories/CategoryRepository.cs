using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CategoryRepository : GenericBaseRepository<Category>
    {
        public CategoryRepository(ADC db) : base(db)
        {
        }
    }
}
