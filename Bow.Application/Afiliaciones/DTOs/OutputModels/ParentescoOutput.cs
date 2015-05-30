using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class ParentescoOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        public string Genero { get; set; }
        public int Repeticiones { get; set; }
        public string Limite { get; set; }
        public int? EdadDiferencia { get; set; }
        public bool CoincidirApellidos { get; set; }
    }
}
