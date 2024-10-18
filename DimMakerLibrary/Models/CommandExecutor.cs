using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimMakerLibrary.Models
{
    public class CommandExecutor
    {
        private CommandQueue _commandQueue  = new CommandQueue ();
        public CommandExecutor(List<ICommandCreator> commandCreators)
        {
            foreach (var commandCreator in commandCreators)
            {
                _commandQueue.AddRange(commandCreator.CreateCommands());
            }
        }
        public void Run()
        {
            _commandQueue.ExecuteCommands();
        }
    }
}
