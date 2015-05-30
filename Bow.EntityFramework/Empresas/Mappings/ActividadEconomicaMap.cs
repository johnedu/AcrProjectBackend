using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class ActividadEconomicaMap: MultiTenantMap<ActividadEconomica>
    {
        public ActividadEconomicaMap()
        {
            //Atributos
            Property(actividadEconomica => actividadEconomica.Codigo).IsRequired();
            Property(actividadEconomica => actividadEconomica.Nombre).HasMaxLength(200);
            Property(actividadEconomica => actividadEconomica.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<Empresa>(empresa => empresa.Empresas)
              .WithRequired(empresa => empresa.ActividadEconomica)
              .HasForeignKey(empresa => empresa.ActividadEconomicaId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("actividad_economica");
        }
    }
}
