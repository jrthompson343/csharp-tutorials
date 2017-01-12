using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SpecificationPatternExample
{
    public class DrugConceptNameSearchSpec : Specification<DrugConcept>
    {
        private readonly string _searchString;
        public DrugConceptNameSearchSpec(string searchString)
        {
            _searchString = searchString;
        }

        public override Expression<Func<DrugConcept, bool>> AsExpression()
        {
            return d => d.Name.StartsWith(_searchString);
        }
    }

    public class DrugConceptIdSearchSpec : Specification<DrugConcept>
    {
        private readonly int _id;
        private readonly DrugConceptIdType _idType;
        public DrugConceptIdSearchSpec(int id, DrugConceptIdType idType)
        {
            _id = id;
            _idType = idType;
        }
        public override Expression<Func<DrugConcept, bool>> AsExpression()
        {
            switch (_idType)
            {
                case DrugConceptIdType.FDBId:
                    return d => d.Id == _id;
                case DrugConceptIdType.FDBGenericId:
                    return d => d.GenericId == _id;
                case DrugConceptIdType.FDBNameId:
                    return d => d.NameId == _id;
                default:
                    return d => d.Id == _id;
            }
        }
    }
}
