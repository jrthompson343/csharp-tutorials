using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using NHibernate.Util;

namespace SpecificationPatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IDrugConcepts drugConcepts =  DrugConceptRepositoryFactory.GetInstance();

            //Each caller of the existing repository will now need to create specs to query the repo, or pull for a pool of commom specs.
            //What happens if the chained specification cannot be executed by both the webservice / database? [real concerns?]
            Specification<DrugConcept> customSearch =
                new DrugConceptNameSearchSpec("amox").Or(new DrugConceptIdSearchSpec(18082, DrugConceptIdType.FDBNameId));

            IEnumerable<DrugConcept> drugs = drugConcepts.Find(customSearch);

            drugs.ForEach(d => Console.WriteLine(d.ToString()));
            DrugConcept drug = drugs.First();

            Specification<DrugConcept> idSearch = new DrugConceptIdSearchSpec(18082, DrugConceptIdType.FDBNameId);
            
            //Doesn't work right now, combining two lamda's loses context on the parameter.  Need to use ExpressionVisitor.  (only available in .net 4.0)
            //Console.WriteLine(customSearch.IsSatisfiedBy(drug));


            Console.ReadLine();
        }




    }
}
