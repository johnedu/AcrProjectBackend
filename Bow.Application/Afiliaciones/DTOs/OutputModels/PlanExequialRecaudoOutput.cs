﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.DTOs.OutputModels
{
    public class PlanExequialRecaudoOutput : EntityDto, IOutputDto
    {
        public string Nombre { get; set; }
        public int RecaudoMasivoId { get; set; }
        public bool EsObligatorio { get; set; }
        public int NumeroLocalidades { get; set; }
    }
}
