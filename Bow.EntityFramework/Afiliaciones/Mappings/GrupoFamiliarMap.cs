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
    public class GrupoFamiliarMap: MultiTenantMap<GrupoFamiliar>
    {
        public GrupoFamiliarMap()
        {
            //Atributos
            Property(grupoFamiliar => grupoFamiliar.Nombre).HasMaxLength(50);
            Property(grupoFamiliar => grupoFamiliar.Nombre).IsRequired();

            Property(grupoFamiliar => grupoFamiliar.Descripcion).HasMaxLength(200);
            Property(grupoFamiliar => grupoFamiliar.Descripcion).IsRequired();

            Property(grupoFamiliar => grupoFamiliar.PermitirAfiliadosAdicionales).HasMaxLength(1);
            Property(grupoFamiliar => grupoFamiliar.PermitirAfiliadosAdicionales).IsRequired();

            Property(grupoFamiliar => grupoFamiliar.ValorPlan).IsRequired();

            Property(grupoFamiliar => grupoFamiliar.TieneCuotaInicial).HasMaxLength(1);
            Property(grupoFamiliar => grupoFamiliar.TieneCuotaInicial).IsRequired();

            //Llaves Foraneas
            HasMany<GestionProspecto>(grupoFamiliar => grupoFamiliar.GruposFamiliaresGestionProspecto)
             .WithOptional(gestionprospecto => gestionprospecto.GrupoFamiliar)
             .HasForeignKey(gestionprospecto => gestionprospecto.GrupoFamiliarId)
             .WillCascadeOnDelete(false);

            HasMany<GrupoFamiliarParentesco>(grupoFamiliar => grupoFamiliar.GruposFamiliaresParentesco)
            .WithRequired(grupofamiliarparentesco => grupofamiliarparentesco.GrupoFamiliar)
            .HasForeignKey(grupofamiliarparentesco => grupofamiliarparentesco.GrupoFamiliarId)
            .WillCascadeOnDelete(false);

            //Tabla
            ToTable("grupo_familiar");
        }
    }
}