using Abp.Localization;
using Abp.UI;
using AutoMapper;
using Bow.Empresas.DTOs.InputModels;
using Bow.Empresas.DTOs.OutputModels;
using Bow.Empresas.Entidades;
using Bow.Empresas.Repositorios;
using Bow.Parametros;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Parametros.Entidades;
using Bow.Personas;
using Bow.Personas.DTOs.InputModels;
using Bow.Personas.DTOs.OutputModels;
using Bow.Zonificacion;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas
{
    public class EmpresasService : IEmpresasService
    {

        # region Repositorios

        private IEmpresaRepositorio _empresaRepositorio;
        private IEmpresaTelefonoRepositorio _empresaTelefonoRepositorio;
        private IEmpresaContactoRepositorio _empresaContactoRepositorio;
        private IEmpresaContactoWebRepositorio _empresaContactoWebRepositorio;
        private IInfoTributariaRepositorio _infoTributariaRepositorio;
        private IInfoTributariaOpcionRepositorio _infoTributariaOpcionRepositorio;
        private IInfoTributariaLocalidadRepositorio _infoTributariaLocalidadRepositorio;
        private IActividadEconomicaRepositorio _actividadEconomicaRepositorio;
        private IOrganizacionRepositorio _organizacionRepositorio;
        private IEmpresaOrganizacionRepositorio _empresaOrganizacionRepositorio;
        private IEmpresaInfoTributariaRepositorio _empresaInfoTributariaRepositorio;
        private ISucursalRepositorio _sucursalRepositorio;
        private ISucursalTelefonoRepositorio _sucursalTelefonoRepositorio;
        private IRecaudoMasivoRepositorio _recaudoMasivoRepositorio;

        private IZonificacionService _zonificacionService;
        private IPersonasService _personasService;
        private IParametrosService _parametrosService;

        # endregion

        public EmpresasService(IEmpresaRepositorio empresaRepositorio, IEmpresaTelefonoRepositorio empresaTelefonoRepositorio, IEmpresaContactoRepositorio empresaContactoRepositorio, IEmpresaContactoWebRepositorio empresaContactoWebRepositorio,
            IInfoTributariaRepositorio infoTributariaRepositorio, IInfoTributariaOpcionRepositorio infoTributariaOpcionRepositorio, IInfoTributariaLocalidadRepositorio infoTributariaLocalidadRepositorio, IZonificacionService zonificacionService,
            IActividadEconomicaRepositorio actividadEconomicaRepositorio, IOrganizacionRepositorio organizacionRepositorio, IEmpresaOrganizacionRepositorio empresaOrganizacionRepositorio, IPersonasService personasService, IParametrosService parametrosService,
            IEmpresaInfoTributariaRepositorio empresaInfoTributariaRepositorio, ISucursalRepositorio sucursalRepositorio, ISucursalTelefonoRepositorio sucursalTelefonoRepositorio, IRecaudoMasivoRepositorio recaudoMasivoRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
            _empresaTelefonoRepositorio = empresaTelefonoRepositorio;
            _empresaContactoRepositorio = empresaContactoRepositorio;
            _empresaContactoWebRepositorio = empresaContactoWebRepositorio;
            _infoTributariaRepositorio = infoTributariaRepositorio;
            _infoTributariaOpcionRepositorio = infoTributariaOpcionRepositorio;
            _infoTributariaLocalidadRepositorio = infoTributariaLocalidadRepositorio;
            _actividadEconomicaRepositorio = actividadEconomicaRepositorio;
            _organizacionRepositorio = organizacionRepositorio;
            _empresaOrganizacionRepositorio = empresaOrganizacionRepositorio;
            _empresaInfoTributariaRepositorio = empresaInfoTributariaRepositorio;
            _sucursalRepositorio = sucursalRepositorio;
            _sucursalTelefonoRepositorio = sucursalTelefonoRepositorio;
            _recaudoMasivoRepositorio = recaudoMasivoRepositorio;

            _zonificacionService = zonificacionService;
            _personasService = personasService;
            _parametrosService = parametrosService;
        }

        /***************************************************************************************************
         * Información Tributaria
         * ************************************************************************************************/

        //  Método para obtener la información tributaria según el id
        public GetInfoTributariaOutput GetInfoTributaria(GetInfoTributariaInput infoTributariaInput)
        {
            return Mapper.Map<GetInfoTributariaOutput>(_infoTributariaRepositorio.Get(infoTributariaInput.Id));
        }

        //  Método para obtener la lista de información tributaria
        public GetInfoTributariasOutput GetInfoTributarias()
        {
            var listaInfoTributarias = _infoTributariaRepositorio.GetAllListWithOpcionesAndLocalidades().OrderBy(p => p.Nombre);
            return new GetInfoTributariasOutput { InfoTributarias = Mapper.Map<List<InfoTributariaOutput>>(listaInfoTributarias) };
        }

        //  Método para guardar una información tributaria
        public void SaveInfoTributaria(SaveInfoTributariaInput nuevaInfoTributaria)
        {
            InfoTributaria existeInfoTributaria = _infoTributariaRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevaInfoTributaria.Nombre.ToLower());
            if (existeInfoTributaria != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_infoTributaria_validacion_nombreExistente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                EstadoWithNombreEstadoOutput estadoInfoTributaria = new EstadoWithNombreEstadoOutput();
                GetEstadoByNombreEstadoAndNombreParametroInput EstadoAndParametro = new GetEstadoByNombreEstadoAndNombreParametroInput();
                EstadoAndParametro.NombreParametro = BowConsts.PARAMETRO_INFO_TRIBUTARIA;
                if (nuevaInfoTributaria.EstadoId == BowConsts.TIPO_INFO_TRIBUTARIA_ESTADO_ACTIVO)
                {
                    EstadoAndParametro.NombreEstado = BowConsts.NOMBREESTADO_NOMBRE_VIGENTE;
                    estadoInfoTributaria = _parametrosService.GetEstadoByNombreEstadoAndNombreParametro(EstadoAndParametro);

                }
                else
                {
                    EstadoAndParametro.NombreEstado = BowConsts.NOMBREESTADO_NOMBRE_NO_VIGENTE;
                    estadoInfoTributaria = _parametrosService.GetEstadoByNombreEstadoAndNombreParametro(EstadoAndParametro);

                }
                nuevaInfoTributaria.EstadoId = estadoInfoTributaria.Id;
                _infoTributariaRepositorio.Insert(Mapper.Map<InfoTributaria>(nuevaInfoTributaria));
            }
        }

        //  Metodo para eliminar una información tributaria
        public void DeleteInfoTributaria(DeleteInfoTributariaInput infoTributariaEliminar)
        {
            _infoTributariaRepositorio.Delete(infoTributariaEliminar.Id);
        }

        //  Metodo para actualizar una información tributaria
        public void UpdateInfoTributaria(UpdateInfoTributariaInput infoTributariaUpdate)
        {
            InfoTributaria existeInfoTributaria = _infoTributariaRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == infoTributariaUpdate.Nombre.ToLower() && d.Id != infoTributariaUpdate.Id);
            if (existeInfoTributaria != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_infoTributaria_validacion_nombreExistente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                EstadoWithNombreEstadoOutput estadoInfoTributaria = new EstadoWithNombreEstadoOutput();
                GetEstadoByNombreEstadoAndNombreParametroInput EstadoAndParametro = new GetEstadoByNombreEstadoAndNombreParametroInput();
                EstadoAndParametro.NombreParametro = BowConsts.PARAMETRO_INFO_TRIBUTARIA;
                if (infoTributariaUpdate.EstadoId == BowConsts.TIPO_INFO_TRIBUTARIA_ESTADO_ACTIVO)
                {
                    EstadoAndParametro.NombreEstado = BowConsts.NOMBREESTADO_NOMBRE_VIGENTE;
                    estadoInfoTributaria = _parametrosService.GetEstadoByNombreEstadoAndNombreParametro(EstadoAndParametro);

                }
                else
                {
                    EstadoAndParametro.NombreEstado = BowConsts.NOMBREESTADO_NOMBRE_NO_VIGENTE;
                    estadoInfoTributaria = _parametrosService.GetEstadoByNombreEstadoAndNombreParametro(EstadoAndParametro);

                }
                infoTributariaUpdate.EstadoId = estadoInfoTributaria.Id;
                var infoTributariaActualizar = _infoTributariaRepositorio.Update(Mapper.Map<InfoTributaria>(infoTributariaUpdate));
            }
        }

        //  Metodo para validar si es posible eliminar una información tributaria
        public PuedeEliminarInfoTributariaOutput PuedeEliminarInfoTributaria(PuedeEliminarInfoTributariaInput infoTributariaEliminar)
        {
            int countListaEmpresaInfoTributaria = _empresaInfoTributariaRepositorio.GetAllList().Where(d => d.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId == infoTributariaEliminar.Id).Count();
            PuedeEliminarInfoTributariaOutput puede = new PuedeEliminarInfoTributariaOutput();

            if (countListaEmpresaInfoTributaria == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        /***************************************************************************************************
         * Opciones de Información Tributaria
         * ************************************************************************************************/

        //  Método para obtener la opción de información tributaria según el id
        public GetInfoTributariaOpcionOutput GetInfoTributariaOpcion(GetInfoTributariaOpcionInput infoOpcionTributariaInput)
        {
            return Mapper.Map<GetInfoTributariaOpcionOutput>(_infoTributariaOpcionRepositorio.Get(infoOpcionTributariaInput.Id));
        }

        //  Método para obtener la lista opciones de una información tributaria específica segun el id
        public GetInfoTributariaOpcionesOutput GetInfoTributariaOpciones(GetInfoTributariaOpcionesInput infoOpcionesTributariaInput)
        {
            var listaInfoTributariaOpciones = _infoTributariaOpcionRepositorio.GetAllList().Where(inf => inf.InfoTributariaId == infoOpcionesTributariaInput.InfoTributariaId).OrderBy(p => p.Nombre);
            return new GetInfoTributariaOpcionesOutput { InfoTributariaOpciones = Mapper.Map<List<InfoTributariaOpcionOutput>>(listaInfoTributariaOpciones) };
        }

        //  Método para guardar una opción de información tributaria
        public void SaveInfoTributariaOpcion(SaveInfoTributariaOpcionInput nuevaInfoTributariaOpcion)
        {
            InfoTributariaOpcion existeInfoTributariaOpcion = _infoTributariaOpcionRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevaInfoTributariaOpcion.Nombre.ToLower() && d.InfoTributariaId == nuevaInfoTributariaOpcion.InfoTributariaId);
            if (existeInfoTributariaOpcion != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_infoTributariaOpcion_validacion_nombreExistente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                _infoTributariaOpcionRepositorio.Insert(Mapper.Map<InfoTributariaOpcion>(nuevaInfoTributariaOpcion));
            }
        }

        //  Metodo para eliminar una opción de información tributaria
        public void DeleteInfoTributariaOpcion(DeleteInfoTributariaOpcionInput infoTributariaOpcionEliminar)
        {
            _infoTributariaOpcionRepositorio.Delete(infoTributariaOpcionEliminar.Id);
        }

        //  Metodo para actualizar una opción de información tributaria
        public void UpdateInfoTributariaOpcion(UpdateInfoTributariaOpcionInput infoTributariaOpcionUpdate)
        {
            InfoTributariaOpcion existeInfoTributariaOpcion = _infoTributariaOpcionRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == infoTributariaOpcionUpdate.Nombre.ToLower() && d.InfoTributariaId == infoTributariaOpcionUpdate.InfoTributariaId && d.Id != infoTributariaOpcionUpdate.Id);
            if (existeInfoTributariaOpcion != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_infoTributariaOpcion_validacion_nombreExistente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                _infoTributariaOpcionRepositorio.Update(Mapper.Map<InfoTributariaOpcion>(infoTributariaOpcionUpdate));
            }
        }

        //  Metodo para validar si es posible eliminar una opción de información tributaria
        public PuedeEliminarInfoTributariaOpcionOutput PuedeEliminarInfoTributariaOpcion(PuedeEliminarInfoTributariaOpcionInput infoTributariaOpcionEliminar)
        {
            int countListaEmpresaInfoTributaria = _empresaInfoTributariaRepositorio.GetAllList().Where(d => d.InfoTributariaOpcionId == infoTributariaOpcionEliminar.Id).Count();
            PuedeEliminarInfoTributariaOpcionOutput puede = new PuedeEliminarInfoTributariaOpcionOutput();

            if (countListaEmpresaInfoTributaria == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        /***************************************************************************************************
         * Localidades de Información Tributaria
         * ************************************************************************************************/
        
        public GetAllLocalidadesByInfoTributariaOutput GetAllLocalidadesByInfoTributaria(GetAllLocalidadesByInfoTributariaInput infoTributariaInput)
        {
            var listaInfoTributariasLocalidades = _infoTributariaLocalidadRepositorio.GetAllWithLocalidadAndDepartamentoAndPaisByInfoTributaria(infoTributariaInput.InfoTributariaId);
            return new GetAllLocalidadesByInfoTributariaOutput { InfoTributariaLocalidades = Mapper.Map<List<LocalidadesByInfoTributariaOutput>>(listaInfoTributariasLocalidades) };
        }

        public GetAllLocalidadesByInfoTributariaAndPaisOutput GetAllLocalidadesByInfoTributariaAndPais(GetAllLocalidadesByInfoTributariaAndPaisInput infoTributariaAndPaisInput)
        {
            var listaInfoTributariasLocalidades = _infoTributariaLocalidadRepositorio.GetAllWithLocalidadByInfoTributariaAndPais(infoTributariaAndPaisInput.InfoTributariaId, infoTributariaAndPaisInput.PaisId);
            return new GetAllLocalidadesByInfoTributariaAndPaisOutput { InfoTributariaLocalidades = Mapper.Map<List<LocalidadesByInfoTributariaAndPaisOutput>>(listaInfoTributariasLocalidades) };
        }

        public GetAllLocalidadesByInfoTributariaAndDepartamentoOutput GetAllLocalidadesByInfoTributariaAndDepartamento(GetAllLocalidadesByInfoTributariaAndDepartamentoInput infoTributariaAndPaisAndDepartamentoInput)
        {
            var listaInfoTributariasLocalidades = _infoTributariaLocalidadRepositorio.GetAllWithLocalidadByInfoTributariaAndDepartamento(infoTributariaAndPaisAndDepartamentoInput.InfoTributariaId, infoTributariaAndPaisAndDepartamentoInput.DepartamentoId);
            return new GetAllLocalidadesByInfoTributariaAndDepartamentoOutput { InfoTributariaLocalidades = Mapper.Map<List<LocalidadesByInfoTributariaAndPaisOutput>>(listaInfoTributariasLocalidades) };
        }

        public GetAllLocalidadesByNotInfoTributariaOutput GetAllLocalidadesByNotInfoTributaria(GetAllLocalidadesByNotInfoTributariaInput localidadesByNotInfoTributaria)
        {
            GetAllLocalidadesWithFilterInput listaInfoTributariaLocalidades = new GetAllLocalidadesWithFilterInput();
            listaInfoTributariaLocalidades.listaLocalidadesQueNoSeMuestra = Mapper.Map<List<LocalidadInput>>(_infoTributariaLocalidadRepositorio.GetAllWithLocalidadAndDepartamentoAndPaisByInfoTributaria(localidadesByNotInfoTributaria.InfoTributariaId));

            GetAllLocalidadesWithFilterOutput listaInfoTributariaLocalidadesFiltradas = _zonificacionService.GetAllLocalidadesWithFilter(listaInfoTributariaLocalidades);

            return new GetAllLocalidadesByNotInfoTributariaOutput { InfoTributariaLocalidades = Mapper.Map<List<LocalidadesByNotInfoTributariaOutput>>(listaInfoTributariaLocalidadesFiltradas.Localidades) };
        }

        //  Método para guardar las localidades de Información tributaria por Pais
        public void SaveInfoTributariaLocalidadByPais(SaveInfoTributariaLocalidadByPaisInput asignarInfoTributariaLocalidadPorPais)
        {
            GetLocalidadesByPaisInput pais = new GetLocalidadesByPaisInput();
            pais.Id = asignarInfoTributariaLocalidadPorPais.PaisId;
            GetLocalidadesByPaisOutput listaLocalidad = _zonificacionService.GetLocalidadesByPais(pais);
            InfoTributariaLocalidad itemInfoTributariaLocalidad;
            foreach (LocalidadOutput localidad in listaLocalidad.Localidades)
            {
                itemInfoTributariaLocalidad = _infoTributariaLocalidadRepositorio.FirstOrDefault(loc => loc.LocalidadId == localidad.Id && loc.InfoTributariaId == asignarInfoTributariaLocalidadPorPais.InfoTributariaId);
                if (itemInfoTributariaLocalidad == null)
                {
                    itemInfoTributariaLocalidad = new InfoTributariaLocalidad();
                    itemInfoTributariaLocalidad.InfoTributariaId = asignarInfoTributariaLocalidadPorPais.InfoTributariaId;
                    itemInfoTributariaLocalidad.LocalidadId = localidad.Id;
                    _infoTributariaLocalidadRepositorio.Insert(itemInfoTributariaLocalidad);
                }
            }
        }

        //  Método para guardar las localidades de Información tributaria por Departamento
        public void SaveInfoTributariaLocalidadByDepartamento(SaveInfoTributariaLocalidadByDepartamentoInput asignarInfoTributariaLocalidadPorDepartamento)
        {
            GetLocalidadesInput departamento = new GetLocalidadesInput();
            departamento.Id = asignarInfoTributariaLocalidadPorDepartamento.DepartamentoId;
            GetLocalidadesOutput listaLocalidad = _zonificacionService.GetLocalidades(departamento);
            InfoTributariaLocalidad itemInfoTributariaLocalidad;
            foreach (LocalidadOutput localidad in listaLocalidad.Localidades)
            {
                itemInfoTributariaLocalidad = _infoTributariaLocalidadRepositorio.FirstOrDefault(loc => loc.LocalidadId == localidad.Id && loc.InfoTributariaId == asignarInfoTributariaLocalidadPorDepartamento.InfoTributariaId);
                if (itemInfoTributariaLocalidad == null)
                {
                    itemInfoTributariaLocalidad = new InfoTributariaLocalidad();
                    itemInfoTributariaLocalidad.InfoTributariaId = asignarInfoTributariaLocalidadPorDepartamento.InfoTributariaId;
                    itemInfoTributariaLocalidad.LocalidadId = localidad.Id;
                    _infoTributariaLocalidadRepositorio.Insert(itemInfoTributariaLocalidad);
                }
            }
        }

        //  Método para guardar la localidad de Información tributaria
        public void SaveInfoTributariaLocalidad(SaveInfoTributariaLocalidadnput asignarInfoTributariaLocalidad)
        {
            InfoTributariaLocalidad itemInfoTributariaLocalidad = _infoTributariaLocalidadRepositorio.FirstOrDefault(loc => loc.LocalidadId == asignarInfoTributariaLocalidad.LocalidadId && loc.InfoTributariaId == asignarInfoTributariaLocalidad.InfoTributariaId);
            if (itemInfoTributariaLocalidad == null)
            {
                _infoTributariaLocalidadRepositorio.Insert(Mapper.Map<InfoTributariaLocalidad>(asignarInfoTributariaLocalidad));
            }
        }

        //  Método para eliminar las localidades de Información tributaria por Pais
        public void DeleteInfoTributariaLocalidadByPais(DeleteInfoTributariaLocalidadByPaisInput eliminarInfoTributariaLocalidadPorPais)
        {
            _infoTributariaLocalidadRepositorio.Delete(loc => loc.Localidad.DepartamentoLocalidad.PaisId == eliminarInfoTributariaLocalidadPorPais.PaisId && loc.InfoTributariaId == eliminarInfoTributariaLocalidadPorPais.InfoTributariaId);
        }

        //  Método para eliminar las localidades de Información tributaria por Departamento
        public void DeleteInfoTributariaLocalidadByDepartamento(DeleteInfoTributariaLocalidadByDepartamentoInput eliminarInfoTributariaLocalidadPorDepartamento)
        {
            _infoTributariaLocalidadRepositorio.Delete(loc => loc.Localidad.DepartamentoId == eliminarInfoTributariaLocalidadPorDepartamento.DepartamentoId && loc.InfoTributariaId == eliminarInfoTributariaLocalidadPorDepartamento.InfoTributariaId);
        }

        //  Método para eliminar las localidades de Información tributaria
        public void DeleteInfoTributariaLocalidad(DeleteInfoTributariaLocalidadInput eliminarInfoTributariaLocalidad)
        {
            _infoTributariaLocalidadRepositorio.Delete(loc => loc.LocalidadId == eliminarInfoTributariaLocalidad.LocalidadId && loc.InfoTributariaId == eliminarInfoTributariaLocalidad.InfoTributariaId);
        }

        public GetAllPaisesByInfoTributariaOutput GetAllPaisesByInfoTributaria(GetAllPaisesByInfoTributariaInput infoTributariaInput)
        {
            var listaInfoTributariasPaises = _infoTributariaLocalidadRepositorio.GetAllPaisesByInfoTributaria(infoTributariaInput.InfoTributariaId);
            return new GetAllPaisesByInfoTributariaOutput { InfoTributariaPaises = Mapper.Map<List<PaisesByInfoTributariaOutput>>(listaInfoTributariasPaises) };
        }

        public GetAllDepartamentosByInfoTributariaOutput GetAllDepartamentosByInfoTributaria(GetAllDepartamentosByInfoTributariaInput infoTributariaInput)
        {
            var listaInfoTributariasDepartamentos = _infoTributariaLocalidadRepositorio.GetAllDepartamentosByInfoTributaria(infoTributariaInput.InfoTributariaId);
            return new GetAllDepartamentosByInfoTributariaOutput { InfoTributariaDepartamentos = Mapper.Map<List<DepartamentosByInfoTributariaOutput>>(listaInfoTributariasDepartamentos) };
        }

        public GetActividadesEconomicasOutput GetActividadesEconomicas()
        {
            var listaActividadesEconomicas = _actividadEconomicaRepositorio.GetAllList().OrderBy(ac => ac.Nombre);
            return new GetActividadesEconomicasOutput { ActividadesEconomicas = Mapper.Map<List<ActividadEconomicaOutput>>(listaActividadesEconomicas) };
        }

        public void SaveActividadEconomica(SaveActividadEconomicaInput nuevaActividadEconomica)
        {
            nuevaActividadEconomica.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaActividadEconomica.Nombre.ToLower());
            ActividadEconomica existeCodigoActividadEconomica = _actividadEconomicaRepositorio.FirstOrDefault(ae => ae.Codigo == nuevaActividadEconomica.Codigo);
            ActividadEconomica existeNombreActividadEconomica = _actividadEconomicaRepositorio.FirstOrDefault(ae => ae.Nombre.ToLower() == nuevaActividadEconomica.Nombre.ToLower());

            if ((existeCodigoActividadEconomica == null) && (existeNombreActividadEconomica == null))
            {
                _actividadEconomicaRepositorio.Insert(Mapper.Map<ActividadEconomica>(nuevaActividadEconomica));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_actividadEconomica_existeActividad");
                throw new UserFriendlyException(mensajeError);
            }

        }

        public GetActividadEconomicaOutput GetActividadEconomica(GetActividadEconomicaInput actividadEconomicaInput)
        {
            return Mapper.Map<GetActividadEconomicaOutput>(_actividadEconomicaRepositorio.Get(actividadEconomicaInput.Id));
        }

        public void UpdateActividadEconomica(UpdateActividadEconomicaInput actividadEconomicaUpdate)
        {
            actividadEconomicaUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(actividadEconomicaUpdate.Nombre.ToLower());
            ActividadEconomica existeCodigoActividadEconomica = _actividadEconomicaRepositorio.FirstOrDefault(ae => ae.Codigo == actividadEconomicaUpdate.Codigo && ae.Id != actividadEconomicaUpdate.Id);
            ActividadEconomica existeNombreActividadEconomica = _actividadEconomicaRepositorio.FirstOrDefault(ae => ae.Nombre.ToLower() == actividadEconomicaUpdate.Nombre.ToLower() && ae.Id != actividadEconomicaUpdate.Id);

            if ((existeCodigoActividadEconomica == null) && (existeNombreActividadEconomica == null))
            {
                var departamentoActualizar = _actividadEconomicaRepositorio.Update(Mapper.Map<ActividadEconomica>(actividadEconomicaUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_actividadEconomica_existeActividad");
                throw new UserFriendlyException(mensajeError);
            }
        }

        /************************************************************************************
         * Organización
         * **********************************************************************************/

        //  Método para obtener la información de la organización según el id
        public GetOrganizacionOutput GetOrganizacion(GetOrganizacionInput organizacionInput)
        {
            //  Cambiar despues por el id de Organización

            return Mapper.Map<GetOrganizacionOutput>(_organizacionRepositorio.GetAll().FirstOrDefault());
        }

        //  Metodo para actualizar la información de la Organización
        public void UpdateOrganizacion(UpdateOrganizacionInput organizacionUpdate)
        {
            Organizacion existeOrganizacion = _organizacionRepositorio.FirstOrDefault(org => org.Nombre.ToLower() == organizacionUpdate.Nombre.ToLower() && org.Id != organizacionUpdate.Id);
            if (existeOrganizacion != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_organizacion_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                _organizacionRepositorio.Update(Mapper.Map<Organizacion>(organizacionUpdate));
            }
        }

        //  Método para obtener la lista de empresas de una organización
        public GetAllEmpresasByOrganizacionOutput GetAllEmpresasByOrganizacion(GetAllEmpresasByOrganizacionInput organizacionInput)
        {
            var listaEmpresas = _empresaOrganizacionRepositorio.GetAllEmpresasByOrganizacion(organizacionInput.OrganizacionId);
            return new GetAllEmpresasByOrganizacionOutput { Empresas = Mapper.Map<List<EmpresaOutput>>(listaEmpresas) };
        }

        //  Método para obtener la lista de empresas de una organización
        public GetAllEmpresasWithSucursalesByOrganizacionOutput GetAllEmpresasWithSucursalesByOrganizacion(GetAllEmpresasWithSucursalesByOrganizacionInput organizacionInput)
        {
            var listaEmpresas = _empresaOrganizacionRepositorio.GetAllEmpresasByOrganizacion(organizacionInput.OrganizacionId);
            return new GetAllEmpresasWithSucursalesByOrganizacionOutput { Empresas = Mapper.Map<List<EmpresaWithSucursalesOutput>>(listaEmpresas) };
        }

        /************************************************************************************
         * Empresa
         * **********************************************************************************/

        //  Método para obtener la informaciónd de la empresa según el id
        public GetEmpresaOutput GetEmpresa(GetEmpresaInput empresaInput)
        {
            return Mapper.Map<GetEmpresaOutput>(_empresaRepositorio.Get(empresaInput.Id));
        }

        //  Método para guardar una empresa
        public SaveEmpresaOutput SaveEmpresa(SaveEmpresaInput empresaSave)
        {
            SaveEmpresaOutput resultado = new SaveEmpresaOutput();
            Empresa existeEmpresa = _empresaRepositorio.FirstOrDefault(e => e.Documento.ToLower() == empresaSave.Documento.ToLower());
            if (existeEmpresa != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresa_guardar_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                resultado.Id = _empresaRepositorio.InsertAndGetId(Mapper.Map<Empresa>(empresaSave));
            }
            return resultado;
        }

        //  Metodo para actualizar la información de una empresa
        public void UpdateEmpresa(UpdateEmpresaInput empresaUpdate)
        {
            Empresa existeEmpresa = _empresaRepositorio.FirstOrDefault(e => e.Documento.ToLower() == empresaUpdate.Documento.ToLower() && e.Id != empresaUpdate.Id);
            if (existeEmpresa != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresa_actualizar_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                var empresaActualizar = _empresaRepositorio.Update(Mapper.Map<Empresa>(empresaUpdate));
            }
        }

        /************************************************************************************
         * Empresa Organización
         * **********************************************************************************/

        //  Método para guardar una empresa organización
        public SaveEmpresaOrganizacionOutput SaveEmpresaOrganizacion(SaveEmpresaOrganizacionInput empresaOrganizacionSave)
        {
            SaveEmpresaOrganizacionOutput resultado = new SaveEmpresaOrganizacionOutput();
            EmpresaOrganizacion existeEmpresaOrganizacion = _empresaOrganizacionRepositorio.FirstOrDefault(e => e.Nombre.ToLower() == empresaOrganizacionSave.Nombre.ToLower());
            var mensajeError = "";
            if (existeEmpresaOrganizacion != null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresaOrganizacion_guardar_nombre_existente");
            }
            else
            {
                ValidarTipoDocumentoInput validacionInput = new ValidarTipoDocumentoInput();
                validacionInput.TipoDocumentoId = empresaOrganizacionSave.TipoDocumentoId;
                validacionInput.NumeroDocumento = empresaOrganizacionSave.Documento;
                ValidarTipoDocumentoOutput validacionOutput = _personasService.ValidarTipoDocumentoPersona(validacionInput);
                if (!validacionOutput.TipoDocumentoValido)
                {
                    mensajeError = validacionOutput.Mensaje;
                }
                else
                {
                    SaveEmpresaInput guardadEmpresa = new SaveEmpresaInput();
                    guardadEmpresa = Mapper.Map<SaveEmpresaInput>(empresaOrganizacionSave);
                    SaveEmpresaOutput respuestaEmpresa = SaveEmpresa(guardadEmpresa);
                    empresaOrganizacionSave.EmpresaId = respuestaEmpresa.Id;

                    GetEstadoByNombreEstadoAndNombreParametroInput EstadoAndParametro = new GetEstadoByNombreEstadoAndNombreParametroInput();
                    EstadoWithNombreEstadoOutput estadoEmpresaOrganizacion = new EstadoWithNombreEstadoOutput();
                    EstadoAndParametro.NombreEstado = empresaOrganizacionSave.Estado;
                    EstadoAndParametro.NombreParametro = BowConsts.PARAMETRO_EMPRESA_ORGANIZACION;
                    estadoEmpresaOrganizacion = _parametrosService.GetEstadoByNombreEstadoAndNombreParametro(EstadoAndParametro);

                    empresaOrganizacionSave.EstadoId = estadoEmpresaOrganizacion.Id;

                    _empresaOrganizacionRepositorio.Insert(Mapper.Map<EmpresaOrganizacion>(empresaOrganizacionSave));

                    resultado.EmpresaId = respuestaEmpresa.Id;
                }
            }
            if (mensajeError != "")
                throw new UserFriendlyException(mensajeError);
            return resultado;
        }

        //  Metodo para actualizar la información de una empresa organización
        public void UpdateEmpresaOrganizacion(UpdateEmpresaOrganizacionInput empresaOrganizacionUpdate)
        {
            var mensajeError = "";
            EmpresaOrganizacion existeEmpresaOrganizacion = _empresaOrganizacionRepositorio.FirstOrDefault(e => e.Nombre.ToLower() == empresaOrganizacionUpdate.Documento.ToLower() && e.Id != empresaOrganizacionUpdate.Id);
            if (existeEmpresaOrganizacion != null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresaOrganizacion_actualizar_nombre_existente");
            }
            else
            {
                ValidarTipoDocumentoInput validacionInput = new ValidarTipoDocumentoInput();
                validacionInput.TipoDocumentoId = empresaOrganizacionUpdate.TipoDocumentoId;
                validacionInput.NumeroDocumento = empresaOrganizacionUpdate.Documento;
                ValidarTipoDocumentoOutput validacionOutput = _personasService.ValidarTipoDocumentoPersona(validacionInput);
                if (!validacionOutput.TipoDocumentoValido)
                {
                    mensajeError = validacionOutput.Mensaje;
                }
                else
                {
                    UpdateEmpresaInput actualizarEmpresa = new UpdateEmpresaInput();
                    actualizarEmpresa = Mapper.Map<UpdateEmpresaInput>(empresaOrganizacionUpdate);
                    UpdateEmpresa(actualizarEmpresa);

                    GetEstadoByNombreEstadoAndNombreParametroInput EstadoAndParametro = new GetEstadoByNombreEstadoAndNombreParametroInput();
                    EstadoWithNombreEstadoOutput estadoEmpresaOrganizacion = new EstadoWithNombreEstadoOutput();
                    EstadoAndParametro.NombreEstado = empresaOrganizacionUpdate.Estado;
                    EstadoAndParametro.NombreParametro = BowConsts.PARAMETRO_EMPRESA_ORGANIZACION;
                    estadoEmpresaOrganizacion = _parametrosService.GetEstadoByNombreEstadoAndNombreParametro(EstadoAndParametro);

                    empresaOrganizacionUpdate.EstadoId = estadoEmpresaOrganizacion.Id;

                    _empresaOrganizacionRepositorio.Update(Mapper.Map<EmpresaOrganizacion>(empresaOrganizacionUpdate));
                }
            }
            if (mensajeError != "")
                throw new UserFriendlyException(mensajeError);
        }

        //  Método para obtener la información de la organización con la información de la empresa
        public GetEmpresaOrganizacionOutput GetEmpresaOrganizacion(GetEmpresaOrganizacionInput empresaOrganizacionInput)
        {
            return Mapper.Map<GetEmpresaOrganizacionOutput>(_empresaOrganizacionRepositorio.GetEmpresaWithOrganizacion(empresaOrganizacionInput.OrganizacionId, empresaOrganizacionInput.EmpresaId));
        }

        public GetAllLocalidadesByNotInfoTributariaPaisOutput GetAllLocalidadesByNotInfoTributariaPais(GetAllLocalidadesByNotInfoTributariaPaisInput infoTributariaAndPais)
        {
            GetAllLocalidadesByPaisWithFilterInput listaInfoTributariaLocalidades = new GetAllLocalidadesByPaisWithFilterInput();
            listaInfoTributariaLocalidades.listaLocalidadesQueNoSeMuestra = _infoTributariaLocalidadRepositorio.GetAllWithLocalidadByInfoTributariaAndPais(infoTributariaAndPais.InfoTributariaId, infoTributariaAndPais.PaisId);
            listaInfoTributariaLocalidades.PaisId = infoTributariaAndPais.PaisId;

            GetAllLocalidadesByPaisWithFilterOutput listaInfoTributariaLocalidadesFiltradas = _zonificacionService.GetAllLocalidadesByPaisWithFilter(listaInfoTributariaLocalidades);

            return new GetAllLocalidadesByNotInfoTributariaPaisOutput { InfoTributariaLocalidades = Mapper.Map<List<LocalidadesByNotInfoTributariaPaisOutput>>(listaInfoTributariaLocalidadesFiltradas.Localidades) };
        }

        public GetAllLocalidadesByNotInfoTributariaDepartamentoOutput GetAllLocalidadesByNotInfoTributariaDepartamento(GetAllLocalidadesByNotInfoTributariaDepartamentoInput infoTributariaAndDepartamento)
        {
            GetAllLocalidadesByDepartamentoWithFilterInput listaInfoTributariaLocalidades = new GetAllLocalidadesByDepartamentoWithFilterInput();
            listaInfoTributariaLocalidades.listaLocalidadesQueNoSeMuestra = _infoTributariaLocalidadRepositorio.GetAllWithLocalidadByInfoTributariaAndDepartamento(infoTributariaAndDepartamento.InfoTributariaId, infoTributariaAndDepartamento.DepartamentoId);
            listaInfoTributariaLocalidades.DepartamentoId = infoTributariaAndDepartamento.DepartamentoId;

            GetAllLocalidadesByDepartamentoWithFilterOutput listaInfoTributariaLocalidadesFiltradas = _zonificacionService.GetAllLocalidadesByDepartamentoWithFilter(listaInfoTributariaLocalidades);

            return new GetAllLocalidadesByNotInfoTributariaDepartamentoOutput { InfoTributariaLocalidades = Mapper.Map<List<LocalidadesByNotInfoTributariaDepartamentoOutput>>(listaInfoTributariaLocalidadesFiltradas.Localidades) };
        }

        public PuedeEliminarActividadEconomicaOutput PuedeEliminarActividadEconomica(PuedeEliminarActividadEconomicaInput actividadEliminar)
        {
            var listaEmpresas = _empresaRepositorio.GetAllList().Where(em => em.ActividadEconomicaId == actividadEliminar.Id);
            PuedeEliminarActividadEconomicaOutput puede = new PuedeEliminarActividadEconomicaOutput();

            if (listaEmpresas.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

       
       public  void DeleteActividadEconomica(DeleteActividadEconomicaInput actividadEliminar)
       {
           var listaEmpresas = _empresaRepositorio.GetAllList().Where(em => em.ActividadEconomicaId == actividadEliminar.Id);
           if (listaEmpresas.Count() == 0)
           {
               _actividadEconomicaRepositorio.Delete(actividadEliminar.Id);
           }
           else
           {
               var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_eliminarPais");
               throw new UserFriendlyException(mensajeError);
           }

        }

        //  Método para obtener la lista de teléfonos de una empresa
        public GetAllTelefonosEmpresaOutput GetAllTelefonosEmpresa(GetAllTelefonosEmpresaInput empresaInput)
        {
            var listaTelefonos = _empresaTelefonoRepositorio.GetAllTelefonosWithLocalidad(empresaInput.EmpresaId);
            return new GetAllTelefonosEmpresaOutput { Telefonos = Mapper.Map<List<TelefonoEmpresaOutput>>(listaTelefonos) };
        }

        //  Método para actualizar la lista de teléfonos de una empresa
        public void UpdateEmpresaTelefono(UpdateEmpresaTelefonoInput empresaTelefonoUpdate)
        {
            foreach (EmpresaTelefonoInput telefono in empresaTelefonoUpdate.Telefonos)
            {
                if (telefono.Accion.Equals("N"))
                {
                    telefono.EmpresaId = empresaTelefonoUpdate.EmpresaId;
                    _empresaTelefonoRepositorio.Insert(Mapper.Map<EmpresaTelefono>(telefono));
                }
                else if (telefono.Accion.Equals("E"))
                {
                    _empresaTelefonoRepositorio.Delete(telefono.Id);
                }
            }
        }

        //  Método para obtener los contactos web de una empresa
        public GetContactosWebEmpresaOutput GetContactosWebEmpresa(GetContactosWebEmpresaInput empresaInput)
        {
            var listaContactosWeb = _empresaContactoWebRepositorio.GetAllContactosWebWithTipo(empresaInput.EmpresaId);
            return new GetContactosWebEmpresaOutput { ContactosWeb = Mapper.Map<List<ContactoWebEmpresaOutput>>(listaContactosWeb) };
        }

        //  Método para obtener los contactos web sin los ya incluidos en la empresa
        public GetContactosWebFilterByEmpresaOutput GetContactosWebFilterByEmpresa(GetContactosWebFilterByEmpresaInput empresaInput)
        {
            var listaContactosYaAsignados = _empresaContactoWebRepositorio.GetAllContactosWebWithTipo(empresaInput.EmpresaId);

            var listaContactosWebFiltrados = _parametrosService.GetAllTiposWithFilter(new GetAllTiposWithFilterInput { Tipos = Mapper.Map<List<TipoInput>>(listaContactosYaAsignados), ParametroNombre = BowConsts.PARAMETRO_MEDIOS_DE_CONTACTO }).Tipos;

            return new GetContactosWebFilterByEmpresaOutput { ContactosWeb = Mapper.Map<List<TipoContactoWebEmpresaOutput>>(listaContactosWebFiltrados) };  
        }

        //  Método para actualizar la lista de contactos web de una empresa
        public void UpdateEmpresaContactosWeb(UpdateEmpresaContactosWebInput empresaContactoWebUpdate)
        {
            foreach (EmpresaContactoWebInput contactoWeb in empresaContactoWebUpdate.ContactosWeb)
            {
                if (contactoWeb.Accion.Equals("N"))
                {
                    contactoWeb.EmpresaId = empresaContactoWebUpdate.EmpresaId;
                    _empresaContactoWebRepositorio.Insert(Mapper.Map<EmpresaContactoWeb>(contactoWeb));
                }
                else if (contactoWeb.Accion.Equals("M"))
                {
                    contactoWeb.EmpresaId = empresaContactoWebUpdate.EmpresaId;
                    _empresaContactoWebRepositorio.Update(Mapper.Map<EmpresaContactoWeb>(contactoWeb));
                }
                else if (contactoWeb.Accion.Equals("E"))
                {
                    _empresaContactoWebRepositorio.Delete(contactoWeb.Id);
                }
            }
        }

        //  Método para obtener los contactos de una empresa
        public GetContactosEmpresaOutput GetContactosEmpresa(GetContactosEmpresaInput empresaInput)
        {
            List<EmpresaContacto> listaContactos = _empresaContactoRepositorio.GetAllContactosEmpresaWithTipo(empresaInput.EmpresaId);
            GetContactosEmpresaOutput resultado = new GetContactosEmpresaOutput { Contactos = new List<ContactoEmpresaOutput>() };
            ContactoEmpresaOutput contactoLista = new ContactoEmpresaOutput();
            foreach (EmpresaContacto contacto in listaContactos)
            {
                contactoLista = Mapper.Map<ContactoEmpresaOutput>(contacto);
                GetTelefonosPersonaOutput resultadoListaTelefonos = _personasService.GetTelefonosPersona(new GetTelefonosPersonaInput { PersonaId = contacto.PersonaId });
                contactoLista.TelefonosContacto = String.Join(", ", resultadoListaTelefonos.TelefonosPersona.Select(tel => tel.TelefonoNumero));
                resultado.Contactos.Add(contactoLista);
            }
            
            return resultado;
        }

        //  Método para obtener los tipos de areas empresariales sin los ya incluidos en la empresa
        public GetTiposAreaFilterByEmpresaOutput GetTiposAreaFilterByEmpresa(GetTiposAreaFilterByEmpresaInput empresaInput)
        {
            var listaTiposYaAsignados = _empresaContactoRepositorio.GetAllContactosEmpresaWithTipo(empresaInput.EmpresaId);

            var listaTiposFiltrados = _parametrosService.GetAllTiposWithFilter(new GetAllTiposWithFilterInput { Tipos = Mapper.Map<List<TipoInput>>(listaTiposYaAsignados), ParametroNombre = BowConsts.PARAMETRO_AREA_EMPRESA }).Tipos;

            return new GetTiposAreaFilterByEmpresaOutput { TiposAreaEmpresa = Mapper.Map<List<TiposAreaEmpresaOutput>>(listaTiposFiltrados) };
        }

        //  Método para actualizar la lista de contactos web de una empresa
        public void UpdateEmpresaContactos(UpdateEmpresaContactosInput empresaContactoUpdate)
        {
            foreach (EmpresaContactoInput contacto in empresaContactoUpdate.Contactos)
            {
                if (contacto.Accion.Equals("N"))
                {
                    contacto.EmpresaId = empresaContactoUpdate.EmpresaId;
                    _empresaContactoRepositorio.Insert(Mapper.Map<EmpresaContacto>(contacto));
                }
                else if (contacto.Accion.Equals("M"))
                {
                    contacto.EmpresaId = empresaContactoUpdate.EmpresaId;
                    _empresaContactoRepositorio.Update(Mapper.Map<EmpresaContacto>(contacto));
                }
                else if (contacto.Accion.Equals("E"))
                {
                    _empresaContactoRepositorio.Delete(contacto.Id);
                }
            }
        }

        /************************************************************************************
         * Empresa Información Tributaria
         * **********************************************************************************/

        //  Método para obtener la lista de información tributaria según la localidad
        public GetAllInfoTributariaByLocalidadOutput GetAllInfoTributariaByLocalidad(GetAllInfoTributariaByLocalidadInput localidadEmpresaInput)
        {
            List<InfoTributaria> listaInfoTributarias = _infoTributariaLocalidadRepositorio.GetAllInfoTributariaActivasByLocalidad(localidadEmpresaInput.LocalidadId);
            List<InfoTributaria> listaInfoTributariasAsignadas = _empresaInfoTributariaRepositorio.GetAllInfoTributariaByEmpresa(localidadEmpresaInput.EmpresaId);
            listaInfoTributarias = listaInfoTributarias.Except(listaInfoTributariasAsignadas).ToList();
            return new GetAllInfoTributariaByLocalidadOutput { InfoTributarias = Mapper.Map<List<InfoTributariaByLocalidadOutput>>(listaInfoTributarias) };
        }

        //  Método para obtener las opciones de información tributaria de una empresa
        public GetAllOpcionesInfoTributariaEmpresaOutput GetAllOpcionesInfoTributariaEmpresa(GetAllOpcionesInfoTributariaEmpresaInput empresaInput)
        {
            List<EmpresaInfoTributaria> listaOpcionesInfoTributaria = _empresaInfoTributariaRepositorio.GetAllOpcionesInfoTributariaByEmpresa(empresaInput.EmpresaId);
            return new GetAllOpcionesInfoTributariaEmpresaOutput { OpcionesInfoTributaria = Mapper.Map<List<OpcionesInfoTributariaEmpresaOutput>>(listaOpcionesInfoTributaria) };
        }

        //  Método para actualizar la lista de opciones de información tributaria de la empresa
        public void UpdateEmpresaInfoTributaria(UpdateEmpresaInfoTributariaInput empresaInfoTributariaUpdate)
        {
            foreach (EmpresaInfoTributariaInput infoTributaria in empresaInfoTributariaUpdate.InfoTributarias)
            {
                infoTributaria.EmpresaId = empresaInfoTributariaUpdate.EmpresaId;
                infoTributaria.FechaActualizacion = DateTime.Now;
                if (infoTributaria.Accion.Equals("N"))
                {
                    EmpresaInfoTributaria EmpresaInfo = _empresaInfoTributariaRepositorio.GetAll().Where(inf => inf.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId == infoTributaria.InfoTributariaId && inf.FechaFin != null).OrderByDescending(inf => inf.FechaFin).FirstOrDefault();
                    if (EmpresaInfo == null)
                    {
                        _empresaInfoTributariaRepositorio.Insert(Mapper.Map<EmpresaInfoTributaria>(infoTributaria));
                    }
                    else
                    {
                        if (EmpresaInfo.FechaFin.Value.Date > infoTributaria.FechaInicio.Date)
                        {
                            var mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresaOrganizacion_infoTributaria_fechaInvalida");
                            throw new UserFriendlyException(mensajeError);
                        }
                        else
                        {
                            _empresaInfoTributariaRepositorio.Insert(Mapper.Map<EmpresaInfoTributaria>(infoTributaria));
                        }
                    }
                    
                }
                else if (infoTributaria.Accion.Equals("M"))
                {
                    _empresaInfoTributariaRepositorio.Update(Mapper.Map<EmpresaInfoTributaria>(infoTributaria));
                }
                else if (infoTributaria.Accion.Equals("E"))
                {
                    infoTributaria.FechaFin = DateTime.Now;
                    _empresaInfoTributariaRepositorio.Update(Mapper.Map<EmpresaInfoTributaria>(infoTributaria));
                }
            }
        }

        /************************************************************************************
         * Sucursal Empresa
         * **********************************************************************************/
        
        //  Método para guardar una sucursal de la empresa
        public SaveSucursalEmpresaOutput SaveSucursalEmpresa(SaveSucursalEmpresaInput empresaSucursalSave)
        {
            Sucursal existeSucursal = _sucursalRepositorio.FirstOrDefault(e => e.Nombre.ToLower() == empresaSucursalSave.Nombre.ToLower() && e.EmpresaOrganizacionId == empresaSucursalSave.EmpresaOrganizacionId);
            SaveSucursalEmpresaOutput resultado = new SaveSucursalEmpresaOutput();
            if (existeSucursal != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresaOrganizacion_sucursal_guardar_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                resultado.Id = _sucursalRepositorio.InsertAndGetId(Mapper.Map<Sucursal>(empresaSucursalSave));
            }
            return resultado;
        }

        //  Método para actualizar una sucursal de la empresa
        public void UpdateSucursalEmpresa(UpdateSucursalEmpresaInput empresaSucursalUpdate)
        {
            Sucursal existeSucursal = _sucursalRepositorio.FirstOrDefault(e => e.Nombre.ToLower() == empresaSucursalUpdate.Nombre.ToLower() && e.EmpresaOrganizacionId == empresaSucursalUpdate.EmpresaOrganizacionId && e.Id != empresaSucursalUpdate.Id);
            if (existeSucursal != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empresas_empresaOrganizacion_sucursal_actualizar_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                _sucursalRepositorio.Update(Mapper.Map<Sucursal>(empresaSucursalUpdate));
            }
        }

        //  Método para actualizar la lista de teléfonos de una sucursal de una empresa
        public void UpdateSucursalEmpresaTelefono(UpdateSucursalEmpresaTelefonoInput sucursalTelefonoUpdate)
        {
            foreach (SucursalTelefonoInput sucursalTelefono in sucursalTelefonoUpdate.Telefonos)
            {
                if (sucursalTelefono.Accion.Equals("N"))
                {
                    sucursalTelefono.SucursalId = sucursalTelefonoUpdate.SucursalId;
                    _sucursalTelefonoRepositorio.Insert(Mapper.Map<SucursalTelefono>(sucursalTelefono));
                }
                else if (sucursalTelefono.Accion.Equals("E"))
                {
                    _sucursalTelefonoRepositorio.Delete(sucursalTelefono.Id);
                }
            }
        }

        //  Método para obtener la lista de sucursales de una empresa
        public GetAllSucursalesEmpresaOutput GetAllSucursalesEmpresa(GetAllSucursalesEmpresaInput empresaOrganizacionInput)
        {
            var listaSucursales = _sucursalRepositorio.GetAllSucursalesWithTipoByEmpresa(empresaOrganizacionInput.EmpresaOrganizacionId);
            return new GetAllSucursalesEmpresaOutput { Sucursales = Mapper.Map<List<SucursalOutput>>(listaSucursales) };
        }

        //  Método para obtener la información de la sucursal de una empresa con la información de los telefonos
        public GetSucursalEmpresaOrganizacionOutput GetSucursalEmpresaOrganizacion(GetSucursalEmpresaOrganizacionInput sucursalEmpresaOrganizacionInput)
        {
            GetSucursalEmpresaOrganizacionOutput respuestaSucursal = Mapper.Map<GetSucursalEmpresaOrganizacionOutput>(_sucursalRepositorio.GetSucursalWithTipoAndEstadoAndDireccion(sucursalEmpresaOrganizacionInput.EmpresaOrganizacionId, sucursalEmpresaOrganizacionInput.SucursalId));
            respuestaSucursal.Telefonos = Mapper.Map<List<TelefonoSucursalOutput>>(_sucursalTelefonoRepositorio.GetAllTelefonosWithLocalidad(sucursalEmpresaOrganizacionInput.SucursalId));
            return respuestaSucursal;
        }

        //  Método para obtener toda la lista de sucursales existentes
        public GetAllSucursalesOutput GetAllSucursales()
        {
            var listaSucursales = _sucursalRepositorio.GetAllSucursalesWithEmpresaAndOrganizacion();
            return new GetAllSucursalesOutput { Sucursales = Mapper.Map<List<SucursalesOutput>>(listaSucursales) };
        }

        //  Método para obtener la lista de convenios de recaudo masivo
        public GetAllConveniosRecaudoMasivoOutput GetAllConveniosRecaudoMasivo()
        {
            var listaConvenios = _recaudoMasivoRepositorio.GetAllList();
            return new GetAllConveniosRecaudoMasivoOutput { Convenios = Mapper.Map<List<RecaudoMasivoOutput>>(listaConvenios) };
        }

        //  Método para obtener la información de la sucursal de una empresa con la información de los telefonos
        public GetSucursalByIdWithEmpresaAndOrganizacionOutput GetSucursalByIdWithEmpresaAndOrganizacion(GetSucursalByIdWithEmpresaAndOrganizacionInput sucursal)
        {
            return Mapper.Map<GetSucursalByIdWithEmpresaAndOrganizacionOutput>(_sucursalRepositorio.GetSucursalByIdWithEmpresaAndOrganizacion(sucursal.SucursalId));
        }


        ////  Método para obtener la lista de información tributaria según la localidad Id
        //public GetAllInfoTributariaByLocalidadIdOutput GetAllInfoTributariaByLocalidadId(GetAllInfoTributariaByLocalidadIdInput localidadInput)
        //{
        //    var listaInfoTributariaLocalidad = _infoTributariaLocalidadRepositorio.GetAll().Where(l => l.LocalidadId == localidadInput.LocalidadId);
        //    return new GetAllInfoTributariaByLocalidadIdOutput { InfoTributarias = Mapper.Map<List<InfoTributariaByLocalidadIdOutput>>(listaInfoTributariaLocalidad) };
        //}
    }
}
