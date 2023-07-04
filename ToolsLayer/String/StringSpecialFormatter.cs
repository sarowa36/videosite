using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsLayer.String
{
    public static class StringSpecialFormatter
    {
        public static string SpecialFormat(this string str,object obj)
        {
            obj.GetType().GetProperties().ToList().ForEach(field =>
            {
              str=  System.Text.RegularExpressions.Regex.Replace(str, $"{{\\s*{field.Name}\\s*}}",field.GetValue(obj).ToString());
            }); 
            return str;
        }
    }
}
