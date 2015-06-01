using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Juego : EntidadMultiTenant
    {
        public string Nombre { get; set; }

        public virtual ICollection<Pregunta> PreguntasJuego { get; set; }

        public Juego()
        {
            PreguntasJuego = new List<Pregunta>();
        }
    }
}
