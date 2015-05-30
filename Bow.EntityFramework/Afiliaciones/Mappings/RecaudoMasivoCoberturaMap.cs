using Bow.Afiliaciones.Entidades;
using Bow.MappingsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Mappings
{
    public class RecaudoMasivoCoberturaMap: MultiTenantMap<RecaudoMasivoCobertura>
    {
        public RecaudoMasivoCoberturaMap()
        {
            //Atributos
            
            //Llaves Foraneas

            //Tabla
            ToTable("recaudo_masivo_cobertura");
        }
    }
}