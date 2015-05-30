﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class GetInfoTributariaOutput : EntityDto
    {
        public string Nombre { get; set; }
        public int? TipoValorId { get; set; }
        public string TipoValor { get; set; }
        public bool EstadoId { get; set; }
    }
}