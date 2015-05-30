using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class GetAfiliadoProspectoOutput : EntityDto
    {
        public int GestionProspectoId { get; set; }
        public int ParentescoId { get; set; }
        public string ParentescoNombre { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public int Edad { get; set; }
        public int CiudadResidenciaId { get; set; }
        public bool BebePorNacer { get; set; }

        public string Localidad { get; set; }
        public int LocalidadId { get; set; }
        public string DepartamentoIndicativo { get; set; }
        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public string PaisIndicativo { get; set; }
        public int PaisId { get; set; }
        public string Pais { get; set; }
    }
}
