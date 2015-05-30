using Bow.Afiliaciones.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.Entidades
{
    public class Sucursal : EntidadMultiTenant
    {
        public int EmpresaOrganizacionId { get; set; }
        public virtual EmpresaOrganizacion EmpresaOrganizacion { get; set; }
        public string Nombre { get; set; }
        public int TipoId { get; set; }
        public virtual Tipo SucursalTipo { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado SucursalEstado { get; set; }
        public int DireccionId { get; set; }
        public virtual Direccion SucursalDireccion { get; set; }

        public virtual ICollection<SucursalTelefono> SucursalTelefonos { get; set; }
        public virtual ICollection<PlanExequialSucursal> PlanExequialSucursales { get; set; }
        public virtual ICollection<GrupoInformal> SucursalGrupoInformal { get; set; }
        public virtual ICollection<GestionProspecto> SucursalGestionProspecto { get; set; }

        public Sucursal()
        {
            SucursalTelefonos = new List<SucursalTelefono>();
            PlanExequialSucursales = new List<PlanExequialSucursal>();
            SucursalGrupoInformal = new List<GrupoInformal>();
            SucursalGestionProspecto = new List<GestionProspecto>();

        }
    }
}