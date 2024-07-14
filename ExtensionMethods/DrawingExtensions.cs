using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

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

        public static void SetWorkPlane(this View view)
        {
            var cs = view.ViewCoordinateSystem;
            Model model = new Model(); 
            var wph = model.GetWorkPlaneHandler();
            wph.SetCurrentTransformationPlane(new TransformationPlane(cs));
        }
        
        public static void ReleaseWorkPlane(this View view)
        {
            new Model().GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());
        }
    }
}
