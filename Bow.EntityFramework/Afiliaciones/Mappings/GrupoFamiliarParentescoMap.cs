using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class GrupoFamiliarParentescoMap: MultiTenantMap<GrupoFamiliarParentesco>
    {
        public GrupoFamiliarParentescoMap()
        {
            //Atributos
            Property(grupo => grupo.ValidarSoloIngreso).IsRequired();

            //Llaves Foraneas
            HasMany<GrupoParentescoRango>(grupo => grupo.GruposParentescoRango)
              .WithRequired(rango => rango.GrupoFamiliarParentesco)
              .HasForeignKey(rango => rango.GrupoFamiliarParentescoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("grupo_familiar_parentesco");
        }
    }
}