using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class InfoTributaria : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int? TipoValorId { get; set; }
        public virtual Tipo TipoValor { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<InfoTributariaOpcion> InfoTributariaOpciones { get; set; }
        public virtual ICollection<InfoTributariaLocalidad> InfoTributariaLocalidades { get; set; }

        public InfoTributaria()
        {
            InfoTributariaOpciones = new List<InfoTributariaOpcion>();
            InfoTributariaLocalidades = new List<InfoTributariaLocalidad>();
        }
    }
}