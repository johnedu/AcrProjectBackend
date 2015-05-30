using Abp.Localization;
using Abp.UI;
using AutoMapper;
using Bow.Parametros;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Parametros.Entidades;
using Bow.Parametros.Repositorios;
using Bow.Personas.DTOs.InputModels;
using Bow.Personas.DTOs.OutputModels;
using Bow.Personas.Entidades;
using Bow.Personas.Repositorios;
using Bow.Zonificacion;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.Entidades;
using Bow.Zonificacion.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Bow.Afiliaciones.Repositorios;
using Abp.Runtime.Session;

namespace Bow.Personas
{
    public class PersonasService : IPersonasService
    {

        # region Repositorios

        private IPreferenciaRepositorio _preferenciaRepositorio;
        private ITipoDocumentoPersonaRepositorio _tipoDocumentoPersonaRepositorio;
        private IOpcionPreferenciaRepositorio _opcionPreferenciaRepositorio;
        private IPersonaRepositorio _personaRepositorio;
        private IPersonaTelefonoRepositorio _personaTelefonoRepositorio;
        private IPersonaDireccionRepositorio _personaDireccionRepositorio;
        private IPersonaContactoWebRepositorio _personaContactoWebRepositorio;
        private IPersonaPreferenciaRepositorio _personaPreferenciaRepositorio;
        private IPersonaAuditoriaRepositorio _personaAuditoriaRepositorio;
        private IEstadoRepositorio _estadoRepositorio;

        //Los siguientes declaraciones se realizan a los repositorios y no a los servicios debido a la consulta linq
        //que se realiza en el método GetBuscadorPersona, ya que la lista que necesita el query linq debe realizarse dentro de la sentencia
        private ITelefonoRepositorio _telefonoRepositorio;
        private IDireccionRepositorio _direccionRepositorio;
        private IBarrioRepositorio _barrioRepositorio;
        private ILocalidadRepositorio _localidadRepositorio;
        private IDepartamentoRepositorio _departamentoRepositorio;
        private IGrupoInformalRepositorio _grupoInformalRepositorio;

        private IZonificacionService _zonificacionService;
        private IParametrosService _parametrosService;

        public IAbpSession AbpSession { get; set; }


        # endregion

        public PersonasService(IPreferenciaRepositorio preferenciaRepositorio, IOpcionPreferenciaRepositorio opcionPreferenciaRepositorio, IPersonaRepositorio personaRepositorio, ITipoDocumentoPersonaRepositorio tipoDocumentoPersonaRepositorio,
            IPersonaTelefonoRepositorio personaTelefonoRepositorio, IPersonaDireccionRepositorio personaDireccionRepositorio, IPersonaContactoWebRepositorio personaContactoWebRepositorio, IPersonaPreferenciaRepositorio personaPreferenciaRepositorio,
            IPersonaAuditoriaRepositorio personaAuditoriaRepositorio, ITelefonoRepositorio telefonoRepositorio, IDireccionRepositorio direccionRepositorio, IBarrioRepositorio barrioRepositorio, ILocalidadRepositorio localidadRepositorio, IDepartamentoRepositorio departamentoRepositorio,
            IZonificacionService zonificacionService, IParametrosService parametrosService, IGrupoInformalRepositorio grupoInformalRepositorio, IEstadoRepositorio estadoRepositorio)
        {
            _preferenciaRepositorio = preferenciaRepositorio;
            _personaRepositorio = personaRepositorio;
            _tipoDocumentoPersonaRepositorio = tipoDocumentoPersonaRepositorio;
            _opcionPreferenciaRepositorio = opcionPreferenciaRepositorio;
            _personaTelefonoRepositorio = personaTelefonoRepositorio;
            _personaDireccionRepositorio = personaDireccionRepositorio;
            _personaContactoWebRepositorio = personaContactoWebRepositorio;
            _personaPreferenciaRepositorio = personaPreferenciaRepositorio;
            _personaAuditoriaRepositorio = personaAuditoriaRepositorio;
            _estadoRepositorio = estadoRepositorio;

            _telefonoRepositorio = telefonoRepositorio;
            _direccionRepositorio = direccionRepositorio;
            _barrioRepositorio = barrioRepositorio;
            _localidadRepositorio = localidadRepositorio;
            _departamentoRepositorio = departamentoRepositorio;
            _grupoInformalRepositorio = grupoInformalRepositorio;

            _zonificacionService = zonificacionService;
            _parametrosService = parametrosService;
            _parametrosService = parametrosService;

            AbpSession = NullAbpSession.Instance;

        }

        /***************************************************************************
         * 
         *  FUNCIONES PRIVADAS
         * 
         * *************************************************************************/
        private Estado GetEstadoActivoPreferencia()
        {
            return _estadoRepositorio.GetAll().Where(est => est.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && est.ParametroEstado.Nombre == BowConsts.PARAMETRO_PREFERENCIA).FirstOrDefault();
        }

        private Estado GetEstadoInactivoPreferencia()
        {
            return _estadoRepositorio.GetAll().Where(est => est.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && est.ParametroEstado.Nombre == BowConsts.PARAMETRO_PREFERENCIA).FirstOrDefault();
        }

        /**************************************************************************
         * 
         * FUNCIONES PÚBLICAS
         * 
         * ************************************************************************/

        public void ProbarFechas(ProbarFechasInput probarFechasInput)
        {
            var fecha = probarFechasInput.MandoFecha;
            var fechaString = fecha.ToString("dd/MM/yyyy");

        }

        public GetPreferenciaOutput GetPreferencia(GetPreferenciaInput preferenciaInput)
        {
            return Mapper.Map<GetPreferenciaOutput>(_preferenciaRepositorio.Get(preferenciaInput.Id));
        }

        public GetOpcionPreferenciaOutput GetOpcionPreferencia(GetOpcionPreferenciaInput opcionPreferenciaInput)
        {
            return Mapper.Map<GetOpcionPreferenciaOutput>(_opcionPreferenciaRepositorio.Get(opcionPreferenciaInput.Id));
        }


        public GetPreferenciasOutput GetPreferenciasWithOpcionesPreferencia()
        {
            var listaPreferencias = _preferenciaRepositorio.GetAllListWithOpcionPreferenciaByPreferencia();
            return new GetPreferenciasOutput { Preferencias = Mapper.Map<List<PreferenciaOutput>>(listaPreferencias) };
        }

        public GetPreferenciasWithEstadoBoolOutput GetPreferenciasWithEstadoBool()
        {
            var listaPreferencias = _preferenciaRepositorio.GetAllListWithNombreEstado();
            return new GetPreferenciasWithEstadoBoolOutput { Preferencias = Mapper.Map<List<GetPreferenciaWithEstadoBoolOutput>>(listaPreferencias) };
        }

        public GetOpcionesPreferenciaOutput GetOpcionesPreferenciaByPreferencia(GetOpcionesPreferenciasInput preferenciaInput)
        {
            var listaOpcionesPreferencias = _opcionPreferenciaRepositorio.GetAllListByPreferencia(preferenciaInput.Id);
            return new GetOpcionesPreferenciaOutput { OpcionesPreferencia = Mapper.Map<List<OpcionPreferenciaOutput>>(listaOpcionesPreferencias) };
        }

        public void SavePreferencia(SavePreferenciaInput nuevaPreferencia)
        {
            nuevaPreferencia.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaPreferencia.Nombre.ToLower());
            Preferencia existePreferencia = _preferenciaRepositorio.FirstOrDefault(pre => pre.Nombre.ToLower() == nuevaPreferencia.Nombre.ToLower());

            if (existePreferencia == null)
            {
                Preferencia nueva = Mapper.Map<Preferencia>(nuevaPreferencia);
                GetEstadoPreferenciaOutput estadoConsultado = _parametrosService.GetEstadoActivoPreferencia();

                nueva.EstadoId = estadoConsultado.Id;

                _preferenciaRepositorio.Insert(nueva);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_validarNombrePreferenciaError");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void SaveOpcionPreferencia(SaveOpcionPreferenciaInput nuevaOpcionPreferencia)
        {

            nuevaOpcionPreferencia.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaOpcionPreferencia.Nombre.ToLower());
            OpcionPreferencia existeOpcionPreferencia = _opcionPreferenciaRepositorio.FirstOrDefault(op => op.Nombre.ToLower() == nuevaOpcionPreferencia.Nombre.ToLower() && op.PreferenciaId == nuevaOpcionPreferencia.PreferenciaId);

            if (existeOpcionPreferencia == null)
            {
                OpcionPreferencia nueva = Mapper.Map<OpcionPreferencia>(nuevaOpcionPreferencia);
                _opcionPreferenciaRepositorio.Insert(nueva);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_validarNombreOpcionPreferenciaError");
                throw new UserFriendlyException(mensajeError);
            }
        }


