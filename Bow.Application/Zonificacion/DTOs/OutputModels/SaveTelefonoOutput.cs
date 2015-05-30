using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class SaveTelefonoOutput : IOutputDto
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Extension { get; set; }
        public string TelefonoCompleto { get; set; }
        public int TipoId { get; set; }
        public string TipoNombre { get; set; }
        public int LocalidadId { get; set; }
        public string LocalidadNombre { get; set; }
        public string Ubicacion { get; set; }
    }
}
