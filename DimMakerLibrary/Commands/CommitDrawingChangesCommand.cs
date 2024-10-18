using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;

namespace DimMakerLibrary.Commands
{
    public class CommitDrawingChangesCommand : IDrawingCommand
    {
        private readonly Drawing _drawing;

        public CommitDrawingChangesCommand(Drawing drawing)
        {
            _drawing = drawing;
        }
        public void Execute(int idx)
        {
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
