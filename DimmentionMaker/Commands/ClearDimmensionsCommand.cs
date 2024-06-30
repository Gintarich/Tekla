using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace DimmentionMaker.Commands
{
    public class ClearDimmensionsCommand : IDimmensionCommand
    {
        private View _view;
        public ClearDimmensionsCommand(View view)
        {
            _view = view;
        }
        public void Execute(int idx)
        {
            var objs = _view.GetObjects();
            foreach (var obj in objs)
            {
                if (obj is StraightDimensionSet)
                {
                    var set = obj as StraightDimensionSet;
                    set.Delete();
                }
            }
        }

        public DimmensionCommandType GetCommandType()
        {
            return DimmensionCommandType.NotADimmension;
        }

        public int GetImportance()
        {
            return 0;
        }
    }
}
