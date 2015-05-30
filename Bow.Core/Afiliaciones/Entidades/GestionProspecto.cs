using Bow.Empleados.Entidades;
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

namespace Bow.Afiliaciones.Entidades
{
    public class GestionProspecto : EntidadMultiTenant
    {
        public int ProspectoId { get; set; }
        public virtual Prospecto Prospecto { get; set; }
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }
        public int? PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
        public int? EstadoNoAfiliacionId { get; set; }
        public virtual Estado EstadoNoAfiliacion { get; set; }
        public int? FunerariaAfiliadoId { get; set; }
        public virtual FunerariaProspecto FunenariaAfiliado { get; set; }
        public int? GrupoFamiliarId { get; set; }
        public virtual GrupoFamiliar GrupoFamiliar { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal SucursalVenta { get; set; }
        public int LocalidadId { get; set; }
        public virtual Localidad Localidad { get; set; }
        public DateTime FechaGestion { get; set; }
        public DateTime? FechaBloqueo { get; set; }
        public string EmpresaAfiliada { get; set; }
        public string Observaciones { get; set; }

        public virtual ICollection<AfiliadoProspecto> AfiliadoProspecto { get; set; }
        public virtual ICollection<BeneficiosGestionProspecto> BeneficiosGestionProspecto { get; set; }

        public GestionProspecto()
        {
            AfiliadoProspecto = new List<AfiliadoProspecto>();
            BeneficiosGestionProspecto = new List<BeneficiosGestionProspecto>();
        }

    }
}
