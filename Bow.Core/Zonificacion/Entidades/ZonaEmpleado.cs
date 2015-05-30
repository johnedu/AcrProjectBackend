using Bow.Empleados.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class ZonaEmpleado : EntidadMultiTenant
    {
        public int ZonaId { get; set; }
        public Zona ZonaZonaEmpleado { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado EmpleadoZonaEmpleado { get; set; }
        public int TipoId { get; set; }
        public Tipo TipoZonaEmpleado { get; set; }
        public int EstadoId { get; set; }
        public Estado EstadoZonaEmpleado { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public DateTime? FechaRetiro { get; set; }
    }
}
