using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class SaveCategoriaInput : IInputDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ParametroId { get; set; }
        public bool EsNuevo { get; set; }
    }
}
