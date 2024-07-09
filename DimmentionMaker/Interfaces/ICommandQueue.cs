using System.Collections.Generic;

namespace DimmentionMaker.Interfaces
{
    public interface ICommandQueue
    {
        void AddRange(List<IDrawingCommand> commands);
        void ExecuteCommands();
    }
}