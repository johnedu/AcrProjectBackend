using Bow.Afiliaciones.DTOs.InputModels;
using Bow.Afiliaciones.DTOs.OutputModels;
using Bow.Afiliaciones.Entidades;
using Bow.Parametros.Entidades;
using Bow.Utilidades.AutoMapper;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion
{
    public class AutoMapperZonificacionProfile : AutoMapperBaseProfile
    {
        public AutoMapperZonificacionProfile()
            : base("AutoMapperZonificacionProfile")
        {
        }

        protected override void CrearMappings()
        {
            CreateMap<SavePaisInput, Pais>();
            CreateMap<SaveBarrioInput, Barrio>();
            CreateMap<Pais, GetPaisOutput>();
            CreateMap<Pais, PaisOutput>();
            CreateMap<Barrio, BarrioOutput>();
            CreateMap<Barrio, GetBarrioOutput>();
            CreateMap<UpdatePaisInput, Pais>();
            CreateMap<SaveDepartamentoInput, Departamento>();
            CreateMap<Departamento, DepartamentoOutput>();
            CreateMap<Departamento, GetDepartamentoWithPaisOutput>()
                .ForMember(dest => dest.IdDepartamento, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IndicativoDepartamento, opt => opt.MapFrom(src => src.Indicativo))
                .ForMember(dest => dest.NombreDepartamento, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.IndicativoPais, opt => opt.MapFrom(src => src.PaisDepartamento.Indicativo))
                .ForMember(dest => dest.NombrePais, opt => opt.MapFrom(src => src.PaisDepartamento.Nombre))
                .ForMember(dest => dest.IdPais, opt => opt.MapFrom(src => src.PaisDepartamento.Id));


            CreateMap<SaveLocalidadInput, Localidad>();
            CreateMap<Localidad, LocalidadOutput>();
            CreateMap<Localidad, GetLocalidadOutput>();
            CreateMap<UpdateLocalidadInput, Localidad>();

            CreateMap<Localidad, GetLocalidadWithDepartamentoAndPaisOutput>()
                .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoId))
                .ForMember(dest => dest.DepartamentoNombre, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Nombre))
                .ForMember(dest => dest.DepartamentoIndicativo, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Indicativo))
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisId))
                .ForMember(dest => dest.PaisNombre, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Nombre))
                .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Indicativo));
            CreateMap<Localidad, GetLocalidadByIdWithDepartamentoAndPaisOutput>()
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoId))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Nombre))
                .ForMember(dest => dest.DepartamentoIndicativo, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Indicativo))
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisId))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Nombre))
                .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Indicativo));
            
            CreateMap<SaveTipoOrientacionInput, TipoOrientacion>();
            CreateMap<TipoOrientacion, TipoOrientacionOutput>();
            CreateMap<TipoOrientacion, TorieDisponibleOutput>();

            CreateMap<Departamento, GetDepartamentoOutput>();
            CreateMap<UpdateDepartamentoInput, Departamento>();
            CreateMap<SaveTipoOrientacionLocalidadInput, TorieLocalidad>();
            CreateMap<TorieLocalidad, GetTorieLocalidadByLocalidadOutput>()
                .ForMember(dest => dest.NombreTipoOrientacion, opt => opt.MapFrom(src => src.TipoOrientacionTorieLocalidad.Nombre));
            CreateMap<TorieLocalidad, GetTorieLocalidadOutput>()
                .ForMember(dest => dest.NombreTipoOrientacion, opt => opt.MapFrom(src => src.TipoOrientacionTorieLocalidad.Nombre));

            CreateMap<Sufijo, SufijoOutput>();
            CreateMap<Sufijo, SufijoDisponibleOutput>();

            CreateMap<SufijoLocalidad, GetSufijoLocalidadByLocalidadOutput>()
               .ForMember(dest => dest.NombreSufijo, opt => opt.MapFrom(src => src.SufijoSufijoLocalidad.Nombre));
            CreateMap<SufijoLocalidad, GetSufijoLocalidadWithSufijoOutput>()
                .ForMember(dest => dest.SufijoSufijoLocalidadNombre, opt => opt.MapFrom(src => src.SufijoSufijoLocalidad.Nombre));

            CreateMap<SaveSufijoLocalidadInput, SufijoLocalidad>();
            CreateMap<SaveSufijoInput, Sufijo>();
            CreateMap<Avenida, AvenidaOutput>();
            CreateMap<SaveAvenidaInput, Avenida>();
            CreateMap<Avenida, GetAvenidaOutput>();
            CreateMap<UpdateAvenidaInput, Avenida>();

            CreateMap<Manzana, ManzanaOutput>();
            CreateMap<SaveManzanaSinNomenclaturaInput, Manzana>();
            CreateMap<TorieLocalidad, TorieLocalidadManzanaOutput>()
                .ForMember(dest => dest.NombreTipoOrientacion, opt => opt.MapFrom(src => src.TipoOrientacionTorieLocalidad.Nombre));

            CreateMap<SufijoLocalidad, SufijoLocalidadManzanaOutput>()
                .ForMember(dest => dest.NombreSufijoLocalidad, opt => opt.MapFrom(src => src.SufijoSufijoLocalidad.Nombre));

            CreateMap<Avenida, AvenidaLocalidadManzanaOutput>();

            CreateMap<SaveManzanaConNomenclaturaInput, Manzana>();

            //Mapper de Direccion
            CreateMap<Direccion, GetDireccionOutput>();
            //CreateMap<SaveDireccionInput, Direccion>();
            //CreateMap<SaveDireccionSinNomenclaturaInput, SaveDireccionSinNomenclaturaOutput>();

            //CreateMap<SaveDireccionConNomenclaturaInput, SaveDireccionConNomenclaturaOutput>();
            CreateMap<Direccion, SaveDireccionConNomenclaturaOutput>()
              .ForMember(dest => dest.LocalidadCompleta, opt => opt.MapFrom(src => src.BarrioDireccion.Localidad.Nombre + " (" + src.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre + ")"));

            //CreateMap<SaveDireccionSinNomenclaturaUsaInput, SaveDireccionSinNomenclaturaUsaOutput>();
            //CreateMap<SaveDireccionConNomenclaturaUsaInput, SaveDireccionConNomenclaturaUsaOutput>();
            CreateMap<Direccion, SaveDireccionConNomenclaturaUsaOutput>()
            .ForMember(dest => dest.LocalidadCompleta, opt => opt.MapFrom(src => src.BarrioDireccion.Localidad.Nombre + " (" + src.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre + ")"));


            CreateMap<SaveDireccionSinNomenclaturaInput, Direccion>();
            CreateMap<SaveDireccionConNomenclaturaInput, Direccion>();
            CreateMap<SaveDireccionSinNomenclaturaUsaInput, Direccion>();
            CreateMap<SaveDireccionConNomenclaturaUsaInput, Direccion>();

            CreateMap<Direccion, SaveDireccionSinNomenclaturaOutput>()
                .ForMember(dest => dest.LocalidadCompleta, opt => opt.MapFrom(src => src.BarrioDireccion.Localidad.Nombre + " (" + src.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre + ")"));

            CreateMap<Direccion, SaveDireccionSinNomenclaturaUsaOutput>()
              .ForMember(dest => dest.LocalidadCompleta, opt => opt.MapFrom(src => src.BarrioDireccion.Localidad.Nombre + " (" + src.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre + ")"));

            CreateMap<SaveZonaInput, Zona>();

            CreateMap<Zona, ZonaOutput>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoZona.Nombre))
                .ForMember(dest => dest.CantidadBarrios, opt => opt.MapFrom(src => src.ZonasBarrios.Count()));

            CreateMap<Zona, GetZonaOutput>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoZona.Nombre));

            CreateMap<UpdateZonaInput, Zona>();

            CreateMap<ZonaBarrio, ZonaBarrioOutput>()
               .ForMember(dest => dest.BarrioNombre, opt => opt.MapFrom(src => src.BarrioZonaBarrio.Nombre))
               .ForMember(dest => dest.BarrioId, opt => opt.MapFrom(src => src.BarrioZonaBarrio.Id));

            CreateMap<Barrio, ZonaBarrioOutput>()
               .ForMember(dest => dest.BarrioNombre, opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.BarrioId, opt => opt.MapFrom(src => src.Id));

            CreateMap<SaveZonaBarrioInput, ZonaBarrio>();

            CreateMap<Localidad, LocalidadDepartamentoPaisOutput>()
               .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.DepartamentoIndicativo, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Indicativo))
               .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Id))
               .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Nombre))
               .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Indicativo))
               .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Id))
               .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Nombre));

            //Mapping guardar teléfono
            CreateMap<SaveTelefonoInput, Telefono>()
                .ForMember(dest => dest.Extension, opt => opt.MapFrom(src => src.Extension == "" ? null : src.Extension ));
            CreateMap<SaveTelefonoInput, ValidarTelefonoInput>();

            CreateMap<Telefono, SaveTelefonoOutput>()
                .ForMember(dest => dest.LocalidadNombre, opt => opt.MapFrom(src => src.LocalidadTelefono.Nombre))
                .ForMember(dest => dest.TelefonoCompleto, opt => opt.MapFrom(src => (src.TipoTelefono.Nombre == BowConsts.TIPO_TELEFONO_FIJO ?
                    ("(" + src.LocalidadTelefono.DepartamentoLocalidad.Indicativo + ") ") : "") +
                    ((src.Extension == null) ? src.Numero : src.Numero + " - Ext: " + src.Extension)))
                .ForMember(dest => dest.TipoNombre, opt => opt.MapFrom(src => src.TipoTelefono.Nombre))
                .ForMember(dest => dest.LocalidadNombre, opt => opt.MapFrom(src => src.LocalidadTelefono.Nombre))
                .ForMember(dest => dest.Ubicacion, opt => opt.MapFrom(src => src.LocalidadTelefono.Nombre + "(" + src.LocalidadTelefono.DepartamentoLocalidad.Nombre + ")"));

            CreateMap<Localidad, LocalidadDepartamentoPaisWithFilterOutput>()
               .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Nombre))
               .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Id))
               .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Nombre))
               .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Id))
               .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Nombre));


            CreateMap<Telefono, GetTelefonoOutput>()
               .ForMember(dest => dest.LocalidadNombre, opt => opt.MapFrom(src => src.LocalidadTelefono.Nombre + " (" + src.LocalidadTelefono.DepartamentoLocalidad.Nombre + ")"));

            //  Mapping localidades info tributaria
            CreateMap<LocalidadInput, Localidad>();

            // Mapping empleados zona
            CreateMap<ZonaEmpleado, EmpleadoByZonaOutput>()
               .ForMember(dest => dest.NombreEmpleado, opt => opt.MapFrom(src => src.EmpleadoZonaEmpleado.PersonaEmpleado.nombreCompleto))
               .ForMember(dest => dest.EstadoNombre, opt => opt.MapFrom(src => src.EstadoZonaEmpleado.EstadoNombreEstado.Nombre))
               .ForMember(dest => dest.TipoNombre, opt => opt.MapFrom(src => src.TipoZonaEmpleado.Nombre))
               .ForMember(dest => dest.FechaAsignacion, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy}", src.FechaAsignacion)))
               .ForMember(dest => dest.FechaRetiro, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy}", src.FechaRetiro)));

            //Mapping ZonaEmpleado
            CreateMap<SaveZonaEmpleadoInput, ZonaEmpleado>();

            CreateMap<ZonaEmpleado, GetZonaEmpleadoOutput>()
               .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.EmpleadoZonaEmpleado.Codigo))
               .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.EmpleadoZonaEmpleado.PersonaEmpleado.nombreCompleto));         
            

        }
    }
}
