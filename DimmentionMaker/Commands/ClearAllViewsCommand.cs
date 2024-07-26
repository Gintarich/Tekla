using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace DimmentionMaker.Commands
{
    internal class ClearAllViewsCommand : IDrawingCommand
    {
        private readonly Drawing _drawing;

        public ClearAllViewsCommand(Drawing drawing )
        {
            _drawing = drawing;
        }

        public void Execute(int idx)
        {
            var views = _drawing.GetSheet().GetAllViews();
            while(views.MoveNext())
            {
                views.Current.Delete();
            }
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
