using DimMakerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace DimMakerLibrary.Commands
{
    public class AddDimmensionCommand : IDrawingCommand
    {
        private PointList _points;
        private View _view;
        private CommandType _type;
        private int _importance;
        private StraightDimensionSet.StraightDimensionSetAttributes _attributes;

        public AddDimmensionCommand(
            PointList points,
            View view,
            CommandType type,
            StraightDimensionSet.StraightDimensionSetAttributes attributes,
            int importance = 1)
        {
            _importance = importance;
            _view = view;
            _points = points;
            _type = type;
            _attributes = attributes;
        }
        public void Execute(int idx)
        {
            
            var startOffset = 200;
            var baselineSpacing = 150;
            var index = idx;
            var strDimSetHandler = new StraightDimensionSetHandler();
            //Insert dim lines
            var dir = _type.GetVector();
            var filter = _attributes.ExcludePartsAccordingToFilter;

            var t = strDimSetHandler.CreateDimensionSet(_view, _points, dir, startOffset + baselineSpacing * index, _attributes);
            t.Attributes.ExcludePartsAccordingToFilter = filter;
            t.Modify();
        }

        public CommandType GetCommandType()
        {
            return _type;
        }

        public int GetImportance()
        {
            return _importance;
        }
        
    }
}
