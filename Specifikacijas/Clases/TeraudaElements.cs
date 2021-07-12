using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using NPOI.SS.Formula.Functions;
using Tekla.Structures.Model;

namespace Specifikacijas.Clases
{
    class TeraudaElements
    {
        public TeraudaElements()
        {
            
        }
        public TeraudaElements(Assembly assembly)
        {
            ElementaMarka = assembly.GetStringReportProperty("ASSEMBLY_POS");
            ElementaNosaukums = assembly.Name;
            ElementuSkaits = assembly.GetIntegerReportProperty("NUMBER");
            ElementuProfils = assembly.GetStringReportProperty("MAINPART.PROFILE");
            ElementaGarums = assembly.GetDoubleReportProperty("MAINPART.LENGTH");
            ElementaSvars = assembly.GetDoubleReportProperty("WEIGHT");
            ElementaLaukums = assembly.GetDoubleReportProperty("AREA");
        }
        public string ElementaMarka { get; set; }
        public string ElementaNosaukums { get; set; }
        public int ElementuSkaits { get; set; }
        public string ElementuProfils { get; set; }
        public double ElementaGarums { get; set; }
        public double ElementaSvars { get; set; }
        public double ElementaLaukums { get; set; }

        public double ElementaSvarsKopa => ElementaSvars * ElementuSkaits;
        public double ElementaLaukumsKopa => ElementaLaukums * ElementuSkaits;



    }
}
