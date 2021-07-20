using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using ExtensionMethods;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace Foundation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Containers
            Model model = new Model();
            Picker picker = new Picker();
            //UserInputs
            var column = (Beam)picker.PickObject(Picker.PickObjectEnum.PICK_ONE_PART,"Izvēlies kolonnu");
            string pamataTips = string.Empty;
            int foundationHeight = 0;
            int foundationWidth = 0;

            //konsoles ievade

            //Logic
            var pt1 = column.StartPoint;
        }
    }
}
