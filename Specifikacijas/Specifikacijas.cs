using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            //Setting up new workbook
            var workbook = ExportToExcel.InitializeWorkbook(args);
            //Setting connection with tekla structures model
            Model model = new Model();
            //Testing if tekla model is open
            if (model.GetConnectionStatus())
            {

                // todo Workbook add Tērauda specifikācijas

                // todo Workbook add Monolītā dzelzsbetona specifikācijas

                // todo Workbook add Mūra speciofikācijas

                //
            }



            //Tērauda specifikācijas
            List<TeraudaElements> teraudaElementi = new List<TeraudaElements>();
            var assemblies = model.GetAssembies(true);

            var groups =assemblies.GroupBy(
                x => x.GetStringReportProperty("ASSEMBLY_POS")
            ).ToList();
            
            groups.EksportetTeraudaSpecifikacijas(model);

            Process.Start(model.GetMyFolderPath());
            
            // var assNum = model.GetAssembieNummerator(true);
            // while (assNum.MoveNext())
            // {
            //     TeraudaElements tel = new TeraudaElements(assNum.Current as Assembly);
            // }
        }

    }
}