        public void UpdateOpcionPreferencia(UpdateOpcionPreferenciaInput opcionPreferenciaUpdate)
        {
            opcionPreferenciaUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(opcionPreferenciaUpdate.Nombre.ToLower());

            OpcionPreferencia existeOpcionPreferencia = _opcionPreferenciaRepositorio.FirstOrDefault(opPre => opPre.Nombre.ToLower() == opcionPreferenciaUpdate.Nombre.ToLower() && opPre.Id != opcionPreferenciaUpdate.Id && opPre.PreferenciaId == opcionPreferenciaUpdate.PreferenciaId);

            if (existeOpcionPreferencia == null)
            {
                var preferenciaActualizar = _opcionPreferenciaRepositorio.Update(Mapper.Map<OpcionPreferencia>(opcionPreferenciaUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_validarNombreOpcionPreferenciaError");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdatePreferencia(UpdatePreferenciaInput preferenciaUpdate)
        {
            preferenciaUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(preferenciaUpdate.Nombre.ToLower());

            Preferencia existePreferencia = _preferenciaRepositorio.FirstOrDefault(pre => pre.Nombre.ToLower() == preferenciaUpdate.Nombre.ToLower() && pre.Id != preferenciaUpdate.Id);
            if (existePreferencia == null)
            {
                Preferencia existe = Mapper.Map<Preferencia>(preferenciaUpdate);
                var preferenciaActualizar = _preferenciaRepositorio.Update(existe);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_validarNombrePreferencia");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdatePreferenciaWithEstadoBool(UpdatePreferenciaWithEstadoBoolInput preferenciaUpdate)
        {
            preferenciaUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(preferenciaUpdate.Nombre.ToLower());

            Preferencia existePreferencia = _preferenciaRepositorio.FirstOrDefault(pre => pre.Nombre.ToLower() == preferenciaUpdate.Nombre.ToLower() && pre.Id != preferenciaUpdate.Id);
            if (existePreferencia == null)
            {
                existePreferencia = _preferenciaRepositorio.Get(preferenciaUpdate.Id);
                existePreferencia.Nombre = preferenciaUpdate.Nombre;
                
                if (preferenciaUpdate.Estado == true)
                    existePreferencia.EstadoPreferencia = GetEstadoActivoPreferencia();
                else
                    existePreferencia.EstadoPreferencia = GetEstadoInactivoPreferencia();

                _preferenciaRepositorio.Update(existePreferencia);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_validarNombrePreferencia");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public PuedeEliminarPreferenciaOutput PuedeEliminarPreferencia(PuedeEliminarPreferenciaInput preferenciaEliminar)
        {
            var listaOpcionesPreferencia = _opcionPreferenciaRepositorio.GetAllList().Where(op => op.PreferenciaId == preferenciaEliminar.Id);
            PuedeEliminarPreferenciaOutput puede = new PuedeEliminarPreferenciaOutput();

            if (listaOpcionesPreferencia.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        public PuedeEliminarOpcionPreferenciaOutput puedeEliminarOpcionPreferencia(PuedeEliminarOpcionPreferenciaInput opcionEliminar)
        {
            PuedeEliminarOpcionPreferenciaOutput puede = new PuedeEliminarOpcionPreferenciaOutput();
            int cantidadOpcion = _opcionPreferenciaRepositorio.GetCantidadOpcionPreferenciaRegistradasPorPersona(opcionEliminar.Id);

            if (cantidadOpcion == 0)
                puede.PuedeEliminar = true;
            else
                puede.PuedeEliminar = false;

            return puede;
                
        }

        public void DeletePreferencia(DeletePreferenciaInput preferenciaEliminar)
        {
            var listaOpcionesPreferencia = _opcionPreferenciaRepositorio.GetAllList().Where(op => op.PreferenciaId == preferenciaEliminar.Id);
            if (listaOpcionesPreferencia.Count() == 0)
            {
                _preferenciaRepositorio.Delete(preferenciaEliminar.Id);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_eliminarPreferencia");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //  Metodo para obtener la información de un tipo de documento según el id
        public GetTipoDocumentoOutput GetTipoDocumento(GetTipoDocumentoInput tipoDocumentoInput)
        {
            return Mapper.Map<GetTipoDocumentoOutput>(_tipoDocumentoPersonaRepositorio.Get(tipoDocumentoInput.Id));
        }

        //  Metodo para obtener la lista de tipos de documento segun el pais
        public GetTiposDocumentoOutput GetTiposDocumento(GetTiposDocumentoInput tiposDocumentoInput)
        {
            var listaTiposDocumento = _tipoDocumentoPersonaRepositorio.GetAllList().Where(p => p.PaisId == tiposDocumentoInput.PaisId).OrderBy(p => p.Nombre);
            return new GetTiposDocumentoOutput { TiposDocumento = Mapper.Map<List<TipoDocumentoOutput>>(listaTiposDocumento) };
        }

        //  Metodo para obtener la lista de tipos de documento con la información del pais
        public GetAllTiposDocumentoWithPaisOutput GetAllTiposDocumentoWithPais()
        {
            var listaTiposDocumento = _tipoDocumentoPersonaRepositorio.GetAllTiposDocumentoWithPais();
            return new GetAllTiposDocumentoWithPaisOutput { TiposDocumento = Mapper.Map<List<TipoDocumentoWithPaisOutput>>(listaTiposDocumento) };
        }

        //  Metodo para guardar un tipo de documento
        public void SaveTipoDocumento(SaveTipoDocumentoInput nuevoTipoDocumento)
        {
            bool validacionesCorrectas = validarTipoDocumento(nuevoTipoDocumento.LongitudMinima, nuevoTipoDocumento.LongitudMaxima, nuevoTipoDocumento.EdadMinima, nuevoTipoDocumento.EdadMaxima);

            nuevoTipoDocumento.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoTipoDocumento.Nombre.ToLower());
            TipoDocumentoPersona existeTipoDocumento = _tipoDocumentoPersonaRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevoTipoDocumento.Nombre.ToLower() && d.PaisId == nuevoTipoDocumento.PaisId);
            if (existeTipoDocumento != null)
            {
                validacionesCorrectas = false;
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_tiposDocumento_validarNombreTipo");
                throw new UserFriendlyException(mensajeError);
            }

            if (validacionesCorrectas)
            {
                if (nuevoTipoDocumento.Default.ToLower().Equals(BowConsts.TIPO_DOCUMENTO_ES_POR_DEFECTO))
                {
                    nuevoTipoDocumento.Default = BowConsts.TIPO_DOCUMENTO_ES_POR_DEFECTO_VALOR_EN_BD;
                    TipoDocumentoPersona TipoDocumentoDefault = _tipoDocumentoPersonaRepositorio.FirstOrDefault(d => d.PaisId == nuevoTipoDocumento.PaisId && d.Default == "1");
                    if (TipoDocumentoDefault != null)
                    {
                        TipoDocumentoDefault.Default = BowConsts.TIPO_DOCUMENTO_NO_ES_POR_DEFECTO_VALOR_EN_BD;
                        _tipoDocumentoPersonaRepositorio.Update(Mapper.Map<TipoDocumentoPersona>(TipoDocumentoDefault));
                    }
                }
                else
                    nuevoTipoDocumento.Default = BowConsts.TIPO_DOCUMENTO_NO_ES_POR_DEFECTO_VALOR_EN_BD;

                if (nuevoTipoDocumento.AplicaEmpresa.ToLower().Equals(BowConsts.TIPO_DOCUMENTO_APLICA_PARA_EMPRESA))
                    nuevoTipoDocumento.AplicaEmpresa = BowConsts.TIPO_DOCUMENTO_APLICA_PARA_EMPRESA_VALOR_EN_BD;
                else
                    nuevoTipoDocumento.AplicaEmpresa = BowConsts.TIPO_DOCUMENTO_NO_APLICA_PARA_EMPRESA_VALOR_EN_BD;

                if (nuevoTipoDocumento.AplicaPersona.ToLower().Equals(BowConsts.TIPO_DOCUMENTO_APLICA_PARA_PERSONA))
                    nuevoTipoDocumento.AplicaPersona = BowConsts.TIPO_DOCUMENTO_APLICA_PARA_PERSONA_VALOR_EN_BD;
                else
                    nuevoTipoDocumento.AplicaPersona = BowConsts.TIPO_DOCUMENTO_NO_APLICA_PARA_PERSONA_VALOR_EN_BD;

                _tipoDocumentoPersonaRepositorio.Insert(Mapper.Map<TipoDocumentoPersona>(nuevoTipoDocumento));
            }
        }

        //  Metodo para eliminar un tipo de documento
        public void DeleteTipoDocumento(DeleteTipoDocumentoInput tipoDocumentoEliminar)
        {
            _tipoDocumentoPersonaRepositorio.Delete(tipoDocumentoEliminar.Id);
        }

        //  Metodo para actualizar un tipo de documento
        public void UpdateTipoDocumento(UpdateTipoDocumentoInput tipoDocumentoUpdate)
        {
            bool validacionesCorrectas = validarTipoDocumento(tipoDocumentoUpdate.LongitudMinima, tipoDocumentoUpdate.LongitudMaxima, tipoDocumentoUpdate.EdadMinima, tipoDocumentoUpdate.EdadMaxima);

            tipoDocumentoUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(tipoDocumentoUpdate.Nombre.ToLower());
            TipoDocumentoPersona existeTipoDocumento = _tipoDocumentoPersonaRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == tipoDocumentoUpdate.Nombre.ToLower() && d.PaisId == tipoDocumentoUpdate.PaisId && d.Id != tipoDocumentoUpdate.Id);

            if (existeTipoDocumento != null)
            {
                validacionesCorrectas = false;
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_tiposDocumento_validarNombreTipo");
                throw new UserFriendlyException(mensajeError);
            }

            if (validacionesCorrectas)
            {
                if (tipoDocumentoUpdate.Default.ToLower().Equals(BowConsts.TIPO_DOCUMENTO_ES_POR_DEFECTO))
                {
                    tipoDocumentoUpdate.Default = BowConsts.TIPO_DOCUMENTO_ES_POR_DEFECTO_VALOR_EN_BD;
                    TipoDocumentoPersona TipoDocumentoDefault = _tipoDocumentoPersonaRepositorio.FirstOrDefault(d => d.PaisId == tipoDocumentoUpdate.PaisId && d.Default == "1" && d.Id != tipoDocumentoUpdate.Id);
                    if (TipoDocumentoDefault != null)
                    {
                        TipoDocumentoDefault.Default = BowConsts.TIPO_DOCUMENTO_NO_ES_POR_DEFECTO_VALOR_EN_BD;
                        _tipoDocumentoPersonaRepositorio.Update(Mapper.Map<TipoDocumentoPersona>(TipoDocumentoDefault));
                    }
                }
                else
                    tipoDocumentoUpdate.Default = BowConsts.TIPO_DOCUMENTO_NO_ES_POR_DEFECTO_VALOR_EN_BD;

                if (tipoDocumentoUpdate.AplicaEmpresa.ToLower().Equals(BowConsts.TIPO_DOCUMENTO_APLICA_PARA_EMPRESA))
                    tipoDocumentoUpdate.AplicaEmpresa = BowConsts.TIPO_DOCUMENTO_APLICA_PARA_EMPRESA_VALOR_EN_BD;
                else
                    tipoDocumentoUpdate.AplicaEmpresa = BowConsts.TIPO_DOCUMENTO_NO_APLICA_PARA_EMPRESA_VALOR_EN_BD;

                if (tipoDocumentoUpdate.AplicaPersona.ToLower().Equals(BowConsts.TIPO_DOCUMENTO_APLICA_PARA_PERSONA))
                    tipoDocumentoUpdate.AplicaPersona = BowConsts.TIPO_DOCUMENTO_APLICA_PARA_PERSONA_VALOR_EN_BD;
                else
                    tipoDocumentoUpdate.AplicaPersona = BowConsts.TIPO_DOCUMENTO_NO_APLICA_PARA_PERSONA_VALOR_EN_BD;

                var tipoDocumentoActualizar = _tipoDocumentoPersonaRepositorio.Update(Mapper.Map<TipoDocumentoPersona>(tipoDocumentoUpdate));
            }
        }

        public PuedeEliminarTipoDocumentoOutput PuedeEliminarTipoDocumento(PuedeEliminarTipoDocumentoInput tipoDocumentoEliminar)
        {
            int listaDocumentosPersona = _tipoDocumentoPersonaRepositorio.GetAll().Where(d => d.Id == tipoDocumentoEliminar.Id).FirstOrDefault().Personas.Count();
            int listaDocumentosEmpresa = _tipoDocumentoPersonaRepositorio.GetAll().Where(d => d.Id == tipoDocumentoEliminar.Id).FirstOrDefault().Empresas.Count();
            PuedeEliminarTipoDocumentoOutput puede = new PuedeEliminarTipoDocumentoOutput();

            if (listaDocumentosPersona == 0 && listaDocumentosEmpresa == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }
            return puede;
        }

        // Función para validar 
        private bool validarTipoDocumento(int LongitudMinima, int LongitudMaxima, int? EdadMinima, int? EdadMaxima)
        {
            bool validacionesCorrectas = true;
            if (LongitudMinima > LongitudMaxima)
            {
                validacionesCorrectas = false;
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_tiposDocumento_notificacionLongitud");
                throw new UserFriendlyException(mensajeError);
            }

            if (EdadMinima != null && EdadMaxima != null)
            {
                if (EdadMinima > EdadMaxima)
                {
                    validacionesCorrectas = false;
                    var mensajeError = LocalizationHelper.GetString("Bow", "personas_tiposDocumento_notificacionEdad");
                    throw new UserFriendlyException(mensajeError);
                }
            }

            if (LongitudMinima < 1 || LongitudMaxima > BowConsts.TIPO_DOCUMENTO_PERSONA_LONGITUD_MAXIMA)
            {
                validacionesCorrectas = false;
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_tiposDocumento_notificacionRangoLongitud");
                throw new UserFriendlyException(mensajeError);
            }

            return validacionesCorrectas;
        }

        //Metodo para guardar una persona
        public SavePersonaOutput SavePersona(SavePersonaInput nuevaPersona)
        {
            SavePersonaOutput personaGuardada = new SavePersonaOutput();

            var mensajeError = "";

            var existeDocumento = new Persona();

            //Verificamos si la persona a ingresar es nueva o es para actualizar
            if (nuevaPersona.Id == 0)
            {
                if (nuevaPersona.NumeroDocumento != null)
                {
                    existeDocumento = _personaRepositorio.FirstOrDefault(b => b.NumeroDocumento.ToLower() == nuevaPersona.NumeroDocumento.ToLower() && b.TipoDocumentoId == nuevaPersona.TipoDocumentoId);
                }
            }
            else
            {
                existeDocumento = null;
            }

            if (existeDocumento == null || existeDocumento.Id == 0)
            {
                DateTime fechaActual = DateTime.Now;

                if (nuevaPersona.TieneDocumento == true && nuevaPersona.TipoDocumentoId != null && nuevaPersona.NumeroDocumento != null)
                {
                    if (nuevaPersona.FechaNacimiento != null)
                    {
                        var fechanacimiento = Convert.ToDateTime(nuevaPersona.FechaNacimiento);
                        if (fechanacimiento > fechaActual)
                        {
                            mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_fechaNacimientoError");
                        }
                        else
                        {
                            ValidarTipoDocumentoConEdadInput validacionConEdadInput = new ValidarTipoDocumentoConEdadInput();
                            validacionConEdadInput.TipoDocumentoId = Int32.Parse(nuevaPersona.TipoDocumentoId.ToString());
                            validacionConEdadInput.NumeroDocumento = nuevaPersona.NumeroDocumento;
                            validacionConEdadInput.Edad = new DateTime(DateTime.Now.Subtract(fechanacimiento).Ticks).Year - 1;
                            ValidarTipoDocumentoConEdadOutput validacionConEdadOutput = ValidarTipoDocumentoPersonaConEdad(validacionConEdadInput);
                            if (!validacionConEdadOutput.TipoDocumentoValido)
                            {
                                mensajeError = validacionConEdadOutput.Mensaje;
                            }
                        }
                    }
                    else
                    {
                        ValidarTipoDocumentoInput validacionInput = new ValidarTipoDocumentoInput();
                        validacionInput.TipoDocumentoId = Int32.Parse(nuevaPersona.TipoDocumentoId.ToString());
                        validacionInput.NumeroDocumento = nuevaPersona.NumeroDocumento;
                        ValidarTipoDocumentoOutput validacionOutput = ValidarTipoDocumentoPersona(validacionInput);
                        if (!validacionOutput.TipoDocumentoValido)
                        {
                            mensajeError = validacionOutput.Mensaje;
                        }
                    }
                }
                else if (nuevaPersona.TieneDocumento == false)
                {
                    nuevaPersona.TipoDocumentoId = null;
                    nuevaPersona.NumeroDocumento = null;
                }

                if (nuevaPersona.FechaFallecimiento.HasValue)
                {
                    var fechaFallecimiento = Convert.ToDateTime(nuevaPersona.FechaFallecimiento);
                    if (fechaFallecimiento > fechaActual)
                    {
                        mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_fechaFallecimientoError");
                        throw new UserFriendlyException(mensajeError);
                    }
                }

                //Validamos que la fecha de nacimiento no sea superior ni inferior a la indicada
                if (nuevaPersona.FechaNacimiento.HasValue)
                {
                    DateTime? fechaNacIngresada = nuevaPersona.FechaNacimiento;

                    FechaNacimientoOutput fechasNac = new FechaNacimientoOutput();

                    fechasNac = FechaNacimiento();

                    if (fechaNacIngresada > fechasNac.FechaMaximaNac)
                    {
                        mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_fechaNacimientoError");
                    }
                    else if (fechaNacIngresada < fechasNac.FechaMinimaNac)
                    {
                        mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_edadSuperiorError");
                    }
                    if (mensajeError != "")
                    {
                        throw new UserFriendlyException(mensajeError);
                    }
                }

                //Validamos que la fecha de expedicion del documento no sea superior ni inferior a la indicada
                if (nuevaPersona.FechaExpDocumento.HasValue)
                {
                    var fechaIngresada = Convert.ToDateTime(nuevaPersona.FechaExpDocumento);
                    FechaExpedicionOutput fechasExp = new FechaExpedicionOutput();

                    fechasExp = FechaExpedicionDocumento();

                    if (fechaIngresada > fechasExp.FechaMaximaExp)
                    {
                        mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_fechaExpedicionError");
                    }
                    else if (fechaIngresada < fechasExp.FechaMinimaExp)
                    {
                        mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_fechaExpedicionSuperirorError");
                    }
                    if (mensajeError != "")
                    {
                        throw new UserFriendlyException(mensajeError);
                    }
                }

                nuevaPersona.FechaUltActualizacion = DateTime.Now;

                //Recortamos el genero que viene Maculino o Femenino para almacenar solo la primera letra
                nuevaPersona.Genero = nuevaPersona.Genero.Substring(0, 1);

                nuevaPersona.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaPersona.Nombre.ToLower());
                nuevaPersona.Apellido1 = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaPersona.Apellido1.ToLower());
                nuevaPersona.Apellido2 = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaPersona.Apellido2.ToLower());

                if (nuevaPersona.Id == 0)
                {
                    nuevaPersona.FechaIngreso = DateTime.Now;

                    Persona nueva = new Persona();
                    nueva = Mapper.Map<Persona>(nuevaPersona);

                    _personaRepositorio.InsertAndGetId(nueva);

                    personaGuardada = Mapper.Map<SavePersonaOutput>(nueva);
                }
                else if (nuevaPersona.Id != 0)
                {
                    PersonaEditadaAuditoriaInput personaEditada = Mapper.Map<PersonaEditadaAuditoriaInput>(nuevaPersona);
                    PersonaOriginalAuditoriaInput personaOriginal = Mapper.Map<PersonaOriginalAuditoriaInput>(_personaRepositorio.Get(nuevaPersona.Id));
                    SavePersonaAuditoria(personaOriginal, personaEditada);

                    Persona personaActualizar = _personaRepositorio.Get(nuevaPersona.Id);
                    personaActualizar.TieneDocumento = nuevaPersona.TieneDocumento;
                    personaActualizar.TipoDocumentoId = nuevaPersona.TipoDocumentoId;
                    personaActualizar.NumeroDocumento = nuevaPersona.NumeroDocumento;
                    personaActualizar.FechaExpDocumento = nuevaPersona.FechaExpDocumento;
                    personaActualizar.Nombre = nuevaPersona.Nombre;
                    personaActualizar.Apellido1 = nuevaPersona.Apellido1;
                    personaActualizar.Apellido2 = nuevaPersona.Apellido2;
                    personaActualizar.TieneFechaNacimiento = nuevaPersona.TieneFechaNacimiento;
                    personaActualizar.FechaNacimiento = nuevaPersona.FechaNacimiento;
                    personaActualizar.Genero = nuevaPersona.Genero;
                    personaActualizar.CorreoElectronico = nuevaPersona.CorreoElectronico;
                    personaActualizar.ContactarSms = nuevaPersona.ContactarSms;
                    personaActualizar.ContactarCorreo = nuevaPersona.ContactarCorreo;
                    personaActualizar.ContactarTelefono = nuevaPersona.ContactarTelefono;
                    personaActualizar.TipoProfesionId = nuevaPersona.TipoProfesionId.Value;
                    personaActualizar.TipoEstadoCivilId = nuevaPersona.TipoEstadoCivilId;
                    personaActualizar.Discapacitada = nuevaPersona.Discapacitada;
                    personaActualizar.FechaFallecimiento = nuevaPersona.FechaFallecimiento;
                    personaActualizar.PaisId = nuevaPersona.PaisId.Value;
                    personaActualizar.FechaUltActualizacion = nuevaPersona.FechaUltActualizacion;

                    _personaRepositorio.Update(personaActualizar);

                    personaGuardada = Mapper.Map<SavePersonaOutput>(personaActualizar);
                }
            }
            else
            {
                var mensajeErrorExiste = LocalizationHelper.GetString("Bow", "personas_nuevapersona_validadPersonaNumeroDocumento");
                throw new UserFriendlyException(mensajeErrorExiste);
            }

            if (mensajeError != "")
            {
                throw new UserFriendlyException(mensajeError);
            }

            //return personaId;

            return personaGuardada;

        }

        //Método para calcular el rango de la fecha de expedición del documento 
        public FechaExpedicionOutput FechaExpedicionDocumento()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaMinima = new DateTime((fechaActual.Year - BowConsts.EXPEDICION_MINIMA), fechaActual.Month, fechaActual.Day, fechaActual.Hour, fechaActual.Minute, fechaActual.Second);

            FechaExpedicionOutput fechasExpedicionDocumento = new FechaExpedicionOutput();

            fechasExpedicionDocumento.FechaMinimaExp = fechaMinima;
            fechasExpedicionDocumento.FechaMaximaExp = fechaActual;

            return fechasExpedicionDocumento;
        }

        //Método para calcular el rango de la fecha de nacimiento 
        public FechaNacimientoOutput FechaNacimiento()
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaMinimaNac = new DateTime((fechaActual.Year - BowConsts.EDAD_MAXIMA), fechaActual.Month, fechaActual.Day, fechaActual.Hour, fechaActual.Minute, fechaActual.Second);

            FechaNacimientoOutput fechasNacimiento = new FechaNacimientoOutput();

            fechasNacimiento.FechaMinimaNac = fechaMinimaNac;
            fechasNacimiento.FechaMaximaNac = fechaActual;

            return fechasNacimiento;
        }

        //Método para calcular la fecha minima de fallecimiento 
        public FechaFallecimientoOutput FechaFallecimiento(FechaFallecimientoInput personaInput)
        {
            var fechaNacimientoPersona = _personaRepositorio.Get(personaInput.PersonaId);

            DateTime fechaActual = DateTime.Now;
            FechaFallecimientoOutput fechasFallecimiento = new FechaFallecimientoOutput();

            fechasFallecimiento.FechaMaximaFall = fechaActual;

            if (fechaNacimientoPersona.FechaNacimiento != null)
            {
                DateTime fechaNacimiento = Convert.ToDateTime(fechaNacimientoPersona.FechaNacimiento);
                fechasFallecimiento.FechaMinimaFall = fechaNacimiento;
            }
            else
            {
                DateTime fechaMinimaFall = new DateTime((fechaActual.Year - BowConsts.EDAD_MAXIMA), fechaActual.Month, fechaActual.Day);
                fechasFallecimiento.FechaMinimaFall = fechaMinimaFall;
            }

            return fechasFallecimiento;
        }

        //  Metodo para obtener la lista de tipos de documento segun el pais
        public GetTiposDocumentoPersonaOutput GetTiposDocumentoPersona(GetTiposDocumentoPersonaInput paisInput)
        {
            var listaTiposDocumento = _tipoDocumentoPersonaRepositorio.GetAllList().Where(p => p.PaisId == paisInput.PaisId).OrderBy(p => p.Nombre);

            if (listaTiposDocumento.Count() == 0)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "personas_nuevapersona_noexisteTiposDocumento");
                throw new UserFriendlyException(mensajeError);
            }
            return new GetTiposDocumentoPersonaOutput { TiposDocumento = Mapper.Map<List<TipoDocumentoOutput>>(listaTiposDocumento) };
        }

        //  Metodo para obtener la lista de tipos de documento segun el pais
        public GetTipoDocumentoPorDefectoOutput GetTipoDocumentoPorDefecto(GetTipoDocumentoPorDefectoInput paisInput)
        {
            return Mapper.Map<GetTipoDocumentoPorDefectoOutput>(_tipoDocumentoPersonaRepositorio.GetAllList().Where(p => p.PaisId == paisInput.PaisId && p.Default == BowConsts.TIPO_DOCUMENTO_ES_POR_DEFECTO_VALOR_EN_BD).FirstOrDefault());
        }

        public void DeleteOpcionPreferencia(DeleteOpcionPreferenciaInput opcionPreferenciaEliminar)
        {
            //var listaOpcionesPreferencia = _opcionPreferenciaRepositorio.GetAllList().Where(op => op.PreferenciaId == preferenciaEliminar.Id);
            /*  if (listaOpcionesPreferencia.Count() == 0)
              {*/
            _opcionPreferenciaRepositorio.Delete(opcionPreferenciaEliminar.Id);
            /* }
             else
             {
                 var mensajeError = LocalizationHelper.GetString("Bow", "personas_preferencia_eliminarPreferencia");
                 throw new UserFriendlyException(mensajeError);
             }*/
        }

        public GetPersonasOutput GetPersonas(GetPersonasInput personaInput)
        {
            GetPersonasOutput personasResultado = new GetPersonasOutput();

            if (personaInput.TieneDocumento == true)
            {
                ValidarTipoDocumentoInput validacionInput = new ValidarTipoDocumentoInput();
                validacionInput.TipoDocumentoId = Int32.Parse(personaInput.TipoDocumentoId.ToString());
                validacionInput.NumeroDocumento = personaInput.NumeroDocumento;
                ValidarTipoDocumentoOutput validacionOutput = ValidarTipoDocumentoPersona(validacionInput);
                if (!validacionOutput.TipoDocumentoValido)
                {
                    var mensajeError = validacionOutput.Mensaje;
                    throw new UserFriendlyException(mensajeError);
                }

                //Consulta de persona que contengan el mismo número de documento
                var listaPersonaByDocument = new List<Persona>();
                listaPersonaByDocument = _personaRepositorio.GetWithTipoDocumentoAndPaisAndDepartamentoByDocumento(personaInput.NumeroDocumento);

                //Se hacen las validaciones correspondientes 
                if (listaPersonaByDocument.Count() != 0)
                {
                    personasResultado = new GetPersonasOutput { Personas = Mapper.Map<List<PersonaOutput>>(listaPersonaByDocument) };
                    personasResultado.PuedeSeleccionarPersona = false;
                }
                else
                {
                    var listaPersonaByDatosBasicosHomonimoConSinDocumento = _personaRepositorio.GetWithTipoDocumentoAndDepartamentoByDatosBasicosHomonimoConSinDocumento(personaInput.Nombre, personaInput.Apellido1, personaInput.Apellido2);

                    if (listaPersonaByDatosBasicosHomonimoConSinDocumento.Count() != 0)
                    {
                        personasResultado = new GetPersonasOutput { Personas = Mapper.Map<List<PersonaOutput>>(listaPersonaByDatosBasicosHomonimoConSinDocumento) };
                        personasResultado.PuedeSeleccionarPersona = true;
                    }
                }
            }
            else
            {
                //Consulta de persona que contengan homónimo de nombre completo con o sin tipo de documento
                var listaPersonaByDatosBasicosHomonimoConSinDocumento = _personaRepositorio.GetWithTipoDocumentoAndDepartamentoByDatosBasicosHomonimoConSinDocumento(personaInput.Nombre, personaInput.Apellido1, personaInput.Apellido2);

                if (listaPersonaByDatosBasicosHomonimoConSinDocumento.Count() != 0)
                {
                    personasResultado = new GetPersonasOutput { Personas = Mapper.Map<List<PersonaOutput>>(listaPersonaByDatosBasicosHomonimoConSinDocumento) };
                    personasResultado.PuedeSeleccionarPersona = true;
                }
            }

            return personasResultado;

        }

        public GetPersonaEditarOutput GetPersonaEditar(GetPersonaEditarInput personaInput)
        {
            Persona personaEditar = _personaRepositorio.GetWithTipoProfesion(personaInput.Id);
            return Mapper.Map<GetPersonaEditarOutput>(personaEditar);
        }

        //  Metodo para obtener la lista de tipos de documento segun el pais y la naturaleza de la empresa
        public GetTiposDocumentoOrganizacionOutput GetTiposDocumentoOrganizacion(GetTiposDocumentoOrganizacionInput tiposDocumentoOrganizacionInput)
        {
            var listaTiposDocumento = new object();
            GetTipoJuridicaOutput tipoNaturalezaJuridica = _parametrosService.GetTipoJuridica();
            if (tiposDocumentoOrganizacionInput.NaturalezaEmpresa == tipoNaturalezaJuridica.Id)
            {
                listaTiposDocumento = _tipoDocumentoPersonaRepositorio.GetAllList().Where(p => p.PaisId == tiposDocumentoOrganizacionInput.PaisId && p.AplicaEmpresa == "1").OrderBy(p => p.Nombre);
            }
            else
            {
                listaTiposDocumento = _tipoDocumentoPersonaRepositorio.GetAllList().Where(p => p.PaisId == tiposDocumentoOrganizacionInput.PaisId && p.AplicaPersona == "1").OrderBy(p => p.Nombre);
            }
            return new GetTiposDocumentoOrganizacionOutput { TiposDocumento = Mapper.Map<List<TipoDocumentoOutput>>(listaTiposDocumento) };
        }

        //  Metodo para validar si un documento es válido
        public ValidarTipoDocumentoOutput ValidarTipoDocumentoPersona(ValidarTipoDocumentoInput datosDocumento)
        {
            TipoDocumentoPersona documento = _tipoDocumentoPersonaRepositorio.Get(Convert.ToInt32(datosDocumento.TipoDocumentoId));
            ValidarTipoDocumentoOutput respuestaValidacion = new ValidarTipoDocumentoOutput();

            respuestaValidacion.TipoDocumentoValido = true;
            if (datosDocumento.NumeroDocumento.Length < documento.LongitudMinima || datosDocumento.NumeroDocumento.Length > documento.LongitudMaxima)
            {
                respuestaValidacion.TipoDocumentoValido = false;
                respuestaValidacion.Mensaje = LocalizationHelper.GetString("Bow", "personas_nuevapersona_numerodocumentoErrorTamano") + documento.LongitudMinima.ToString() + " " + LocalizationHelper.GetString("Bow", "personas_nuevapersona_numerodocumentoErrorTamanoConcatenar") + " " + documento.LongitudMaxima.ToString();
            }
            else
            {
                try
                {
                    if (documento.ConjuntoCaracteres == "N")
                    {
                        var numerodoc = Convert.ToInt32(datosDocumento.NumeroDocumento);
                    }
                }
                catch
                {
                    respuestaValidacion.TipoDocumentoValido = false;
                    respuestaValidacion.Mensaje = LocalizationHelper.GetString("Bow", "personas_nuevapersona_validarNumeroDocumento");
                }
            }
            return respuestaValidacion;
        }

        //  Metodo para validar si un documento es válido y si está en el rango de edad
        public ValidarTipoDocumentoConEdadOutput ValidarTipoDocumentoPersonaConEdad(ValidarTipoDocumentoConEdadInput datosDocumento)
        {
            TipoDocumentoPersona documento = _tipoDocumentoPersonaRepositorio.Get(Convert.ToInt32(datosDocumento.TipoDocumentoId));
            ValidarTipoDocumentoConEdadOutput respuestaValidacion = new ValidarTipoDocumentoConEdadOutput();

            respuestaValidacion.TipoDocumentoValido = true;
            if (datosDocumento.NumeroDocumento.Length < documento.LongitudMinima || datosDocumento.NumeroDocumento.Length > documento.LongitudMaxima)
            {
                respuestaValidacion.TipoDocumentoValido = false;
                respuestaValidacion.Mensaje = LocalizationHelper.GetString("Bow", "personas_nuevapersona_numerodocumentoErrorTamano") + documento.LongitudMinima.ToString() + " " + LocalizationHelper.GetString("Bow", "personas_nuevapersona_numerodocumentoErrorTamanoConcatenar") + " " + documento.LongitudMaxima.ToString();
            }
            else if (datosDocumento.Edad < documento.EdadMinima || datosDocumento.Edad > documento.EdadMaxima)
            {
                respuestaValidacion.TipoDocumentoValido = false;
                respuestaValidacion.Mensaje = LocalizationHelper.GetString("Bow", "personas_nuevapersona_edadError") + documento.EdadMinima.ToString() + " " + LocalizationHelper.GetString("Bow", "personas_nuevapersona_numerodocumentoErrorTamanoConcatenar") + " " + documento.EdadMaxima.ToString();
            }
            else if (datosDocumento.Edad > BowConsts.EDAD_MAXIMA)
            {
                respuestaValidacion.TipoDocumentoValido = false;
                respuestaValidacion.Mensaje = LocalizationHelper.GetString("Bow", "personas_nuevapersona_edadSuperiorError");
            }
            else
            {
                try
                {
                    if (documento.ConjuntoCaracteres == "N")
                    {
                        var numerodoc = Convert.ToInt32(datosDocumento.NumeroDocumento);
                    }
                }
                catch
                {

                    respuestaValidacion.TipoDocumentoValido = false;
                    respuestaValidacion.Mensaje = LocalizationHelper.GetString("Bow", "personas_nuevapersona_validarNumeroDocumento");
                }
            }
            return respuestaValidacion;
        }

        //  Metodo para validar si un documento existe
        public ValidarDocumentoExisteOutput ValidarDocumentoExiste(ValidarDocumentoExisteInput datosDocumento)
        {
            Persona existePersona = _personaRepositorio.FirstOrDefault(b => b.NumeroDocumento.ToLower() == datosDocumento.NumeroDocumento.ToLower() && b.TipoDocumentoId == datosDocumento.TipoDocumentoId);
            ValidarDocumentoExisteOutput respuesta = new ValidarDocumentoExisteOutput();
            if (existePersona != null)
            {
                respuesta.Id = existePersona.Id;
                respuesta.DocumentoExiste = true;
            }
            else
            {
                respuesta.DocumentoExiste = false;
            }
            return respuesta;
        }

        public GetTelefonosPersonaOutput GetTelefonosPersona(GetTelefonosPersonaInput personaInput)
        {
            List<PersonaTelefono> listaTelefonosPersona = _personaTelefonoRepositorio.GetWithTelefonoWithLocalidadAndEstadoByPersona(personaInput.PersonaId).ToList();
            return new GetTelefonosPersonaOutput { TelefonosPersona = Mapper.Map<List<TelefonoPersonaOutput>>(listaTelefonosPersona) };
        }

        public void SavePersonaTelefono(SavePersonaTelefonoInput personatelefonoInput)
        {
            if (personatelefonoInput.PersonaTelefonos != null)
            {
                GetEstadosTipoUbicacionOutput parametroEstadoTelefono = _parametrosService.GetEstadosTipoUbicacion();
                PersonaTelefono telefonosPersona = new PersonaTelefono();

                foreach (PersonaTelefonoInput telefono in personatelefonoInput.PersonaTelefonos)
                {
                    if (telefono.TipoCambio == "N")
                    {
                        if (telefono.FechaCancelacion != null && telefono.NombreEstado == false)
                        {
                            telefono.EstadoId = parametroEstadoTelefono.EstadoInactivoId;
                        }
                        else
                        {
                            telefono.EstadoId = parametroEstadoTelefono.EstadoActivoId;
                        }

                        telefono.FechaIngreso = DateTime.Now;
                        telefono.PersonaId = personatelefonoInput.PersonaId;

                        telefonosPersona = Mapper.Map<PersonaTelefono>(telefono);
                        _personaTelefonoRepositorio.Insert(telefonosPersona);

                    }
                    else if (telefono.TipoCambio == "M")
                    {
                        var personaTelefono = _personaTelefonoRepositorio.Get(telefono.Id);

                        if (telefono.FechaCancelacion != null && telefono.NombreEstado == false)
                        {
                            personaTelefono.FechaCancelacion = telefono.FechaCancelacion;
                            personaTelefono.EstadoId = parametroEstadoTelefono.EstadoInactivoId;
                        }

                        personaTelefono.PersonaId = telefono.PersonaId;
                        personaTelefono.TelefonoId = telefono.TelefonoId;
                        personaTelefono.TipoUbicacionId = telefono.TipoUbicacionId;

                        _personaTelefonoRepositorio.Update(personaTelefono);

                    }
                }
            }
        }

        public GetDireccionesPersonaOutput GetDireccionesPersona(GetDireccionesPersonaInput personaInput)
        {
            List<PersonaDireccion> listaDireccionesPersona = _personaDireccionRepositorio.GetWithDireccionWithLocalidadAndEstadoByPersona(personaInput.PersonaId).ToList();
            return new GetDireccionesPersonaOutput { DireccionesPersona = Mapper.Map<List<DireccionPersonaOutput>>(listaDireccionesPersona) };
        }

        public void SavePersonaDireccion(SavePersonaDireccionInput personadireccionInput)
        {
            if (personadireccionInput.PersonaDirecciones != null)
            {
                GetEstadosTipoUbicacionOutput parametroEstadoDireccion = _parametrosService.GetEstadosTipoUbicacion();
                PersonaDireccion direccionesPersona = new PersonaDireccion();


                foreach (PersonaDireccionInput direccion in personadireccionInput.PersonaDirecciones)
                {
                    if (direccion.TipoCambio == "N")
                    {
                        if (direccion.FechaCancelacion != null && direccion.NombreEstado == false)
                        {
                            direccion.EstadoId = parametroEstadoDireccion.EstadoInactivoId;
                        }
                        else
                        {
                            direccion.EstadoId = parametroEstadoDireccion.EstadoActivoId;
                        }

                        direccion.FechaIngreso = DateTime.Now;
                        direccion.PersonaId = personadireccionInput.PersonaId;

                        direccionesPersona = Mapper.Map<PersonaDireccion>(direccion);
                        _personaDireccionRepositorio.Insert(direccionesPersona);

                    }
                    else if (direccion.TipoCambio == "M")
                    {
                        var personaDireccion = _personaDireccionRepositorio.Get(direccion.Id);

                        if (direccion.FechaCancelacion != null && direccion.NombreEstado == false)
                        {
                            personaDireccion.FechaCancelacion = direccion.FechaCancelacion;
                            personaDireccion.EstadoId = parametroEstadoDireccion.EstadoInactivoId;
                        }

                        personaDireccion.PersonaId = direccion.PersonaId;
                        personaDireccion.DireccionId = direccion.DireccionId;
                        personaDireccion.TipoUbicacionId = direccion.TipoUbicacionId;

                        _personaDireccionRepositorio.Update(personaDireccion);

                    }
                }

            }
        }

        public GetPersonaContactoWebOutput GetPersonaContactoWeb(GetPersonaContactoWebInput personaInput)
        {
            var listaMediosContactoPersona = _personaContactoWebRepositorio.GetWithTipoMedioContacto(personaInput.PersonaId);
            return new GetPersonaContactoWebOutput { MediosContactoPersona = Mapper.Map<List<PersonaContactoWebOutput>>(listaMediosContactoPersona) };
        }

        public GetContactosWebFilterByPersonaOutput GetContactosWebFilterByPersona(GetContactosWebFilterByPersonaInput personaInput)
        {
            var listaContactosYaAsignados = _personaContactoWebRepositorio.GetWithTipoMedioContacto(personaInput.PersonaId);

            var listaContactosWebFiltrados = _parametrosService.GetAllTiposContactoWebWithFilterPersona(new GetAllTiposContactoWebWithFilterPersonaInput { Tipos = Mapper.Map<List<TipoInput>>(listaContactosYaAsignados) }).Tipos;

            return new GetContactosWebFilterByPersonaOutput { ContactosWeb = Mapper.Map<List<TipoContactoWebPersonaOutput>>(listaContactosWebFiltrados) };
        }

        public void SavePersonaContactoWeb(SavePersonaContactoWebInput personacontactowebInput)
        {
            if (personacontactowebInput.PersonaContactoWeb != null)
            {
                PersonaContactoWeb contactowebPersona = new PersonaContactoWeb();

                for (int i = 0; i < personacontactowebInput.PersonaContactoWeb.Count(); i++)
                {
                    if (personacontactowebInput.PersonaContactoWeb[i].TipoCambio == "N")
                    {
                        contactowebPersona = Mapper.Map<PersonaContactoWeb>(personacontactowebInput.PersonaContactoWeb[i]);
                        _personaContactoWebRepositorio.Insert(contactowebPersona);
                    }
                    else if (personacontactowebInput.PersonaContactoWeb[i].TipoCambio == "M")
                    {
                        contactowebPersona = Mapper.Map<PersonaContactoWeb>(personacontactowebInput.PersonaContactoWeb[i]);
                        _personaContactoWebRepositorio.Update(contactowebPersona);
                    }
                    else if (personacontactowebInput.PersonaContactoWeb[i].TipoCambio == "E")
                    {
                        _personaContactoWebRepositorio.Delete(personacontactowebInput.PersonaContactoWeb[i].Id);
                    }
                }
            }
        }

        //******************************* Personas Preferencia **************************
        //Retorna las preferencias registradas en el sistema
        public GetPreferenciaPersonaOutput GetPreferenciaPersona()
        {
            var preferenciaEstadoActivo = _parametrosService.GetEstadoActivoPreferencia();

            return new GetPreferenciaPersonaOutput { Preferencias = Mapper.Map<List<PreferenciaPersonaOutput>>(_preferenciaRepositorio.GetAllListWithOpcionPreferenciaByPreferencia().Where(pr => pr.EstadoId == preferenciaEstadoActivo.Id).ToList()) };
        }

        //Retorna las opciones de las preferencias de la persona indicada
        public GetOpcionPreferenciaPersonaOutput GetOpcionPreferenciaPersona(GetOpcionPreferenciaPersonaInput personaInput)
        {
            var personaPreferencias = _personaPreferenciaRepositorio.GetWithPreferencia(personaInput.PersonaId).ToList();
            return new GetOpcionPreferenciaPersonaOutput { OpcionPreferenciaPersona = Mapper.Map<List<OpcionPreferenciaPersonaOutput>>(personaPreferencias) };
        }

        public void SavePersonaPreferencia(SavePersonaPreferenciaInput preferenciasPersona)
        {
            PersonaPreferencia opcion = new PersonaPreferencia();

            if (preferenciasPersona.PreferenciasPersona != null)
            {
                for (int i = 0; i < preferenciasPersona.PreferenciasPersona.Count(); i++)
                {
                    if (preferenciasPersona.PreferenciasPersona[i].TipoCambio == "N")
                    {
                        opcion = Mapper.Map<PersonaPreferencia>(preferenciasPersona.PreferenciasPersona[i]);
                        _personaPreferenciaRepositorio.Insert(opcion);
                    }
                    else if (preferenciasPersona.PreferenciasPersona[i].TipoCambio == "M")
                    {
                        PersonaPreferencia actualizar = _personaPreferenciaRepositorio.Get(preferenciasPersona.PreferenciasPersona[i].Id);

                        actualizar.PersonaId = preferenciasPersona.PreferenciasPersona[i].PersonaId;
                        actualizar.OpcionPreferenciaId = preferenciasPersona.PreferenciasPersona[i].OpcionPreferenciaId;

                        _personaPreferenciaRepositorio.Update(actualizar);
                    }
                }
            }
        }

        public GetPersonaWithTelefonoOutput GetPersonaWithTelefono(GetPersonaWithTelefonoInput personaDocumentoInput)
        {
            Persona persona = _personaRepositorio.GetAll().Where(p => p.NumeroDocumento == personaDocumentoInput.Documento).FirstOrDefault();
            GetPersonaWithTelefonoOutput personaReturn = new GetPersonaWithTelefonoOutput();
            if (persona != null)
            {
                personaReturn.Id = persona.Id;
                personaReturn.NombreCompleto = persona.nombreCompleto;
                personaReturn.Documento = persona.NumeroDocumento;
                personaReturn.Telefonos = Mapper.Map<List<PersonaTelefonoOutput>>(_personaTelefonoRepositorio.GetWithTelefonoWithLocalidadAndEstadoByPersona(persona.Id));
            }
            return personaReturn;
        }

        public void SavePersonaAuditoria(PersonaOriginalAuditoriaInput personaOriginal, PersonaEditadaAuditoriaInput personaEditada)
        {
            List<Auditoria> datosAuditoria = new List<Auditoria>();

            PersonaAuditoria savePersonaAuditoria = new PersonaAuditoria();

            if (personaOriginal.TieneDocumento != personaEditada.TieneDocumento)
            {
                if (personaOriginal.TieneDocumento == "True")
                {
                    personaOriginal.TieneDocumento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneDocumentoSi");
                }
                else
                {
                    personaOriginal.TieneDocumento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneDocumentoNo");
                }

                if (personaEditada.TieneDocumento == "True")
                {
                    personaEditada.TieneDocumento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneDocumentoSi");
                }
                else
                {
                    personaEditada.TieneDocumento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneDocumentoNo");
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneDocumento"), ValorAnterior = personaOriginal.TieneDocumento, ValorNuevo = personaEditada.TieneDocumento });
            }

            if (personaOriginal.TipoDocumentoId != personaEditada.TipoDocumentoId)
            {
                var documentoOriginal = _tipoDocumentoPersonaRepositorio.FirstOrDefault(t => t.Id == personaOriginal.TipoDocumentoId);
                var documentoEditado = _tipoDocumentoPersonaRepositorio.FirstOrDefault(t => t.Id == personaEditada.TipoDocumentoId);

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tipoDocumento"), ValorAnterior = documentoOriginal.Nombre, ValorNuevo = documentoEditado.Nombre });
            }

            if (personaOriginal.NumeroDocumento != personaEditada.NumeroDocumento)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_numeroDocumento"), ValorAnterior = personaOriginal.NumeroDocumento, ValorNuevo = personaEditada.NumeroDocumento });
            }

            if (personaOriginal.Nombre != personaEditada.Nombre)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_nombre"), ValorAnterior = personaOriginal.Nombre, ValorNuevo = personaEditada.Nombre });
            }

            if (personaOriginal.Apellido1 != personaEditada.Apellido1)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_primerApellido"), ValorAnterior = personaOriginal.Apellido1, ValorNuevo = personaEditada.Apellido1 });
            }

            if (personaOriginal.Apellido2 != personaEditada.Apellido2)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_segundoApellido"), ValorAnterior = personaOriginal.Apellido2, ValorNuevo = personaEditada.Apellido2 });
            }

