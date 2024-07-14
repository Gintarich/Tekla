using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using ExtensionMethods;

namespace DimmentionMaker.Commands
{
    public class PrintValueCommand : IDrawingCommand
    {
        private readonly Drawing _drawing;

        public PrintValueCommand(Drawing drawing)
        {
            _drawing = drawing;
        }
        public void Execute(int idx)
        {
            var views = _drawing.GetSheet()
                                .GetAllViews()
                                .FilterType<View>();
            foreach ( var view in views )
            {
                string value = "";
                view.GetUserProperty("Test", ref value);
                Console.WriteLine(value);
            }
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
