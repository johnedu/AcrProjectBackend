﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas.DTOs.OutputModels
{
    public class TipoContactoWebEmpresaOutput : EntityDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}