using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class GetAllLocalidadesByPaisWithFilterOutput : IOutputDto
    {
        public List<LocalidadDepartamentoPaisWithFilterOutput> Localidades { get; set; }
    }
}
