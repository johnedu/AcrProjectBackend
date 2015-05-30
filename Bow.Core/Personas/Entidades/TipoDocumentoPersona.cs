using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.Entidades
{
   public class TipoDocumentoPersona: EntidadMultiTenant
    {
        public string Nombre { get; set; }
        public int LongitudMinima { get; set; }
        public int LongitudMaxima { get; set; }
        public string ConjuntoCaracteres { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public string Default { get; set; }
        public string AplicaEmpresa { get; set; }
        public string AplicaPersona { get; set; }
        public int PaisId { get; set; }
        public virtual Pais TipoDocumentoPersonaPais { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }

        public TipoDocumentoPersona()
        {
            Personas = new List<Persona>();
            Empresas = new List<Empresa>();
        }
    }
}