using DimmentionMaker.Commands;
using DimmentionMaker.Creators;
using DimmentionMaker.Interfaces;
using DimmentionMaker.Models;
using DimmentionMaker.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Managers
{
    internal class RebarSectionCreator : IViewCreator
    {
        private readonly ICommandQueue _commandQueue = new CommandQueue();
        private readonly Assembly _assembly;
        private readonly View _view;
        private readonly Drawing _drawing;
        private readonly List<CreationType> _creationTypes;

        public RebarSectionCreator(Assembly assembly, View view, Drawing drawing)
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
                new CreationType("WELDA", ViewCreationType.Horizontal,0),
                new CreationType("NEO", ViewCreationType.Vertical,0),
            };
        }

        private void SetupCommands()
        {
            _commandQueue.AddRange(new RebarSectionCmdProvider(_creationTypes, _assembly, _view).GetCommands());
        }

        public void RunCommands()
        {
            _commandQueue.ExecuteCommands();
        }

    }
}
