using System.IO;
using System.Threading;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model;
using ExtensionMethods;
using System.Linq;


namespace TestingConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            TestDimmension();
        }
        public static void DisplayReport(string path)
        {

            var mypath = path;
            Operation.CreateReportFromAll("ARA_ASSEMBLY_KOPEJA_SPEC", mypath, "MyTitle", "", "");

            if (File.Exists(mypath))
            {
                // wait until Tekla Structures has unlocked the file, or timeout
                if (IfLockedWait(mypath))
                {
                    // display the report
                    Operation.DisplayReport(mypath);
                }
            }

        }
        public static bool IfLockedWait(string FileName)
        {
            // try 10 times
            int RetryNumber = 10;
            while (true)
            {
                try
                {
                    using (FileStream FileStream = new FileStream(
                        FileName, FileMode.Open,
                        FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        byte[] ReadText = new byte[FileStream.Length];
                        FileStream.Seek(0, SeekOrigin.Begin);
                        FileStream.Read(ReadText, 0, (int)FileStream.Length);
                    }
                    return true;
                }
                catch (IOException)
                {
                    // wait one second
                    Thread.Sleep(1000);
                    RetryNumber--;
                    if (RetryNumber == 0)
                        return false;
                }
            }
        }
        public static void TestDimmension()
        {
            var dh = new DrawingHandler();
            var view = dh.GetActiveDrawing().GetSheet().GetAllViews().FilterType<View>().Where(x => x.Name == "C").FirstOrDefault();
            PointList points = new PointList()
            {
                new Point(0,-100,0),
                new Point(6330,-100,0),
            };
            var strDimSetHandler = new StraightDimensionSetHandler();
            //Insert dim lines
            strDimSetHandler.CreateDimensionSet(view, points, new Vector(0, -1, 0), 200);
        }

    }
}
