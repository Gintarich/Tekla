using DimmentionMaker.Commands;
using DimmentionMaker.Interfaces;
using DimmentionMaker.Models;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.DialogInternal;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;

namespace DimmentionMaker.Creators
{
    public class GeoSectionCmdProvider
    {
        private readonly List<IDrawingCommand> _commands = new List<IDrawingCommand>();
        private readonly List<CreationType> _creationTypes;
        private readonly Assembly _assembly;
        private readonly View _view;
        private readonly List<Assembly> _subassemblies;
        private List<BooleanPart> _openings;
        private int _horCount = 0;
        private int _vertCount = 0;

        public GeoSectionCmdProvider(List<CreationType> vc, Assembly assembly, View view)
        {
            _creationTypes = vc;
            _assembly = assembly;
            _view = view;
            _subassemblies = _assembly.GetSubAssemblies().GetEnumerator().FilterType<Assembly>();
            _openings = (_assembly.GetMainPart() as Part).GetBooleans().FilterType<BooleanPart>();
            _view.SetWorkPlane();
            CreateCommands();
            _view.ReleaseWorkPlane();
        }

        private void CreateCommands()
        {
            foreach (var type in _creationTypes)
            {
                switch (type.Type)
                {
                    case ViewCreationType.Horizontal:
                        AddHorizontalCommand(type.Name, type.Count);
                        break;
                    case ViewCreationType.Vertical:
                        AddVerticalCommand(type.Name, type.Count);
                        break;
                    case ViewCreationType.Both:
                        AddBothCommands(type.Name, type.Count);
                        break;
                }
            }
        }

        private void AddBasedOnLocation(ViewCreationType type, double location)
        {
            if (type is ViewCreationType.Horizontal)
            {
                var box = _assembly.GetBox();
                var startPoint = new Point(box.MinPoint.X, location, 0);
                var endPoint = new Point(box.MaxPoint.X, location, 0);
                var placement = CalculateHorViewPlacement();
                _commands.Add(new GeometrySectionCreationCommand(placement, _view, startPoint, endPoint));
            }
            else
            {
                var box = _assembly.GetBox();
                var endPoint = new Point(location, box.MaxPoint.Y, 0);
                var startPoint = new Point(location, box.MinPoint.Y, 0);
                var placement = CalculateVertViewPlacement();
                _commands.Add(new GeometrySectionCreationCommand(placement, _view, startPoint, endPoint));
            }
        }

        private void AddHorizontalCommand(string name, int index)
        {
            var points = GetPoints(name);
            var locationForHorizontalSection = points.RemoveRedundant(Dirrections.Bottom)[index].Y;
            AddBasedOnLocation(ViewCreationType.Horizontal, locationForHorizontalSection);
        }

        private void AddVerticalCommand(string name, int index)
        {
            var points = GetPoints(name);
            var locationForVerticalSection = points.RemoveRedundant(Dirrections.Left)[index].X;
            AddBasedOnLocation(ViewCreationType.Vertical, locationForVerticalSection);
        }

        private void AddBothCommands(string name, int index)
        {
            var points = GetPoints(name);
            var locationForHorizontalSection = points.RemoveRedundant(Dirrections.Bottom)[index].Y;
            AddBasedOnLocation(ViewCreationType.Horizontal, locationForHorizontalSection);
            var locationForVerticalSection = points.RemoveRedundant(Dirrections.Left)[index].X;
            AddBasedOnLocation(ViewCreationType.Vertical, locationForVerticalSection);
        }

        private PointList GetPoints(string name)
        {
            var points = new PointList();
            if (_subassemblies.Any(x => x.Name.Contains(name)))
            {
                var target = _subassemblies.Where(x => x.Name.Contains(name)).ToList();
                points.AddRange(target.GetSubAssemblyPoints());
            }
            else if (_openings.Any(x => x.OperativePart.Name.Contains(name)))
            {
                var pts = _openings.Select(x => x.OperativePart.GetBox().GetCenterPoint()).ToList().GetPointList();
                points.AddRange(pts);
            }
            return points;
        }

        private Point CalculateVertViewPlacement()
        {
            double startingOffset = 50;
            double step = 40;
            double scaleFactor = 1 / _view.Attributes.Scale;
            var box = _assembly.GetBox().Scale(scaleFactor).Move(new Vector(_view.Origin));
            var y = (box.MaxPoint.Y + box.MinPoint.Y) / 2;
            var scaled = _view.RestrictionBox.Scale(scaleFactor).Move(new Vector(_view.Origin));
            var x = scaled.MaxPoint.X + startingOffset + step * _vertCount;
            _vertCount++;
            return new Point(x,y,0);
        }

        private Point CalculateHorViewPlacement()
        {
            double startingOffset = 50;
            double step = 50;
            double scaleFactor = 1 / _view.Attributes.Scale;
            var box = _assembly.GetBox().Scale(scaleFactor).Move(new Vector(_view.Origin));
            var x = (box.MaxPoint.X + box.MinPoint.X) / 2;
            var scaled = _view.RestrictionBox.Scale(scaleFactor).Move(new Vector(_view.Origin));
            var y = scaled.MinPoint.Y - startingOffset - step * _horCount;
            _horCount++;
            return new Point(x,y,0);
        }
        public List<IDrawingCommand> GetCommands() { return _commands; }
    }
}
