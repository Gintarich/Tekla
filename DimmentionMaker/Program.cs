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

namespace DimmentionMaker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileGenerator.GenerateFilters();
            List<ICommandCreator> cmdCreators = new List<ICommandCreator>
            {
                new FrontViewCommandCreator(),
            };
            DimmensionLineCreator dimCreator = new DimmensionLineCreator(cmdCreators);
            dimCreator.Run();
            Console.ReadLine();
        }
    }
}
