using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Utilidades.AutoMapper
{
    public abstract class AutoMapperBaseProfile : Profile
    {
        private readonly string _nombreProfile;

        protected AutoMapperBaseProfile(string nombreProfile)
        {
            _nombreProfile = nombreProfile;
        }

        public override string ProfileName
        {
            get
            {
                return _nombreProfile;
            }
        }

        protected override void Configure()
        {
            CrearMappings();
        }

        protected abstract void CrearMappings();
    }
}
