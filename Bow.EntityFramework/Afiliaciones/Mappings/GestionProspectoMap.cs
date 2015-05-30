using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class GestionProspectoMap: MultiTenantMap<GestionProspecto>
    {
        public GestionProspectoMap()
        {
            //Atributos
            Property(gestionprospecto => gestionprospecto.FechaGestion).IsRequired();

            Property(gestionprospecto => gestionprospecto.FechaBloqueo).IsOptional();

            Property(gestionprospecto => gestionprospecto.EmpresaAfiliada).HasMaxLength(50);
            Property(gestionprospecto => gestionprospecto.Observaciones).IsOptional();

            Property(gestionprospecto => gestionprospecto.Observaciones).HasMaxLength(500);
            Property(gestionprospecto => gestionprospecto.Observaciones).IsOptional();          

            //Llaves Foraneas
            HasMany<AfiliadoProspecto>(gestionprospecto => gestionprospecto.AfiliadoProspecto)
              .WithRequired(afiliadoprospecto => afiliadoprospecto.GestionProspecto)
              .HasForeignKey(afiliadoprospecto => afiliadoprospecto.GestionProspectoId)
              .WillCascadeOnDelete(false);

            HasMany<BeneficiosGestionProspecto>(gestionprospecto => gestionprospecto.BeneficiosGestionProspecto)
              .WithRequired(beneficiosgestionprospecto => beneficiosgestionprospecto.GestionProspecto)
              .HasForeignKey(beneficiosgestionprospecto => beneficiosgestionprospecto.GestionProspectoId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("gestion_prospecto");
        }
    }
}