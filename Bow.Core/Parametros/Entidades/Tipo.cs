using Abp.Domain.Entities;
using Bow.Afiliaciones.Entidades;
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
    public class Tipo : EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int ParametroId { get; set; }
        public Parametro ParametroTipo { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Telefono> Telefonos { get; set; }
        public virtual ICollection<Zona> Zonas { get; set; }
        public virtual ICollection<Persona> PersonasTipoProfesion { get; set; }
        public virtual ICollection<Persona> PersonasTipoEstadoCivil { get; set; }
        public virtual ICollection<EmpresaContactoWeb> EmpresaContactosWeb { get; set; }
        public virtual ICollection<InfoTributaria> InfoTributarias { get; set; }
        public virtual ICollection<PersonaTelefono> PersonaTelefonoTipoUbicacion { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<PersonaDireccion> PersonaDireccionTipoUbicacion { get; set; }
        public virtual ICollection<PersonaContactoWeb> PersonaContactoWebTipoRed { get; set; }
        public virtual ICollection<EmpresaContacto> EmpresaContactos { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
        public virtual ICollection<ZonaEmpleado> TiposZonaEmpleado { get; set; }
        public virtual ICollection<Beneficio> TiposBeneficio { get; set; }

        public Tipo()
        {
            PersonasTipoEstadoCivil = new List<Persona>();
            PersonasTipoProfesion = new List<Persona>();
            Telefonos = new List<Telefono>();
            Zonas = new List<Zona>();
            EmpresaContactosWeb = new List<EmpresaContactoWeb>();
            InfoTributarias = new List<InfoTributaria>();
            PersonaTelefonoTipoUbicacion = new List<PersonaTelefono>();
            Empresas = new List<Empresa>();
            PersonaDireccionTipoUbicacion = new List<PersonaDireccion>();
            PersonaContactoWebTipoRed = new List<PersonaContactoWeb>();
            EmpresaContactos = new List<EmpresaContacto>();
            Sucursales = new List<Sucursal>();
            TiposZonaEmpleado = new List<ZonaEmpleado>();
            TiposBeneficio = new List<Beneficio>();
        }
    }
}
