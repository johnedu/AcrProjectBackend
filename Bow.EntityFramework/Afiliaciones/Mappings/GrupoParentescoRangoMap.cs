using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class GrupoParentescoRangoMap: MultiTenantMap<GrupoParentescoRango>
    {
        public GrupoParentescoRangoMap()
        {
            //Atributos
            Property(rango => rango.EdadMinima).IsRequired();
            Property(rango => rango.EdadMaxima).IsRequired();

            Property(rango => rango.PeriodoCarencia).IsRequired();
            Property(rango => rango.UnidadPeriodoCarencia).HasMaxLength(1);
            Property(rango => rango.UnidadPeriodoCarencia).IsRequired();

            Property(rango => rango.TipoValorBasico).HasMaxLength(1);
            Property(rango => rango.TipoValorBasico).IsRequired();
            Property(rango => rango.ValorBasico).IsRequired();

            Property(rango => rango.TipoValorAdicional).HasMaxLength(1);
            Property(rango => rango.TipoValorAdicional).IsRequired();
            Property(rango => rango.ValorAdicional).IsRequired();

            //Llaves Foraneas

            //Tabla
            ToTable("grupo_parentesco_rango");
        }
    }
}