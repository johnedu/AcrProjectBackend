using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetAllLocalidadesByConvenioAndPaisOutput : IOutputDto
    {
        public List<LocalidadesByConvenioAndPaisOutput> RecaudoMasivoLocalidades { get; set; }
    }
}
