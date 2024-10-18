using System.Collections.Generic;

namespace DimMakerLibrary.Interfaces
{
    public interface ICommandQueue
    {
        void AddRange(List<IDrawingCommand> commands);
        void Add(IDrawingCommand command);
        void ExecuteCommands();
    }
}