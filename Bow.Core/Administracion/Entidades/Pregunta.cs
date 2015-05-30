using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Pregunta : EntidadMultiTenant
    {
        public string Texto { get; set; }
        public int JuegoId { get; set; }
        public Juego JuegoPregunta { get; set; }
        public int DimensionId { get; set; }
        public Dimension DimensionPregunta { get; set; }
        public string Nivel { get; set; }
        public string Pista { get; set; }
        public bool EstadoActiva { get; set; }
        public string FechaCreacion { get; set; }
        public int UsuarioIdCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public int? UsuarioIdModificacion { get; set; }

        public virtual ICollection<Puntaje> PuntajesPregunta { get; set; }
        public virtual ICollection<Respuesta> RespuestasPregunta { get; set; }

        public Pregunta()
        {
            PuntajesPregunta = new List<Puntaje>();
            RespuestasPregunta = new List<Respuesta>();
        }
    }
}
