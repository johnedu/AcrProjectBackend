using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Parametros.Entidades;
using Bow.Utilidades.AutoMapper;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros
{
   public class AutoMapperParametrosProfile : AutoMapperBaseProfile
    {
       public AutoMapperParametrosProfile()
           : base("AutoMapperParametrosProfile")
        {
        }

        protected override void CrearMappings()
        {
            //Mapping de Parametros
            CreateMap<Parametro, ParametroOutput>();
            CreateMap<SaveParametroInput, Parametro>();
            CreateMap<Parametro, GetParametroOutput>();
            CreateMap<UpdateParametroInput, Parametro>();

            //Mapping de Tipos
            CreateMap<Tipo, TipoOutput>();
            CreateMap<SaveTipoInput, Tipo>();
            CreateMap<Tipo, GetTipoOutput>();
            CreateMap<UpdateTipoInput, Tipo>();
            CreateMap<Tipo, TipoProfesionesOutput>();

            //Mapping de NombreEstado y Estado
            CreateMap<SaveNombreEstadoInput, NombreEstado>();
            CreateMap<NombreEstado, NombreEstadoOutput>();
            CreateMap<Estado, GetEstadosWithNombresEstadosOutput>();
            CreateMap<Estado, EstadoWithNombreEstadoOutput>()
                .ForMember(dest => dest.ParametroId, opt => opt.MapFrom(src => src.ParametroEstado.Id))
                .ForMember(dest => dest.ParametroNombre, opt => opt.MapFrom(src => src.ParametroEstado.Nombre))
                .ForMember(dest => dest.EstadoNombreId, opt => opt.MapFrom(src => src.EstadoNombreEstado.Id))
                .ForMember(dest => dest.EstadoNombreNombre, opt => opt.MapFrom(src => src.EstadoNombreEstado.Nombre))
                .ForMember(dest => dest.EstadoNombreAbreviacion, opt => opt.MapFrom(src => src.EstadoNombreEstado.Abreviacion));

            CreateMap<SaveEstadoInput, Estado>();
            CreateMap<UpdateEstadoParametroInput, Estado>();

            //Mapping tipo zona
            CreateMap<Tipo, TipoZonaOutput>();

            CreateMap<Tipo, TipoTelefonoOutput>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            //Mapping tipo información tributaria
            CreateMap<Tipo, TipoInfoTributariaOutput>();
            CreateMap<Estado, GetEstadoPreferenciaOutput>();

            //Mapping tipo naturaleza empresa
            CreateMap<Tipo, TipoNaturalezaEmpresaOutput>();
            CreateMap<Tipo, GetTipoJuridicaOutput>();

            //Mapping tipo medios de contacto
            CreateMap<TipoInput, Tipo>();

            //Mapping tipo y estado sucursal empresa
            CreateMap<Tipo, TipoSucursalEmpresaOutput>();
            CreateMap<Estado, EstadoSucursalEmpresaOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.EstadoNombreEstado.Nombre));
            //Mapping estados empleado
            CreateMap<Estado, EstadoEmpleadoOutput>()
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.EstadoNombreEstado.Nombre));

            CreateMap<Estado, GetEstadoActivoZonaEmpleadoOutput>();
            CreateMap<Estado, GetEstadoInactivoZonaEmpleadoOutput>();

            //Mapping tipo de rol empleado en zona
            CreateMap<Tipo, TipoRolOutput>();

            
            //*****************************
            //**Mapping módulo afiliaciones
            //*****************************

            CreateMap<SaveCategoriaInput, Tipo>();
            CreateMap<Tipo, CategoriaOutput>();

            CreateMap<Estado, EstadoGrupoFamiliarOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.EstadoNombreEstado.Nombre));

            CreateMap<Estado, EstadoBeneficiosPlanExequialOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.EstadoNombreEstado.Nombre));

            CreateMap<Estado, EstadoClienteProspecto>();
            

            //Mapping módulo personas
            CreateMap<Tipo, GetTipoUbicacionResidencialOutput>();
            CreateMap<Tipo, GetTipoTelefonoFijoOutput>();

        }
    }
}
