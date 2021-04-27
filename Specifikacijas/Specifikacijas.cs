using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExtensionMethods;
using Tekla.Structures.Model;

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
            //allAssemblies.Where(x=>x.GetMainPart())
            foreach (var group in groups)
            {
                Console.WriteLine($" {group.First().GetStringReportProperty("ASSEMBLY_POS")}, {group.Count()},{group.First().Name}");
               // Console.WriteLine($"Assembly nosaukums :{assembly.Name} un Marka : {assembly.AssemblyNumber.Prefix} {assembly.GetStringReportProperty("ASSEMBLY_POS")}");
            }


            Console.ReadLine();
        }
    }
}
