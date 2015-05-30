using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class GetPreferenciasOutput : IOutputDto
    {
        public List<PreferenciaOutput> Preferencias { get; set; }
       
    }
}
