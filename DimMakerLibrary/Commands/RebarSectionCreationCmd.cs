using System;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace DimMakerLibrary.Commands
{
    public class RebarSectionCreationCmd : IDrawingCommand
    {
        private readonly Point _insertionPoint;
        private readonly View _frontView;
        private readonly Point _startPoint;
        private readonly Point _endPoint;
        private readonly string _attrName;

        public RebarSectionCreationCmd(Point insertionPoint,
            View frontView,
            Point startPoint,
            Point endPoint,
            string attribute = "standard")
        {
            _insertionPoint = insertionPoint;
            _frontView = frontView;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _attrName = attribute;
        }

        public void Execute(int idx)
        {
            View generatedView = null;
            SectionMark sectionMark = null;
            View.ViewAttributes attr = new View.ViewAttributes();
            attr.LoadAttributes(_attrName);
            SectionMark.SectionMarkAttributes sectionMarkAttributes = new SectionMark.SectionMarkAttributes();
            sectionMarkAttributes.LoadAttributes("standard");
            var test = View.CreateSectionView(_frontView, _startPoint, _endPoint, _insertionPoint, 200, 200, attr, sectionMarkAttributes,
                out generatedView, out sectionMark);
            GeoSectionTypeManager.SetSectionType(generatedView, SectionType.Reinforcement);
            generatedView.Modify();
            var succ = generatedView.GetDrawing().CommitChanges();
            if (!succ) Console.WriteLine("WTF");
        }

        public CommandType GetCommandType()
        {
            return CommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 2;
        }
    }
}
