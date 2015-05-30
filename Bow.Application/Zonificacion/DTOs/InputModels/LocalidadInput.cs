using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class LocalidadInput : EntityDto
    {
        public string Nombre { get; set; }
        public int DepartamentoId { get; set; }
        public int Habitantes { get; set; }
    }
        
}
