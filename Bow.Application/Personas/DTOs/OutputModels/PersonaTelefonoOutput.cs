using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class PersonaTelefonoOutput : EntityDto
    {
        public string Numero { get; set; }
    }
}

