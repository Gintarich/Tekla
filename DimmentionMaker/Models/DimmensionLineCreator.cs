using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimmentionMaker.Models
{
    public class DimmensionLineCreator
    {
        private DimmensionCommandQueue _commandQueue  = new DimmensionCommandQueue ();
        public DimmensionLineCreator(List<ICommandCreator> commandCreators)
        {
            foreach (var commandCreator in commandCreators)
            {
                _commandQueue.AddRange(commandCreator.CreateCommands());
            }
            _commandQueue.Sort();
        }
        public void Run()
        {
            _commandQueue.ExecuteCommands();
        }
    }
}
