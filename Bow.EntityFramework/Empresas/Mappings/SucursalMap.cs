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
    public class SucursalMap: MultiTenantMap<Sucursal>
    {
        public SucursalMap()
        {
            //Atributos
            Property(sucursal => sucursal.Nombre).HasMaxLength(100);
            Property(sucursal => sucursal.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<SucursalTelefono>(sucursalTelefono => sucursalTelefono.SucursalTelefonos)
              .WithRequired(sucursalTelefono => sucursalTelefono.SucursalSucursalTelefono)
              .HasForeignKey(sucursalTelefono => sucursalTelefono.SucursalId)
              .WillCascadeOnDelete(false);

            HasMany<PlanExequialSucursal>(s => s.PlanExequialSucursales)
              .WithRequired(p => p.SucursalPlanExequialSucursal)
              .HasForeignKey(p => p.SucursalId)
              .WillCascadeOnDelete(false);

            HasMany<GrupoInformal>(s => s.SucursalGrupoInformal)
             .WithRequired(gr => gr.SucursalGrupoInformal)
             .HasForeignKey(gr => gr.SucursalId)
             .WillCascadeOnDelete(false);

            HasMany<GestionProspecto>(sucursalGestionProspecto => sucursalGestionProspecto.SucursalGestionProspecto)
              .WithRequired(gestionprospecto => gestionprospecto.SucursalVenta)
              .HasForeignKey(gestionprospecto => gestionprospecto.SucursalId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("sucursal");
        }
    }
}