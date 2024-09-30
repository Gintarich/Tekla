using SpecificationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationCreator.Model
{
    public class SpecificationCreator
    {
        IDataAccess _dataAccess;
        IEnumerable<SteelElement> _elements;
        public SpecificationCreator(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _elements = _dataAccess.GetData<SteelElement>().Cast<SteelElement>();
        }

        public void CreateSpecification()
        {

        }
    }
}
