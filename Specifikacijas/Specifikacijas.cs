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

namespace Specifikacijas
{
    class Specifikacijas
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            Console.WriteLine("Hey there");
            var assemblies = model.GetAssembies(true);
            var groups =assemblies.GroupBy(
                x => x.GetStringReportProperty("ASSEMBLY_POS")
            ).ToList();

            IWorkbook wb = new XSSFWorkbook();
            ISheet s1 = wb.CreateSheet("Tērauda specifikācija");
            IRow headerRow = s1.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ELEMENTA MARKA");
            headerRow.CreateCell(1).SetCellValue("ELEMENTA NOSAUKUMS");
            headerRow.CreateCell(2).SetCellValue("SKAITS");
            var row = 1;
            foreach (var group in groups)
            {
                GenerateRow(s1,row, group.First().GetStringReportProperty("ASSEMBLY_POS"), group.First().Name, group.Count());
                row++;
            }

            s1.ForceFormulaRecalculation = true;

            FileStream fs = File.Create("C:\\Users\\NR9-VV\\Documents\\Spec\\test.xls");
            wb.Write(fs);
            fs.Close();
            Console.ReadLine();
        }
        static void GenerateRow(ISheet sheet1, int rowid, string elementaMarka, string elementaNosaukums, double elementaSkaits)
        {
            IRow row = sheet1.CreateRow(rowid);
            row.CreateCell(0).SetCellValue(elementaMarka);  //A2
            row.CreateCell(1).SetCellValue(elementaNosaukums);   //B2
            row.CreateCell(2).SetCellValue(elementaSkaits);   //C2
        }
    }
}
