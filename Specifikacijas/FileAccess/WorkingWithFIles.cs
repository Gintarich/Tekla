using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace Specifikacijas
{
    public static class WorkingWithFIles
    {
        public static string SetFolderPath(this string fileName, Model _model)
        {
            var modelpath = _model.GetInfo().ModelPath;
            var araPath = Path.GetFullPath(Path.Combine(modelpath, @"..\..\"));
            var specPath = Path.Combine(araPath, @"1_BK sadala\5_Specifikacijas\");
            var myFileName = DateTime.Now.ToString("yyyy.MM.dd")+"_"+fileName+ ".xlsx";
            var myFilePath = Path.Combine(specPath, myFileName);

            /*
            if (!Directory.Exists(specPath))
            {
                Directory.CreateDirectory(specPath);
            }
            */
            return myFilePath;
        }
	}
}
