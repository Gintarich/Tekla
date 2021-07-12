using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Tekla.Structures.Model;

namespace Specifikacijas.NPOI_Helpers
{
    public static class NpoiStaticHelperMethods
    {
        /// <summary>
        /// Initialize a new workbook depending on excel file type
        /// </summary>
        /// <param name="args"></param>
        /// <returns>workbook to work with</returns>
        public static IWorkbook InitializeWorkbook(string[] args)
        {
            IWorkbook workbook;
            if (args.Length > 0 && args[0].Equals("-xls"))
                workbook = new HSSFWorkbook();
            else
                workbook = new XSSFWorkbook();
            return workbook;
        }

        public static Dictionary<string, ICellStyle> CreateStyles(IWorkbook wb)
        {
            var styles = new Dictionary<string, ICellStyle>();
            ICellStyle style;
            IFont titleFont = wb.CreateFont();
            titleFont.FontHeightInPoints = 11;
            titleFont.IsBold = true;
            titleFont.FontName = "Times New Roman";
            style = wb.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.SetFont(titleFont);
            style.WrapText = true;
            styles.Add("Title", style);

            style.BorderBottom = BorderStyle.Medium;
            style.BorderTop = BorderStyle.Medium;
            style.BorderLeft = BorderStyle.Medium;
            style.BorderRight = BorderStyle.Medium;
            styles.Add("Date_Header", style);

            titleFont.FontHeightInPoints = 14;
            style.BorderBottom = BorderStyle.None;
            style.BorderTop = BorderStyle.None;
            style.BorderLeft = BorderStyle.None;
            style.BorderRight = BorderStyle.None;
            style.SetFont(titleFont);
            styles.Add("Object",style);

            IFont regFont = wb.CreateFont();


            return styles;
        }

    }
    /*
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

     
     */
}
