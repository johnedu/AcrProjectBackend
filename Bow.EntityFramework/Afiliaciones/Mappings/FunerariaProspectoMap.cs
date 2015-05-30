using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class FunerariaProspectoMap : MultiTenantMap<FunerariaProspecto>
    {
        public FunerariaProspectoMap()
        {
            //Atributos
            Property(paren => paren.Nombre).HasMaxLength(100);
            Property(paren => paren.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<GestionProspecto>(funerariaprospecto => funerariaprospecto.Funeraria)
              .WithOptional(gestionprospecto => gestionprospecto.FunenariaAfiliado)
              .HasForeignKey(gestionprospecto => gestionprospecto.FunerariaAfiliadoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("funeraria_prospecto");
        }
    }
}