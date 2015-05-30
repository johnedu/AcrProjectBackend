using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.InputModels
{
    public class AfiliadoProspectoInput : EntityDto
    {
        public int GestionProspectoId { get; set; }
        public int ParentescoId { get; set; }
        public string ParentescoNombre { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Edad { get; set; }
        public int CiudadResidenciaId { get; set; }
        public string CiudadResidenciaNombre { get; set; }
        public bool BebePorNacer { get; set; }
    }
}