            if (personaOriginal.TieneFechaNacimiento != personaEditada.TieneFechaNacimiento)
            {
                if (personaOriginal.TieneFechaNacimiento == "True")
                {
                    personaOriginal.TieneFechaNacimiento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneFechaNacimientoSi");
                }
                else
                {
                    personaOriginal.TieneFechaNacimiento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneFechaNacimientoNo"); ;
                }

                if (personaEditada.TieneFechaNacimiento == "True")
                {
                    personaEditada.TieneFechaNacimiento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneFechaNacimientoSi"); ;
                }
                else
                {
                    personaEditada.TieneFechaNacimiento = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneFechaNacimientoNo"); ;
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tieneFechaNacimiento"), ValorAnterior = personaOriginal.TieneFechaNacimiento, ValorNuevo = personaEditada.TieneFechaNacimiento });
            }

            if (personaOriginal.FechaNacimiento != personaEditada.FechaNacimiento)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_fechaNacimiento"), ValorAnterior = Convert.ToString(personaOriginal.FechaNacimiento), ValorNuevo = Convert.ToString(personaEditada.FechaNacimiento) });
            }

            if (personaOriginal.Genero != personaEditada.Genero)
            {
                if (personaOriginal.Genero == "M")
                {
                    personaOriginal.Genero = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_generoMasculino");
                }
                else
                {
                    personaOriginal.Genero = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_generoFemenino");
                }

                if (personaEditada.Genero == "M")
                {
                    personaEditada.Genero = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_generoMasculino");
                }
                else
                {
                    personaEditada.Genero = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_generoFemenino");
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_genero"), ValorAnterior = personaOriginal.Genero, ValorNuevo = personaEditada.Genero });
            }

            if (personaOriginal.CorreoElectronico != personaEditada.CorreoElectronico)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_coreoElectronico"), ValorAnterior = personaOriginal.CorreoElectronico, ValorNuevo = personaEditada.CorreoElectronico });
            }

