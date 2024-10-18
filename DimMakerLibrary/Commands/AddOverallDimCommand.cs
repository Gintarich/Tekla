using DimMakerLibrary.Models;
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

namespace DimMakerLibrary.Commands
{
    public class AddOverallDimCommand : IDrawingCommand
    {
        private Vector _dirrection;
        private View _view;
        private readonly Assembly _assembly;
        private AABB _aabb;
        public AddOverallDimCommand(View view, Assembly assembly, Vector dir)
        {
            _dirrection = dir;
            _view = view;
            _assembly = assembly;
            _aabb = _assembly.GetBox();
        }
        public void Execute(int idx)
        {
            var startOffset = 200;
            var baselineSpacing = 150;
            var index = idx;
            var minPt = _aabb.MinPoint;
            var maxPt = _aabb.MaxPoint;
            //Bottom points
            var list1 = new PointList();
            if (_dirrection == Dirrections.Bottom)
            {
                list1.Add(new Point(minPt.X, minPt.Y, 0));
                list1.Add(new Point(maxPt.X, minPt.Y, 0));
            }
            else if (_dirrection == Dirrections.Left)
            {
                list1.Add(new Point(minPt.X, maxPt.Y, 0));
                list1.Add(new Point(minPt.X, minPt.Y, 0));
            }
            else if (_dirrection == Dirrections.Top)
            {
                list1.Add(new Point(minPt.X, maxPt.Y, 0));
                list1.Add(new Point(maxPt.X, maxPt.Y, 0));
            }
            else if (_dirrection == Dirrections.Right)
            {
                list1.Add(new Point(maxPt.X, maxPt.Y, 0));
                list1.Add(new Point(maxPt.X, minPt.Y, 0));
            }
            else
            {
                return;
            }
            var strDimSetHandler = new StraightDimensionSetHandler();
            //Insert dim lines
            var attr = AttributeProvider.GetAttribute(_view, _assembly);
            strDimSetHandler.CreateDimensionSet(_view, list1, _dirrection, startOffset + baselineSpacing * index, attr);
        }

        public CommandType GetCommandType()
        {
            return Utils.GetDimCommandTypeFromDir(_dirrection);
        }

        public int GetImportance()
        {
            return 10;
        }
    }
}
