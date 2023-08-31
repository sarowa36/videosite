using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolsLayer.TypeHelper
{
    public static class StaticMethods
    {
        public static bool IsType<TType>(this Type type)
        {
            if (!(type == typeof(TType)))
            {
                return typeof(TType)!.IsAssignableFrom(type);
            }

            return true;

        }
        public static bool IsEnumerable(this Type type)
        {
            if (type.IsType<IEnumerable>() && type.IsNotType<byte[]>())
            {
                return type.IsNotType<string>();
            }

            return false;
        }
        public static bool IsNotType<TType>(this Type type)
        {
            return !type.IsType<TType>();
        }

        public static bool HasCustomAttribute<TAttribute>(this PropertyInfo property) where TAttribute : Attribute
        {
            TAttribute attribute;
            return property.HasCustomAttribute<TAttribute>(out attribute);
        }

        public static bool HasCustomAttribute<TAttribute>(this PropertyInfo property, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = (((object)property != null) ? property.GetCustomAttribute<TAttribute>(inherit: true) : null);
            return attribute != null;
        }

        public static bool HasCustomAttribute<TAttribute>(this FieldInfo field, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = (((object)field != null) ? field.GetCustomAttribute<TAttribute>(inherit: true) : null);
            return attribute != null;
        }

        public static bool HasCustomAttribute<TAttribute>(this Type type, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = type.GetCustomAttribute<TAttribute>(inherit: true);
            return attribute != null;
        }
    }
}
