using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace DimmentionMaker.Creators
{
    public class OpeningCommandCreator
    {
        private readonly string _openingName;
        private readonly List<Vector> _directions;
        private readonly Assembly _assembly;

        public OpeningCommandCreator(string openingName, List<Vector> directions,Assembly assembly)
        {
            _openingName = openingName;
            _directions = directions;
            _assembly = assembly;
        }
    }
}
