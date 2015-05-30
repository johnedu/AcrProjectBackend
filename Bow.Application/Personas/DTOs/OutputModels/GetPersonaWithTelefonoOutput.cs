using Abp.Application.Services.Dto;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas.DTOs.OutputModels
{
    public class GetPersonaWithTelefonoOutput : EntityDto
    {
        public string Documento { get; set; }
        public string NombreCompleto { get; set; }
        public List<PersonaTelefonoOutput> Telefonos { get; set; }
       
    }
}
