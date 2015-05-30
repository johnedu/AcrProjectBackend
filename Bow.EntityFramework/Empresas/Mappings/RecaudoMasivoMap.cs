using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class RecaudoMasivoMap: MultiTenantMap<RecaudoMasivo>
    {
        public RecaudoMasivoMap()
        {
            //Atributos
            Property(empresa => empresa.Nombre).HasMaxLength(100);
            Property(empresa => empresa.Nombre).IsRequired();

            Property(empresa => empresa.Clave).HasMaxLength(30);
            Property(empresa => empresa.Clave).IsRequired();

            //Llaves Foraneas
            HasMany<RecaudoMasivoCobertura>(cob => cob.RecaudoMasivoCobertura)
              .WithRequired(rec => rec.RecaudoMasivoCoberturaRecaudoMasivo)
              .HasForeignKey(rec => rec.RecaudoMasivoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("recaudo_masivo");
        }
    }
}