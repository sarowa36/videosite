using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolsLayer.Test
{
    public static class CreateImageContentForTest
    {
        /// <summary>
        /// Only Useful when debugging
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns><code>ByteArrayContent</code></returns>
        public static ByteArrayContent Create(string? filePath=null)
        {
            if(filePath == null)
            {
                filePath = @".\Test\test.png";
            }
           var upfilebytes = File.ReadAllBytes(filePath);
           return new ByteArrayContent(upfilebytes);
        }
    }
}
