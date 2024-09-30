using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationModel
{
    public interface IDataAccess
    {
         List<BaseElement> GetData<T>();
    }
}
