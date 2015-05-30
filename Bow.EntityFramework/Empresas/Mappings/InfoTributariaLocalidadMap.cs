using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class InfoTributariaLocalidadMap: MultiTenantMap<InfoTributariaLocalidad>
    {
        public InfoTributariaLocalidadMap()
        {
            //Atributos

            //Tabla
            ToTable("info_tributaria_localidad");
        }
    }
}