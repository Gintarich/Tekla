using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;

namespace DimMakerLibrary.Models
{
    public static class Dirrections
    {
        public static Vector Bottom = new Vector(0,-1,0);
        public static Vector Top = new Vector(0, 1, 0);
        public static Vector Left = new Vector(-1, 0, 0);
        public static Vector Right = new Vector(1, 0, 0);
    }
}
