using DimmentionMaker.Commands;
using DimmentionMaker.Models;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;

namespace DimmentionMaker.Creators
{
    public class FrontViewCommandCreator : ICommandCreator
    {
        private List<IDimmensionCommand> _commands = new List<IDimmensionCommand>();
        private View _view;
        private TransformationPlane _tPlane; 
        private CastUnitDrawing _drawing;
        private Assembly _assembly;
        private AABB _mainPartBounds;
        private Tekla.Structures.Drawing.ModelObject _drawingObject;

        public FrontViewCommandCreator()
        {
            Setup();
            AddCommands();
        }
        public List<IDimmensionCommand> CreateCommands()
        {
            return _commands;
        }

        private void Testing(Solid solid)
        {
        }

        private void Setup()
        {
            var dh = new DrawingHandler();
            _drawing = dh.GetActiveDrawing() as CastUnitDrawing;
            if (_drawing is null) Console.WriteLine("The script is supported for cast unit drawings");
            _view =_drawing.GetSheet().GetAllViews().ToAList<View>().Where(x=>x.Name=="VIRSSKATS").ToList().First();
            SetupCoordinates();
            var cuId = _drawing.CastUnitIdentifier;
            _assembly = (new Model().SelectModelObject(cuId) as Assembly);
            var mainPart = _assembly.GetMainPart() as Part;
            var points =mainPart.GetSolid().GetPointList();
            _drawingObject = _view.GetModelObjects(mainPart.Identifier).ToAList<DrawingObject>().First() 
                as Tekla.Structures.Drawing.ModelObject;
            if (mainPart is null ) { return; }
            var solid = mainPart.GetSolid();
            var minPt = solid.MinimumPoint;
            var maxPt = solid.MaximumPoint;
            _mainPartBounds = new AABB(minPt,maxPt);
        }

        private void SetupCoordinates()
        {
            var cs = _view.ViewCoordinateSystem;
            Model model = new Model(); 
            var wph = model.GetWorkPlaneHandler();
            _tPlane = wph.GetCurrentTransformationPlane();
            wph.SetCurrentTransformationPlane(new TransformationPlane(cs));
        }

        private void AddCommands()
        {
            _commands.Add(new ClearDimmensionsCommand(_view));
            _commands.Add(new AddOverallDimCommand(_view,_mainPartBounds,Dirrections.Left));
            _commands.Add(new AddOverallDimCommand(_view,_mainPartBounds,Dirrections.Bottom));
            _commands.AddRange(new EmbedDimmensionCommandCreator(_assembly,_view).GetCommands());
            _commands.AddRange(new OpeningCommandCreator(
                new List<string> { "LIFTING CUT" }, 
                new List<Vector> { Dirrections.Top, Dirrections.Left},
                _assembly,
                _view).GetCommands());
            _commands.Add(new ResetCoordinateSystemCommand(_tPlane,_drawing));
            _drawing.CommitChanges();
        }
    }
}
