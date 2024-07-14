﻿using DimmentionMaker.Creators;
using DimmentionMaker.Interfaces;
using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;

namespace DimmentionMaker.Managers
{
    public class TieBeamDrawingManager : IDrawingManager
    {
        private readonly List<IAnotationManager> _anotationManagers = new List<IAnotationManager>();
        private readonly List<IViewCreator> _viewCreators = new List<IViewCreator>();
        private CastUnitDrawing _drawing;
        private View _view;
        private Assembly _assembly;

        public TieBeamDrawingManager()
        {
            GetData();
        }

        public void Execute()
        {
            SetupCreators();
            foreach(var creator in _viewCreators)
            {
                creator.RunCommands(); 
            }
            SetupManagers();
            foreach (var viewManager in _anotationManagers)
            {
                viewManager.RunCommands();
            }
        }

        private void SetupCreators()
        {
            _viewCreators.Add(new GeoSectionCreator(_assembly,_view,_drawing));
        }

        private void SetupManagers()
        {
            _anotationManagers.Add(new FrontViewAnotationManager());
            _anotationManagers.Add(new GeometryDimensionManager(_drawing, _assembly));
        }

        private void GetData()
        {
            var dh = new DrawingHandler();
            _drawing = dh.GetActiveDrawing() as CastUnitDrawing;
            if (_drawing is null) Console.WriteLine("The script is supported for cast unit drawings");
            _view = _drawing.GetSheet().GetAllViews().ToAList<View>().Where(x => x.Name == "VIRSSKATS").ToList().First();
            var cuId = _drawing.CastUnitIdentifier;
            _assembly = (new Model().SelectModelObject(cuId) as Assembly);
        }

    }
}
