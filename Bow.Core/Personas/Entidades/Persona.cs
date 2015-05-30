using Bow.Afiliaciones.Entidades;
using Bow.Empleados.Entidades;
using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
    public class Persona : EntidadMultiTenant
    {
        public bool TieneDocumento { get; set; }
        public int? TipoDocumentoId { get; set; }
        public virtual TipoDocumentoPersona PersonaTipoDocumentoPersona { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime? FechaExpDocumento { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public bool TieneFechaNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public bool ContactarCorreo { get; set; }
        public bool ContactarSms { get; set; }
        public bool ContactarTelefono { get; set; }
        public DateTime FechaIngreso { get; set; }
        public int TipoProfesionId { get; set; }
        public virtual Tipo TipoProfesion { get; set; }
        public int TipoEstadoCivilId { get; set; }
        public virtual Tipo TipoEstadoCivil { get; set; }
        public bool Discapacitada { get; set; }
        public DateTime? FechaFallecimiento { get; set; }
        public int PaisId { get; set; }
        public virtual Pais PersonaPais { get; set; }
        public DateTime FechaUltActualizacion { get; set; }
        public string Usuario { get; set; }

        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<PersonaTelefono> PersonaTelefonos { get; set; }
        public virtual ICollection<PersonaDireccion> PersonaDirecciones { get; set; }
        public virtual ICollection<PersonaContactoWeb> PersonaContactoWeb { get; set; }
        public virtual ICollection<EmpresaContacto> PersonaEmpresaContactos { get; set; }
        public virtual ICollection<PersonaPreferencia> PersonaPreferencia { get; set; }
        public virtual ICollection<PersonaAuditoria> PersonaAuditoria { get; set; }
        public virtual ICollection<Empleado> PersonaEmpleado { get; set; }
        public virtual ICollection<GestionProspecto> PersonaGestionProspecto { get; set; }
        public virtual ICollection<GrupoInformal> PersonaGrupoInformal { get; set; }

        public Persona()
        {
            Empresas = new List<Empresa>();
            PersonaTelefonos = new List<PersonaTelefono>();
            PersonaDirecciones = new List<PersonaDireccion>();
            PersonaContactoWeb = new List<PersonaContactoWeb>();
            PersonaEmpresaContactos = new List<EmpresaContacto>();
            PersonaPreferencia = new List<PersonaPreferencia>();
            PersonaAuditoria = new List<PersonaAuditoria>();
            PersonaEmpleado = new List<Empleado>();
            PersonaGestionProspecto = new List<GestionProspecto>();
            PersonaGrupoInformal = new List<GrupoInformal>();
        }
        
      
        public string nombreCompleto
        {
            get
            {
                if (Apellido2 == null)
                    return Nombre + " " + Apellido1;
                else
                    return Nombre + " " + Apellido1 + " " + Apellido2;
            }
        }

        public int? edad
        {
            get
            {
                if (FechaNacimiento.HasValue)
                {
                    DateTime fechaNac = Convert.ToDateTime(FechaNacimiento);
                    int edad = new DateTime(DateTime.Now.Subtract(fechaNac).Ticks).Year - 1;
                    return edad;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}