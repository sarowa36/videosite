using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ToolsLayer.TypeHelper;

namespace ToolsLayer.FormDataSerializer
{
    public class ObjectToMultipartFormDataConverter
    {
        public MultipartFormDataContent Content { get; set; }
        internal ObjectToMultipartFormDataConverter(object obj)
        {
            this.Content = new MultipartFormDataContent();
            GetContent(obj);
        }
        public static MultipartFormDataContent Convert(object obj)
        {
            return new ObjectToMultipartFormDataConverter(obj).Content;
        }

        private void GetContent(object obj, string? parentName = null)
        {
            foreach (var item in obj.GetType().GetProperties())
            {
                if (item.GetValue(obj) != null && !item.HasCustomAttribute<FormDataSerializeIgnoreAttribute>())
                {
                    if (item.PropertyType.IsPrimitive || item.PropertyType.IsType<string>())
                    {
                        Content.Add(new StringContent(item.GetValue(obj).ToString()), string.IsNullOrWhiteSpace(parentName) ? item.Name : parentName + $"[{item.Name}]");
                    }
                    else if (item.PropertyType.IsEnumerable() || item.PropertyType.IsArray)
                    {
                        int indexer = 0;
                        IEnumerable loopvalue = item.PropertyType.IsArray ? ((object[])item.GetValue(obj)).ToList() : (IEnumerable)item.GetValue(obj);
                        foreach (var enumItem in loopvalue)
                        {
                            GetContent(enumItem, string.IsNullOrWhiteSpace(parentName) ? item.Name + $"[{indexer}]" : parentName + $"[{item.Name}][{indexer}]");
                            indexer++;
                        }
                    }
                }
            }
        }

    }
}
