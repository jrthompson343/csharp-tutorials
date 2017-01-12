using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace SpecificationPatternExample
{
    public class DrugConceptMap : ClassMap<DrugConcept>
    {
        public DrugConceptMap()
        {
            Table("wfdc_FDB_DrugConceptView");
            ReadOnly();
            Id(m => m.Id);
            Map(m => m.DoseForm).Column("MED_DOSAGE_FORM_DESC");
            Map(m => m.DoseFormName).Column("DOSE_FORM_NAME");
            Map(m => m.FormulationId).Column("gcn_seqno");
            Map(m => m.FullName).Column("FULL_NAME");
            Map(m => m.GenericId).Column("GENERIC_ID");
            Map(m => m.LegendCode).Column("LEGEND_CODE");
            Map(m => m.Name).Column("NAME");
            Map(m => m.Route).Column("MEDROUTE");
            Map(m => m.Strength).Column("STRENGTH");
            Map(m => m.NameId).Column("NAME_ID");
        }
    }
}
