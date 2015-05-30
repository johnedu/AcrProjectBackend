using Abp.Application.Services.Dto;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class GetAllLocalidadesByPaisWithFilterInput : IInputDto
    {
        public List<Localidad> listaLocalidadesQueNoSeMuestra { get; set; }
        public int PaisId { get; set; }
    }
}
