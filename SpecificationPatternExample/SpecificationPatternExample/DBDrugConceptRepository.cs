using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace SpecificationPatternExample
{
    public class DBDrugConceptRepository : IDrugConcepts
    {
        private static DBDrugConceptRepository _instance;

        private DBDrugConceptRepository()
        {
        }

        public static DBDrugConceptRepository Instance()
        {
            return _instance ?? (_instance = new DBDrugConceptRepository());
        }

        public List<DrugConcept> Find(Specification<DrugConcept> spec)
        {
            List<DrugConcept> drugConcepts = null;
            LambdaExpression expression = spec.AsExpression();
            using (var session = HibernateHelper.CreateSessionFactory().OpenSession())
            {
                //drugConcepts = session.Query<DrugConcept>().Where(d => d.NameId == 18082 || d.Name.StartsWith("amox")).ToList();

                drugConcepts = session.Query<DrugConcept>().Where(spec.AsExpression()).ToList();
            }
            return drugConcepts;
        }
    }
}
