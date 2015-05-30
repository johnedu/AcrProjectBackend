using Bow.Empleados.Entidades;
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
    public class GrupoInformalEmpleado : EntidadMultiTenant
    {
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int GrupoInformalId { get; set; }
        public virtual GrupoInformal GrupoInformalEmpleadoGrupoInformal { get; set; }
        public int EmpleadoId { get; set; }
        public virtual Empleado GrupoInformalEmpleadoEmpleado { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado GrupoInformalEmpleadoEstado { get; set; }
    }
}