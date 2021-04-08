using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace HelperMethods
{
    public class SelectionMethods
    {
        private static readonly Model model = new Model();
		public static void SelectModelObjectsInUi(List<int> ids)
        {
            var modelObjects = new ArrayList();

            ids.ForEach(id =>
            {
                var modelObject = model.SelectModelObject(new Identifier(id));
                if (modelObject == null) return;
                modelObjects.Add(modelObject);
            });

            var selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            selector.Select(modelObjects);
        }
	}
}
