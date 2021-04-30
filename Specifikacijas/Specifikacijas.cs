using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExtensionMethods;
using Tekla.Structures.Model;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Specifikacijas.Clases;

namespace Specifikacijas
{
    class Specifikacijas
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            List<TeraudaElements> teraudaElementi = new List<TeraudaElements>();
            var assemblies = model.GetAssembies(true);

            // var assNum = model.GetAssembieNummerator(true);
            // while (assNum.MoveNext())
            // {
            //     TeraudaElements tel = new TeraudaElements(assNum.Current as Assembly);
            // }

            var groups =assemblies.GroupBy(
                x => x.GetStringReportProperty("ASSEMBLY_POS")
            ).ToList();


            groups.EksportetTeraudaSpecifikacijas(model);
        }
       
    }
}
