using DimmentionMaker.Commands;
using DimmentionMaker.Creators;
using DimmentionMaker.Interfaces;
using DimmentionMaker.Models;
using Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Managers
{
    public class GeoSectionCreator : IViewCreator
    {

        private readonly ICommandQueue _commandQueue = new CommandQueue();
        private readonly Assembly _assembly;
        private readonly View _view;
        private readonly Drawing _drawing;
        private readonly List<CreationType> _creationTypes;

        public GeoSectionCreator(Assembly assembly, View view, Drawing drawing)
        {
            _assembly = assembly;
            _view = view;
            _drawing = drawing;
            _creationTypes = ConfigureSections();
            SetupCommands();
        }

        private List<CreationType> ConfigureSections()
        {
            return new List<CreationType>
            {
                new CreationType("WELDA", ViewCreationType.Both),
                new CreationType("LIFTING CUT", ViewCreationType.Vertical),
            };
        }

        private void SetupCommands()
        {
            _commandQueue.Add(new ClearGeometrySectionsCommand(_drawing as CastUnitDrawing));
            _commandQueue.AddRange(new GeoSectionCmdProvider(_creationTypes, _assembly, _view).GetCommands());
        }

        public void RunCommands()
        {
            _commandQueue.ExecuteCommands();
        }
    }
}
