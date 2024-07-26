using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace DimmentionMaker.Commands
{
    public class CreateFrontViewCmd : IDrawingCommand
    {
        private readonly Point _insertionPoint;
        private readonly Drawing _drawing;
        private readonly View.ViewAttributes _attributes;
        private readonly string _viewName;

        public CreateFrontViewCmd(Point insertionPoint, Drawing drawing, View.ViewAttributes attributes, string viewName)
        {
            _insertionPoint = insertionPoint;
            _drawing = drawing;
            _attributes = attributes;
            _viewName = viewName;
        }

        public void Execute(int idx)
        {
            View createdView;
            View.CreateFrontView(_drawing, _insertionPoint, _attributes, out createdView);
            createdView.Name = _viewName;
            var offset = (createdView.RestrictionBox.MaxPoint.X - createdView.RestrictionBox.MinPoint.X) / (2*createdView.Attributes.Scale);
            createdView.Origin.X += offset;
            createdView.Modify();
            _drawing.CommitChanges();
        }

        public CommandType GetCommandType()
        {
            return CommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 0;
        }
    }
}
