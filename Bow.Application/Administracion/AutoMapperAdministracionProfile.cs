using Bow.Utilidades.AutoMapper;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using Bow.Administracion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    public class AutoMapperZonificacionProfile : AutoMapperBaseProfile
    {
        public AutoMapperZonificacionProfile()
            : base("AutoMapperZonificacionProfile")
        {
        }

        protected override void CrearMappings()
        {
            //  Preguntas Frecuentes
            CreateMap<SavePreguntaFrecuenteInput, PreguntaFrecuente>();
            CreateMap<UpdatePreguntaFrecuenteInput, PreguntaFrecuente>();
            CreateMap<PreguntaFrecuente, GetPreguntaFrecuenteOutput>();
            CreateMap<PreguntaFrecuente, PreguntaFrecuenteOutput>();

            //  Mensajes
            CreateMap<EnviarMensajeInput, Mensaje>();
            CreateMap<Mensaje, GetMensajeOutput>()
                .ForMember(dest => dest.Emisor, opt => opt.MapFrom(src => src.UsuarioEmisor.Nombre))
                .ForMember(dest => dest.ReceptorId, opt => opt.MapFrom(src => src.UsuarioReceptorId))
                .ForMember(dest => dest.Receptor, opt => opt.MapFrom(src => src.UsuarioReceptor.Nombre));
            CreateMap<Mensaje, MensajeOutput>()
                .ForMember(dest => dest.EmisorId, opt => opt.MapFrom(src => src.UsuarioEmisorId))
                .ForMember(dest => dest.Emisor, opt => opt.MapFrom(src => src.UsuarioEmisor.Nombre))
                .ForMember(dest => dest.ReceptorId, opt => opt.MapFrom(src => src.UsuarioReceptorId))
                .ForMember(dest => dest.Receptor, opt => opt.MapFrom(src => src.UsuarioReceptor.Nombre));

            //  Usuario
            CreateMap<Usuario, GetUsuarioByCODAOutput>();
            CreateMap<SaveUsuarioInput, Usuario>();   

            //  Preguntas Juego
            CreateMap<SavePreguntaInput, Pregunta>();
            CreateMap<UpdatePreguntaInput, Pregunta>();
            CreateMap<Pregunta, GetPreguntaOutput>();
            CreateMap<Pregunta, GetPreguntaAleatoriaByDimensionAndJuegoOutput>();
            CreateMap<Pregunta, PreguntasByDimensionOutput>()
                .ForMember(dest => dest.Juego, opt => opt.MapFrom(src => src.JuegoPregunta.Nombre));

            //  Respuestas Juego
            CreateMap<SaveRespuestaInput, Respuesta>();
            CreateMap<UpdateRespuestaInput, Respuesta>();
            CreateMap<Respuesta, GetRespuestaOutput>();
            CreateMap<Respuesta, RespuestasByPreguntaOutput>();

            //  Tipos de Usuario

            CreateMap<Tipo, GetTipoPPROutput>();
            CreateMap<Tipo, GetTipoProfesionalReintegradorOutput>();

            //  Puntajes de Usuario
            CreateMap<Usuario, GetPuntajeUsuarioOutput>();
            CreateMap<Puntaje, PuntajeUsuarioOutput>()
                .ForMember(dest => dest.Dimension, opt => opt.MapFrom(src => src.PreguntaPuntaje.DimensionPregunta.Nombre))
                .ForMember(dest => dest.Juego, opt => opt.MapFrom(src => src.PreguntaPuntaje.JuegoPregunta.Nombre))
                .ForMember(dest => dest.Puntaje, opt => opt.MapFrom(src => src.PuntajeValor));
            CreateMap<SavePuntajeInput, Puntaje>();

            CreateMap<Juego, JuegoOutput>();

            CreateMap<Dimension, DimensionOutput>();

            //  Entidades Dimensiones
            CreateMap<SaveEntidadInput, Entidad>();
            CreateMap<UpdateEntidadInput, Entidad>();
            CreateMap<Entidad, GetEntidadOutput>();
            CreateMap<Entidad, EntidadByDimensionOutput>();
        }
    }
}
