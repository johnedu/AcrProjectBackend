using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros.DTOs.OutputModels
{
    public class GetTiposByParametroEstadoCivilWithIdNoIdentificadoOutput : IOutputDto
    {
        public int IdNoIdentificado { get; set; }
        public List<TipoOutput> Tipos { get; set; }
    }
}