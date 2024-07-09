using DimmentionMaker.Commands;
using DimmentionMaker.Models;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.RemotingHelper;
using Part = Tekla.Structures.Model.Part;
using TSD = Tekla.Structures.Drawing;

namespace DimmentionMaker.Creators
{
    public class EmbedDimmensionCommandCreator
    {
        private Assembly _assembly;
        private View _view;
        private AABB _aabb;
        private List<IDrawingCommand> _commandList = new List<IDrawingCommand>();

        public EmbedDimmensionCommandCreator(Assembly assembly, View view)
        {
            _assembly = assembly;
            _view = view;
            Setup();
            CreateCommands();
        }

        private void Setup()
        {
            _aabb = _assembly.GetBox();
        }

        public List<IDrawingCommand> GetCommands() { return _commandList; }

        public void CreateCommands()
        {
            //Select all assemblies and group them by name
            var groups = _assembly.GetSubAssemblies().Cast<Assembly>().GroupBy(x => x.Name);

            //Foreach group find Top, Bottom, Right and Left Points
            foreach (var group in groups)
            {
                var id = _assembly.GetMainPart().Identifier;
                var obj = _view.GetModelObjects(id).ToAList<TSD.ModelObject>().First();
                var rightAttr = AttributeProvider.GetRightMarkAttributes(obj);
                var leftAttr = AttributeProvider.GetLeftMarkAttributes(obj);
                string name = group.Key;
                var cl = (group.First().GetMainPart() as Part).Class;
                var topPoints = group.ToList().GetPoints(_aabb, Dirrections.Top, name);
                if (topPoints.Count > 2)
                    _commandList.Add(new AddDimmensionCommand(topPoints, _view, CommandType.TopDimmension, leftAttr));
                var botPoints = group.ToList().GetPoints(_aabb, Dirrections.Bottom, name);
                if (botPoints.Count > 2 && cl == "100")
                    _commandList.Add(new AddDimmensionCommand(botPoints, _view, CommandType.BottomDimmension, rightAttr));
                var leftPoints = group.ToList().GetPoints(_aabb, Dirrections.Left, name);
                if (leftPoints.Count > 2 && cl == "100")
                    _commandList.Add(new AddDimmensionCommand(leftPoints, _view, CommandType.LeftDimmension, leftAttr));
                var rightPoints = group.ToList().GetPoints(_aabb, Dirrections.Right, name);
                if (rightPoints.Count > 2 && cl == "100")
                    _commandList.Add(new AddDimmensionCommand(rightPoints, _view, CommandType.RightDimmension, rightAttr));
            }
        }
    }
}
