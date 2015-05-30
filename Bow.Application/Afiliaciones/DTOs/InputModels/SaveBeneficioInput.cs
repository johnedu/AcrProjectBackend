using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveBeneficioInput : IInputDto
    {
        public int Id { get; set; }
        public int TipoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsNuevo { get; set; }
    }
}
