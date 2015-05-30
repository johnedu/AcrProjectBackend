
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion.DTOs.OutputModels
{
    public class GetUsuarioByCODAOutput : EntityDto, IOutputDto
    {
        public string Coda { get; set; }
        public string Nombre { get; set; }
    }
}
