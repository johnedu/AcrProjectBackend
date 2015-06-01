using Abp.Application.Services;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    /// <summary>
    ///     Definición de los servicios ofrecidos por el módulo de Administración
    /// </summary>
    public interface IAdministracionService : IApplicationService
    {
        GetPreguntaFrecuenteOutput GetPreguntaFrecuente(GetPreguntaFrecuenteInput paisInput);

        GetAllPreguntasFrecuentesOutput GetAllPreguntasFrecuentes();

        GetAllPreguntasFrecuentesActivasOutput GetAllPreguntasFrecuentesActivas();

        void SavePreguntaFrecuente(SavePreguntaFrecuenteInput nuevoPais);

        void DeletePreguntaFrecuente(DeletePreguntaFrecuenteInput paisEliminar);

        PuedeEliminarPreguntaOutput PuedeEliminarPreguntaOutput(PuedeEliminarPreguntaOutputInput preguntaEliminar);

        void UpdatePreguntaFrecuente(UpdatePreguntaFrecuenteInput paisUpdate);

        GetPreguntaAleatoriaByDimensionAndJuegoOutput GetPreguntaAleatoriaByDimensionAndJuego(GetPreguntaAleatoriaByDimensionAndJuegoInput dimensionAndJuego);

        void EnviarMensaje(EnviarMensajeInput mensaje);

        GetMensajeOutput GetMensaje(GetMensajeInput mensaje);

        GetAllMensajesByEmisorOutput GetAllMensajesByEmisor(GetAllMensajesByEmisorInput mensajeEmisor);

        GetAllMensajesByReceptorOutput GetAllMensajesByReceptor(GetAllMensajesByReceptorInput mensajeEmisor);

        void DeleteMensaje(DeleteMensajeInput mensajeEliminar);

        GetMensajesSinLeerByUsuarioOutput GetMensajesSinLeerByUsuario(GetMensajesSinLeerByUsuarioInput usuario);

        GetUsuarioByCODAOutput GetUsuarioByCODA(GetUsuarioByCODAInput usuario);

        GetPuntajeUsuarioOutput GetPuntajeUsuario(GetPuntajeUsuarioInput usuario);

        void SaveUsuario(SaveUsuarioInput usuario);

        GetHistorialPuntajesUsuarioOutput GetHistorialPuntajesUsuario(GetHistorialPuntajesUsuarioInput usuario);

        GetPreguntaOutput GetPregunta(GetPreguntaInput preguntaInput);

        GetAllPreguntasByDimensionOutput GetAllPreguntasByDimension(GetAllPreguntasByDimensionInput dimension);

        void SavePregunta(SavePreguntaInput nuevaPregunta);

        void DeletePregunta(DeletePreguntaInput preguntaEliminar);

        void UpdatePregunta(UpdatePreguntaInput preguntaUpdate);

        GetRespuestaOutput GetRespuesta(GetRespuestaInput respuestaInput);

        GetAllRespuestasByPreguntaOutput GetAllRespuestasByPregunta(GetAllRespuestasByPreguntaInput pregunta);

        void SaveRespuesta(SaveRespuestaInput nuevaRespuesta);

        void DeleteRespuesta(DeleteRespuestaInput respuestaEliminar);

        void UpdateRespuesta(UpdateRespuestaInput respuestaUpdate);

        GetTipoPPROutput GetTipoPPR();

        GetTipoProfesionalReintegradorOutput GetTipoProfesionalReintegrador();

        void SavePuntaje(SavePuntajeInput puntaje);

        GetAllJuegosOutput GetAllJuegos();

        GetAllDimensionesOutput GetAllDimensiones();

        GetEntidadOutput GetEntidad(GetEntidadInput entidadInput);

        GetAllEntidadesByDimensionOutput GetAllEntidadesByDimension(GetAllEntidadesByDimensionInput dimension);
        
        void SaveEntidad(SaveEntidadInput nuevaEntidad);

        void DeleteEntidad(DeleteEntidadInput entidadEliminar);

        void UpdateEntidad(SaveEntidadInput entidadUpdate);
    }
}


