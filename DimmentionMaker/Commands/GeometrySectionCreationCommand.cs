using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Dialog.UIControls;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace DimmentionMaker.Commands
{
    public class GeometrySectionCreationCommand : IDrawingCommand
    {
        private readonly Point _insertionPoint;
        private readonly View _frontView;
        private readonly Point _startPoint;
        private readonly Point _endPoint;

        public GeometrySectionCreationCommand(Point insertionPoint, View frontView, Point startPoint, Point endPoint)
        {
            _insertionPoint = insertionPoint;
            _frontView = frontView;
            _startPoint = startPoint;
            _endPoint = endPoint;
        }
        public void Execute(int idx)
        {
            View generatedView = null;
            SectionMark sectionMark = null;
            View.ViewAttributes attr = new View.ViewAttributes();
            attr.LoadAttributes("standard");
            SectionMark.SectionMarkAttributes sectionMarkAttributes = new SectionMark.SectionMarkAttributes();
            sectionMarkAttributes.LoadAttributes("standard");
            View.CreateSectionView(_frontView, _startPoint, _endPoint, _insertionPoint, 200, 200, attr, sectionMarkAttributes,
                out generatedView, out sectionMark);
        }

        public CommandType GetCommandType()
        {
            return CommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 99;
        }
    }
}
