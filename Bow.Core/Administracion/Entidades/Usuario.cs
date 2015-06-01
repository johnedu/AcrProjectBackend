using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Usuario : EntidadMultiTenant
    {
        public string Coda { get; set; }
        public string Nombre { get; set; }
        public int TipoId { get; set; }
        public Tipo TipoUsuario { get; set; }

        public virtual ICollection<Mensaje> MensajesEmisor { get; set; }
        public virtual ICollection<Mensaje> MensajesReceptor { get; set; }
        public virtual ICollection<Puntaje> PuntajesUsuario { get; set; }

        public Usuario()
        {
            MensajesEmisor = new List<Mensaje>();
            MensajesReceptor = new List<Mensaje>();
            PuntajesUsuario = new List<Puntaje>();
        }
    }
}
