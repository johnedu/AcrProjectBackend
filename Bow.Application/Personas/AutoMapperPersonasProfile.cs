using Abp.Localization;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Personas.DTOs.InputModels;
using Bow.Personas.DTOs.OutputModels;
using Bow.Personas.Entidades;
using Bow.Utilidades.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas
{
    public class AutoMapperPersonasProfile : AutoMapperBaseProfile
    {
        public AutoMapperPersonasProfile()
            : base("AutoMapperPersonasProfile")
        {
        }

        protected override void CrearMappings()
        {
            CreateMap<SavePreferenciaInput, Preferencia>();
            CreateMap<SaveOpcionPreferenciaInput, OpcionPreferencia>();
            CreateMap<Preferencia, PreferenciaOutput>();
            CreateMap<OpcionPreferencia, GetOpcionPreferenciaOutput>();

            CreateMap<Preferencia, GetPreferenciaOutput>();
            CreateMap<UpdatePreferenciaInput, Preferencia>();
            CreateMap<UpdateOpcionPreferenciaInput, OpcionPreferencia>();
            CreateMap<OpcionPreferencia, OpcionPreferenciaOutput>();
            CreateMap<Preferencia, PreferenciaOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.CantidadOpcionesPreferencias, opt => opt.MapFrom(src => src.OpcionesPreferencias.Count));
            CreateMap<Preferencia, GetPreferenciaWithEstadoBoolOutput>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EstadoPreferencia.EstadoNombreEstado.Nombre == BowConsts.ESTADO_MOTIVO_ACTIVO ? true : false));
            CreateMap<UpdatePreferenciaWithEstadoBoolInput, Preferencia>()
                .ForMember(dest => dest.EstadoPreferencia, opt => opt.MapFrom(src => src.Estado == true ? true : false));
            CreateMap<SavePersonaInput, Persona>();
            CreateMap<Persona, SavePersonaOutput>();

            //Mapping gestionar tipos de documento
            CreateMap<SaveTipoDocumentoInput, TipoDocumentoPersona>();
            CreateMap<UpdateTipoDocumentoInput, TipoDocumentoPersona>();
            CreateMap<TipoDocumentoPersona, GetTipoDocumentoOutput>()
                .ForMember(dest => dest.PaisNombre, opt => opt.MapFrom(src => src.TipoDocumentoPersonaPais.Nombre))
                .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.TipoDocumentoPersonaPais.Indicativo));
            CreateMap<TipoDocumentoPersona, TipoDocumentoOutput>();
            CreateMap<TipoDocumentoPersona, TipoDocumentoWithPaisOutput>()
                .ForMember(dest => dest.PaisNombre, opt => opt.MapFrom(src => src.TipoDocumentoPersonaPais.Nombre));
            CreateMap<TipoDocumentoPersona, GetTiposDocumentoPersonaOutput>();
            CreateMap<TipoDocumentoPersona, GetTipoDocumentoPorDefectoOutput>();

            CreateMap<Persona, PersonaOutput>()
                .ForMember(dest => dest.TipoDocumentoNombre, opt => opt.MapFrom(src => src.PersonaTipoDocumentoPersona.Nombre))
                .ForMember(dest => dest.PaisNombre, opt => opt.MapFrom(src => src.PersonaPais.Nombre))
                .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.edad == null ? "" : src.edad + LocalizationHelper.GetString("Bow", "personas_nuevapersona_añosPersona") + " - " + String.Format("{0:dd MMM yyyy}", src.FechaNacimiento)));

            CreateMap<Persona, GetPersonaEditarOutput>()
              .ForMember(dest => dest.TipoProfesionNombre, opt => opt.MapFrom(src => src.TipoProfesion.Nombre))
              .ForMember(dest => dest.PaisNombre, opt => opt.MapFrom(src => src.PersonaPais.Nombre))
              .ForMember(dest => dest.Indicativo, opt => opt.MapFrom(src => src.PersonaPais.Indicativo))
              .ForMember(dest => dest.TipoEstadoCivilNombre, opt => opt.MapFrom(src => src.TipoEstadoCivil.Nombre))
              .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.edad));

            CreateMap<Persona, GetPersonaOutput>();

            //Mapping PersonaTelefono
            CreateMap<PersonaTelefono, TelefonoPersonaOutput>()
                .ForMember(dest => dest.TelefonoNumero, opt => opt.MapFrom(src => (src.TelefonoPersonaTelefono.TipoTelefono.Nombre == BowConsts.TIPO_TELEFONO_FIJO ?
                        ("(" + src.TelefonoPersonaTelefono.LocalidadTelefono.DepartamentoLocalidad.Indicativo + ") ") : "") +
                        ((src.TelefonoPersonaTelefono.Extension == null || src.TelefonoPersonaTelefono.Extension == "") ? src.TelefonoPersonaTelefono.Numero : src.TelefonoPersonaTelefono.Numero + " - Ext: " + src.TelefonoPersonaTelefono.Extension)))
                .ForMember(dest => dest.TelefonoTipo, opt => opt.MapFrom(src => src.TelefonoPersonaTelefono.TipoTelefono.Nombre))
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy}", src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy}", src.FechaCancelacion)))
                .ForMember(dest => dest.TipoUbicacionNombre, opt => opt.MapFrom(src => src.TipoUbicacion.Nombre))
                .ForMember(dest => dest.NombreEstado, opt => opt.MapFrom(src => src.Estado.EstadoNombreEstado.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO ? BowConsts.NOMBREESTADO_TELEFONO_DIRECCION_ACTIVO : BowConsts.NOMBREESTADO_TELEFONO_DIRECCION_INACTIVO))
                .ForMember(dest => dest.NombreLocalidad, opt => opt.MapFrom(src => src.TelefonoPersonaTelefono.LocalidadTelefono.Nombre + " (" + src.TelefonoPersonaTelefono.LocalidadTelefono.DepartamentoLocalidad.Nombre + ") "));

            CreateMap<PersonaTelefonoInput, PersonaTelefono>();

            CreateMap<PersonaDireccion, DireccionPersonaOutput>()
                 .ForMember(dest => dest.DireccionCompleta, opt => opt.MapFrom(src => src.DireccionPersonaDireccion.DireccionCompleta))
                 .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy}", src.FechaIngreso)))
                 .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy}", src.FechaCancelacion)))
                 .ForMember(dest => dest.TipoUbicacionNombre, opt => opt.MapFrom(src => src.TipoUbicacion.Nombre))
                 .ForMember(dest => dest.Pista, opt => opt.MapFrom(src => src.DireccionPersonaDireccion.Pista))
                 .ForMember(dest => dest.NombreEstado, opt => opt.MapFrom(src => src.Estado.EstadoNombreEstado.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO ? BowConsts.NOMBREESTADO_TELEFONO_DIRECCION_ACTIVO : BowConsts.NOMBREESTADO_TELEFONO_DIRECCION_INACTIVO))
                 .ForMember(dest => dest.NombreLocalidad, opt => opt.MapFrom(src => src.DireccionPersonaDireccion.BarrioDireccion.Localidad.Nombre + " (" + src.DireccionPersonaDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre + ") "));

            CreateMap<PersonaDireccionInput, PersonaDireccion>();

            //Mapping PersonaContactoWeb
            CreateMap<PersonaContactoWeb, PersonaContactoWebOutput>()
                .ForMember(dest => dest.MedioContactoNombre, opt => opt.MapFrom(src => src.TipoPersonaContactoWeb.Nombre));

            CreateMap<PersonaContactoWebInput, PersonaContactoWeb>();

            CreateMap<PersonaContactoWeb, TipoInput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TipoId))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.TipoPersonaContactoWeb.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.TipoPersonaContactoWeb.Descripcion));

            CreateMap<TipoOutput, TipoContactoWebPersonaOutput>();

            CreateMap<Preferencia, PreferenciaPersonaOutput>()
                 .ForMember(dest => dest.OpcionesPreferencia, opt => opt.MapFrom(src => src.OpcionesPreferencias));

            CreateMap<PersonaPreferenciaInput, PersonaPreferencia>();

            CreateMap<PersonaPreferencia, OpcionPreferenciaPersonaOutput>()
                .ForMember(dest => dest.PreferenciaId, opt => opt.MapFrom(src => src.OpcionPreferenciaPersonaPreferencia.PreferenciaOpcion.Id));

            CreateMap<PersonaTelefono, PersonaTelefonoOutput>()
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => (src.TelefonoPersonaTelefono.TipoTelefono.Nombre == BowConsts.TIPO_TELEFONO_FIJO ?
                        ("(" + src.TelefonoPersonaTelefono.LocalidadTelefono.DepartamentoLocalidad.Indicativo + ") ") : "") +
                        ((src.TelefonoPersonaTelefono.Extension == null) ? src.TelefonoPersonaTelefono.Numero : src.TelefonoPersonaTelefono.Numero + " - Ext: " + src.TelefonoPersonaTelefono.Extension)));

            // Mapping PersonaAuditoria
            CreateMap<SavePersonaInput, PersonaEditadaAuditoriaInput>();

            CreateMap<Persona, PersonaOriginalAuditoriaInput>();

            CreateMap<PersonaAuditoria, AuditoriaPersonaOutput>()
                .ForMember(dest => dest.FechaCambio, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy HH:mm:ss tt}", src.FechaCambio)))
                .ForMember(dest => dest.FechaUsuario, opt => opt.MapFrom(src => String.Format("{0:dd MMM yyyy HH:mm:ss tt}", src.FechaCambio) + " :: " + (src.Usuario)));


            // Mapping clientes prospecto
            CreateMap<SavePersonaProspectoInput, SavePersonaProspectoOutput>();

            CreateMap<SavePersonaProspectoInput, Persona>();

        }
    }
}