            if (personaOriginal.ContactarSms != personaEditada.ContactarSms)
            {
                if (personaOriginal.ContactarSms == "True")
                {
                    personaOriginal.ContactarSms = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarSmsSi");
                }
                else
                {
                    personaOriginal.ContactarSms = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarSmsNo");
                }

                if (personaEditada.ContactarSms == "True")
                {
                    personaEditada.ContactarSms = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarSmsSi");
                }
                else
                {
                    personaEditada.ContactarSms = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarSmsNo");
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarSms"), ValorAnterior = personaOriginal.ContactarSms, ValorNuevo = personaEditada.ContactarSms });
            }

            if (personaOriginal.ContactarCorreo != personaEditada.ContactarCorreo)
            {
                if (personaOriginal.ContactarCorreo == "True")
                {
                    personaOriginal.ContactarCorreo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarCorreoSi");
                }
                else
                {
                    personaOriginal.ContactarCorreo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarCorreoNo");
                }

                if (personaEditada.ContactarCorreo == "True")
                {
                    personaEditada.ContactarCorreo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarCorreoSi");
                }
                else
                {
                    personaEditada.ContactarCorreo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarCorreoNo");
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarCorreo"), ValorAnterior = personaOriginal.ContactarCorreo, ValorNuevo = personaEditada.ContactarCorreo });
            }

            if (personaOriginal.ContactarTelefono != personaEditada.ContactarTelefono)
            {
                if (personaOriginal.ContactarTelefono == "True")
                {
                    personaOriginal.ContactarTelefono = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarTelefonoSi");
                }
                else
                {
                    personaOriginal.ContactarTelefono = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarTelefonoNo");
                }

                if (personaEditada.ContactarTelefono == "True")
                {
                    personaEditada.ContactarTelefono = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarTelefonoSi");
                }
                else
                {
                    personaEditada.ContactarTelefono = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarTelefonoNo");
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_contactarTelefono"), ValorAnterior = personaOriginal.ContactarTelefono, ValorNuevo = personaEditada.ContactarTelefono });
            }

            if (personaOriginal.TipoProfesionId != personaEditada.TipoProfesionId)
            {
                GetTipoInput profesionOriginalInput = new GetTipoInput();
                profesionOriginalInput.Id = personaOriginal.TipoProfesionId;
                var profesionOriginal = _parametrosService.GetTipo(profesionOriginalInput);

                GetTipoInput profesionEditadaInput = new GetTipoInput();
                profesionEditadaInput.Id = personaEditada.TipoProfesionId;
                var profesionEditada = _parametrosService.GetTipo(profesionEditadaInput);

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_tipoProfesion"), ValorAnterior = profesionOriginal.Nombre, ValorNuevo = profesionEditada.Nombre });
            }

            if (personaOriginal.TipoEstadoCivilId != personaEditada.TipoEstadoCivilId)
            {
                GetTipoInput estadocivilOriginalInput = new GetTipoInput();
                estadocivilOriginalInput.Id = personaOriginal.TipoEstadoCivilId;
                var estadoCivilOriginal = _parametrosService.GetTipo(estadocivilOriginalInput);

                GetTipoInput estadoCivilEditadaInput = new GetTipoInput();
                estadoCivilEditadaInput.Id = personaEditada.TipoEstadoCivilId;
                var estadoCivilEditada = _parametrosService.GetTipo(estadoCivilEditadaInput);

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_estadoCivil"), ValorAnterior = estadoCivilOriginal.Nombre, ValorNuevo = estadoCivilEditada.Nombre });
            }

            if (personaOriginal.Discapacitada != personaEditada.Discapacitada)
            {
                if (personaOriginal.Discapacitada == "True")
                {
                    personaOriginal.Discapacitada = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_discapacitadaSi");
                }
                else
                {
                    personaOriginal.Discapacitada = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_discapacitadaNo");
                }

                if (personaEditada.Discapacitada == "True")
                {
                    personaEditada.Discapacitada = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_discapacitadaSi");
                }
                else
                {
                    personaEditada.Discapacitada = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_discapacitadaNo");
                }

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_discapacitada"), ValorAnterior = personaOriginal.Discapacitada, ValorNuevo = personaEditada.Discapacitada });
            }

            if (personaOriginal.FechaFallecimiento != personaEditada.FechaFallecimiento)
            {
                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_fechaFallecimiento"), ValorAnterior = Convert.ToString(personaOriginal.FechaFallecimiento), ValorNuevo = Convert.ToString(personaEditada.FechaFallecimiento) });
            }

            if (personaOriginal.PaisId != personaEditada.PaisId)
            {
                EsPaisUsaInput paisOriginalInput = new EsPaisUsaInput();
                paisOriginalInput.Id = personaOriginal.PaisId;
                var paisOriginal = _zonificacionService.GetPais(paisOriginalInput);

                EsPaisUsaInput paisEditadaInput = new EsPaisUsaInput();
                paisEditadaInput.Id = personaEditada.PaisId;
                var paisEditada = _zonificacionService.GetPais(paisEditadaInput);

                datosAuditoria.Add(new Auditoria { Campo = LocalizationHelper.GetString("Bow", "personas_nuevapersona_modalNuevaPersona_tabAuditoria_pais"), ValorAnterior = paisOriginal.Nombre, ValorNuevo = paisEditada.Nombre });
            }

            if (datosAuditoria.Count != 0)
            {
                savePersonaAuditoria.FechaCambio = DateTime.Now;
                savePersonaAuditoria.PersonaId = personaEditada.Id;

                savePersonaAuditoria.Cambios = JsonConvert.SerializeObject(datosAuditoria);
                _personaAuditoriaRepositorio.Insert(savePersonaAuditoria);
            }
        }

        //Retorna la Auditoria de la persona indicada
        public GetAuditoriaPersonaOutput GetAuditoriaPersona(GetAuditoriaPersonaInput personaInput)
        {
            GetAuditoriaPersonaOutput listaPersonaAuditorias = new GetAuditoriaPersonaOutput();

            listaPersonaAuditorias.AuditoriaPersona = Mapper.Map<List<AuditoriaPersonaOutput>>(_personaAuditoriaRepositorio.GetAllList().Where(p => p.PersonaId == personaInput.PersonaId).ToList());

            if (listaPersonaAuditorias.AuditoriaPersona.Count() != 0)
            {
                List<Auditoria> lista = new List<Auditoria>();

                for (int i = 0; i < listaPersonaAuditorias.AuditoriaPersona.Count(); i++)
                {
                    lista = JsonConvert.DeserializeObject<List<Auditoria>>(listaPersonaAuditorias.AuditoriaPersona[i].Cambios);
                    listaPersonaAuditorias.AuditoriaPersona[i].AuditoriaCambios = lista;
                }
            }

            return listaPersonaAuditorias;
        }


        //****************** Metodos relacionados con empleados **************************

        public GetPersonaOutput GetPersona(GetPersonaInput personaInput)
        {
            var persona = _personaRepositorio.FirstOrDefault(p => p.NumeroDocumento == personaInput.NumeroDocumento);
            return Mapper.Map<GetPersonaOutput>(persona);
        }

        //Método para calcular el rango de la fecha de asignacion del empleado a una zona (ZonaEmpleado) 
        public FechaAsignacionOutput FechaAsignacion(FechaAsignacionInput personaInput)
        {
            var fechaIngresoPersona = _personaRepositorio.FirstOrDefault(p => p.Id == personaInput.PersonaId);

            DateTime fechaActual = DateTime.Now;

            FechaAsignacionOutput fechaAsignacion = new FechaAsignacionOutput();

            fechaAsignacion.FechaMinimaAsignacion = fechaIngresoPersona.FechaIngreso;
            fechaAsignacion.FechaMaximaAsignacion = fechaActual;

            return fechaAsignacion;
        }

        //Metodo para la directiva de buscador de persona
        public GetBuscadorPersonaOutput GetBuscadorPersona(GetBuscadorPersonaInput datosInput)
        {
            var tipoUbicacionResidencial = _parametrosService.GetTipoUbicacionResidencial().Id;

            //Consulta linq con leftjoin y distinct
            var resultado = (from persona in _personaRepositorio.GetAll()
                             join personaTelefono in _personaTelefonoRepositorio.GetAll() on persona.Id equals personaTelefono.PersonaId into pertel
                             from pt in pertel.DefaultIfEmpty().Distinct()
                             join telefono in _telefonoRepositorio.GetAll() on pt.TelefonoId equals telefono.Id into tel
                             from t in tel.DefaultIfEmpty()
                             join personaDireccion in _personaDireccionRepositorio.GetAll() on persona.Id equals personaDireccion.PersonaId into perdir
                             from pd in perdir.Where(s => s.TipoUbicacionId.Equals(tipoUbicacionResidencial)).DefaultIfEmpty()
                             join direccion in _direccionRepositorio.GetAll() on pd.DireccionId equals direccion.Id into dir
                             from d in dir.DefaultIfEmpty()
                             join barrio in _barrioRepositorio.GetAll() on d.BarrioId equals barrio.Id into barr
                             from b in barr.DefaultIfEmpty()
                             join localidad in _localidadRepositorio.GetAll() on b.LocalidadId equals localidad.Id into loc
                             from l in loc.DefaultIfEmpty()
                             join departamento in _departamentoRepositorio.GetAll() on l.DepartamentoId equals departamento.Id into dep
                             from de in dep.DefaultIfEmpty()
                             where ((string.IsNullOrEmpty(datosInput.Nombre) || (persona.Nombre.Contains(datosInput.Nombre)))
                             && ((string.IsNullOrEmpty(datosInput.Apellido1)) || (persona.Apellido1.Contains(datosInput.Apellido1)))
                             && ((string.IsNullOrEmpty(datosInput.Apellido2)) || (persona.Apellido2.Contains(datosInput.Apellido2)))
                             && ((string.IsNullOrEmpty(datosInput.Documento)) || (persona.NumeroDocumento.Contains(datosInput.Documento)))
                             && ((string.IsNullOrEmpty(datosInput.CorreoElectronico)) || (persona.CorreoElectronico.Contains(datosInput.CorreoElectronico)))
                             && ((string.IsNullOrEmpty(datosInput.Telefono)) || (t.Numero.Contains(datosInput.Telefono)))
                             && ((string.IsNullOrEmpty(datosInput.ZipCode)) || (d.ZipCode.Contains(datosInput.ZipCode))))

                             select new BuscadorPersonaOutput
                             {
                                 Id = persona.Id,
                                 TieneDocumento = persona.TieneDocumento,
                                 TipoDocumentoId = persona.TipoDocumentoId.ToString(),
                                 NumeroDocumento = persona.NumeroDocumento,
                                 FechaExpDocumento = persona.FechaExpDocumento.ToString(),
                                 Nombre = persona.Nombre,
                                 Apellido1 = persona.Apellido1,
                                 Apellido2 = persona.Apellido2,
                                 TieneFechaNacimiento = persona.TieneFechaNacimiento,
                                 FechaNacimiento = persona.FechaNacimiento.ToString(),
                                 Genero = persona.Genero,
                                 CorreoElectronico = persona.CorreoElectronico,
                                 ContactarCorreo = persona.ContactarCorreo,
                                 ContactarSms = persona.ContactarSms,
                                 ContactarTelefono = persona.ContactarTelefono,
                                 FechaIngreso = persona.FechaIngreso.ToString(),
                                 TipoProfesionId = persona.TipoProfesionId,
                                 TipoEstadoCivilId = persona.TipoEstadoCivilId,
                                 Discapacitada = persona.Discapacitada,
                                 FechaFallecimiento = persona.FechaFallecimiento.ToString(),
                                 PaisId = persona.PaisId,
                                 NombreCompleto = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2,
                                 LocalidadDepartamento = de.Nombre == null ? null : de.Nombre + " - " + l.Nombre,
                             }).Distinct().Take(50).ToList();

            return new GetBuscadorPersonaOutput { Personas = Mapper.Map<List<BuscadorPersonaOutput>>(resultado) };

        }

        public SavePersonaProspectoOutput SavePersonaProspecto(SavePersonaProspectoInput personaProspectoInput)
        {
            SavePersonaProspectoOutput personaProspecto = new SavePersonaProspectoOutput();

            if (personaProspectoInput.Id == 0)
            {
                var estadoCivilNoIdentificado = _parametrosService.GetTiposByParametroEstadoCivilWithIdNoIdentificado();
                var profesionNoIdentificado = _parametrosService.GetTiposByParametroProfesionesWithIdNoIdentificado();

                personaProspectoInput.TipoEstadoCivilId = estadoCivilNoIdentificado.IdNoIdentificado;
                personaProspectoInput.TipoProfesionId = profesionNoIdentificado.IdNoIdentificado;
                personaProspectoInput.Genero = "F";
                personaProspectoInput.FechaIngreso = DateTime.Now;
                personaProspectoInput.FechaUltActualizacion = DateTime.Now;

                personaProspecto = Mapper.Map<SavePersonaProspectoOutput>(personaProspectoInput);

                //Paso para guardar el tenantId 
                Persona pers = new Persona();
                pers = Mapper.Map<Persona>(personaProspectoInput);
                pers.TenantId = AbpSession.TenantId.Value;

                personaProspecto.Id = _personaRepositorio.InsertAndGetId(Mapper.Map<Persona>(pers));
                //personaProspecto.Id = _personaRepositorio.InsertAndGetId(Mapper.Map<Persona>(personaProspectoInput));

            }
            else
            {
                Persona personaEditar = _personaRepositorio.Get(personaProspectoInput.Id);

                personaEditar.Nombre = personaProspectoInput.Nombre;
                personaEditar.Apellido1 = personaProspectoInput.Apellido1;
                personaEditar.Apellido2 = personaProspectoInput.Apellido2;
                personaEditar.FechaNacimiento = personaProspectoInput.FechaNacimiento;
                personaEditar.FechaUltActualizacion = DateTime.Now;

                personaProspecto = Mapper.Map<SavePersonaProspectoOutput>(personaProspectoInput);
                personaProspecto.Id = personaProspectoInput.Id;

                _personaRepositorio.Update(personaEditar);
            }

            return personaProspecto;
        }

        //Metodo para la directiva de buscador de persona
        public GetBuscadorPersonaGrupoInformalOutput GetBuscadorPersonaGrupoInformal(GetBuscadorPersonaGrupoInformalInput datosInput)
        {
            var tipoUbicacionResidencial = _parametrosService.GetTipoUbicacionResidencial().Id;

            //Consulta linq con leftjoin y distinct
            var resultado = (from persona in _personaRepositorio.GetAll()
                             join personaTelefono in _personaTelefonoRepositorio.GetAll() on persona.Id equals personaTelefono.PersonaId into pertel
                             from pt in pertel.DefaultIfEmpty().Distinct()
                             join telefono in _telefonoRepositorio.GetAll() on pt.TelefonoId equals telefono.Id into tel
                             from t in tel.DefaultIfEmpty()
                             join personaDireccion in _personaDireccionRepositorio.GetAll() on persona.Id equals personaDireccion.PersonaId into perdir
                             from pd in perdir.Where(s => s.TipoUbicacionId.Equals(tipoUbicacionResidencial)).DefaultIfEmpty()
                             join direccion in _direccionRepositorio.GetAll() on pd.DireccionId equals direccion.Id into dir
                             from d in dir.DefaultIfEmpty()
                             join barrio in _barrioRepositorio.GetAll() on d.BarrioId equals barrio.Id into barr
                             from b in barr.DefaultIfEmpty()
                             join localidad in _localidadRepositorio.GetAll() on b.LocalidadId equals localidad.Id into loc
                             from l in loc.DefaultIfEmpty()
                             join departamento in _departamentoRepositorio.GetAll() on l.DepartamentoId equals departamento.Id into dep
                             from de in dep.DefaultIfEmpty()
                             join grupoInformal in _grupoInformalRepositorio.GetAll() on persona.Id equals grupoInformal.PersonaId into perGrInf
                             from pgi in perGrInf
                             where ((string.IsNullOrEmpty(datosInput.Nombre) || (persona.Nombre.Contains(datosInput.Nombre)))
                             && ((string.IsNullOrEmpty(datosInput.Apellido1)) || (persona.Apellido1.Contains(datosInput.Apellido1)))
                             && ((string.IsNullOrEmpty(datosInput.Apellido2)) || (persona.Apellido2.Contains(datosInput.Apellido2)))
                             && ((string.IsNullOrEmpty(datosInput.Documento)) || (persona.NumeroDocumento.Contains(datosInput.Documento)))
                             && ((string.IsNullOrEmpty(datosInput.CorreoElectronico)) || (persona.CorreoElectronico.Contains(datosInput.CorreoElectronico)))
                             && ((string.IsNullOrEmpty(datosInput.Telefono)) || (t.Numero.Contains(datosInput.Telefono)))
                             && ((string.IsNullOrEmpty(datosInput.ZipCode)) || (d.ZipCode.Contains(datosInput.ZipCode))))

                             select new BuscadorPersonaOutput
                             {
                                 Id = persona.Id,
                                 TieneDocumento = persona.TieneDocumento,
                                 TipoDocumentoId = persona.TipoDocumentoId.ToString(),
                                 NumeroDocumento = persona.NumeroDocumento,
                                 FechaExpDocumento = persona.FechaExpDocumento.ToString(),
                                 Nombre = persona.Nombre,
                                 Apellido1 = persona.Apellido1,
                                 Apellido2 = persona.Apellido2,
                                 TieneFechaNacimiento = persona.TieneFechaNacimiento,
                                 FechaNacimiento = persona.FechaNacimiento.ToString(),
                                 Genero = persona.Genero,
                                 CorreoElectronico = persona.CorreoElectronico,
                                 ContactarCorreo = persona.ContactarCorreo,
                                 ContactarSms = persona.ContactarSms,
                                 ContactarTelefono = persona.ContactarTelefono,
                                 FechaIngreso = persona.FechaIngreso.ToString(),
                                 TipoProfesionId = persona.TipoProfesionId,
                                 TipoEstadoCivilId = persona.TipoEstadoCivilId,
                                 Discapacitada = persona.Discapacitada,
                                 FechaFallecimiento = persona.FechaFallecimiento.ToString(),
                                 PaisId = persona.PaisId,
                                 NombreCompleto = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2,
                                 LocalidadDepartamento = de.Nombre == null ? null : de.Nombre + " - " + l.Nombre,
                             }).Distinct().Take(50).ToList();

            return new GetBuscadorPersonaGrupoInformalOutput { Personas = Mapper.Map<List<BuscadorPersonaOutput>>(resultado) };

        }
    }
}

