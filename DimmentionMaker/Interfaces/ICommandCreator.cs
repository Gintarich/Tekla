using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimmentionMaker
{
    public interface ICommandCreator
    {
        List<IDimmensionCommand> CreateCommands();
    }
}
