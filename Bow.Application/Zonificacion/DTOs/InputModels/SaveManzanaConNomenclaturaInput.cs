using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class SaveManzanaConNomenclaturaInput : IInputDto
    {
        [Required]
        public int TorieLocalidad1Id { get; set; }
        public int TorieLocalidad2Id { get; set; }
        public int? SufijoLocalidad1Id { get; set; }
        public int? SufijoLocalidad2Id { get; set; }
        public int? Orientacion1 { get; set; }
        public int? Orientacion2 { get; set; }
        public int BarrioId { get; set; }
        public string Nombre { get; set; }
        //public int Avenida1Id { get; set; }
        //public int Avenida2Id { get; set; }
        //public bool EsAvenida1 { get; set; }
        //public bool EsAvenida2 { get; set; }
       
    }
}