using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationPatternExample
{
    public interface IDrugConcepts
    {
        List<DrugConcept> Find(Specification <DrugConcept> spec);
    }
}
