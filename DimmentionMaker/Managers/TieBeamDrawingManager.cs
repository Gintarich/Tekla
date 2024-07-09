using DimmentionMaker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimmentionMaker.Managers
{
    public class TieBeamDrawingManager : IDrawingManager
    {
        private readonly List<IViewManager> _viewManagers = new List<IViewManager>();

        public TieBeamDrawingManager()
        {
            Setup();
        }

        private void Setup()
        {
            _viewManagers.Add(new FrontViewAnotationManager());
        }
        
        public void Execute()
        {
            foreach (var viewManager in _viewManagers)
            {
                viewManager.RunCommands();
            }
        }
    }
}
