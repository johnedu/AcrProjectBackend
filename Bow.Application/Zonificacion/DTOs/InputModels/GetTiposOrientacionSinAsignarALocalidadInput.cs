﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion.DTOs.InputModels
{
    public class GetTiposOrientacionSinAsignarALocalidadInput : IInputDto
    {
        public int LocalidadId { get; set; }
    }
}