using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ContentRepository:GenericBaseRepository<Content>
    {
        public ContentRepository(ADC db):base(db)
        {
        }
    }
}
