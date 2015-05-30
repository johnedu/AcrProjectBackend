using Abp.Localization;
using Abp.UI;
using AutoMapper;
using Bow.Empresas;
using Bow.Empresas.DTOs.InputModels;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Parametros.Entidades;
using Bow.Parametros.Repositorios;
using Bow.Personas;
using Bow.Personas.DTOs.InputModels;
using Bow.Zonificacion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros
{
    public class ParametrosService : IParametrosService
    {
        #region Repositorios
        private IParametroRepositorio _parametroRepositorio;
        private ITipoRepositorio _tipoRepositorio;
        private IEstadoRepositorio _estadoRepositorio;
        private INombreEstadoRepositorio _nombreestadoRepositorio;

        #endregion

        //Inyección de Dependencia en el Servicio
        public ParametrosService(IParametroRepositorio parametroRepositorio, ITipoRepositorio tipoRepositorio, IEstadoRepositorio estadoRepositorio, INombreEstadoRepositorio nombreestadoRepositorio)
        {
            _parametroRepositorio = parametroRepositorio;
            _tipoRepositorio = tipoRepositorio;
            _estadoRepositorio = estadoRepositorio;
            _nombreestadoRepositorio = nombreestadoRepositorio;
            //_personasService = personasService;
        }

        /***************************************************************************************************
         * 
         * MÉTODOS PRIVADOS
         * 
         **************************************************************************************************/ 


        /********************************* PARAMETROS *************************************************/

        //Metodo para obtener todos los parametros registrados
        public GetParametrosOutput GetParametros()
        {
            var listaParametros = _parametroRepositorio.GetAllList().OrderBy(d => d.Nombre);
            return new GetParametrosOutput { Parametros = Mapper.Map<List<ParametroOutput>>(listaParametros) };
        }

        //Metodo para guardar un parametro.
        public void SaveParametro(SaveParametroInput nuevoParametro)
        {
            nuevoParametro.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoParametro.Nombre.ToLower());
            Parametro existeParametro = _parametroRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevoParametro.Nombre.ToLower());

            if (existeParametro == null)
            {
                _parametroRepositorio.Insert(Mapper.Map<Parametro>(nuevoParametro));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_parametro_validarNombreParametro");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para identificar los datos del parametro para editar
        public GetParametroOutput GetParametro(GetParametroInput parametroInput)
        {
            return Mapper.Map<GetParametroOutput>(_parametroRepositorio.Get(parametroInput.Id));
        }

        //Metodo para actualizar un parametro.
        public void UpdateParametro(UpdateParametroInput parametroUpdate)
        {
            parametroUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(parametroUpdate.Nombre.ToLower());
            Parametro existeParametro = _parametroRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == parametroUpdate.Nombre.ToLower() && d.Id != parametroUpdate.Id);

            if (existeParametro == null)
            {
                var parametroActualizar = _parametroRepositorio.Update(Mapper.Map<Parametro>(parametroUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_parametro_validarNombreParametro");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para eliminar un parametro.
        public void DeleteParametro(DeleteParametroInput parametroEliminar)
        {
            _parametroRepositorio.Delete(parametroEliminar.Id);
        }

        //Metodo para verificar si el parametro indicado se puede eliminar o no del sistema
        public PuedeEliminarParametroOutput PuedeEliminarParametro(DeleteParametroInput parametroEliminar)
        {
            var estadosConsulta = _estadoRepositorio.GetAllList().Where(d => d.ParametroId == parametroEliminar.Id);
            var tiposConsulta = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametroEliminar.Id);

            PuedeEliminarParametroOutput puede = new PuedeEliminarParametroOutput();

            if ((estadosConsulta.Count() == 0) && (tiposConsulta.Count() == 0))
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        ///********************************* TIPOS DE PARAMETROS *************************************************/

        //Metodo para identificar los tipos de un parametro seleccionado - el valor de entrada es un objeto tipo parametro
        public GetTiposOutput GetTipos(GetParametroInput parametroInput)
        {
            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametroInput.Id && d.Nombre != BowConsts.TIPO_ESTADO_CIVIL_NO_IDENTIFICADO).OrderBy(d => d.Nombre);
            return new GetTiposOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
        }

        //Metodo para identificar los datos del tipo de parametro para editar/eliminar
        public GetTipoOutput GetTipo(GetTipoInput tipoInput)
        {
            return Mapper.Map<GetTipoOutput>(_tipoRepositorio.Get(tipoInput.Id));
        }

        //Metodo para guardar un tipo de parametro.
        public void SaveTipo(SaveTipoInput nuevoTipo)
        {
            nuevoTipo.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoTipo.Nombre.ToLower());
            Tipo existeTipo = _tipoRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevoTipo.Nombre.ToLower() && d.ParametroId == nuevoTipo.parametroId);

            if (existeTipo == null)
            {
                _tipoRepositorio.Insert(Mapper.Map<Tipo>(nuevoTipo));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "parametro_tipo_validarNombreTipo");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para actualizar un tipo de parametro.
        public void UpdateTipo(UpdateTipoInput tipoUpdate)
        {
            tipoUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(tipoUpdate.Nombre.ToLower());
            Tipo existeTipo = _tipoRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == tipoUpdate.Nombre.ToLower() && d.Id != tipoUpdate.Id);
            Tipo tipoConsultar = _tipoRepositorio.Get(tipoUpdate.Id);

            if (existeTipo == null)
            {
                tipoConsultar.Nombre = tipoUpdate.Nombre;
                tipoConsultar.Descripcion = tipoUpdate.Descripcion;

                var tipoActualizar = _tipoRepositorio.Update(Mapper.Map<Tipo>(tipoConsultar));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "parametro_tipo_validarNombreTipo");
                throw new UserFriendlyException(mensajeError);
            }
        }

        ////Metodo para eliminar un tipo de parametro.
        public void DeleteTipo(DeleteTipoInput tipoEliminar)
        {
            _tipoRepositorio.Delete(tipoEliminar.Id);
        }


        ///********************************* NOMBRES DE ESTADOS *************************************************/

        //Metodo para guardar un NombreEstado.
        public void SaveNombreEstado(SaveNombreEstadoInput nuevoNombreEstado)
        {
            nuevoNombreEstado.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoNombreEstado.Nombre.ToLower());
            nuevoNombreEstado.Abreviacion = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoNombreEstado.Abreviacion.ToLower());

            NombreEstado existeEstado = _nombreestadoRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevoNombreEstado.Nombre.ToLower() || d.Abreviacion == nuevoNombreEstado.Abreviacion);

            if (existeEstado == null)
            {
                _nombreestadoRepositorio.Insert(Mapper.Map<NombreEstado>(nuevoNombreEstado));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "parametro_nombreestado_validarNombreEstadoNombre");
                throw new UserFriendlyException(mensajeError);
            }
        }


        ///********************************* ESTADOS DE PARAMETROS *************************************************/

        //Metodo para eliminar un estado de un parametro.
        public void DeleteEstado(DeleteEstadoInput estadoEliminar)
        {
            _estadoRepositorio.Delete(estadoEliminar.Id);
        }

        //Metodo para identificar los datos del estado de parametro para editar/eliminar
        public EstadoWithNombreEstadoOutput GetEstado(GetEstadoInput estadoInput)
        {
            return Mapper.Map<EstadoWithNombreEstadoOutput>(_estadoRepositorio.Get(estadoInput.Id));
        }

        //Metodo para consultar el estado a partir del nombre de estado y el nombre del parametro
        public EstadoWithNombreEstadoOutput GetEstadoByNombreEstadoAndNombreParametro(GetEstadoByNombreEstadoAndNombreParametroInput estadoAndParametroInput)
        {
            return Mapper.Map<EstadoWithNombreEstadoOutput>(_estadoRepositorio.GetByNombreEstadoAndNombreParametro(estadoAndParametroInput.NombreEstado, estadoAndParametroInput.NombreParametro));
        }

        //Metodo para identificar los estados de un parametro seleccionado
        public GetEstadosWithNombresEstadosOutput GetEstadosWithNombreEstado(GetParametroInput parametroInput)
        {
            var estadosConsulta = _estadoRepositorio.GetWithNombreEstado(parametroInput.Id);
            return new GetEstadosWithNombresEstadosOutput { Estados = Mapper.Map<List<EstadoWithNombreEstadoOutput>>(estadosConsulta) };
        }

        public GetEstadosWithNombresEstadosOutput GetEstadosWithNombreEstadoPreferencia()
        {
            var estadosConsulta = _estadoRepositorio.GetAllList().Where(est => est.ParametroEstado.Nombre == BowConsts.PARAMETRO_PREFERENCIA);
            return new GetEstadosWithNombresEstadosOutput { Estados = Mapper.Map<List<EstadoWithNombreEstadoOutput>>(estadosConsulta) };
        }

        //Metodo para cargar los estados en el dropdown
        public GetNombresEstadosOutput GetNombresEstados()
        {
            var listaEstadosNombre = _nombreestadoRepositorio.GetAllList().OrderBy(p => p.Nombre);
            return new GetNombresEstadosOutput { NombresEstados = Mapper.Map<List<NombreEstadoOutput>>(listaEstadosNombre) };
        }

        // Metodo para asignar un estado a un parametro indicado
        public void saveAsignarEstadoParametro(SaveEstadoInput nuevoEstadoAsignado)
        {
            var existeEstado = _estadoRepositorio.FirstOrDefault(d => d.Motivo.ToLower() == nuevoEstadoAsignado.Motivo.ToLower() && d.ParametroId == nuevoEstadoAsignado.ParametroId && d.EstadoNombreId == nuevoEstadoAsignado.EstadoNombreId);

            if (existeEstado == null)
            {
                _estadoRepositorio.Insert(Mapper.Map<Estado>(nuevoEstadoAsignado));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "parametro_estado_validarEstadoAsignadoParametro");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para actualizar el estado de un parametro.
        public void UpdateEstadoParametro(UpdateEstadoParametroInput estadoUpdate)
        {
            estadoUpdate.Motivo = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(estadoUpdate.Motivo.ToLower());

            var existeEstado = _estadoRepositorio.FirstOrDefault(d => d.Motivo.ToLower() == estadoUpdate.Motivo.ToLower() && d.Id != estadoUpdate.Id);
            var estadoConsultar = _estadoRepositorio.Get(estadoUpdate.Id);

            if (existeEstado == null)
            {
                estadoConsultar.Motivo = estadoUpdate.Motivo;
                _estadoRepositorio.Update(Mapper.Map<Estado>(estadoConsultar));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "parametro_estado_validarEstadoAsignadoParametro");
                throw new UserFriendlyException(mensajeError);
            }
        }


        //******************************** Zonas *********************************

        //Metodo para identificar los tipos de un parametro Tipos de zona
        public GetTiposZonaOutput GetTiposZona()
        {
            var parametroTipoZona = _parametroRepositorio.FirstOrDefault(tz => tz.Nombre == BowConsts.PARAMETRO_TIPOS_DE_ZONA);

            var listaTiposZona = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametroTipoZona.Id).OrderBy(d => d.Nombre);
            return new GetTiposZonaOutput { Tipos = Mapper.Map<List<TipoZonaOutput>>(listaTiposZona) };
        }

        //  Metodo para obtener todas los tipos de teléfono
        public GetAllTiposOutput GetAllTiposTelefono()
        {
            var listaTipos = _tipoRepositorio.GetAllTipos().Where(p => p.ParametroTipo.Nombre == BowConsts.PARAMETRO_TIPOS_DE_TELEFONO).OrderBy(l => l.Nombre);
            return new GetAllTiposOutput { Tipos = Mapper.Map<List<TipoTelefonoOutput>>(listaTipos) };
        }

        //  Metodo para obtener el tipo de teléfono fijo
        public GetTipoTelefonoFijoOutput GetTipoTelefonoFijo()
        {
            return Mapper.Map<GetTipoTelefonoFijoOutput>(_tipoRepositorio.GetAll().Where(p => p.ParametroTipo.Nombre == BowConsts.PARAMETRO_TIPOS_DE_TELEFONO && p.Nombre == BowConsts.TIPO_TELEFONO_FIJO).FirstOrDefault());
        }

        //Metodo para identificar los tipos de parametro (profesiones), para cargar las profesiones existentes
        public GetTiposByParametroProfesionesOutput GetTiposByParametroProfesiones()
        {
            Parametro parametro = _parametroRepositorio.FirstOrDefault(pr => pr.Nombre == BowConsts.PARAMETRO_PROFESIONES);

            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametro.Id).OrderBy(d => d.Nombre);
            return new GetTiposByParametroProfesionesOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
        }

        //Metodo para identificar los tipos de parametro (Estado civil), para cargar los estados civiles existentes
        public GetTiposByParametroEstadoCivilOutput GetTiposByParametroEstadoCivil()
        {
            Parametro parametro = _parametroRepositorio.FirstOrDefault(pr => pr.Nombre == BowConsts.PARAMETRO_ESTADO_CIVIL);

            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametro.Id).OrderBy(d => d.Nombre);
            return new GetTiposByParametroEstadoCivilOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
        }

        //******************************** Información Tributaria *********************************

        //  Metodo para obtener todos los tipos de valor de Información Tributaria
        public GetTiposInfoTributariaOutput GetTiposInfoTributaria()
        {
            var listaTipos = _tipoRepositorio.GetAllTipos().Where(p => p.ParametroTipo.Nombre == BowConsts.PARAMETRO_INFO_TRIBUTARIA).OrderBy(l => l.Nombre);
            return new GetTiposInfoTributariaOutput { Tipos = Mapper.Map<List<TipoInfoTributariaOutput>>(listaTipos) };
        }

        //  Metodo para obtener todos los tipos de ubicacion
        public GetTiposByParametroUbicacionOutput GetTiposByParametroUbicacion()
        {
            Parametro parametro = _parametroRepositorio.FirstOrDefault(pr => pr.Nombre == BowConsts.PARAMETRO_TIPOS_DE_UBICACION);

            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametro.Id).OrderBy(d => d.Nombre);
            return new GetTiposByParametroUbicacionOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
        }


        //******************************** Organización Empresa *********************************

        //  Metodo para obtener todos los tipos de valor de Naturaleza de Empresa
        public GetTiposNaturalezaEmpresaOutput GetTiposNaturalezaEmpresa()
        {
            var listaTipos = _tipoRepositorio.GetAllTipos().Where(p => p.ParametroTipo.Nombre == BowConsts.PARAMETRO_NATURALEZA_EMPRESA).OrderBy(l => l.Nombre);
            return new GetTiposNaturalezaEmpresaOutput { Tipos = Mapper.Map<List<TipoNaturalezaEmpresaOutput>>(listaTipos) };
        }

        //  Metodo para obtener el tipo de valor de Naturaleza de Empresa Jurídica
        public GetTipoJuridicaOutput GetTipoJuridica()
        {
            return Mapper.Map<GetTipoJuridicaOutput>(_tipoRepositorio.GetAll().Where(tp => tp.Nombre == BowConsts.NATURALEZA_EMPRESA_JURIDICA).FirstOrDefault());
        }

        public GetEstadoPreferenciaOutput GetEstadoActivoPreferencia()
        {
            return Mapper.Map<GetEstadoPreferenciaOutput>(_estadoRepositorio.GetAll().Where(est => est.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && est.ParametroEstado.Nombre == BowConsts.PARAMETRO_PREFERENCIA).FirstOrDefault());
        }

        //  Metodo para obtener todos los tipos de contacto web de una empresa
        public GetAllTiposWithFilterOutput GetAllTiposWithFilter(GetAllTiposWithFilterInput tiposAFiltrar)
        {
            List<Tipo> listaTipos = _tipoRepositorio.GetAll().Where(p => p.ParametroTipo.Nombre == tiposAFiltrar.ParametroNombre).ToList();
            List<Tipo> listaTiposAFiltrar = Mapper.Map<List<Tipo>>(tiposAFiltrar.Tipos);
            listaTipos = listaTipos.Except(listaTiposAFiltrar).ToList();

            return new GetAllTiposWithFilterOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
        }

        //****************** Métodos llamados desde PersonaService ********************
        public GetEstadosTipoUbicacionOutput GetEstadosTipoUbicacion()
        {
            GetEstadosTipoUbicacionOutput estados = new GetEstadosTipoUbicacionOutput();

            var parametroEstadoDireccion = _parametroRepositorio.FirstOrDefault(e => e.Nombre == BowConsts.PARAMETRO_TIPOS_DE_UBICACION);

            Estado estadoActivo = _estadoRepositorio.GetWithNombreEstado(parametroEstadoDireccion.Id).Where(e => e.EstadoNombreEstado.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_ACTIVO).FirstOrDefault();
            Estado estadoInactivo = _estadoRepositorio.GetWithNombreEstado(parametroEstadoDireccion.Id).Where(e => e.EstadoNombreEstado.Nombre == BowConsts.ESTADO_TELEFONO_DIRECCION_INACTIVO).FirstOrDefault();

            estados.EstadoActivoId = estadoActivo.Id;
            estados.EstadoInactivoId = estadoInactivo.Id;

            return estados;
        }

        public GetAllTiposContactoWebWithFilterPersonaOutput GetAllTiposContactoWebWithFilterPersona(GetAllTiposContactoWebWithFilterPersonaInput tiposContactoWebPersonaAsignados)
        {
            List<Tipo> listaTipos = _tipoRepositorio.GetAll().Where(p => p.ParametroTipo.Nombre == BowConsts.PARAMETRO_MEDIOS_DE_CONTACTO).ToList();
            List<Tipo> listaTiposYaAsignados = Mapper.Map<List<Tipo>>(tiposContactoWebPersonaAsignados.Tipos);
            listaTipos = listaTipos.Except(listaTiposYaAsignados).ToList();

            return new GetAllTiposContactoWebWithFilterPersonaOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
        }

        //  Metodo para obtener todos los tipos de valor de Naturaleza de Empresa
        public GetTiposSucursalEmpresaOutput GetTiposSucursalEmpresa()
        {
            var listaTipos = _tipoRepositorio.GetAllTipos().Where(p => p.ParametroTipo.Nombre == BowConsts.PARAMETRO_SUCURSAL_EMPRESA).OrderBy(l => l.Nombre);
            return new GetTiposSucursalEmpresaOutput { TipoSucursales = Mapper.Map<List<TipoSucursalEmpresaOutput>>(listaTipos) };
        }
        //***************** estados empleado ************************
        public GetEstadosEmpleadoOutput GetEstadosEmpleado()
        {
            var parametroEstadosEmpleado = _parametroRepositorio.FirstOrDefault(e => e.Nombre == BowConsts.PARAMETRO_ESTADO_DE_EMPLEADO);
            var estadosEmpleado = _estadoRepositorio.GetWithNombreEstado(parametroEstadosEmpleado.Id);

            return new GetEstadosEmpleadoOutput { Estados = Mapper.Map<List<EstadoEmpleadoOutput>>(estadosEmpleado) };
        }

        //  Metodo para obtener todos los estados de valor de Naturaleza de Empresa
        public GetEstadosSucursalEmpresaOutput GetEstadosSucursalEmpresa()
        {
            var listaEstados = _estadoRepositorio.GetAllList().Where(p => p.ParametroEstado.Nombre == BowConsts.PARAMETRO_SUCURSAL_EMPRESA).OrderBy(l => l.EstadoNombreEstado.Nombre);
            return new GetEstadosSucursalEmpresaOutput { EstadosSucursales = Mapper.Map<List<EstadoSucursalEmpresaOutput>>(listaEstados) };
        }
        //Metodo para obtener el Id del estado activo del parametro rol empleado en zona
        public GetEstadoActivoZonaEmpleadoOutput GetEstadoActivoZonaEmpleado()
        {
            //var parametroRolEmpleadoZona = _parametroRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA);
            var estadoActivoEmpleadoZona = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO);

            return Mapper.Map<GetEstadoActivoZonaEmpleadoOutput>(estadoActivoEmpleadoZona);
        }

        //Metodo para obtener el Id del estado inactivo del parametro rol empleado en zona
        public GetEstadoInactivoZonaEmpleadoOutput GetEstadoInactivoZonaEmpleado()
        {
            var estadoInactivoEmpleadoZona = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO);
            return Mapper.Map<GetEstadoInactivoZonaEmpleadoOutput>(estadoInactivoEmpleadoZona);
        }

        //Metodo para identificar los tipos de un parametro Tipos de rol empleado en zona
        public GetTiposRolOutput GetTiposRol()
        {
            var parametroTipoRol = _parametroRepositorio.FirstOrDefault(tr => tr.Nombre == BowConsts.PARAMETRO_ROL_EMPLEADO_ZONA);
            var listaTiposRol = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametroTipoRol.Id).OrderBy(d => d.Nombre);

            return new GetTiposRolOutput { Roles = Mapper.Map<List<TipoRolOutput>>(listaTiposRol) };
        }

        //Metodo para identificar los tipos de parametro (profesiones) con el id del tipo No Identificado, para cargar las profesiones existentes
        public GetTiposByParametroProfesionesWithIdNoIdentificadoOutput GetTiposByParametroProfesionesWithIdNoIdentificado()
        {
            Parametro parametro = _parametroRepositorio.FirstOrDefault(pr => pr.Nombre == BowConsts.PARAMETRO_PROFESIONES);

            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametro.Id).OrderBy(d => d.Nombre);
            var idNoIdentificado = _tipoRepositorio.FirstOrDefault(d => d.ParametroId == parametro.Id && d.Nombre == BowConsts.TIPO_PROFESION_NO_IDENTIFICADA);

            GetTiposByParametroProfesionesWithIdNoIdentificadoOutput profesiones = new GetTiposByParametroProfesionesWithIdNoIdentificadoOutput { Tipos = Mapper.Map<List<TipoProfesionesOutput>>(listaTipos) };

            profesiones.IdNoIdentificado = idNoIdentificado.Id;
            profesiones.Nombre = idNoIdentificado.Nombre;
            profesiones.Descripcion = idNoIdentificado.Descripcion;

            return profesiones;
        }

        //Metodo para identificar los tipos de parametro (Estado civil), con el Id No identificado para cargar por defecto
        public GetTiposByParametroEstadoCivilWithIdNoIdentificadoOutput GetTiposByParametroEstadoCivilWithIdNoIdentificado()
        {
            Parametro parametro = _parametroRepositorio.FirstOrDefault(pr => pr.Nombre == BowConsts.PARAMETRO_ESTADO_CIVIL);

            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametro.Id).OrderBy(d => d.Nombre);
            var idNoIdentificado = _tipoRepositorio.FirstOrDefault(d => d.ParametroId == parametro.Id && d.Nombre == BowConsts.TIPO_ESTADO_CIVIL_NO_IDENTIFICADO);

            GetTiposByParametroEstadoCivilWithIdNoIdentificadoOutput estadosCiviles = new GetTiposByParametroEstadoCivilWithIdNoIdentificadoOutput { Tipos = Mapper.Map<List<TipoOutput>>(listaTipos) };
            estadosCiviles.IdNoIdentificado = idNoIdentificado.Id;

            return estadosCiviles;
        }

        //***************************************************************************
        // **************************** MÓDULO AFILIACIONES *************************
        //***************************************************************************

        //Metodo para guardar un tipo (Categorias).
        public void SaveCategoria(SaveCategoriaInput categoriaInput)
        {
            var parametroCategoriaBeneficio = _parametroRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_CATEGORIAS_BENEFICIOS);

            categoriaInput.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(categoriaInput.Nombre.ToLower());
            categoriaInput.ParametroId = parametroCategoriaBeneficio.Id;

            Tipo existeTipo = _tipoRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == categoriaInput.Nombre.ToLower() && d.ParametroId == categoriaInput.ParametroId);


            if (existeTipo == null)
            {
                if (categoriaInput.EsNuevo == true)
                {
                    _tipoRepositorio.Insert(Mapper.Map<Tipo>(categoriaInput));
                }
                else
                {
                    _tipoRepositorio.Update(Mapper.Map<Tipo>(categoriaInput));
                }
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_beneficio_existeCategoria");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para obtener los tipos de parametro (Categorias Beneficios)
        public GetCategoriasOutput GetCategorias()
        {
            var parametroCategoriaBeneficio = _parametroRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_CATEGORIAS_BENEFICIOS);
            var listaTipos = _tipoRepositorio.GetAllList().Where(d => d.ParametroId == parametroCategoriaBeneficio.Id).OrderBy(d => d.Nombre);

            return new GetCategoriasOutput { Categorias = Mapper.Map<List<CategoriaOutput>>(listaTipos) };
        }

        //Metodo para eliminar un tipo (Categoria).
        public void DeleteCategoria(DeleteCategoriaInput categoriaEliminar)
        {
            _tipoRepositorio.Delete(categoriaEliminar.Id);
        }

        //Metodo para obtener el Id del estado activo del parametro plan exequial
        public GetEstadoActivoPlanExequialOutput GetEstadoActivoPlanExequial()
        {
            var estadoActivoPlanExequialId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_PLAN_EXEQUIAL && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO).Id;
            return new GetEstadoActivoPlanExequialOutput { Id = estadoActivoPlanExequialId };
        }

        //Metodo para obtener el Id del estado inactivo del parametro plan exequial
        public GetEstadoInactivoPlanExequialOutput GetEstadoInactivoPlanExequial()
        {
            var estadoActivoPlanExequialId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_PLAN_EXEQUIAL && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO).Id;
            return new GetEstadoInactivoPlanExequialOutput { Id = estadoActivoPlanExequialId };
        }

        //Metodo para obtener los estados del parametro grupo familiar
        public GetAllEstadosGrupoFamiliarOutput GetAllEstadosGrupoFamiliar()
        {
            var listaEstadosGrupoFamiliar = _estadoRepositorio.GetAll().Where(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_FAMILIAR).OrderBy(e => e.EstadoNombreEstado.Nombre);
            return new GetAllEstadosGrupoFamiliarOutput { Estados = Mapper.Map<List<EstadoGrupoFamiliarOutput>>(listaEstadosGrupoFamiliar) };
        }

        //Metodo para obtener el Id del estado activo del parametro grupo familiar
        public GetEstadoActivoGrupoFamiliarOutput GetEstadoActivoGrupoFamiliar()
        {
            var estadoActivoGrupoFamiliarId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_GRUPO_FAMILIAR && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO).Id;
            return new GetEstadoActivoGrupoFamiliarOutput { Id = estadoActivoGrupoFamiliarId };
        }

        public GetAllTiposBeneficioOutput GetAllTiposBeneficio()
        {
            Parametro parametroTipoBeneficio = _parametroRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL);
            var listaBeneficios = _tipoRepositorio.GetAll().Where(t => t.ParametroId == parametroTipoBeneficio.Id);
            return new GetAllTiposBeneficioOutput { TiposGruposBeneficio = Mapper.Map<List<TipoOutput>>(listaBeneficios) };
        }

        //Metodo para obtener los estados del parametro beneficios del plan exequial
        public GetAllEstadosBeneficiosPlanExequialOutput GetAllEstadosBeneficiosPlanExequial()
        {
            var listaEstadosBeneficiosPlanExequial = _estadoRepositorio.GetAll().Where(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL).OrderBy(e => e.EstadoNombreEstado.Nombre);
            return new GetAllEstadosBeneficiosPlanExequialOutput { Estados = Mapper.Map<List<EstadoBeneficiosPlanExequialOutput>>(listaEstadosBeneficiosPlanExequial) };
        }

        //Metodo para obtener el Id del estado activo del parametro beneficios del plan exequial
        public GetEstadoActivoBeneficiosPlanExequialOutput GetEstadoActivoBeneficiosPlanExequial()
        {
            var estadoActivoBeneficiosPlanExequialId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO).Id;
            return new GetEstadoActivoBeneficiosPlanExequialOutput { Id = estadoActivoBeneficiosPlanExequialId };
        }

        //Metodo para obtener el Id del estado inactivo del parametro beneficios del plan exequial
        public GetEstadoInactivoBeneficiosPlanExequialOutput GetEstadoInactivoBeneficiosPlanExequial()
        {
            var estadoInactivoBeneficiosPlanExequialId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO).Id;
            return new GetEstadoInactivoBeneficiosPlanExequialOutput { Id = estadoInactivoBeneficiosPlanExequialId };
        }

        //Se encargar de consultar el tipo de ubicacion residencial
        public GetTipoUbicacionResidencialOutput GetTipoUbicacionResidencial()
        {
            var parametroTipoUbicacion = _parametroRepositorio.FirstOrDefault(e => e.Nombre == BowConsts.PARAMETRO_TIPOS_DE_UBICACION);
            var tipoUbicacionResidencial = _tipoRepositorio.FirstOrDefault(t => t.ParametroId == parametroTipoUbicacion.Id && t.Nombre == BowConsts.TIPO_UBICACION_RESIDENCIAL);

            return Mapper.Map<GetTipoUbicacionResidencialOutput>(tipoUbicacionResidencial);
        }

        //Metodo para obtener todos los estados de motivo de no afiliación del cliente prospecto
        public GetAllEstadosClienteProspectoOutput GetAllEstadosClienteProspecto()
        {
            var parametroClienteProspecto = _parametroRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_ESTADO_CLIENTE_PROSPECTO);

            var listaEstadosClienteProspecto = _estadoRepositorio.GetAll().Where(e => e.ParametroId == parametroClienteProspecto.Id);
            return new GetAllEstadosClienteProspectoOutput { Estados = Mapper.Map<List<EstadoClienteProspecto>>(listaEstadosClienteProspecto) };
        }

        //Metodo para obtener el Id del estado activo del parametro ingresado
        public GetEstadoActivoByNombreParametroOutput GetEstadoActivoByNombreParametro(GetEstadoActivoByNombreParametroInput parametro)
        {
            var estadoActivoId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == parametro.NombreParametro && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_ACTIVO).Id;
            return new GetEstadoActivoByNombreParametroOutput { EstadoId = estadoActivoId };
        }

        //Metodo para obtener el Id del estado inactivo del parametro ingresado
        public GetEstadoInactivoByNombreParametroOutput GetEstadoInactivoByNombreParametro(GetEstadoInactivoByNombreParametroInput parametro)
        {
            var estadoInactivoId = _estadoRepositorio.FirstOrDefault(e => e.ParametroEstado.Nombre == parametro.NombreParametro && e.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_INACTIVO && e.EstadoNombreEstado.Abreviacion == BowConsts.NOMBREESTADO_ABREVIACION_INACTIVO).Id;
            return new GetEstadoInactivoByNombreParametroOutput { EstadoId = estadoInactivoId };
        }

        //Metodo para obtener los beneficios propios, modificables y adicionales del plan exequial
        public GetTiposBeneficiosPlanExequialOutPut GetTiposBeneficiosPlanExequial()
        {
            GetTiposBeneficiosPlanExequialOutPut tiposBeneficio = new GetTiposBeneficiosPlanExequialOutPut();

            var parametroId = _parametroRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PARAMETRO_BENEFICIOS_PLAN_EXEQUIAL).Id;

            tiposBeneficio.PropiosId = _tipoRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_PROPIO && p.ParametroId == parametroId).Id;
            tiposBeneficio.AdicionalesId = _tipoRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_ADICIONAL && p.ParametroId == parametroId).Id;
            tiposBeneficio.EstadoActivoId = GetEstadoActivoBeneficiosPlanExequial().Id;

            return tiposBeneficio;
        }

    }
}
