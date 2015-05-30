using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using Bow.Seguridad.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class OrganizacionMap: MultiTenantMap<Organizacion>
    {
        public OrganizacionMap()
        {
            //Atributos
            Property(organizacion => organizacion.Nombre).HasMaxLength(100);
            Property(organizacion => organizacion.Nombre).IsRequired();

            Property(organizacion => organizacion.Logo).HasMaxLength(200);

            //Llaves Foraneas
            HasMany<EmpresaOrganizacion>(empresaOrganizacion => empresaOrganizacion.EmpresaOrganizacion)
              .WithRequired(empresaOrganizacion => empresaOrganizacion.OrganizacionEmpresaOrganizacion)
              .HasForeignKey(empresaOrganizacion => empresaOrganizacion.OrganizacionId)
              .WillCascadeOnDelete(false);

            //Llaves Foraneas
            HasMany<RecaudoMasivo>(rec => rec.RecaudoMasivoOrganizacion)
              .WithRequired(org => org.OrganizacionRecaudoMasivo)
              .HasForeignKey(org => org.OrganizacionId)
              .WillCascadeOnDelete(false);

            HasMany<Tenant>(tenant => tenant.TenantsOrganizacion)
              .WithOptional(tenant => tenant.Organizacion)
              .HasForeignKey(tenant => tenant.OrganizacionId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("organizacion");
        }
    }
}