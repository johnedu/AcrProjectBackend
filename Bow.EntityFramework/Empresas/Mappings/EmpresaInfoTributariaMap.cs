using Bow.Empresas.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Mappings
{
    public class EmpresaInfoTributariaMap: MultiTenantMap<EmpresaInfoTributaria>
    {
        public EmpresaInfoTributariaMap()
        {
            //Atributos
            Property(empresaInfo => empresaInfo.Valor).HasMaxLength(20);
            Property(empresaInfo => empresaInfo.Valor).IsRequired();

            Property(empresaInfo => empresaInfo.FechaInicio).IsRequired();

            //Tabla
            ToTable("empresa_info_tributaria");
        }
    }
}