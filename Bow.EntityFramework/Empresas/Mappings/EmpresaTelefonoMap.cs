using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class EmpresaTelefonoMap: MultiTenantMap<EmpresaTelefono>
    {
        public EmpresaTelefonoMap()
        {
            //Atributos

            //Tabla
            ToTable("empresa_telefono");
        }
    }
}