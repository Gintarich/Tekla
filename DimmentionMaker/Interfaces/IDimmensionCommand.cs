using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimmentionMaker
{
    public enum DimmensionCommandType
    {
        TopDimmension = 0,
        BottomDimmension = 1,
        LeftDimmension = 2,
        RightDimmension = 3,
        NotADimmension = 4,
    }
    public interface IDimmensionCommand
    {
        void Execute(int idx);
        int GetImportance();
        DimmensionCommandType GetCommandType();
    }
}
