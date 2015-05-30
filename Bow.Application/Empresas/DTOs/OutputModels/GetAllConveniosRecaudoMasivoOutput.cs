using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class GetAllConveniosRecaudoMasivoOutput : IOutputDto
    {
        public List<RecaudoMasivoOutput> Convenios { get; set; }
    }
}

