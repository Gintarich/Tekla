using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace ExtensionMethods
{
    public static class DrawingExtensions
    {
        public static List<Drawing> ToList(this DrawingEnumerator ds)
        {
            var list = new List<Drawing>();
            while (ds.MoveNext())
            {
                list.Add(ds.Current);
            }
            return list;
        }
    }
}
