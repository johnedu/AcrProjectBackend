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
    public class EmpresaOrganizacion : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int OrganizacionId { get; set; }
        public virtual Organizacion OrganizacionEmpresaOrganizacion { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa EmpresaEmpresaOrganizacion { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado EstadoEmpresaOrganizacion { get; set; }

        public virtual ICollection<Sucursal> EmpresaOrganizacionSucursal { get; set; }

        public EmpresaOrganizacion()
        {
            EmpresaOrganizacionSucursal = new List<Sucursal>();
        }
    }
}