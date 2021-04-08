using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;


namespace HelperMethods
{
    public static class ExtensionMethods
    {
        public static List<ModelObject> ToList(this ModelObjectEnumerator enumerator)
        {
            var modelObjects = new List<ModelObject>();
            while (enumerator.MoveNext())
            {
                var modelObject = enumerator.Current;
                modelObjects.Add(modelObject);
            }
            return modelObjects;
        }
	}

    
}
