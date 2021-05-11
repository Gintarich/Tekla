using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using Tekla.Structures;

namespace ConsoleApp1
{
    class Gridline
    {
        static void Main(string[] args)
        {
            Grid myGrid = new Grid();
            Model model = new Model();
            Picker picker = new Picker();
            GeometricPlane myPlanes = new GeometricPlane();
            //----------------------------------------------------------------------------
            //var pickedElement = picker.PickObject(Picker.PickObjectEnum.PICK_ONE_OBJECT);
            //var x = TeklaStructuresDatabaseTypeEnum.GRID.ToString();
            //var y = pickedElement.GetType().Name.ToUpper();
            //
            //if (y == x)
            //{
            //    pickedElement = (Grid)pickedElement;
            //  
            //}
            //----------------------------------------------------------------------------

            var selectedGrids = model.GetModelObjectSelector().GetAllObjectsWithType(ModelObject.ModelObjectEnum.GRID);
            while (selectedGrids.MoveNext())
            {
                myGrid = (Grid)selectedGrids.Current;
            }

            var test = myGrid.GetChildren();

            while(test.MoveNext())
            {
                var z = test.Current.GetType();
                GridPlane gridplane = (GridPlane)test.Current;
                var gridplaneCoordSys = gridplane.GetCoordinateSystem();
            }
            Console.ReadLine();
        }
    }
}
