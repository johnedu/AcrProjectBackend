using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.InputModels
{
    public class GetAllTiposWithFilterInput : IInputDto
    {
        public string ParametroNombre;
        public List<TipoInput> Tipos { get; set; }
    }
}
