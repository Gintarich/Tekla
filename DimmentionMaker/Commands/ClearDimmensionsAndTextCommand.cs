using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace DimmentionMaker.Commands
{
    public class ClearDimmensionsAndTextCommand : IDrawingCommand
    {
        private View _view;
        public ClearDimmensionsAndTextCommand(View view)
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
                else if (obj is Text)
                {
                    var text = obj as Text;
                    text.Delete();
                }
            }
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
