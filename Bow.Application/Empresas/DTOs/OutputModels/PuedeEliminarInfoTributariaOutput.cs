using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class PuedeEliminarInfoTributariaOutput : IOutputDto
    {
        public bool PuedeEliminar { get; set; }
    }
}