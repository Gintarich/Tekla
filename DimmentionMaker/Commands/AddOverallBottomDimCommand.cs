using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;

namespace DimmentionMaker.Commands
{
    public class AddOverallBottomDimCommand : IDimmensionCommand
    {
        private int _importance;
        private View _view;
        private AABB _aabb;
        public AddOverallBottomDimCommand(View view, AABB mainPartBounds)
        {
            _view = view; 
            _importance = 10;
            _aabb = mainPartBounds;
        }
        public void Execute(int idx)
        {
            var startOffset = 200;
            var baselineSpacing = 150;
            var index = idx;
            var minPt = _aabb.MinPoint;
            var maxPt = _aabb.MaxPoint;
            //Bottom points
            var p1 = new Point(minPt.X, minPt.Y, 0);
            var p2 = new Point(maxPt.X,minPt.Y, 0);
            var list1 = new PointList { p1, p2 };
            //Side points 
            var strDimSetHandler = new StraightDimensionSetHandler();
            //Insert dim lines
            strDimSetHandler.CreateDimensionSet(_view, list1, new Vector(0, -1, 0), startOffset + baselineSpacing * index);
        }

        public DimmensionCommandType GetCommandType()
        {
            return DimmensionCommandType.BottomDimmension;
        }

        public int GetImportance()
        {
            return _importance;
        }
    }
}
