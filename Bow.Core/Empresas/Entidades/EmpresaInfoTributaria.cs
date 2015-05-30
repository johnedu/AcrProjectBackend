using Bow.EntidadBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class EmpresaInfoTributaria : EntidadMultiTenant
    {
        public int EmpresaId { get; set; }
        public virtual Empresa EmpresaEmpresaInfoTributaria { get; set; }
        public int InfoTributariaOpcionId { get; set; }
        public virtual InfoTributariaOpcion InfoTributariaOpcionEmpresaInfoTributaria { get; set; }
        public string Valor { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}