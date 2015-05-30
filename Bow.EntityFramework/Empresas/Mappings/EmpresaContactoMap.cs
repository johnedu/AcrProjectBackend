using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class EmpresaContactoMap: MultiTenantMap<EmpresaContacto>
    {
        public EmpresaContactoMap()
        {
            //Atributos
            Property(contacto => contacto.Cargo).HasMaxLength(50);
            Property(contacto => contacto.Cargo).IsRequired();

            //Tabla
            ToTable("empresa_contacto");
        }
    }
}