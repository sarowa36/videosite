using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CrudProcedures.Enum
{
    [Flags]
    public enum HttpMethods
    {
        Get=0,
        Post=1,
        Put=2,
        Delete=4
    }
}
