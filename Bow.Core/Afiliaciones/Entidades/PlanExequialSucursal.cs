using Bow.Empresas.Entidades;
using Bow.EntidadBase;
using Bow.Parametros.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones.Entidades
{
    public class PlanExequialSucursal : EntidadMultiTenant
    {
        public int PlanExequialId { get; set; }
        public virtual PlanExequial PlanExequialPlanExequialSucursal { get; set; }
        public int SucursalId { get; set; }
        public virtual Sucursal SucursalPlanExequialSucursal { get; set; }
    }
}