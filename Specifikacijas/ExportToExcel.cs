using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Tekla.Structures.Model;

namespace Specifikacijas
{
    public static class ExportToExcel
    {
        public static void EksportetTeraudaSpecifikacijas(this List<IGrouping<string, Assembly>> groupings,Model model)
        {

            IWorkbook wb = new XSSFWorkbook();
            ISheet s1 = wb.CreateSheet("Tērauda specifikācija");
            IRow dateRow = s1.CreateRow(0);
            IRow objectRow = s1.CreateRow(1);
            IRow titleRow = s1.CreateRow(2);
            IRow headerRow = s1.CreateRow(3);
            var objectName = model.GetProjectInfo().Object;


            #region styles

            var titleStyle = wb.CreateCellStyle();
            titleStyle.BorderBottom = BorderStyle.Medium;
            titleStyle.BorderTop = BorderStyle.Medium;
            titleStyle.BorderLeft = BorderStyle.Medium;
            titleStyle.BorderRight = BorderStyle.Medium;
            titleStyle.WrapText = true;
            var titleFont = wb.CreateFont();
            titleFont.IsBold = true;
            titleFont.FontName = "Times New Roman";
            titleFont.FontHeightInPoints = 11;
            titleStyle.SetFont(titleFont);

            var titleStyle2 = wb.CreateCellStyle();
            titleStyle2.WrapText = true;
            var titleFont2 = wb.CreateFont();
            titleFont2.IsBold = true;
            titleFont2.FontName = "Times New Roman";
            titleFont2.FontHeightInPoints = 11;
            titleStyle2.SetFont(titleFont2);
            titleStyle2.Alignment = HorizontalAlignment.Center;

            var regularstyle = wb.CreateCellStyle();
            regularstyle.BorderBottom = BorderStyle.Thin;
            regularstyle.BorderTop = BorderStyle.Thin;
            regularstyle.BorderLeft = BorderStyle.Thin;
            regularstyle.BorderRight = BorderStyle.Thin;
            regularstyle.WrapText = true;
            var regularFont = wb.CreateFont();
            regularFont.IsBold = false;
            regularFont.FontName = "Arial";
            regularFont.FontHeightInPoints = 11;
            regularstyle.SetFont(regularFont);

            var boldStyle = wb.CreateCellStyle();
            var boldFont = wb.CreateFont();
            boldStyle.Alignment = HorizontalAlignment.Right;
            boldFont.IsBold = true;
            boldFont.FontName = "Times New Roman";
            boldFont.FontHeightInPoints = 14;
            boldStyle.SetFont(boldFont);

            #endregion

            #region ObjectDataRows
            dateRow.CreateCell(8).SetCellValue("Datums:");
            dateRow.GetCell(8).CellStyle = titleStyle;
            dateRow.CreateCell(9).SetCellValue(DateTime.Now.ToString("dd.MM.yyyy"));
            dateRow.GetCell(9).CellStyle = titleStyle;

            s1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 9));
            var mycell = objectRow.CreateCell(0);
            mycell.SetCellValue("OBJEKTS:"+objectName);
            mycell.CellStyle = boldStyle;

            s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 9));
            var titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue("TĒRAUDA ELEMENTU SPECIFIKĀCIJA");
            titleCell.CellStyle = titleStyle2;
            
            #endregion

            #region HeaderRow

            headerRow.HeightInPoints = 31;
            s1.SetColumnWidth(0,3400); //A
            s1.SetColumnWidth(1, 5900); //B
            s1.SetColumnWidth(2, 2200); //C
            s1.SetColumnWidth(3, 3264); //D
            s1.SetColumnWidth(4, 3200); //E
            s1.SetColumnWidth(5, 3000); //F
            s1.SetColumnWidth(6, 3400); //G
            s1.SetColumnWidth(7, 3000); //H
            s1.SetColumnWidth(8, 3200); //I
            s1.SetColumnWidth(9, 3200); //I

            headerRow.CreateCell(0).SetCellValue("ELEMENTA MARKA");
            headerRow.CreateCell(1).SetCellValue("ELEMENTA NOSAUKUMS");
            headerRow.CreateCell(2).SetCellValue("SKAITS");
            headerRow.CreateCell(3).SetCellValue("PROFILS");
            headerRow.CreateCell(4).SetCellValue("GARUMS 1 ELEM.(m)");
            headerRow.CreateCell(5).SetCellValue("SVARS 1 ELEM.(kg)");
            headerRow.CreateCell(6).SetCellValue("LAUKUMS 1 ELEM.(m)");
            headerRow.CreateCell(7).SetCellValue("SVARS KOPĀ (kg)");
            headerRow.CreateCell(8).SetCellValue("LAUKUMS KOPĀ (m2)");
            headerRow.CreateCell(9).SetCellValue("PIEZĪMES");

            for (int i = 0; i < 10; i++)
            {
                headerRow.GetCell(i).CellStyle = titleStyle;
            }

            #endregion

            #region DataRows

            var myrow = 4;
            foreach (var group in groupings)
            {
                ArrayList myList = new ArrayList();
                string elementaMarka = group.First().GetStringReportProperty("ASSEMBLY_POS");
                myList.Add(elementaMarka);
                string elementaNosaukums = group.First().Name;
                myList.Add(elementaNosaukums);
                var elementuskaits = group.Count();
                myList.Add(elementuskaits);
                var profils = group.First().GetStringReportProperty("MAINPART.PROFILE");
                myList.Add(profils);
                var gar1el = group.First().GetDoubleReportProperty("LENGTH");
                myList.Add(Math.Round(gar1el, 2));
                var sv1el = group.First().GetDoubleReportProperty("WEIGHT");
                myList.Add(Math.Round(sv1el, 2));
                var lauk1el = group.First().GetDoubleReportProperty("AREA")/1000000;
                myList.Add(Math.Round(lauk1el, 2));
                var svkopa = group.Sum(x => x.GetDoubleReportProperty("WEIGHT"));
                myList.Add(Math.Round(svkopa, 2));
                var laukumskopa = group.Sum(x => x.GetDoubleReportProperty("AREA")/1000000);
                myList.Add(Math.Round(laukumskopa, 2));

                //Generate row
                IRow row = s1.CreateRow(myrow);
                row.CreateCell(0).SetCellValue(myList[0] as string);  //A2
                row.CreateCell(1).SetCellValue(myList[1] as string);   //B2
                row.CreateCell(2).SetCellValue((int)myList[2]);   //C2
                row.CreateCell(3).SetCellValue(myList[3] as string);
                row.CreateCell(4).SetCellValue((double)myList[4]);
                row.CreateCell(5).SetCellValue((double)myList[5]);
                row.CreateCell(6).SetCellValue((double)myList[6]);
                row.CreateCell(7).SetCellValue((double)myList[7]);
                row.CreateCell(8).SetCellValue((double)myList[8]);
                row.CreateCell(9).SetCellValue("");

                for (int i = 0; i < 10; i++)
                {
                    row.GetCell(i).CellStyle = regularstyle;
                }
                myrow++;
            }

            #endregion

            string mystring = @"Test123".SetFolderPath(model);

            s1.ForceFormulaRecalculation = true;

            FileStream fs = File.Create(mystring);
            wb.Write(fs);
            fs.Close();
        }
    }
}
