using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class ParentescoMap: MultiTenantMap<Parentesco>
    {
        public ParentescoMap()
        {
            //Atributos
            Property(paren => paren.Nombre).HasMaxLength(20);
            Property(paren => paren.Nombre).IsRequired();

            Property(paren => paren.Posicion).IsRequired();

            Property(paren => paren.Genero).HasMaxLength(1);
            Property(paren => paren.Genero).IsRequired();

            Property(paren => paren.Repeticiones).IsRequired();

            Property(paren => paren.CoincidirApellidos).IsRequired();

            //Llaves Foraneas
            HasMany<GrupoFamiliarParentesco>(paren => paren.GruposFamiliaresParentesco)
              .WithRequired(grupo => grupo.Parentesco)
              .HasForeignKey(grupo => grupo.ParentescoId)
              .WillCascadeOnDelete(false);

            HasMany<AfiliadoProspecto>(paren => paren.AfiliadosProspecto)
             .WithRequired(afiliadosprospecto => afiliadosprospecto.Parentesco)
             .HasForeignKey(afiliadosprospecto => afiliadosprospecto.ParentescoId)
             .WillCascadeOnDelete(false);

            //Tabla
            ToTable("parentesco");
        }
    }
}