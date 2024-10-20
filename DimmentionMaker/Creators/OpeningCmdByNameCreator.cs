﻿using DimMakerLibrary.Commands;
using DimMakerLibrary.Models;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using ModelObject = Tekla.Structures.Model.ModelObject;
using Part = Tekla.Structures.Model.Part;
using TSD = Tekla.Structures.Drawing;

namespace DimMakerLibrary.Creators
{
    public class OpeningCmdByNameCreator
    {
        private readonly List<string> _openingNames;
        private readonly List<Vector> _directions;
        private readonly Assembly _assembly;
        private readonly View _view;
        private List<IDrawingCommand> _commandList = new List<IDrawingCommand>();

        public OpeningCmdByNameCreator(List<string> openingName, List<Vector> directions, Assembly assembly, View view)
        {
            _directions = directions;
            _openingNames = openingName;
            _assembly = assembly;
            _view = view;
            CreateCommands();
        }

        public List<IDrawingCommand> GetCommands() { return _commandList; }

        private void CreateCommands()
        {
            var mp = _assembly.GetMainPart();
            var id = mp.Identifier;
            var obj = _view.GetModelObjects(id).ToAList<TSD.ModelObject>().First();
            var viewBox = _view.RestrictionBox;
            //Find Boolean parts using opening name
            var booleanParts = (_assembly.GetMainPart() as Part).GetBooleans()
                .ToAList<ModelObject>()
                .Where(x => x is BooleanPart && _openingNames.Contains((x as BooleanPart).OperativePart.Name))
                .GetEnumerator()
                .ToAList<BooleanPart>();
            //Check if boolean part is in view box
            var filteredParts = booleanParts.Where(x => viewBox.Collide(x.OperativePart.GetBox())).Select(x => x.OperativePart.GetSolid());
            var pointList = filteredParts.GetPointList();
            //Add corner points

            pointList.AddRange((_assembly.GetMainPart() as Part)
                .GetBox()
                .GetCornerPoints()
                .GetPointList());

            //var solid = (mp as Part).GetSolid(Solid.SolidCreationTypeEnum.NORMAL_WITHOUT_EDGECHAMFERS);
            //var pointList = viewBox.Intersection(solid);
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
