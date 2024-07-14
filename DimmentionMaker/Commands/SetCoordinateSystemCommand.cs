using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Commands
{
    public class SetCoordinateSystemCommand : IDrawingCommand
    {
        private readonly View _view;

        public SetCoordinateSystemCommand(View view)
        {
            _view = view;
        }

        public void Execute(int idx)
        {
            var cs = _view.ViewCoordinateSystem;
            Model model = new Model(); 
            var wph = model.GetWorkPlaneHandler();
            wph.SetCurrentTransformationPlane(new TransformationPlane(cs));
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
