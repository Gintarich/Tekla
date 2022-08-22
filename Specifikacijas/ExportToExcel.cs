using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using ExtensionMethods;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Tekla.Structures.Model;
using static Specifikacijas.NPOI_Helpers.NpoiStaticHelperMethods;

namespace Specifikacijas
{
    public static class ExportToExcel
    {
        
        public static void EksportetTeraudaSpecifikacijas(this List<IGrouping<string, 
            Assembly>> groupings, Model model)
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

            var regularstyle2 = wb.CreateCellStyle();
            var regularFont2 = wb.CreateFont();
            regularFont2.IsBold = false;
            regularFont2.FontName = "Arial";
            regularFont2.FontHeightInPoints = 11;
            regularstyle2.SetFont(regularFont2);
            regularstyle2.Alignment = HorizontalAlignment.Center;

            var regularstyle3 = wb.CreateCellStyle();
            var regularFont3 = wb.CreateFont();
            regularFont3.IsBold = false;
            regularFont3.FontName = "Arial";
            regularFont3.FontHeightInPoints = 11;
            regularstyle3.SetFont(regularFont2);

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
            mycell.SetCellValue("OBJEKTS:" + objectName);
            mycell.CellStyle = boldStyle;

            s1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 9));
            var titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue("TĒRAUDA ELEMENTU SPECIFIKĀCIJA");
            titleCell.CellStyle = titleStyle2;

            #endregion

            #region HeaderRow----------------------------------------------------------------------

            headerRow.HeightInPoints = 31;
            s1.SetColumnWidth(0, 3400); //A
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

            #region DataRows-----------------------------------------------------------------------

            int startingRow = 4;
            var myrow = startingRow;
            foreach (var group in groupings)
            {
                //Generate row
                IRow row = s1.CreateRow(myrow);
                row.CreateCell(0).SetCellValue(group.First().GetStringReportProperty("ASSEMBLY_POS")); //A2
                row.CreateCell(1).SetCellValue(group.First().Name); //B2
                row.CreateCell(2).SetCellValue(group.Count()); //C2
                row.CreateCell(3).SetCellValue(group.First().GetStringReportProperty("MAINPART.PROFILE"));
                row.CreateCell(4).SetCellValue(Math.Round(group.First().GetDoubleReportProperty("LENGTH"), 2));
                row.CreateCell(5).SetCellValue(Math.Round(group.First().GetDoubleReportProperty("WEIGHT"), 2));
                row.CreateCell(6).SetCellValue(Math.Round(group.First().GetDoubleReportProperty("AREA") / 1000000, 2));
                row.CreateCell(7).SetCellValue(Math.Round(group.Sum(x => x.GetDoubleReportProperty("WEIGHT")), 2));
                row.CreateCell(8)
                    .SetCellValue(Math.Round(group.Sum(x => x.GetDoubleReportProperty("AREA") / 1000000), 2));
                row.CreateCell(9).SetCellValue("");

                for (int i = 0; i < 10; i++)
                {
                    row.GetCell(i).CellStyle = regularstyle;
                }

                myrow++;
            }

            #endregion

            #region BottomPart---------------------------------------------------------------------

            var totalRow = s1.CreateRow(myrow);

            string fmla = "SUM(H" + (startingRow + 1) + ":H" + myrow + ")";
            totalRow.CreateCell(7).SetCellFormula(fmla);
            totalRow.CreateCell(8).SetCellFormula("SUM(I" + (startingRow + 1) + ":I" + myrow + ")");
            totalRow.GetCell(7).CellStyle = regularstyle2;
            totalRow.GetCell(8).CellStyle = regularstyle2;
            myrow++;
            var papilduRinda = s1.CreateRow(myrow);
            string str = "Apjomos papildus ievērtēt 5% fasonlapu un papildus detaļu izgatavošanai (kg)";
            papilduRinda.CreateCell(0).SetCellValue(str);
            papilduRinda.GetCell(0).CellStyle = regularstyle3;
            papilduRinda.CreateCell(7).SetCellFormula("H" + (myrow) + "*0.05");
            papilduRinda.GetCell(7).CellStyle = regularstyle2;
            myrow++;
            var kopejaRinda = s1.CreateRow(myrow);
            kopejaRinda.CreateCell(6).SetCellValue("Kopā(kg):");
            kopejaRinda.GetCell(6).CellStyle = titleStyle2;
            var fmla2 = "SUM(H" + (myrow - 1) + ":H" + myrow + ")";
            kopejaRinda.CreateCell(7).SetCellFormula(fmla2);
            kopejaRinda.GetCell(7).CellStyle = titleStyle2;
            myrow++;

            #endregion

            #region Footer

            string[] myStrArray =
            {
                "MATERIĀLU APJOMOS NAV IEVĒRTĒTS:",
                "1) Materiālu zudumi, kas rodas būvniecības tehnoloģisko procesu rezultātā;",
                "2) Iebetonējamo detaļu apjomi;",
                "3) Skrūvju apjomi;",
                "4) Metinājuma šuvju apjomi;",
                "5) Kāpņu margu, pakāpienu un kāpņu laukumu režģu apjomi;",
                "6) Papildus metāla elementu apjomi, kas nepieciešami inženieriekārtām un ailu aizpildījuma elementiem, ko nosaka katra ražotāja specifikācija;"
            };
            var test123 =myStrArray[0];
            #endregion


            string mystring = @"Test123".SetFolderPath(model);

            s1.ForceFormulaRecalculation = true;

            FileStream fs = File.Create(mystring);
            wb.Write(fs);
            fs.Close();
        }
        
        public static void EksportetTeraudaSpecifikacijas(IWorkbook wb, Model model)
        {
            
        }
        public static void EksportetMonolitaDzelzsbetonaSpecifikacijas(IWorkbook wb, Model model)
        {

        }
        public static void EksportetMuraSpecifikacijas(IWorkbook wb, Model model)
        {

        }
    }
}
