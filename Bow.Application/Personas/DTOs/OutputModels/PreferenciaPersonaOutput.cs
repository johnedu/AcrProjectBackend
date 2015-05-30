using Abp.Application.Services.Dto;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class PreferenciaPersonaOutput : IOutputDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int EstadoId { get; set; }

        public int PersonaId { get; set; }
        public string TipoCambio { get; set; }
        public int OpcionPreferenciaId { get; set; }
        public int PersonaPreferenciaId { get; set; }

        public List<OpcionPreferenciaOutput> OpcionesPreferencia { get; set; }
    }
}
