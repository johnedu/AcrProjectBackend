using Bow.Afiliaciones.Entidades;
using Bow.Cartera.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class PlanExequialRecaudoMasivoMap: MultiTenantMap<PlanExequialRecaudoMasivo>
    {
        public PlanExequialRecaudoMasivoMap()
        {
            //Atributos

            Property(plan => plan.EsObligatorio).IsRequired();

            //Llaves Foraneas

            //Tabla
            ToTable("plan_exequial_recaudo_masivo");
        }
    }
}