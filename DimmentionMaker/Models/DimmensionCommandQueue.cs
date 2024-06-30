using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Models
{
    public class DimmensionCommandQueue
    {
        private List<IDimmensionCommand> _commands = new List<IDimmensionCommand>();
        private int _topLineCount;
        private int _bottomLineCount;
        private int _rightLineCount;
        private int _leftLineCount;

        public void AddRange(List<IDimmensionCommand> commands)
        {
            _commands.AddRange(commands);
        }
        public void Sort()
        {
            _commands = _commands.OrderBy(x => x.GetImportance()).ToList();
            foreach (var command in _commands)
            {
            }
        }

        internal void ExecuteCommands()
        {
            foreach (IDimmensionCommand command in _commands)
            {
                switch (command.GetCommandType())
                {
                    case DimmensionCommandType.TopDimmension:
                        command.Execute(_topLineCount);
                        _topLineCount++;
                        break;
                    case DimmensionCommandType.BottomDimmension:
                        command.Execute(_bottomLineCount);
                        _bottomLineCount++;
                        break;
                    case DimmensionCommandType.LeftDimmension:
                        command.Execute(_leftLineCount);
                        _leftLineCount++;
                        break;
                    case DimmensionCommandType.RightDimmension:
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
