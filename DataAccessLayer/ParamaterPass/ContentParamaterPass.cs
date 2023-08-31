using EntityLayer.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ParamaterPass
{
    public class ContentParamaterPass:IParamaterPass<Content>
    {
        public List<int> RequestCategoryIds { get; set; }
    }
}
