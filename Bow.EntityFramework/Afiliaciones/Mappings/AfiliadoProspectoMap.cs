using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class AfiliadoProspectoMap : MultiTenantMap<AfiliadoProspecto>
    {
       public AfiliadoProspectoMap()
        {
            //Atributos
            Property(afiliadoprospecto => afiliadoprospecto.Nombre).IsOptional();

            Property(afiliadoprospecto => afiliadoprospecto.Apellido1).IsOptional();

            Property(afiliadoprospecto => afiliadoprospecto.Apellido2).IsOptional();

            Property(afiliadoprospecto => afiliadoprospecto.Edad).IsRequired();

            Property(afiliadoprospecto => afiliadoprospecto.BebePorNacer).IsRequired();


            //Tabla
            ToTable("afiliado_prospecto");
        }
    }
}