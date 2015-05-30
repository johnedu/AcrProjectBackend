using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados.Entidades
{
    public class Empleado : EntidadMultiTenant
    {
        public int Codigo { get; set; }
        public int PersonaId { get; set; }
        public Persona PersonaEmpleado { get; set; }
        public int SucursalId { get; set; }
        public Sucursal SucursalEmpleado { get; set; }
        public int EstadoId { get; set; }
        public Estado EstadoEmpleado { get; set; }
        public virtual ICollection<ZonaEmpleado> ZonasEmpleado { get; set; }
        public virtual ICollection<GestionProspecto> EmpleadoGestionProspecto { get; set; }
        public virtual ICollection<GrupoInformalEmpleado> EmpleadoGrupoInformalEmpleado { get; set; }

        public Empleado()
        {
            ZonasEmpleado = new List<ZonaEmpleado>();
            EmpleadoGestionProspecto = new List<GestionProspecto>();
            EmpleadoGrupoInformalEmpleado = new List<GrupoInformalEmpleado>();
        }
    }
}