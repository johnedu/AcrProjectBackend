using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class EmpresaOrganizacionMap: MultiTenantMap<EmpresaOrganizacion>
    {
        public EmpresaOrganizacionMap()
        {
            //Atributos
            Property(emp => emp.Nombre).HasMaxLength(100);
            Property(emp => emp.Nombre).IsRequired();

            //Llaves Foraneas
            HasMany<Sucursal>(sucursal => sucursal.EmpresaOrganizacionSucursal)
              .WithRequired(sucursal => sucursal.EmpresaOrganizacion)
              .HasForeignKey(sucursal => sucursal.EmpresaOrganizacionId)
              .WillCascadeOnDelete(false);

            //Tabla
            ToTable("empresa_organizacion");
        }
    }
}