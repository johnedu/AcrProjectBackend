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
    public class Empresa : EntidadMultiTenant
    {
        public int TipoNaturalezaId { get; set; }
        public virtual Tipo TipoNaturaleza { get; set; }
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumentoPersona TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public int? PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
        public int ActividadEconomicaId { get; set; }
        public virtual ActividadEconomica ActividadEconomica { get; set; }
        public int DireccionId { get; set; }
        public virtual Direccion Direccion { get; set; }

        public virtual ICollection<EmpresaContacto> EmpresaContactos { get; set; }
        public virtual ICollection<EmpresaContactoWeb> EmpresaContactosWeb { get; set; }
        public virtual ICollection<EmpresaTelefono> EmpresaTelefonos { get; set; }
        public virtual ICollection<EmpresaOrganizacion> EmpresaOrganizacion { get; set; }
        public virtual ICollection<EmpresaInfoTributaria> EmpresaInfoTributaria { get; set; }

        public Empresa()
        {
            EmpresaContactos = new List<EmpresaContacto>();
            EmpresaContactosWeb = new List<EmpresaContactoWeb>();
            EmpresaTelefonos = new List<EmpresaTelefono>();
            EmpresaOrganizacion = new List<EmpresaOrganizacion>();
            EmpresaInfoTributaria = new List<EmpresaInfoTributaria>();
        }
    }
}