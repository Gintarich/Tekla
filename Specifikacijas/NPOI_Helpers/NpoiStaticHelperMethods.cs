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

        public static IWorkbook InitializeWorkbook(string[] args)
        {
            IWorkbook workbook;
            if (args.Length > 0 && args[0].Equals("-xls"))
                workbook = new HSSFWorkbook();
            else
                workbook = new XSSFWorkbook();
            return workbook;
        }

    }
}
