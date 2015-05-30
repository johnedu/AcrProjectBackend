using Bow.Cartera.DTOs.OutputModels;
using Bow.Cartera.Entidades;
using Bow.Utilidades.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Cartera
{
    public class AutoMapperCarteraProfile : AutoMapperBaseProfile
    {
        public AutoMapperCarteraProfile()
            : base("AutoMapperCarteraProfile")
        {
        }

        protected override void CrearMappings()
        {
            //Mapping de Moneda
            CreateMap<Moneda, MonedaOutput>();

        }
    }
}
