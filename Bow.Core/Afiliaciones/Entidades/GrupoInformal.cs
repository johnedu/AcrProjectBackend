using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class GrupoInformal : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int PorcentajeDescuento { get; set; }
        public bool EncargadoExento { get; set; }
        public int PersonaId { get; set; }
        public virtual Persona PersonaGrupoInformal { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoGrupoInformal { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal SucursalGrupoInformal { get; set; }

        public virtual ICollection<GrupoInformalEmpleado> GrupoInformalEmpleadoGrupoInformal { get; set; }

        public GrupoInformal()
        {
            GrupoInformalEmpleadoGrupoInformal = new List<GrupoInformalEmpleado>();
        }
    }
}