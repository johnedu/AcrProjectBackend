using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.OutputModels
{
    public class GetTiposBeneficiosPlanExequialOutPut : IOutputDto
    {
        public int PropiosId { get; set; }
        public int AdicionalesId { get; set; }
        public int EstadoActivoId { get; set; }
    }
}


