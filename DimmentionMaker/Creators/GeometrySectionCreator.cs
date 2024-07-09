using DimmentionMaker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimmentionMaker.Creators
{
    public class GeometrySectionCreator 
    {
        private readonly List<IViewManager> _viewManagers;



        public List<IViewManager> GetManagers() { return _viewManagers; }
    }
}
