using Abp.Application.Services.Dto;
using Bow.Personas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class AuditoriaPersonaOutput : IOutputDto
    {
        public string FechaCambio { get; set; }
        public string Usuario { get; set; }
        public string FechaUsuario { get; set; }
        public string Cambios { get; set; }

        public List<Auditoria> AuditoriaCambios { get; set; }
    }
}
