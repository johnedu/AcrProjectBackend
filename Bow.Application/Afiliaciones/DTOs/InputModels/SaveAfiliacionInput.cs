using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class SaveAfiliacionInput : EntityDto
    {
        public int GrupoFamiliarId { get; set; }
        public int GestionProspectoId { get; set; }
        public List<BeneficioPlanExequialInput> BeneficiosAdicionales { get; set; }
    }
}
