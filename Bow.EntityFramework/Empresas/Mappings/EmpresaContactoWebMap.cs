using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class EmpresaContactoWebMap: MultiTenantMap<EmpresaContactoWeb>
    {
        public EmpresaContactoWebMap()
        {
            //Atributos
            Property(empresaContactoWeb => empresaContactoWeb.Identificador).HasMaxLength(100);
            Property(empresaContactoWeb => empresaContactoWeb.Identificador).IsRequired();

            //Tabla
            ToTable("empresa_contacto_web");
        }
    }
}