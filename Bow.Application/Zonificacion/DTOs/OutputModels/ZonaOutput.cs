using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class ZonaOutput : EntityDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TipoId { get; set; }
        public string Tipo { get; set; }
        public int CantidadBarrios { get; set; }
        public int CantidadEmpleados { get; set; }
    }
}