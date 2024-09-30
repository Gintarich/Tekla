using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla2024DataAccess;

namespace SpecificationCreator.Model
{
    public class Application
    {
        public void Run()
        {
            var dataAccess = new TeklaDataAccess();
            var creator = new SpecificationCreator(dataAccess);
        }
    }
}
