using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationPatternExample
{
    public class DrugConceptRepositoryFactory
    {
        public static IDrugConcepts GetInstance()
        {
            return DBDrugConceptRepository.Instance();
        }
    }
}
