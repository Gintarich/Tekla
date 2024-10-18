using System;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;
using TSD = Tekla.Structures.Drawing;
using DimMakerLibrary.Commands;
using DimMakerLibrary.Models;

namespace DimMakerLibrary.Creators
{
    public class OpeningCmdAutoCreator
    {
        private readonly List<Vector> _directions;
        private readonly Assembly _assembly;
        private readonly View _view;
        private List<IDrawingCommand> _commandList = new List<IDrawingCommand>();

        public OpeningCmdAutoCreator( List<Vector> directions, Assembly assembly, View view)
        {
            _directions = directions;
            _assembly = assembly;
            _view = view;
            CreateCommands();
        }

        public List<IDrawingCommand> GetCommands() { return _commandList; }

        private void CreateCommands()
        {
            var mp = _assembly.GetMainPart();
            var viewBox = _view.RestrictionBox;
            var id = mp.Identifier;
            var obj = _view.GetModelObjects(id).ToAList<TSD.ModelObject>().First();

            var solid = (mp as Part).GetSolid(Solid.SolidCreationTypeEnum.NORMAL_WITHOUT_EDGECHAMFERS);
            var pointList = viewBox.Intersection(solid);
            //Create commands based on dirrections
            foreach (var dir in _directions)
            {
                var cleanPts = pointList.RemoveRedundant(dir);
                if (cleanPts.Count < 3) continue; // Opening not found
                var command = new AddDimmensionCommand(
                    pointList.RemoveRedundant(dir),
                    _view,
                    Utils.GetDimCommandTypeFromDir(dir),
                    AttributeProvider.GetAttribute(obj));
                _commandList.Add(command);
            }
        }
    }
}
