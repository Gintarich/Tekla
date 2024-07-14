using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using ExtensionMethods;
using TSD = Tekla.Structures.Drawing;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;
using DimmentionMaker.Models;

namespace AnotherConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dh = new DrawingHandler();
            var view = dh.GetActiveDrawing().GetSheet().GetAllViews().FilterType<View>().Where(x => x.Name == "B").FirstOrDefault();
            view.SetWorkPlane();
            var drawing = dh.GetActiveDrawing() as CastUnitDrawing;
            var cuId = drawing.CastUnitIdentifier;
            var assembly = (new Model().SelectModelObject(cuId) as Assembly);
            var box = view.RestrictionBox;
            var mainpart = assembly.GetMainPart() as Part;
            var solid = mainpart.GetSolid(Solid.SolidCreationTypeEnum.NORMAL_WITHOUT_EDGECHAMFERS);
            var ipoints = box.Intersection(solid);
            var pts = ipoints.RemoveRedundant(Dirrections.Top);
            view.ReleaseWorkPlane();
        }
        public static void OldTest()
        {
            var dh = new DrawingHandler();
            var view = dh.GetActiveDrawing().GetSheet().GetAllViews().FilterType<View>().Where(x => x.Name == "A").FirstOrDefault();
            var objs = view.GetModelObjects().ToAList<TSD.ModelObject>();
            PointList points = new PointList()
            {
                new Point(0,-100,0),
                new Point(6330,-100,0),
            };
            var strDimSetHandler = new StraightDimensionSetHandler();
            //Insert dim lines
            var set = strDimSetHandler.CreateDimensionSet(view, points, new Vector(0, -1, 0), 200);
            dh.GetActiveDrawing().CommitChanges();
        }
    }
}
