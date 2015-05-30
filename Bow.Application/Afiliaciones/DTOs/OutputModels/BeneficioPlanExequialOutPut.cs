using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class BeneficioPlanExequialOutPut : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
        public bool EsAsignable { get; set; }
        public int Asignables { get; set; }
        public string FechaIngreso { get; set; }
        public string FechaCancelacion { get; set; }
        public int CategoriaBeneficioId { get; set; }
        public string CategoriaBeneficioNombre { get; set; }
        public string Estado { get; set; }
    }
}
