using DimmentionMaker.Commands;
using DimmentionMaker.Creators;
using DimmentionMaker.Interfaces;
using DimmentionMaker.Models;
using DimmentionMaker.Providers;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace DimmentionMaker.Managers
{
    public class GeometryDimensionManager : IAnotationManager
    {
        private List<ICommandQueue> _commandQueues = new List<ICommandQueue>();
        private readonly Drawing _drawing;
        private readonly Assembly _assembly;

        public GeometryDimensionManager(Drawing drawing, Assembly assembly)
        {
            _drawing = drawing;
            _assembly = assembly;
            SetupCommands();
        }

        private void SetupCommands()
        {
            var drawing = new DrawingHandler().GetActiveDrawing();
            var sectionViews = drawing.GetSheet()
                .GetViews()
                .ToAList<View>()
                .Where(v => GeoSectionTypeManager.GetSectionType(v) == SectionType.Geometry);
            foreach (var sectionView in sectionViews)
            {
                var comQ = new CommandQueue();
                sectionView.SetWorkPlane();
                comQ.Add(new ClearDimmensionsAndTextCommand(sectionView));
                comQ.Add(new AddOverallDimCommand(sectionView, _assembly, Dirrections.Bottom));
                comQ.Add(new AddOverallDimCommand(sectionView, _assembly, Dirrections.Left));
                comQ.AddRange(new OpeningCommandCreator(
                    new List<string> { "LIFTING CUT", "BEAM" },
                    new List<Vector> { Dirrections.Top, Dirrections.Left },
                    _assembly,
                    sectionView).GetCommands());
                comQ.AddRange(new ChamferCmdProvider(_assembly, sectionView).GetCommands());
                comQ.Add(new CommitDrawingChangesCommand(_drawing));
                sectionView.ReleaseWorkPlane();
                _commandQueues.Add(comQ);
            }
        }

        public void RunCommands()
        {
            foreach (var commandQueue in _commandQueues)
            {
                commandQueue.ExecuteCommands();
            }
        }
    }
}
