using DimmentionMaker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Models
{
    public class CommandQueue : ICommandQueue
    {
        private List<IDrawingCommand> _commands = new List<IDrawingCommand>();
        private int _topLineCount;
        private int _bottomLineCount;
        private int _rightLineCount;
        private int _leftLineCount;

        public void AddRange(List<IDrawingCommand> commands)
        {
            _commands.AddRange(commands);
            Sort();
        }

        public void Add(IDrawingCommand command)
        {
            _commands.Add(command);
        }

        private void Sort()
        {
            _commands = _commands.OrderBy(x => x.GetImportance()).ToList();
        }

        public void ExecuteCommands()
        {
            foreach (IDrawingCommand command in _commands)
            {
                switch (command.GetCommandType())
                {
                    case CommandType.TopDimmension:
                        command.Execute(_topLineCount);
                        _topLineCount++;
                        break;
                    case CommandType.BottomDimmension:
                        command.Execute(_bottomLineCount);
                        _bottomLineCount++;
                        break;
                    case CommandType.LeftDimmension:
                        command.Execute(_leftLineCount);
                        _leftLineCount++;
                        break;
                    case CommandType.RightDimmension:
                        command.Execute(_rightLineCount);
                        _rightLineCount++;
                        break;
                    default:
                        command.Execute(0);
                        break;
                }
            }
        }
    }
}
