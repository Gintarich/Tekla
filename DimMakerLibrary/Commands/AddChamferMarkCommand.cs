using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace DimMakerLibrary.Commands
{
    public class AddChamferMarkCommand : IDrawingCommand
    {
        private readonly View _view;
        private readonly Point _placementPoint;

        public AddChamferMarkCommand(View view, Point placementPoint)
        {
            _view = view;
            _placementPoint = placementPoint;
        }
        public void Execute(int idx)
        {
            var txt = new Text(_view, _placementPoint, "F");
            txt.Insert();
            _view.GetDrawing().CommitChanges();
        }

        public CommandType GetCommandType()
        {
            return CommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 100;
        }
    }
}
