using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsLayer.List
{
    public static class ManageList
    {
        public static void RemoveBeforeAddNew(this IEnumerable<int> beforeList, IEnumerable<int> newList, Action<IEnumerable<int>> removeFunc, Action<IEnumerable<int>> addFunc)
        {
            var removeList = (beforeList ?? new List<int>()).Except(newList??new List<int>());
            var addlist = (newList??new List<int>()).Except(beforeList ?? new List<int>());
            if (removeList != null && removeList.Count() > 0)
                removeFunc.Invoke(removeList);
            if (addlist != null && addlist.Count() > 0)
                addFunc.Invoke(addlist);
        }
        /*public static void RemoveBeforeAddNew<T>(this IEnumerable<int> beforeList, IEnumerable<int> newList, ADC db,Func<T,bool> func, string propName) where T : class
        {
            var removeList = beforeList.Except(newList);
            var addlist = newList.Except(beforeList);
            if (removeList != null && removeList.Count() > 0)
                db.Set<T>().RemoveRange(db.Set<T>().Where(x=>func.Invoke(x) && removeList.Contains(EF.Property<int>(x,propName))));
           
        }*/
    }
}
