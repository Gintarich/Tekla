using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Drawing;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;
using Tekla.Structures.Model;
using Tekla.Structures;

namespace DimmentionMaker.Models
{
    public static class FileGenerator
    {
        public static void GenerateFilters()
        {
            GenerateExcludeFilters();
        }

        private static void GenerateExcludeFilters()
        {
            ObjectFilterExpressions.Type objtype = new ObjectFilterExpressions.Type();
            var part = new NumericConstantFilterExpression(TeklaStructuresDatabaseTypeEnum.PART);
            var bExpr = new BinaryFilterExpression(objtype, NumericOperatorType.IS_NOT_EQUAL, part);
            BinaryFilterExpressionCollection filter = new BinaryFilterExpressionCollection
            {
                new BinaryFilterExpressionItem(bExpr,BinaryFilterOperatorType.EMPTY),
            };
            var first = filter.GetFirst();
            Filter f = new Filter(filter);
            string name = "0000_EXCLUDE_FILTER";
            SaveViewFilter(f,name);
        }
        private static void SaveViewFilter(Filter f, string name)
        {
            string filterName = name;
            var modelPath = new Model().GetInfo().ModelPath;
            var attributes = Path.Combine(modelPath, "attributes");
            var filePath = Path.Combine(attributes, filterName);
            f.CreateFile(FilterExpressionFileType.DRAWING_CAST_UNIT, filePath);
            filePath = filePath + ".cuf";
            var newFilePath = Path.ChangeExtension(filePath, ".vf");
            if (File.Exists(filePath) && !File.Exists(newFilePath)) { File.Move(filePath, newFilePath); }
        }
    }
}
