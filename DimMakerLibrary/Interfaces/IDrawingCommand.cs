using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimMakerLibrary
{
    public enum CommandType
    {
        TopDimmension = 0,
        BottomDimmension = 1,
        LeftDimmension = 2,
        RightDimmension = 3,
        NotADimmension = 4,
    }
    public interface IDrawingCommand
    {
        void Execute(int idx);
        int GetImportance();
        CommandType GetCommandType();
    }
}
