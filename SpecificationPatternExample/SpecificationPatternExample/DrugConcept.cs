using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationPatternExample
{
    public enum DrugConceptIdType {FDBId, FDBNameId, FDBGenericId};
    public class DrugConcept
    {
        public virtual int Id { get; set; }
        public virtual int NameId { get; set; }
        public virtual int FormulationId { get; set; }
        public virtual int GenericId { get; set; }
        public virtual string Name { get; set; }
        public virtual string DoseFormName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string DoseForm { get; set; }
        public virtual string Route { get; set; }
        public virtual string Strength { get; set; }
        public virtual string LegendCode { get; set; }

        public override string ToString()
        {
            return $"[Id: {Id} Name: {Name}]";
        }
    }
}
