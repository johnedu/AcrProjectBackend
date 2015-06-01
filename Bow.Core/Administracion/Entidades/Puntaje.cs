using Abp.Domain.Entities;
using Bow.EntidadBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.Entidades
{
    public class Puntaje : EntidadMultiTenant
    {
        public int UsuarioId { get; set; }
        public Usuario UsuarioPuntaje { get; set; }
        public int PreguntaId { get; set; }
        public Pregunta PreguntaPuntaje { get; set; }
        public int PuntajeValor { get; set; }
        public string Respuesta { get; set; }
    }
}
