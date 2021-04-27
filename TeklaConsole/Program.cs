using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;
using ModelObjectSelector = Tekla.Structures.Model.UI.ModelObjectSelector;

namespace TeklaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Identifier> idsList = new List<Identifier>();
            Model model = new Model();
            var atverumiArray = new ArrayList();

            if (model.GetConnectionStatus())
            {
                Console.WriteLine("Viss izdevas");
                Picker picker = new Picker();
                Part selectedObj = (Part)picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART);
                var atverumi = selectedObj.GetBooleans();
                
                while (atverumi.MoveNext())
                {
                    atverumiArray.Add(atverumi.Current);
                }

                var selector = new ModelObjectSelector();
                selector.Select(atverumiArray);

                Console.ReadLine();
            }
        }
    }
}
