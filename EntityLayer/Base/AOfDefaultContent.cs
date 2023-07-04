using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Base
{
    public abstract class AOfDefaultContent : ISoftDeletable, IIdentify
    {
        public abstract int Id { get; set; }
        public abstract bool IsDeleted { get; set; }
    }
}
