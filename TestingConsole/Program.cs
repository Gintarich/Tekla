using System.IO;
using System.Threading;
using Tekla.Structures.Model.Operations;

namespace TestingConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mypath =
                @"\\192.168.10.9\Darbs\DARBS\2022\PROJEKTI\ARA_2022_L07_BK_EVOPIPES_A_V_DS\3_KONSTRUKCIJAS\3_Teklas modeli\2022.07.18_ARA_2022_L07_BK_EVOPIPES\PlotFiles\TEST.xsr";
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

    }
}
