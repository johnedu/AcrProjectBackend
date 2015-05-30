using Bow.Afiliaciones.Entidades;
using Bow.Empleados.Entidades;
using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Personas.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.Entidades
{
    public class Estado : EntidadMultiTenant
    {
        public string Motivo { get; set; }
        public int EstadoNombreId { get; set; }
        public virtual NombreEstado EstadoNombreEstado { get; set; }
        public int ParametroId { get; set; }
        public virtual Parametro ParametroEstado { get; set; }
        public virtual ICollection<Preferencia> Preferencias { get; set; }
        public virtual ICollection<InfoTributaria> InfoTributarias { get; set; }
        public virtual ICollection<PersonaTelefono> EstadosPersonaTelefono { get; set; }
        public virtual ICollection<PersonaDireccion> EstadosPersonaDireccion { get; set; }
        public virtual ICollection<EmpresaOrganizacion> EstadosEmpresaOrganizacion { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
        public virtual ICollection<Empleado> EstadosEmpleado { get; set; }
        public virtual ICollection<ZonaEmpleado> EstadoZonaEmpleado { get; set; }
        public virtual ICollection<PlanExequial> EstadoPlanesExequiales { get; set; }
        public virtual ICollection<GrupoFamiliar> EstadoGruposFamiliares { get; set; }
        public virtual ICollection<BeneficioPlanExequial> EstadoBeneficioPlanExequial { get; set; }
        public virtual ICollection<BeneficioAdicionalPlanExequial> EstadoBeneficiosAdicionalesPlanExequial { get; set; }
        public virtual ICollection<GestionProspecto> EstadoGestionProspecto { get; set; }
        public virtual ICollection<GrupoInformal> EstadoGrupoInformal { get; set; }
        public virtual ICollection<GrupoInformalEmpleado> EstadoGrupoInformalEmpleado { get; set; }

        public Estado()
        {
            Preferencias = new List<Preferencia>();
            InfoTributarias = new List<InfoTributaria>();
            EstadosPersonaTelefono = new List<PersonaTelefono>();
            EstadosPersonaDireccion = new List<PersonaDireccion>();
            EstadosEmpresaOrganizacion = new List<EmpresaOrganizacion>();
            Sucursales = new List<Sucursal>();
            EstadosEmpleado = new List<Empleado>();
            EstadoZonaEmpleado = new List<ZonaEmpleado>();
            EstadoPlanesExequiales = new List<PlanExequial>();
            EstadoGruposFamiliares = new List<GrupoFamiliar>();
            EstadoBeneficioPlanExequial = new List<BeneficioPlanExequial>();
            EstadoBeneficiosAdicionalesPlanExequial = new List<BeneficioAdicionalPlanExequial>();
            EstadoGestionProspecto = new List<GestionProspecto>();
            EstadoGrupoInformal = new List<GrupoInformal>();
            EstadoGrupoInformalEmpleado = new List<GrupoInformalEmpleado>();
        }
    }
}
