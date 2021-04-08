using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperMethods;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace TeklaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Identifier> idsList = new List<Identifier>();
            Model model = new Model();
            if (model.GetConnectionStatus())
            {
                Console.WriteLine("Viss izdevas");
                Picker picker = new Picker();
                Part selectedObj = (Part)picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART);
                var atverumi = selectedObj.GetBooleans();
                while (atverumi.MoveNext())
                {
                    var id =atverumi.Current.Identifier;
                    idsList.Add(id);

                    Console.WriteLine(id.ToString());
                }

                Console.ReadLine();
            }
        }
    }
}
