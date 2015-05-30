using Abp.Domain.Entities;
using Bow.Afiliaciones.Entidades;
using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.Entidades
{
    public class Telefono : EntidadMultiTenant
    {
        public string Numero { get; set; }
        public string Extension { get; set; }
        public int TipoId { get; set; }
        public virtual Tipo TipoTelefono { get; set; }
        public int LocalidadId { get; set; }
        public virtual Localidad LocalidadTelefono { get; set; }

        public virtual ICollection<EmpresaTelefono> EmpresaTelefonos { get; set; }
        public virtual ICollection<PersonaTelefono> PersonaTelefonos { get; set; }
        public virtual ICollection<Prospecto> TelefonoProspecto { get; set; }

        public Telefono()
        {
            EmpresaTelefonos = new List<EmpresaTelefono>();
            PersonaTelefonos = new List<PersonaTelefono>();
            TelefonoProspecto = new List<Prospecto>();
        }

    }
}
