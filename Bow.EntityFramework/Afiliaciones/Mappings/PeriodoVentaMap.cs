using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
   public class PeriodoVentaMap: MultiTenantMap<PeriodoVenta>
    {
       public PeriodoVentaMap()
        {
            //Atributos
            Property(periodoventa => periodoventa.Nombre).IsRequired();

            //Tabla
            ToTable("periodo_venta");
        }
    }
}