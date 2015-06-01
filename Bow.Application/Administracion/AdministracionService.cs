using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using Bow.Administracion.DTOs.InputModels;
using Bow.Administracion.DTOs.OutputModels;
using Bow.Administracion.Entidades;
using Bow.Administracion.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Administracion
{
    public class AdministracionService : IAdministracionService
    {
        #region Repositorios
        private IPreguntaFrecuenteRepositorio _preguntaFrecuenteRepositorio;
        private IMensajeRepositorio _mensajeRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;
        private IPuntajeRepositorio _puntajeRepositorio;
        private IPreguntaRepositorio _preguntaRepositorio;
        private IRespuestaRepositorio _respuestaRepositorio;
        private ITipoRepositorio _tipoRepositorio;
        private IJuegoRepositorio _juegoRepositorio;
        private IDimensionRepositorio _dimensionRepositorio;
        private IEntidadRepositorio _entidadRepositorio;

        public IAbpSession AbpSession { get; set; }

        #endregion

        //Inyección de Dependencia en el Servicio
        public AdministracionService(
            IPreguntaFrecuenteRepositorio preguntaFrecuenteRepositorio,
            IMensajeRepositorio mensajeRepositorio,
            IUsuarioRepositorio usuarioRepositorio,
            IPuntajeRepositorio puntajeRepositorio,
            IPreguntaRepositorio preguntaRepositorio,
            IRespuestaRepositorio respuestaRepositorio,
            ITipoRepositorio tipoRepositorio,
            IJuegoRepositorio juegoRepositorio,
            IDimensionRepositorio dimensionRepositorio,
            IEntidadRepositorio entidadRepositorio)
        {
            _preguntaFrecuenteRepositorio = preguntaFrecuenteRepositorio;
            _mensajeRepositorio = mensajeRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _puntajeRepositorio = puntajeRepositorio;
            _preguntaRepositorio = preguntaRepositorio;
            _respuestaRepositorio = respuestaRepositorio;
            _tipoRepositorio = tipoRepositorio;
            _juegoRepositorio = juegoRepositorio;
            _dimensionRepositorio = dimensionRepositorio;
            _entidadRepositorio = entidadRepositorio;
            AbpSession = NullAbpSession.Instance;
        }

        /*********************************************************************************************
         ***********************************  Preguntas Frecuentes  **********************************
         *********************************************************************************************/

        public GetPreguntaFrecuenteOutput GetPreguntaFrecuente(GetPreguntaFrecuenteInput faqInput)
        {
            return Mapper.Map<GetPreguntaFrecuenteOutput>(_preguntaFrecuenteRepositorio.Get(faqInput.Id));
        }

        public GetAllPreguntasFrecuentesOutput GetAllPreguntasFrecuentes()
        {
            var listaFAQs = _preguntaFrecuenteRepositorio.GetAllList().OrderBy(p => p.Pregunta);
            return new GetAllPreguntasFrecuentesOutput { PreguntasFrecuentes = Mapper.Map<List<PreguntaFrecuenteOutput>>(listaFAQs) };
        }

        public GetAllPreguntasFrecuentesActivasOutput GetAllPreguntasFrecuentesActivas()
        {
            var listaFAQs = _preguntaFrecuenteRepositorio.GetAll().Where(p => p.EstadoActiva).OrderBy(p => p.Pregunta);
            return new GetAllPreguntasFrecuentesActivasOutput { PreguntasFrecuentes = Mapper.Map<List<PreguntaFrecuenteOutput>>(listaFAQs) };
        }

        public void SavePreguntaFrecuente(SavePreguntaFrecuenteInput nuevaFaq)
        {
            PreguntaFrecuente existeFAQ = _preguntaFrecuenteRepositorio.FirstOrDefault(p => p.Pregunta.ToLower() == nuevaFaq.Pregunta.ToLower());

            if (existeFAQ == null)
            {
                PreguntaFrecuente preguntaFrecuente = Mapper.Map<PreguntaFrecuente>(nuevaFaq);
                preguntaFrecuente.FechaCreacion = DateTime.Now.ToString();
                preguntaFrecuente.TenantId = 1;
                _preguntaFrecuenteRepositorio.Insert(preguntaFrecuente);
            }
            else
            {
                //var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                var mensajeError = "Ya existe la pregunta frecuente.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeletePreguntaFrecuente(DeletePreguntaFrecuenteInput faqEliminar)
        {
            _preguntaFrecuenteRepositorio.Delete(faqEliminar.Id);
        }

        public void UpdatePreguntaFrecuente(UpdatePreguntaFrecuenteInput faqUpdate)
        {
            PreguntaFrecuente existeFAQ = _preguntaFrecuenteRepositorio.FirstOrDefault(p => p.Pregunta.ToLower() == faqUpdate.Pregunta.ToLower() && p.Id != faqUpdate.Id);

            if (existeFAQ == null)
            {
                PreguntaFrecuente preguntaFrecuente = _preguntaFrecuenteRepositorio.Get(faqUpdate.Id);
                Mapper.Map(faqUpdate, preguntaFrecuente);
                preguntaFrecuente.FechaModificacion = DateTime.Now.ToString();
                _preguntaFrecuenteRepositorio.Update(preguntaFrecuente);
            }
            else
            {
                var mensajeError = "Ya existe la pregunta frecuente.";
                throw new UserFriendlyException(mensajeError);
            }
        }

        /*********************************************************************************************
         *****************************************  Mensajes  ****************************************
         *********************************************************************************************/

        public void EnviarMensaje(EnviarMensajeInput mensaje)
        {
            Mensaje mensajeEditado = Mapper.Map<Mensaje>(mensaje);
            mensajeEditado.UsuarioEmisorId = _usuarioRepositorio.GetAll().Where(u => u.Coda == mensaje.CodaEmisor).FirstOrDefault().Id;
            mensajeEditado.UsuarioReceptorId = _usuarioRepositorio.GetAll().Where(u => u.Coda == mensaje.CodaReceptor).FirstOrDefault().Id;
            mensajeEditado.TenantId = 1;
            _mensajeRepositorio.Insert(mensajeEditado);
        }

        public GetMensajeOutput GetMensaje(GetMensajeInput mensaje)
        {
            Mensaje mensajeRespuesta = _mensajeRepositorio.GetMensajeByIdWithReceptorAndEmisor(mensaje.Id);
            if (mensajeRespuesta.UsuarioReceptor.Coda == mensaje.Receptor)
            {
                mensajeRespuesta.FueLeido = true;
                _mensajeRepositorio.Update(mensajeRespuesta);
            }
            return Mapper.Map<GetMensajeOutput>(mensajeRespuesta);
        }

        public GetAllMensajesByEmisorOutput GetAllMensajesByEmisor(GetAllMensajesByEmisorInput mensajeEmisor)
        {
            return new GetAllMensajesByEmisorOutput { Mensajes = Mapper.Map<List<MensajeOutput>>(_mensajeRepositorio.GetAllMensajesByEmisor(mensajeEmisor.Emisor)) };
        }

        public GetAllMensajesByReceptorOutput GetAllMensajesByReceptor(GetAllMensajesByReceptorInput mensajeEmisor)
        {
            return new GetAllMensajesByReceptorOutput { Mensajes = Mapper.Map<List<MensajeOutput>>(_mensajeRepositorio.GetAllMensajesByReceptor(mensajeEmisor.Receptor)) };
        }

        public void DeleteMensaje(DeleteMensajeInput mensajeEliminar)
        {
            _mensajeRepositorio.Delete(mensajeEliminar.Id);
        }

        public GetMensajesSinLeerByUsuarioOutput GetMensajesSinLeerByUsuario(GetMensajesSinLeerByUsuarioInput usuario)
        {
            return new GetMensajesSinLeerByUsuarioOutput { Mensajes = Mapper.Map<List<MensajeOutput>>(_mensajeRepositorio.GetAll().Where(m => m.UsuarioReceptor.Coda == usuario.Usuario && m.FueLeido == false).OrderBy(m => m.Id)) };
        }

        /*********************************************************************************************
         ******************************************  Usuario  ****************************************
         *********************************************************************************************/

        public GetUsuarioByCODAOutput GetUsuarioByCODA(GetUsuarioByCODAInput usuario)
        {
            return Mapper.Map<GetUsuarioByCODAOutput>(_usuarioRepositorio.GetAll().Where(u => u.Coda == usuario.Coda).FirstOrDefault());
        }

        public GetPuntajeUsuarioOutput GetPuntajeUsuario(GetPuntajeUsuarioInput usuario)
        {
            return new GetPuntajeUsuarioOutput { PuntajeTotal = _puntajeRepositorio.GetAll().Where(p => p.UsuarioPuntaje.Coda == usuario.Usuario).Sum(p => p.PuntajeValor) };
        }

        public void SaveUsuario(SaveUsuarioInput usuario)
        {
            Usuario usuarioRegistrado = _usuarioRepositorio.GetAll().Where(u => u.Coda == usuario.Coda).FirstOrDefault();
            if (usuarioRegistrado == null)
            {
                if (usuario.Tipo.ToLower().Equals("ppr"))
                {
                    usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PPR).FirstOrDefault().Id;
                }
                else
                {
                    usuario.TipoId = _tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault().Id;
                }
                Usuario usuarioEditado = Mapper.Map<Usuario>(usuario);
                usuarioEditado.TenantId = 1;
                _usuarioRepositorio.Insert(usuarioEditado);
            }
        }

        public GetHistorialPuntajesUsuarioOutput GetHistorialPuntajesUsuario(GetHistorialPuntajesUsuarioInput usuario)
        {
            return new GetHistorialPuntajesUsuarioOutput { Puntajes = Mapper.Map<List<PuntajeUsuarioOutput>>(_puntajeRepositorio.GetAllHistorialPuntajesByUsuario(usuario.Usuario)),
                                                           PuntajeTotal = _puntajeRepositorio.GetAll().Where(p => p.UsuarioPuntaje.Coda == usuario.Usuario).Sum(p => p.PuntajeValor)
            };
        }

        /*********************************************************************************************
         ****************************************  Preguntas  ****************************************
         *********************************************************************************************/

        public GetPreguntaOutput GetPregunta(GetPreguntaInput preguntaInput)
        {
            return Mapper.Map<GetPreguntaOutput>(_preguntaRepositorio.Get(preguntaInput.Id));
        }

        public GetAllPreguntasByDimensionOutput GetAllPreguntasByDimension(GetAllPreguntasByDimensionInput dimension)
        {
            var listaPreguntas = _preguntaRepositorio.GetAllWithJuego(dimension.DimensionId, dimension.JuegoId);
            return new GetAllPreguntasByDimensionOutput { Preguntas = Mapper.Map<List<PreguntasByDimensionOutput>>(listaPreguntas) };
        }

        public void SavePregunta(SavePreguntaInput nuevaPregunta)
        {
            Pregunta existePregunta = _preguntaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == nuevaPregunta.Texto.ToLower() && p.DimensionId == nuevaPregunta.DimensionId && p.JuegoId == nuevaPregunta.JuegoId);

            if (existePregunta == null)
            {
                Pregunta pregunta = Mapper.Map<Pregunta>(nuevaPregunta);
                pregunta.FechaCreacion = DateTime.Now.ToString();
                pregunta.TenantId = 1;
                _preguntaRepositorio.Insert(pregunta);
            }
            else
            {
                var mensajeError = "Ya existe la pregunta";
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeletePregunta(DeletePreguntaInput preguntaEliminar)
        {
            _respuestaRepositorio.Delete(r => r.PreguntaId == preguntaEliminar.Id);
            _preguntaRepositorio.Delete(preguntaEliminar.Id);
        }

        public PuedeEliminarPreguntaOutput PuedeEliminarPreguntaOutput(PuedeEliminarPreguntaOutputInput preguntaEliminar)
        {
            var listaPuntajes = _puntajeRepositorio.GetAll().Where(op => op.PreguntaId == preguntaEliminar.Id);
            PuedeEliminarPreguntaOutput puede = new PuedeEliminarPreguntaOutput();

            if (listaPuntajes.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }
            else
            {
                puede.PuedeEliminar = false;
            }
            return puede;
        }

        public void UpdatePregunta(UpdatePreguntaInput preguntaUpdate)
        {
            Pregunta existePregunta = _preguntaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == preguntaUpdate.Texto.ToLower() && p.DimensionId == preguntaUpdate.DimensionId && p.JuegoId == preguntaUpdate.JuegoId && p.Id != preguntaUpdate.Id);

            if (existePregunta == null)
            {
                Pregunta pregunta = _preguntaRepositorio.Get(preguntaUpdate.Id);
                Mapper.Map(preguntaUpdate, pregunta);
                pregunta.FechaModificacion = DateTime.Now.ToString();
                _preguntaRepositorio.Update(Mapper.Map<Pregunta>(pregunta));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public GetPreguntaAleatoriaByDimensionAndJuegoOutput GetPreguntaAleatoriaByDimensionAndJuego(GetPreguntaAleatoriaByDimensionAndJuegoInput dimensionAndJuego)
        {
            List<Pregunta> preguntas = _preguntaRepositorio.GetAll().Where(p => p.DimensionId == dimensionAndJuego.DimensionId && p.JuegoId == dimensionAndJuego.JuegoId).ToList();
            var rand = new Random();
            GetPreguntaAleatoriaByDimensionAndJuegoOutput preguntaSeleccionada = Mapper.Map<GetPreguntaAleatoriaByDimensionAndJuegoOutput>(preguntas[rand.Next(preguntas.Count())]);
            preguntaSeleccionada.Respuestas = GetAllRespuestasByPregunta(new GetAllRespuestasByPreguntaInput { PreguntaId = preguntaSeleccionada.Id }).Respuestas;
            return preguntaSeleccionada;
        }

        /*********************************************************************************************
         ****************************************  Respuestas  ***************************************
         *********************************************************************************************/

        public GetRespuestaOutput GetRespuesta(GetRespuestaInput respuestaInput)
        {
            return Mapper.Map<GetRespuestaOutput>(_respuestaRepositorio.Get(respuestaInput.Id));
        }

        public GetAllRespuestasByPreguntaOutput GetAllRespuestasByPregunta(GetAllRespuestasByPreguntaInput pregunta)
        {
            var listaRespuestas = _respuestaRepositorio.GetAllList().Where(p => p.PreguntaId == pregunta.PreguntaId).OrderBy(p => p.Texto);
            return new GetAllRespuestasByPreguntaOutput { Respuestas = Mapper.Map<List<RespuestasByPreguntaOutput>>(listaRespuestas), Comodines5050 = listaRespuestas.Count(m => m.Comodin50_50 == true) };
        }

        public void SaveRespuesta(SaveRespuestaInput nuevaRespuesta)
        {
            Respuesta existeRespuesta = _respuestaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == nuevaRespuesta.Texto.ToLower() && p.PreguntaId == nuevaRespuesta.PreguntaId);

            if (existeRespuesta == null)
            {
                if (nuevaRespuesta.RespuestaVerdadera)
                {
                    Respuesta respuestaCorrectaActual = _respuestaRepositorio.FirstOrDefault(r => r.PreguntaId == nuevaRespuesta.PreguntaId && r.RespuestaVerdadera);
                    if (respuestaCorrectaActual != null)
                    {
                        respuestaCorrectaActual.RespuestaVerdadera = false;
                        _respuestaRepositorio.Update(respuestaCorrectaActual);
                    }
                }
                Respuesta respuesta = Mapper.Map<Respuesta>(nuevaRespuesta);
                respuesta.TenantId = 1;
                _respuestaRepositorio.Insert(respuesta);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteRespuesta(DeleteRespuestaInput respuestaEliminar)
        {

            _respuestaRepositorio.Delete(respuestaEliminar.Id);
        }

        public void UpdateRespuesta(UpdateRespuestaInput respuestaUpdate)
        {
            Respuesta existeRespuesta = _respuestaRepositorio.FirstOrDefault(p => p.Texto.ToLower() == respuestaUpdate.Texto.ToLower() && p.PreguntaId == respuestaUpdate.PreguntaId && p.Id != respuestaUpdate.Id);

            if (existeRespuesta == null)
            {
                respuestaUpdate.Fecha = DateTime.Now.ToString();
                if (respuestaUpdate.RespuestaVerdadera)
                {
                    Respuesta respuestaCorrectaActual = _respuestaRepositorio.FirstOrDefault(r => r.PreguntaId == respuestaUpdate.PreguntaId && r.RespuestaVerdadera && r.Id != respuestaUpdate.Id);
                    if (respuestaCorrectaActual != null)
                    {
                        respuestaCorrectaActual.RespuestaVerdadera = false;
                        _respuestaRepositorio.Update(respuestaCorrectaActual);
                    }
                }
                if (respuestaUpdate.Comodin50_50)
                {
                    int conteo = _respuestaRepositorio.GetAll().Where(r => r.PreguntaId == respuestaUpdate.PreguntaId && r.Comodin50_50 && r.Id != respuestaUpdate.Id).Count();
                    if (conteo == 2)
                    {
                        respuestaUpdate.Comodin50_50 = false;
                    }
                }
                Respuesta respuesta = _respuestaRepositorio.Get(respuestaUpdate.Id);
                Mapper.Map(respuestaUpdate, respuesta);
                _respuestaRepositorio.Update(respuesta);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

        /*********************************************************************************************
         ***************************************  Tipos Usuario  *************************************
         *********************************************************************************************/

        public GetTipoPPROutput GetTipoPPR()
        {
            return Mapper.Map<GetTipoPPROutput>(_tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PPR).FirstOrDefault());
        }

        public GetTipoProfesionalReintegradorOutput GetTipoProfesionalReintegrador()
        {
            return Mapper.Map<GetTipoProfesionalReintegradorOutput>(_tipoRepositorio.GetAll().Where(t => t.Nombre == BowConsts.TIPO_USUARIO_PROFESIONAL).FirstOrDefault());
        }

        /*********************************************************************************************
         *************************************  Puntajes Usuario  ************************************
         *********************************************************************************************/

        public void SavePuntaje(SavePuntajeInput puntaje)
        {
            puntaje.UsuarioId = _usuarioRepositorio.GetAll().Where(u => u.Coda == puntaje.Usuario).FirstOrDefault().Id;
            Puntaje puntajeEditado = Mapper.Map<Puntaje>(puntaje);
            puntajeEditado.TenantId = 1;
            var p = _puntajeRepositorio.Insert(puntajeEditado);
        }

        public GetAllJuegosOutput GetAllJuegos()
        {
            List<Juego> listaJuegos = _juegoRepositorio.GetAll().OrderBy(p => p.Nombre).ToList();
            if (listaJuegos.Count() == 0)
            {
                Juego juego = new Juego { Nombre = BowConsts.JUEGO_MILLONARIO, TenantId = AbpSession.GetTenantId() };
                _juegoRepositorio.Insert(juego);
                listaJuegos.Add(juego);
                juego = new Juego { Nombre = BowConsts.JUEGO_FALSO_VERDADERO, TenantId = AbpSession.GetTenantId() };
                _juegoRepositorio.Insert(juego);
                listaJuegos.Add(juego);
                juego = new Juego { Nombre = BowConsts.JUEGO_PASAPALABRAS, TenantId = AbpSession.GetTenantId() };
                _juegoRepositorio.Insert(juego);
                listaJuegos.Add(juego);
            }
            return new GetAllJuegosOutput { Juegos = Mapper.Map<List<JuegoOutput>>(listaJuegos) };
        }

        public GetAllDimensionesOutput GetAllDimensiones()
        {
            List<Dimension> listaDimensiones = _dimensionRepositorio.GetAll().OrderBy(p => p.Nombre).ToList();
            if (listaDimensiones.Count() == 0)
            {
                Dimension dimension = new Dimension { Nombre = BowConsts.DIMENSION_PERSONAL, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_PRODUCTIVA, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_FAMILIAR, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_HABITABILIDAD, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_SALUD, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_EDUCATIVA, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_CIUDADANA, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
                dimension = new Dimension { Nombre = BowConsts.DIMENSION_SEGURIDAD, TenantId = AbpSession.GetTenantId() };
                _dimensionRepositorio.Insert(dimension);
                listaDimensiones.Add(dimension);
            }
            
            return new GetAllDimensionesOutput { Dimensiones = Mapper.Map<List<DimensionOutput>>(listaDimensiones) };
        }

        /*********************************************************************************************
         ****************************************  Entidades  ****************************************
         *********************************************************************************************/

        public GetEntidadOutput GetEntidad(GetEntidadInput entidadInput)
        {
            return Mapper.Map<GetEntidadOutput>(_entidadRepositorio.Get(entidadInput.Id));
        }

        public GetAllEntidadesByDimensionOutput GetAllEntidadesByDimension(GetAllEntidadesByDimensionInput dimension)
        {
            var listaEntidades = _entidadRepositorio.GetAll().Where(e => e.DimensionId == dimension.DimensionId).ToList();
            return new GetAllEntidadesByDimensionOutput { Entidades = Mapper.Map<List<EntidadByDimensionOutput>>(listaEntidades) };
        }

        public void SaveEntidad(SaveEntidadInput nuevaEntidad)
        {
            Entidad existeEntidad = _entidadRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevaEntidad.Nombre.ToLower());

            if (existeEntidad == null)
            {
                Entidad entidad = Mapper.Map<Entidad>(nuevaEntidad);
                entidad.FechaCreacion = DateTime.Now.ToString();
                entidad.TenantId = 1;
                _entidadRepositorio.Insert(entidad);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteEntidad(DeleteEntidadInput entidadEliminar)
        {

            _entidadRepositorio.Delete(entidadEliminar.Id);
        }

        public void UpdateEntidad(SaveEntidadInput entidadUpdate)
        {
            Entidad existeEntidad = _entidadRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == entidadUpdate.Nombre.ToLower() && p.Id != entidadUpdate.Id);

            if (existeEntidad == null)
            {
                Entidad entidad = _entidadRepositorio.Get(entidadUpdate.Id);
                Mapper.Map(entidadUpdate, entidad);
                entidad.FechaModificacion = DateTime.Now.ToString();
                _entidadRepositorio.Update(entidad);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

    }
}
