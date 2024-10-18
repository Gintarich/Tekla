using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimMakerLibrary
{
    public interface ICommandCreator
    {
        List<IDrawingCommand> CreateCommands();
    }
}
