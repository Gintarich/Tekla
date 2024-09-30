using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SpecificationModel;

namespace Tekla2024DataAccess
{
    public class TeklaDataAccess : IDataAccess
    {
        public List<BaseElement> GetData<T>()
        {
            if(typeof(T) == typeof(SteelElement))
            {
                return GetSteelElements();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private List<BaseElement> GetSteelElements()
        {
            var lst = new List<BaseElement>();
            var el = new SteelElement("P1", "Sija", 1, "IPE", 200, 5, 12, 12, 12);
            lst.Add(el);
            return lst;
        }
    }
}
