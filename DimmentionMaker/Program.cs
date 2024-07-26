using DimmentionMaker.Commands;
using DimmentionMaker.Creators;
using DimmentionMaker.Models;
using Tekla.Structures.Geometry3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using ExtensionMethods;
using Tekla.Structures.Model;
using DimmentionMaker.Managers;

namespace DimmentionMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileGenerator.GenerateFilters();
            FileGenerator.GenerateViewAttributes();
            var tieBeamDrawing = new TieBeamDrawingManager();
            tieBeamDrawing.Execute();
            //Console.ReadLine();
        }
    }
}
