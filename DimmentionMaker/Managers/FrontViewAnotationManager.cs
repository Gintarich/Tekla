using DimmentionMaker.Commands;
using DimmentionMaker.Creators;
using DimmentionMaker.Interfaces;
using DimmentionMaker.Models;
using DimmentionMaker.Providers;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;

namespace DimmentionMaker.Managers
{
    public class FrontViewAnotationManager : IAnotationManager
    {
        private CommandQueue _commands = new CommandQueue();
        private View _view;
        private CastUnitDrawing _drawing;
        private Assembly _assembly;
        private TieBeamConfig _config = TieBeamConfig.Instance;

        public FrontViewAnotationManager()
        {
            Setup();
            AddCommands();
        }

        private void Setup()
        {
            var dh = new DrawingHandler();
            _drawing = dh.GetActiveDrawing() as CastUnitDrawing;
            if (_drawing is null) Console.WriteLine("The script is supported for cast unit drawings");
            _view = _drawing.GetSheet().GetAllViews().ToAList<View>().Where(x => x.Name == _config.GeoFrontViewName).ToList().First();
            var cuId = _drawing.CastUnitIdentifier;
            _assembly = (new Model().SelectModelObject(cuId) as Assembly);

            var mainPart = _assembly.GetMainPart() as Part;
            var points = mainPart.GetSolid().GetPointList();
            if (mainPart is null) { return; }
            var solid = mainPart.GetSolid();
            var minPt = solid.MinimumPoint;
            var maxPt = solid.MaximumPoint;
        }

        private void AddCommands()
        {
            _view.SetWorkPlane();
            _commands.Add(new ClearDimmensionsAndTextCommand(_view));
            _commands.Add(new AddOverallDimCommand(_view, _assembly, Dirrections.Left));
            _commands.Add(new AddOverallDimCommand(_view, _assembly, Dirrections.Bottom));
            _commands.AddRange(new EmbedDimmensionCmdProvider(_assembly, _view).GetCommands());
            _commands.AddRange(new OpeningCmdByNameCreator(
                new List<string> { "LIFTING CUT" },
                new List<Vector> { Dirrections.Top, Dirrections.Left },
                _assembly,
                _view).GetCommands());
            _commands.AddRange(new OpeningCmdByNameCreator(
                new List<string> {"BEAM"},
                new List<Vector> { Dirrections.Top },
                _assembly,
                _view).GetCommands());
            _commands.AddRange(new ChamferCmdProvider(_assembly, _view).GetCommands());
            _view.ReleaseWorkPlane();
            _drawing.CommitChanges();
        }

        public void RunCommands()
        {
            _commands.ExecuteCommands();
        }
    }
}
