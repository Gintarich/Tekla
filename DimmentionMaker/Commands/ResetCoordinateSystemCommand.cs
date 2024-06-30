using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Commands
{
    public class ResetCoordinateSystemCommand : IDimmensionCommand
    {
        private Drawing _drawing;
        private TransformationPlane _transformationPlane;
        public ResetCoordinateSystemCommand(TransformationPlane tp, Drawing d)
        {
            _drawing = d;
            _transformationPlane = tp;
        }
        public void Execute(int idx)
        {
            _drawing.CommitChanges();
            new Model().GetWorkPlaneHandler().SetCurrentTransformationPlane(_transformationPlane);
        }
        public DimmensionCommandType GetCommandType()
        {
            return DimmensionCommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 99;
        }
    }
}
