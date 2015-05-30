using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class BeneficiosGestionProspectoMap : MultiTenantMap<BeneficiosGestionProspecto>
    {
       public BeneficiosGestionProspectoMap()
        {

            //Tabla
            ToTable("beneficios_gestion_prospecto");
        }
    }
}