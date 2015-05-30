using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class LocalidadesWithDepartamentoOutput : EntityDto
    {
        public string Nombre { get; set; }
        public string LocalidadNombre { get; set; }
        public int DepartamentoId { get; set; }
        public string DepartamentoNombre { get; set; }
        
    }
}
