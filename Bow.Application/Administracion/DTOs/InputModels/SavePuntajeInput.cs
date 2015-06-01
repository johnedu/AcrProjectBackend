using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.InputModels
{
    public class SavePuntajeInput : IInputDto
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public int PreguntaId { get; set; }
        public int PuntajeValor { get; set; }
        public string Respuesta { get; set; }
    }
}
