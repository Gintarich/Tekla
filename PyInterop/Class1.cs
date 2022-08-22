using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model.UI;

namespace PyInterop
{
    public static class Greetings
    {
        public static string Greet()
        {
            Picker picker = new Picker();
            picker.PickObjects(Picker.PickObjectsEnum.PICK_N_PARTS);
            return "Hello World!!!";
        }
    }
}
