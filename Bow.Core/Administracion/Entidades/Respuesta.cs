using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Respuesta : EntidadMultiTenant
    {
        public string Texto { get; set; }
        public bool Comodin50_50 { get; set; }
        public bool RespuestaVerdadera { get; set; }
        public int PreguntaId { get; set; }
        public Pregunta PreguntaRespuesta { get; set; }
        public bool EstadoActiva { get; set; }
        public string FechaCreacion { get; set; }
        public int UsuarioIdCreacion { get; set; }
        public string FechaModificacion { get; set; }
        public int? UsuarioIdModificacion { get; set; }
    }
}
