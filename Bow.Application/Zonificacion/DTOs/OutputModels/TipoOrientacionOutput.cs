﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.OutputModels
{
    public class TipoOrientacionOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
    }
}