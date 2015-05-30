using Abp.Domain.Entities;
using Bow.EntidadBase;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Pais : EntidadMultiTenant, IMustHaveTenant
    {
        public string Nombre { get; set; }
        public string Indicativo { get; set; }
        public virtual ICollection<Departamento> Departamentos { get; set; }
        public virtual ICollection<TipoDocumentoPersona> TipoDocumentoPersonas { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }

        public Pais()
        {
            Departamentos = new List<Departamento>();
            TipoDocumentoPersonas = new List<TipoDocumentoPersona>();
            Personas = new List<Persona>();
        }
    }
}
