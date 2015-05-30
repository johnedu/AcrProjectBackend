using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using Bow.Afiliaciones;
using Bow.Afiliaciones.DTOs.InputModels;
using Bow.Empresas;
using Bow.Empresas.DTOs.InputModels;
using Bow.Parametros;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Parametros.Entidades;
using Bow.Parametros.Repositorios;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using Bow.Zonificacion.Entidades;
using Bow.Zonificacion.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion
{
    public class ZonificacionService : IZonificacionService
    {
        #region Repositorios
        private IPaisRepositorio _paisRepositorio;
        private IDepartamentoRepositorio _departamentoRepositorio;
        private ILocalidadRepositorio _localidadRepositorio;
        private ITipoOrientacionRepositorio _tipoOrientacionRepositorio;
        private ITorieLocalidadRepositorio _torieLocalidadRepositorio;
        private ISufijoRepositorio _sufijoRepositorio;
        private ISufijoLocalidadRepositorio _sufijoLocalidadRepositorio;
        private IAvenidaRepositorio _avenidaRepositorio;
        private IBarrioRepositorio _barrioRepositorio;
        private IManzanaRepositorio _manzanaRepositorio;
        private IZonaRepositorio _zonaRepositorio;
        private IDireccionRepositorio _direccionRepositorio;
        private IZonaBarrioRepositorio _zonabarrioRepositorio;
        private ITelefonoRepositorio _telefonoRepositorio;
        private ITipoRepositorio _tipoRepositorio;
        private IZonaEmpleadoRepositorio _zonaEmpleadoRepositorio;

        private IParametrosService _parametrosService;

        public IAbpSession AbpSession { get; set; }

        #endregion

        //Inyección de Dependencia en el Servicio
        public ZonificacionService(IPaisRepositorio paisRepositorio,
                                    IDepartamentoRepositorio departamentoRepositorio,
                                    ILocalidadRepositorio localidadRepositorio,
                                    ITipoOrientacionRepositorio tipoOrientacionRepositorio,
                                    ITorieLocalidadRepositorio torieLocalidadRepositorio,
                                    ISufijoRepositorio sufijoRepositorio,
                                    ISufijoLocalidadRepositorio sufijoLocalidadRepositorio,
                                    IAvenidaRepositorio avenidaRepositorio,
                                    IBarrioRepositorio barrioRepositorio,
                                    IManzanaRepositorio manzanaRepositorio,
                                    IZonaRepositorio zonaRepositorio,
                                    IDireccionRepositorio direccionRepositorio,
                                    IZonaBarrioRepositorio zonabarrioRepositorio,
                                    ITelefonoRepositorio telefonoRepositorio,
                                    ITipoRepositorio tipoRepositorio,
                                    IZonaEmpleadoRepositorio zonaEmpleadoRepositorio,
                                    IParametrosService parametrosService
            )
        {
            _paisRepositorio = paisRepositorio;
            _departamentoRepositorio = departamentoRepositorio;
            _localidadRepositorio = localidadRepositorio;
            _barrioRepositorio = barrioRepositorio;
            _tipoOrientacionRepositorio = tipoOrientacionRepositorio;
            _torieLocalidadRepositorio = torieLocalidadRepositorio;
            _sufijoRepositorio = sufijoRepositorio;
            _sufijoLocalidadRepositorio = sufijoLocalidadRepositorio;
            _avenidaRepositorio = avenidaRepositorio;
            _manzanaRepositorio = manzanaRepositorio;
            _direccionRepositorio = direccionRepositorio;
            _zonaRepositorio = zonaRepositorio;
            _zonabarrioRepositorio = zonabarrioRepositorio;
            _telefonoRepositorio = telefonoRepositorio;
            _tipoRepositorio = tipoRepositorio;
            _zonaEmpleadoRepositorio = zonaEmpleadoRepositorio;
            _parametrosService = parametrosService;
            AbpSession = NullAbpSession.Instance;


        }

        public GetPaisOutput GetPais(EsPaisUsaInput paisInput)
        {
            return Mapper.Map<GetPaisOutput>(_paisRepositorio.Get(paisInput.Id));
        }

        public GetLocalidadOutput GetLocalidad(GetLocalidadInput localidadInput)
        {
            return Mapper.Map<GetLocalidadOutput>(_localidadRepositorio.Get(localidadInput.Id));
        }

        public GetPaisesOutput GetPaises()
        {
            var listaPaises = _paisRepositorio.GetAllList().OrderBy(p => p.Nombre);
            return new GetPaisesOutput { Paises = Mapper.Map<List<PaisOutput>>(listaPaises) };
        }

        //Metodo para identificar las localidades que pertenecen a un departamento
        public GetLocalidadesOutput GetLocalidades(GetLocalidadesInput departamentoInput)
        {
            var listaLocalidades = _localidadRepositorio.GetAllList().Where(l => l.DepartamentoId == departamentoInput.Id).OrderBy(l => l.Nombre);
            return new GetLocalidadesOutput { Localidades = Mapper.Map<List<LocalidadOutput>>(listaLocalidades) };
        }

        //Metodo para identificar las localidades que pertenecen a un pais
        public GetLocalidadesByPaisOutput GetLocalidadesByPais(GetLocalidadesByPaisInput paisInput)
        {
            var listaLocalidades = _localidadRepositorio.GetAll().Where(l => l.DepartamentoLocalidad.PaisId == paisInput.Id).OrderBy(l => l.Nombre).ToList();
            return new GetLocalidadesByPaisOutput { Localidades = Mapper.Map<List<LocalidadOutput>>(listaLocalidades) };
        }

        //Metodo para obtener todas las localidades con el departamento y el pais al que pertenece
        public GetAllLocalidadesOutput GetAllLocalidades()
        {
            var listaLocalidades = _localidadRepositorio.GetAllWithDepartamentoAndPais().OrderBy(l => l.Nombre);
            return new GetAllLocalidadesOutput { Localidades = Mapper.Map<List<LocalidadDepartamentoPaisOutput>>(listaLocalidades) };
        }

        //  Metodo para obtener todas las localidades con el departamento y el pais al que pertenece sin las localidades de otra lista
        public GetAllLocalidadesWithFilterOutput GetAllLocalidadesWithFilter(GetAllLocalidadesWithFilterInput listaLocalidadesARemover)
        {
            var listaLocalidades = _localidadRepositorio.GetAllWithDepartamentoAndPais().Except(Mapper.Map<List<Localidad>>(listaLocalidadesARemover.listaLocalidadesQueNoSeMuestra));
            return new GetAllLocalidadesWithFilterOutput { Localidades = Mapper.Map<List<LocalidadDepartamentoPaisWithFilterOutput>>(listaLocalidades) };
        }

        //  Metodo para obtener todas las localidades con el departamento y el pais de un pais al que pertenecen sin las localidades de otra lista
        public GetAllLocalidadesByPaisWithFilterOutput GetAllLocalidadesByPaisWithFilter(GetAllLocalidadesByPaisWithFilterInput listaLocalidadesAQuitar)
        {
            var listaLocalidades = _localidadRepositorio.GetAllWithDepartamentoAndPaisByPais(listaLocalidadesAQuitar.PaisId).Except(listaLocalidadesAQuitar.listaLocalidadesQueNoSeMuestra);
            return new GetAllLocalidadesByPaisWithFilterOutput { Localidades = Mapper.Map<List<LocalidadDepartamentoPaisWithFilterOutput>>(listaLocalidades) };
        }

        //  Metodo para obtener todas las localidades con el departamento y el pais de un departamento al que pertenece sin las localidades de otra lista
        public GetAllLocalidadesByDepartamentoWithFilterOutput GetAllLocalidadesByDepartamentoWithFilter(GetAllLocalidadesByDepartamentoWithFilterInput listaLocalidadesAQuitar)
        {
            var listaLocalidades = _localidadRepositorio.GetAllWithDepartamentoAndPaisByDepartamento(listaLocalidadesAQuitar.DepartamentoId).Except(listaLocalidadesAQuitar.listaLocalidadesQueNoSeMuestra);
            return new GetAllLocalidadesByDepartamentoWithFilterOutput { Localidades = Mapper.Map<List<LocalidadDepartamentoPaisWithFilterOutput>>(listaLocalidades) };
        }

        public void SavePais(SavePaisInput nuevoPais)
        {
            nuevoPais.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoPais.Nombre.ToLower());
            Pais existePais = _paisRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoPais.Nombre.ToLower());

            if (existePais == null)
            {
                Pais nuevo = Mapper.Map<Pais>(nuevoPais);
                nuevo.TenantId = AbpSession.GetTenantId();
                _paisRepositorio.Insert(nuevo);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void SaveBarrio(SaveBarrioInput nuevoBarrio)
        {

            nuevoBarrio.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoBarrio.Nombre.ToLower());
            Barrio existeBarrio = _barrioRepositorio.FirstOrDefault(b => b.Nombre.ToLower() == nuevoBarrio.Nombre.ToLower() && nuevoBarrio.LocalidadId == b.LocalidadId);

            if (existeBarrio == null)
            {
                _barrioRepositorio.Insert(Mapper.Map<Barrio>(nuevoBarrio));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_barrio_validarNombreBarrio");
                throw new UserFriendlyException(mensajeError);
            }

        }

        public PuedeEliminarPaisOutput PuedeEliminarPais(PuedeEliminarPaisInput paisEliminar)
        {
            var listaDepartamentos = _departamentoRepositorio.GetAllList().Where(d => d.PaisId == paisEliminar.Id);
            PuedeEliminarPaisOutput puede = new PuedeEliminarPaisOutput();

            if (listaDepartamentos.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        public PuedeEliminarDepartamentoOutput PuedeEliminarDepartamento(PuedeEliminarDepartamentoInput deptoEliminar)
        {
            var listaLocalidades = _localidadRepositorio.GetAllList().Where(l => l.DepartamentoId == deptoEliminar.Id);
            PuedeEliminarDepartamentoOutput puede = new PuedeEliminarDepartamentoOutput();

            if (listaLocalidades.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        public PuedeEliminarManzanaOutput PuedeEliminarManzana(PuedeEliminarManzanaInput manzanaEliminar)
        {
            PuedeEliminarManzanaOutput puede = new PuedeEliminarManzanaOutput();
            puede.PuedeEliminar = true;
            Manzana manzanaConsultar = _manzanaRepositorio.Get(manzanaEliminar.Id);
            if (manzanaConsultar.Direcciones.Count > 0)
                puede.PuedeEliminar = false;

            return puede;
        }

        public PuedeEliminarBarrioOutput PuedeEliminarBarrio(PuedeEliminarBarrioInput barrioEliminar)
        {

            var listaManzanas = _manzanaRepositorio.GetAllList().Where(m => m.BarrioId == barrioEliminar.Id);
            PuedeEliminarBarrioOutput puede = new PuedeEliminarBarrioOutput();

            if (listaManzanas.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        public PuedeEliminarLocalidadOutput PuedeEliminarLocalidad(PuedeEliminarLocalidadInput localidadEliminar)
        {
            PuedeEliminarLocalidadOutput puede = new PuedeEliminarLocalidadOutput();

            var localidad = _localidadRepositorio.Get(localidadEliminar.Id);

            if (localidad.Barrios.Count() == 1)
            {
                if (localidad.Barrios.First().Nombre == BowConsts.BARRIO_NO_IDENTIFICADO)
                {
                    if (localidad.Zonas.Count() == 0)
                    {
                        if (localidad.TiposOrientacionLocalidad.Count() == 0)
                        {
                            if (localidad.SufijosLocalidad.Count() == 0)
                            {
                                if (localidad.InfoTributariaLocalidades.Count() == 0)
                                {
                                    if (localidad.Telefonos.Count() == 0)
                                    {
                                        if (localidad.RecaudoMasivoCobertura.Count() == 0)
                                        {
                                            if (localidad.AfiliadosProspecto.Count() == 0)
                                            {
                                                if (localidad.LocalidadGestionProspecto.Count() == 0)
                                                {
                                                    puede.PuedeEliminar = true;
                                                }
                                                else
                                                {
                                                    puede.PuedeEliminar = false;
                                                }
                                            }
                                            else
                                            {
                                                puede.PuedeEliminar = false;
                                            }
                                        }
                                        else
                                        {
                                            puede.PuedeEliminar = false;
                                        }
                                    }
                                    else
                                    {
                                        puede.PuedeEliminar = false;
                                    }
                                }
                                else
                                {
                                    puede.PuedeEliminar = false;
                                }
                            }
                            else
                            {
                                puede.PuedeEliminar = false;
                            }
                        }
                        else
                        {
                            puede.PuedeEliminar = false;
                        }
                    }
                    else
                    {
                        puede.PuedeEliminar = false;
                    }
                }
                else
                {
                    puede.PuedeEliminar = false;
                }
            }
            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        public void DeletePais(DeletePaisInput paisEliminar)
        {
            var listaDepartamentos = _departamentoRepositorio.GetAllList().Where(d => d.PaisId == paisEliminar.Id);
            if (listaDepartamentos.Count() == 0)
            {
                _paisRepositorio.Delete(paisEliminar.Id);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_eliminarPais");
                throw new UserFriendlyException(mensajeError);
            }

        }

        public void DeleteBarrio(DeleteBarrioInput barrioEliminar)
        {
            var listaManzanas = _manzanaRepositorio.GetAllList().Where(m => m.BarrioId == barrioEliminar.Id);
            if (listaManzanas.Count() == 0)
            {
                _barrioRepositorio.Delete(barrioEliminar.Id);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_eliminarPais");
                throw new UserFriendlyException(mensajeError);
            }

        }

        public void UpdatePais(UpdatePaisInput paisUpdate)
        {
            paisUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(paisUpdate.Nombre.ToLower());
            Pais existePais = _paisRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == paisUpdate.Nombre.ToLower() && p.Id != paisUpdate.Id);

            if (existePais == null)
            {
                var paisActualizar = _paisRepositorio.Update(Mapper.Map<Pais>(paisUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_pais_validarNombrePais");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void DeleteLocalidad(DeleteLocalidadInput localidadEliminar)
        {
            var localidad = _localidadRepositorio.Get(localidadEliminar.Id);

            if (localidad.Barrios.Count() == 1)
            {
                if (localidad.Barrios.First().Nombre == BowConsts.BARRIO_NO_IDENTIFICADO)
                {
                    _barrioRepositorio.Delete(localidad.Barrios.First().Id);

                    _localidadRepositorio.Delete(localidadEliminar.Id);
                }
            }

        }

        /********************************* DEPARTAMENTOS *************************************************/
        //Metodo para guardar un departamento.
        public void SaveDepartamento(SaveDepartamentoInput nuevoDepartamento)
        {
            nuevoDepartamento.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoDepartamento.Nombre.ToLower());
            Departamento existeDepto = _departamentoRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevoDepartamento.Nombre.ToLower() && d.PaisId == nuevoDepartamento.PaisId);

            if (existeDepto == null)
            {
                _departamentoRepositorio.Insert(Mapper.Map<Departamento>(nuevoDepartamento));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_departamento_validarNombreDpto");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void SaveLocalidad(SaveLocalidadInput nuevaLocalidad)
        {
            nuevaLocalidad.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaLocalidad.Nombre.ToLower());
            Localidad existeLoc = _localidadRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == nuevaLocalidad.Nombre.ToLower());

            if (existeLoc == null)
            {
                var NuevaLocalidad = _localidadRepositorio.Insert(Mapper.Map<Localidad>(nuevaLocalidad));

                SaveBarrioInput nuevoBarrio = new SaveBarrioInput();
                nuevoBarrio.Nombre = BowConsts.BARRIO_NO_IDENTIFICADO;
                nuevoBarrio.LocalidadId = NuevaLocalidad.Id;
                SaveBarrio(nuevoBarrio);

            }
            else
            {
                throw new UserFriendlyException(LocalizationHelper.GetString("Bow", "zonificacion_localidad_existeLocalidad"));
            }
        }

        //Metodo para identificar los departamentos que pertenecen a un pais
        public GetDepartamentosOutput GetDepartamentos(GetDepartamentosInput paisInput)
        {
            var listaDeptos = _departamentoRepositorio.GetAllList().Where(d => d.PaisId == paisInput.Id).OrderBy(d => d.Nombre);
            return new GetDepartamentosOutput { Departamentos = Mapper.Map<List<DepartamentoOutput>>(listaDeptos) };
        }

        //Metodo para listar todos los departamentos
        public GetAllDepartamentosOutput GetAllDepartamentos()
        {
            var listaDeptos = _departamentoRepositorio.GetAllList().OrderBy(d => d.Nombre);
            return new GetAllDepartamentosOutput { Departamentos = Mapper.Map<List<DepartamentoOutput>>(listaDeptos) };
        }

        //Metodo para identificar los datos del departamento para editar
        public GetDepartamentoOutput GetDepartamento(GetDepartamentoInput departamentoInput)
        {
            return Mapper.Map<GetDepartamentoOutput>(_departamentoRepositorio.Get(departamentoInput.Id));
        }

        //Metodo para eliminar un departamento.
        public void DeleteDepartamento(DeleteDepartamentoInput departamentoEliminar)
        {
            _departamentoRepositorio.Delete(departamentoEliminar.Id);
        }

        //Metodo para actualizar un departamento.
        public void UpdateDepartamento(UpdateDepartamentoInput departamentoUpdate)
        {
            departamentoUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(departamentoUpdate.Nombre.ToLower());
            Departamento existeDepto = _departamentoRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == departamentoUpdate.Nombre.ToLower() && d.PaisId == departamentoUpdate.PaisId && d.Id != departamentoUpdate.Id);

            if (existeDepto == null)
            {
                var departamentoActualizar = _departamentoRepositorio.Update(Mapper.Map<Departamento>(departamentoUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_departamento_validarNombreDpto");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public GetLocalidadWithDepartamentoAndPaisOutput GetLocalidadWithDepartamentoAndPais(GetLocalidadWithDepartamentoAndPaisInput localidadInput)
        {
            Localidad localidadConsulta = _localidadRepositorio.GetWithDepartamentoAndPais(localidadInput.Id);
            return Mapper.Map<GetLocalidadWithDepartamentoAndPaisOutput>(localidadConsulta);
        }

        public GetLocalidadByIdWithDepartamentoAndPaisOutput GetLocalidadByIdWithDepartamentoAndPais(GetLocalidadByIdWithDepartamentoAndPaisInput localidad)
        {
            Localidad localidadConsulta = _localidadRepositorio.GetWithDepartamentoAndPais(localidad.Id);
            return Mapper.Map<GetLocalidadByIdWithDepartamentoAndPaisOutput>(localidadConsulta);
        }

        /************************************************************************************
         * TIPOS DE ORIENTACION 
         * **********************************************************************************/
        public void SaveTipoOrientacion(SaveTipoOrientacionInput nuevoTipoOrientacion)
        {
            nuevoTipoOrientacion.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoTipoOrientacion.Nombre.ToLower());
            TipoOrientacion existeTipo = _tipoOrientacionRepositorio.FirstOrDefault(t => t.Nombre.ToLower() == nuevoTipoOrientacion.Nombre.ToLower());

            if (existeTipo == null)
                _tipoOrientacionRepositorio.Insert(Mapper.Map<TipoOrientacion>(nuevoTipoOrientacion));
            else
                throw new UserFriendlyException("Ya existe un tipo de orientación con el nombre indicado");
        }

        public GetTiposOrientacionOutput GetTiposOrientacion()
        {
            var tiposOrientacion = _tipoOrientacionRepositorio.GetAllList().OrderBy(to => to.Nombre);
            return new GetTiposOrientacionOutput { TiposOrientacion = Mapper.Map<List<TipoOrientacionOutput>>(tiposOrientacion) };
        }

        public void UpdateLocalidad(UpdateLocalidadInput localidadUpdate)
        {
            localidadUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(localidadUpdate.Nombre.ToLower());
            Localidad localidadConsultada = _localidadRepositorio.Get(localidadUpdate.Id);
            Localidad existeLocalidad = _localidadRepositorio.FirstOrDefault(l => l.Nombre.ToLower() == localidadUpdate.Nombre.ToLower() && l.DepartamentoId == localidadConsultada.DepartamentoId && l.Id != localidadUpdate.Id);
            if (existeLocalidad == null)
            {
                localidadConsultada.Nombre = localidadUpdate.Nombre;
                localidadConsultada.Habitantes = localidadUpdate.Habitantes;

                var localidadActualizar = _localidadRepositorio.Update(localidadConsultada);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_localidad_validarNombreLocalidad");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void UpdateBarrio(UpdateBarrioInput barrioUpdate)
        {
            barrioUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(barrioUpdate.Nombre.ToLower());
            Barrio barrioConsultado = _barrioRepositorio.Get(barrioUpdate.Id);
            Barrio existeBarrio = _barrioRepositorio.FirstOrDefault(b => b.Nombre.ToLower() == barrioUpdate.Nombre.ToLower() && b.LocalidadId == barrioConsultado.LocalidadId && b.Id != barrioUpdate.Id);
            if (existeBarrio == null)
            {
                barrioConsultado.Nombre = barrioUpdate.Nombre;

                var barrioActualizar = _barrioRepositorio.Update(barrioConsultado);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_localidad_validarNombreBarrio");
                throw new UserFriendlyException(mensajeError);
            }
        }

        public void SaveTipoOrientacionLocalidad(SaveTipoOrientacionLocalidadInput tipoOrientacionLocalidad)
        {
            TorieLocalidad existeTorieLocalidad = _torieLocalidadRepositorio.FirstOrDefault(to => to.TipoOrientacionId == tipoOrientacionLocalidad.TipoOrientacionId && to.LocalidadId == tipoOrientacionLocalidad.LocalidadId);

            if (existeTorieLocalidad == null)
                _torieLocalidadRepositorio.Insert(Mapper.Map<TorieLocalidad>(tipoOrientacionLocalidad));
            else
                throw new UserFriendlyException(LocalizationHelper.GetString("Bow", "zonificacion_parametrosLocalidad_existeTorieLocalidad"));
        }

        public GetToriesLocalidadByLocalidadOutput GetToriesLocalidadByLocalidad(GetToriesLocalidaByLocalidadInput localidad)
        {
            IList<TorieLocalidad> toriesLocalidad = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(localidad.LocalidadId).OrderBy(tl => tl.TipoOrientacionTorieLocalidad.Nombre).ToList();
            return new GetToriesLocalidadByLocalidadOutput { ToriesLocalidad = Mapper.Map<List<GetTorieLocalidadByLocalidadOutput>>(toriesLocalidad) };
        }


        public GetDepartamentoWithPaisOutput GetDepartamentoWithPais(GetDepartamentoWithPaisInput departamento)
        {
            Departamento dpto = _departamentoRepositorio.GetWithPais(departamento.Id);
            return Mapper.Map<GetDepartamentoWithPaisOutput>(dpto);

        }

        public void DeleteTorieLocalidad(DeleteTorieLocalidadInput torieLocalidadEliminar)
        {
            _torieLocalidadRepositorio.Delete(torieLocalidadEliminar.Id);
        }

        public GetTorieLocalidadOutput GetTorieLocalidad(GetTorieLocalidadInput torieLocalidad)
        {
            return Mapper.Map<GetTorieLocalidadOutput>(_torieLocalidadRepositorio.GetWithTipoOrientacion(torieLocalidad.Id));
        }

        //Metodo para cargar el dropdown con los tipos de orientacion que no estan asignados a la localidad
        public GetTiposOrientacionSinAsignarALocalidadOutput GetTiposOrientacionSinAsignarALocalidad(GetTiposOrientacionSinAsignarALocalidadInput localidad)
        {
            List<TipoOrientacion> tipoorientacion = _tipoOrientacionRepositorio.GetAllList();
            List<TorieLocalidad> torieslocalidad = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(localidad.LocalidadId);

            var toriesdisponibles = tipoorientacion.Where(to => !torieslocalidad.Any(tol => tol.TipoOrientacionId == to.Id));

            return new GetTiposOrientacionSinAsignarALocalidadOutput { Tories = Mapper.Map<List<TorieDisponibleOutput>>(toriesdisponibles) };
        }

        //Metodo para consultar si el tipo de orientacion localidad indicado se puede eliminar; si no esta siendo utilizado en manzana
        public PuedeEliminarTorieLocalidadOutput PuedeEliminarTorieLocalidad(PuedeEliminarTorieLocalidadInput torieQuitar)
        {
            var listaTorieLocalidad = _manzanaRepositorio.GetAllList().Where(m => m.TorieLocalidad1Id == torieQuitar.Id || m.TorieLocalidad2Id == torieQuitar.Id);
            PuedeEliminarTorieLocalidadOutput puede = new PuedeEliminarTorieLocalidadOutput();

            if (listaTorieLocalidad.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }


        /************************************************************************************
       *  SUFIJOS
       * **********************************************************************************/

        //Metodo para obtener el nombre del sufijo desde el sufijo_localidad
        public GetSufijoLocalidadWithSufijoOutput GetSufijoLocalidadWithSufijo(GetSufijoLocalidadWithSufijoInput sufijolocalidadInput)
        {
            return Mapper.Map<GetSufijoLocalidadWithSufijoOutput>(_sufijoLocalidadRepositorio.GetWithSufijo(sufijolocalidadInput.Id));
        }

        //Metodo para obtener el listado de sufijos existentes
        public GetSufijosOutput GetSufijos()
        {
            var sufijos = _sufijoRepositorio.GetAll().OrderBy(su => su.Nombre);
            return new GetSufijosOutput { Sufijos = Mapper.Map<List<SufijoOutput>>(sufijos) };
        }

        //Metodo para cargar lista de sufijos por localidad con el nombre del sufijo
        public GetSufijosLocalidadByLocalidadOutput GetSufijosLocalidadByLocalidad(GetSufijosLocalidadByLocalidadInput localidad)
        {
            var sufijoLocalidad = _sufijoLocalidadRepositorio.GetAllListWithSufijoByLocalidad(localidad.LocalidadId);
            return new GetSufijosLocalidadByLocalidadOutput { SufijosLocalidad = Mapper.Map<List<GetSufijoLocalidadByLocalidadOutput>>(sufijoLocalidad) };
        }

        //Metodo para asignar el sufijo a una localidad indicada
        public void SaveSufijoLocalidad(SaveSufijoLocalidadInput sufijoLocalidad)
        {
            SufijoLocalidad existeSufijoLocalidad = _sufijoLocalidadRepositorio.FirstOrDefault(su => su.SufijoId == sufijoLocalidad.sufijoId && su.LocalidadId == sufijoLocalidad.localidadId);

            if (existeSufijoLocalidad == null)
                _sufijoLocalidadRepositorio.Insert(Mapper.Map<SufijoLocalidad>(sufijoLocalidad));
            else
                throw new UserFriendlyException(LocalizationHelper.GetString("Bow", "zonificacion_parametrosLocalidad_existeSufijoLocalidad"));
        }

        //Metodo para agregar un nuevo sufijo
        public void SaveSufijo(SaveSufijoInput nuevoSufijo)
        {
            nuevoSufijo.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoSufijo.Nombre.ToLower());
            Sufijo existeSufijo = _sufijoRepositorio.FirstOrDefault(s => s.Nombre.ToLower() == nuevoSufijo.Nombre.ToLower());

            if (existeSufijo == null)
                _sufijoRepositorio.Insert(Mapper.Map<Sufijo>(nuevoSufijo));
            else
                throw new UserFriendlyException(LocalizationHelper.GetString("Bow", "zonificacion_sufijo_existeSufijo"));
        }

        //Metodo para quitar un sufijo ya asignado a una localidad
        public void DeleteSufijoLocalidad(DeleteSufijoLocalidadInput sufijolocalidadInput)
        {
            _sufijoLocalidadRepositorio.Delete(sufijolocalidadInput.Id);
        }

        //Metodo para cargar el dropdown con los sufijos que no estan asignados a la localidad
        public GetSufijosSinAsignarALocalidadOutput GetSufijosSinAsignarALocalidad(GetSufijosSinAsignarALocalidadInput localidad)
        {
            List<Sufijo> sufijos = _sufijoRepositorio.GetAllList();
            List<SufijoLocalidad> sufijoslocalidad = _sufijoLocalidadRepositorio.GetAllListWithSufijoByLocalidad(localidad.LocalidadId);

            var sufijosdisponibles = sufijos.Where(su => !sufijoslocalidad.Any(sl => sl.SufijoId == su.Id));

            return new GetSufijosSinAsignarALocalidadOutput { Sufijos = Mapper.Map<List<SufijoDisponibleOutput>>(sufijosdisponibles) };

            //List<Sufijo> sufijos = _sufijoRepositorio.GetAllList();
            //List<SufijoLocalidad> sufijoslocalidad = _sufijoLocalidadRepositorio.GetAllListWithSufijoByLocalidad(localidad.LocalidadId);
            //var sufijosdisponibles =
            //from category in sufijoslocalidad
            //join prod in sufijos on category.SufijoId equals prod.Id into prodGroup
            //from item in prodGroup.DefaultIfEmpty(new Sufijo { Nombre = String.Empty, Id = 0 })
            //select new { Nombre = category.SufijoId, ProdName = item.Nombre };
        }

        //Metodo para eliminar una manzana.
        public void DeleteSufijo(DeleteSufijoInput sufijoEliminar)
        {
            _sufijoRepositorio.Delete(sufijoEliminar.Id);
        }

        //Metodo para consultar si el sufijo indicado se puede eliminar.
        public PuedeEliminarSufijoOutput PuedeEliminarSufijo(PuedeEliminarSufijoInput sufijoEliminar)
        {
            var listaSufijos = _sufijoLocalidadRepositorio.GetAllList().Where(m => m.SufijoId == sufijoEliminar.Id);
            PuedeEliminarSufijoOutput puede = new PuedeEliminarSufijoOutput();

            if (listaSufijos.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        //Metodo para consultar si el sufijolocalidad indicado se puede eliminar; si no esta siendo utilizado en manzana
        public PuedeEliminarSufijoLocalidadOutput PuedeEliminarSufijoLocalidad(PuedeEliminarSufijoLocalidadInput sufijoQuitar)
        {
            var listaSufijosLocalidad = _manzanaRepositorio.GetAllList().Where(m => m.SufijoLocalidad1Id == sufijoQuitar.Id || m.SufijoLocalidad2Id == sufijoQuitar.Id);
            PuedeEliminarSufijoLocalidadOutput puede = new PuedeEliminarSufijoLocalidadOutput();

            if (listaSufijosLocalidad.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }

        /************************************************************************************
        *  AVENIDAS
        * **********************************************************************************/

        //Metodo para cargar las avenidas que pertenecen a una localidad
        public GetAvenidasByLocalidadOutput GetAvenidasByLocalidad(GetAvenidasByLocalidadInput localidadInput)
        {
            var listaAvenidas = _avenidaRepositorio.GetAllList().Where(d => d.LocalidadId == localidadInput.Id).OrderBy(d => d.Nombre);
            return new GetAvenidasByLocalidadOutput { Avenidas = Mapper.Map<List<AvenidaOutput>>(listaAvenidas) };
        }

        //Metodo para agregar una Avenida.
        public void SaveAvenida(SaveAvenidaInput nuevoAvenida)
        {
            nuevoAvenida.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoAvenida.Nombre.ToLower());
            Avenida existeAvenida = _avenidaRepositorio.FirstOrDefault(s => s.Nombre.ToLower() == nuevoAvenida.Nombre.ToLower() && s.LocalidadId == nuevoAvenida.LocalidadId);

            if (existeAvenida == null)
                _avenidaRepositorio.Insert(Mapper.Map<Avenida>(nuevoAvenida));
            else
                throw new UserFriendlyException(LocalizationHelper.GetString("Bow", "zonificacion_avenida_existeAvenida"));
        }

        //Metodo para obtener los datos de una Avenida para editar / eliminar
        public GetAvenidaOutput GetAvenida(GetAvenidaInput avenidaInput)
        {
            return Mapper.Map<GetAvenidaOutput>(_avenidaRepositorio.Get(avenidaInput.Id));
        }

        //Metodo para actualizar una Avenida.
        public void UpdateAvenida(UpdateAvenidaInput avenidaUpdate)
        {
            avenidaUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(avenidaUpdate.Nombre.ToLower());
            Avenida existeAvenida = _avenidaRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == avenidaUpdate.Nombre.ToLower() && d.LocalidadId == avenidaUpdate.LocalidadId && d.Id != avenidaUpdate.Id);

            if (existeAvenida == null)
            {
                var avenidaActualizar = _avenidaRepositorio.Update(Mapper.Map<Avenida>(avenidaUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_avenida_existeAvenida");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para eliminar una Avenida.
        public void DeleteAvenida(DeleteAvenidaInput avenidaEliminar)
        {
            _avenidaRepositorio.Delete(avenidaEliminar.Id);
        }

        //Metodo para consultar si el sufijolocalidad indicado se puede eliminar; si no esta siendo utilizado en manzana
        public PuedeEliminarAvenidaOutput PuedeEliminarAvenida(PuedeEliminarAvenidaInput avenidaEliminar)
        {
            TipoOrientacion avenida = _tipoOrientacionRepositorio.GetByNombre(BowConsts.TIPO_ORIENTACION_AVENIDA);
            var listaManzanasLocalidad = _manzanaRepositorio.GetAllList();
            var listaToriesLocalidad = _torieLocalidadRepositorio.GetAllList();

            var consultaAvenidas =
               from manzanas1 in listaManzanasLocalidad
               from manzanas2 in listaManzanasLocalidad
               join tories1 in listaToriesLocalidad on manzanas1.TorieLocalidad1Id equals tories1.Id
               join tories2 in listaToriesLocalidad on manzanas2.TorieLocalidad2Id equals tories2.Id
               where tories1.TipoOrientacionId == avenida.Id && manzanas1.Orientacion1 == avenidaEliminar.Id || tories2.TipoOrientacionId == avenida.Id && manzanas2.Orientacion2 == avenidaEliminar.Id
               select manzanas1;

            PuedeEliminarAvenidaOutput puede = new PuedeEliminarAvenidaOutput();

            if (consultaAvenidas.Count() == 0)
            {
                puede.PuedeEliminar = true;
            }

            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }


        /************************************************************************************
       *  BARRIOS
       * **********************************************************************************/
        //Metodo para cargar las avenidas que pertenecen a una localidad
        public GetBarriosByLocalidadOutput GetBarriosByLocalidad(GetBarriosByLocalidadInput localidadInput)
        {
            var listaBarrios = _barrioRepositorio.GetAllList().Where(b => b.LocalidadId == localidadInput.Id).OrderBy(b => b.Nombre);
            return new GetBarriosByLocalidadOutput { Barrios = Mapper.Map<List<BarrioOutput>>(listaBarrios) };
        }

        public GetBarrioOutput GetBarrio(GetBarrioInput barrioInput)
        {
            return Mapper.Map<GetBarrioOutput>(_barrioRepositorio.Get(barrioInput.Id));
        }

        /************************************************************************************
        *  MANZANAS
        * **********************************************************************************/

        //Metodo para consultar las manzanas que pertenecen a un barrio
        public GetManzanasByBarrioOutput GetManzanasByBarrio(GetManzanasByBarrioInput barrioInput)
        {
            var listaManzanas = _manzanaRepositorio.GetAllList().Where(d => d.BarrioId == barrioInput.Id).OrderBy(d => d.Nombre);
            return new GetManzanasByBarrioOutput { Manzanas = Mapper.Map<List<ManzanaOutput>>(listaManzanas) };
        }

        //Metodo para guardar el nombre de la manzana (Sin Nomenclatura)
        public void SaveManzanaSinNomenclatura(SaveManzanaSinNomenclaturaInput nuevoManzanaSinNomenclatura)
        {
            nuevoManzanaSinNomenclatura.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoManzanaSinNomenclatura.Nombre.ToLower());
            Manzana existeManzana = _manzanaRepositorio.FirstOrDefault(s => s.Nombre.ToLower() == nuevoManzanaSinNomenclatura.Nombre.ToLower() && s.BarrioId == nuevoManzanaSinNomenclatura.BarrioId);

            if (existeManzana == null)
            {
                _manzanaRepositorio.Insert(Mapper.Map<Manzana>(nuevoManzanaSinNomenclatura));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_existeManzana");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para guardar la manzana (Con Nomenclatura)
        public void SaveManzanaConNomenclatura(SaveManzanaConNomenclaturaInput nuevoManzanaConNomenclatura)
        {
            var mensajeError = "";

            var torie1 = _torieLocalidadRepositorio.GetWithTipoOrientacion(nuevoManzanaConNomenclatura.TorieLocalidad1Id);

            var torie2 = _torieLocalidadRepositorio.GetWithTipoOrientacion(nuevoManzanaConNomenclatura.TorieLocalidad2Id);

            if (nuevoManzanaConNomenclatura.TorieLocalidad1Id == 0)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaRequerida");
            }
            else if (torie1.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA && nuevoManzanaConNomenclatura.Orientacion1 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaAvenidaRequerida");
            }
            else if (torie1.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && nuevoManzanaConNomenclatura.Orientacion1 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaNumeroRequerido");
            }
            else if (torie1.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && nuevoManzanaConNomenclatura.Orientacion1 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaNumeroEnteroRequerido");
            }
            else if (nuevoManzanaConNomenclatura.TorieLocalidad2Id == 0)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaRequerida");
            }
            else if (torie2.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA && nuevoManzanaConNomenclatura.Orientacion2 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaAvenidaRequerida");
            }
            else if (torie2.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && nuevoManzanaConNomenclatura.Orientacion2 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaNumeroRequerido");
            }
            else if (torie2.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && nuevoManzanaConNomenclatura.Orientacion2 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaNumeroEnteroRequerido");
            }
            else
            {
                var existeManzana = _manzanaRepositorio.FirstOrDefault(m => m.TorieLocalidad1Id == nuevoManzanaConNomenclatura.TorieLocalidad1Id && m.TorieLocalidad2Id == nuevoManzanaConNomenclatura.TorieLocalidad2Id && m.Orientacion1 == nuevoManzanaConNomenclatura.Orientacion1 && m.SufijoLocalidad1Id == nuevoManzanaConNomenclatura.SufijoLocalidad1Id && m.SufijoLocalidad2Id == nuevoManzanaConNomenclatura.SufijoLocalidad2Id
                                    && m.Orientacion2 == nuevoManzanaConNomenclatura.Orientacion2 && m.BarrioId == nuevoManzanaConNomenclatura.BarrioId || m.TorieLocalidad1Id == nuevoManzanaConNomenclatura.TorieLocalidad2Id && m.TorieLocalidad2Id == nuevoManzanaConNomenclatura.TorieLocalidad1Id && m.Orientacion1 == nuevoManzanaConNomenclatura.Orientacion2 && m.Orientacion2 == nuevoManzanaConNomenclatura.Orientacion1 && m.SufijoLocalidad1Id == nuevoManzanaConNomenclatura.SufijoLocalidad2Id && m.SufijoLocalidad2Id == nuevoManzanaConNomenclatura.SufijoLocalidad1Id
                                    && m.BarrioId == nuevoManzanaConNomenclatura.BarrioId);

                if (existeManzana == null)
                {
                    //Variable para obtener los datos de torieLocalidad 1 para consultar el nombre 
                    var torieLocalidad1 = _torieLocalidadRepositorio.GetWithTipoOrientacion(nuevoManzanaConNomenclatura.TorieLocalidad1Id);
                    var nombreTorie1 = torieLocalidad1.TipoOrientacionTorieLocalidad.Nombre;

                    //Variable para obtener los datos de torieLocalidad 2 para consultar el nombre
                    var torieLocalidad2 = _torieLocalidadRepositorio.GetWithTipoOrientacion(nuevoManzanaConNomenclatura.TorieLocalidad2Id);
                    var nombreTorie2 = torieLocalidad2.TipoOrientacionTorieLocalidad.Nombre;

                    var nombreSufijo1 = "";
                    var nombreSufijo2 = "";

                    if (nuevoManzanaConNomenclatura.SufijoLocalidad1Id != null)
                    {
                        var sufijo1Id = Convert.ToInt32(nuevoManzanaConNomenclatura.SufijoLocalidad1Id);
                        //Variable para obtener los datos de sufijoLocalidad 1 para consultar el nombre
                        var sufijoLocalidad1 = _sufijoLocalidadRepositorio.GetWithSufijo(sufijo1Id);
                        nombreSufijo1 = sufijoLocalidad1.SufijoSufijoLocalidad.Nombre;
                    }

                    if (nuevoManzanaConNomenclatura.SufijoLocalidad2Id != null)
                    {
                        var sufijo2Id = Convert.ToInt32(nuevoManzanaConNomenclatura.SufijoLocalidad2Id);
                        //Variable para obtener los datos de sufijoLocalidad 2 para consultar el nombre
                        var sufijoLocalidad2 = _sufijoLocalidadRepositorio.GetWithSufijo(sufijo2Id);
                        nombreSufijo2 = sufijoLocalidad2.SufijoSufijoLocalidad.Nombre;
                    }

                    //Proceso para consultar el nombre de la avenida para concatenarlo en el nombre
                    if (torie1.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA)
                    {
                        var nombreAvenida1 = _avenidaRepositorio.Get(Convert.ToInt32(nuevoManzanaConNomenclatura.Orientacion1));
                        nuevoManzanaConNomenclatura.Nombre = nombreTorie1 + ' ' + nombreAvenida1.Nombre + " - " + nombreTorie2 + ' ' + nuevoManzanaConNomenclatura.Orientacion2;
                    }
                    else if (torie2.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA)
                    {
                        var nombreAvenida2 = _avenidaRepositorio.Get(Convert.ToInt32(nuevoManzanaConNomenclatura.Orientacion2));
                        nuevoManzanaConNomenclatura.Nombre = nombreTorie2 + ' ' + nombreAvenida2.Nombre + " - " + nombreTorie1 + ' ' + nuevoManzanaConNomenclatura.Orientacion1;
                    }
                    else
                    {
                        var nombreGuardar = nombreTorie1 + ' ' + nuevoManzanaConNomenclatura.Orientacion1 + ' ' + nombreSufijo1 + " - " + nombreTorie2 + ' ' + nuevoManzanaConNomenclatura.Orientacion2 + ' ' + nombreSufijo2;
                        nuevoManzanaConNomenclatura.Nombre = nombreGuardar;
                    }

                    _manzanaRepositorio.Insert(Mapper.Map<Manzana>(nuevoManzanaConNomenclatura));
                }
                else
                {
                    var mensajeErrorExiste = LocalizationHelper.GetString("Bow", "zonificacion_manzana_existeManzana");
                    throw new UserFriendlyException(mensajeErrorExiste);
                }
            }
            if (mensajeError != "")
            {
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para gestionar el comportamiento de los datos del dropdown via principal, validacion calle, carrera, diagonal y transversal
        public GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidadOutput GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidad(GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidadInput torielocalidad)
        {
            //Se consulta el Id del tipoOrientacion en torieLocalidad para comparar si es calle, carrera, diagonal, transversal o avenida para validar que datos cargar en el dropdown via secundaria
            var torie = _torieLocalidadRepositorio.GetWithTipoOrientacion(torielocalidad.TorieLocalidadId);

            List<TorieLocalidad> toriesLocalidad2;

            if (torie.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_CALLE)
            {
                toriesLocalidad2 = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(torielocalidad.LocalidadId).Where(lo => lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_DIAGONAL && lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_CALLE).OrderBy(l => l.TipoOrientacionTorieLocalidad.Nombre).ToList();
            }
            else if (torie.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_CARRERA)
            {
                toriesLocalidad2 = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(torielocalidad.LocalidadId).Where(lo => lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_TRANSVERSAL && lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_CARRERA).OrderBy(l => l.TipoOrientacionTorieLocalidad.Nombre).ToList();
            }
            else if (torie.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_DIAGONAL)
            {
                toriesLocalidad2 = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(torielocalidad.LocalidadId).Where(lo => lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_CALLE && lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_DIAGONAL).OrderBy(l => l.TipoOrientacionTorieLocalidad.Nombre).ToList();
            }
            else if (torie.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_TRANSVERSAL)
            {
                toriesLocalidad2 = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(torielocalidad.LocalidadId).Where(lo => lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_CARRERA && lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_TRANSVERSAL).OrderBy(l => l.TipoOrientacionTorieLocalidad.Nombre).ToList();
            }
            else if (torie.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA)
            {
                toriesLocalidad2 = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(torielocalidad.LocalidadId).Where(lo => lo.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA).OrderBy(l => l.TipoOrientacionTorieLocalidad.Nombre).ToList();
            }
            else
            {
                toriesLocalidad2 = _torieLocalidadRepositorio.GetAllListWithTipoOrientacionByLocalidad(torielocalidad.LocalidadId).OrderBy(l => l.TipoOrientacionTorieLocalidad.Nombre).ToList();
            }

            return new GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidadOutput { ToriesLocalidad = Mapper.Map<List<TorieLocalidadManzanaOutput>>(toriesLocalidad2) };
        }

        //Metodo para verificar si el torie seleccionado en el dropdown es 'Avenida' comparado con el archivo de las constantes
        public EsTipoOrientacionAvenidaOutput EsTipoOrientacionAvenida(EsTipoOrientacionAvenidaInput torieSeleccion)
        {
            //Se consulta el Id del tipoOrientacion en torieLocalidad para comparar si es avenida
            var torieLocalidadId = _torieLocalidadRepositorio.GetWithTipoOrientacion(torieSeleccion.Id);

            EsTipoOrientacionAvenidaOutput esAvenida = new EsTipoOrientacionAvenidaOutput();

            if (torieLocalidadId.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA)
            {
                esAvenida.EsAvenida = true;
            }

            else
            {
                esAvenida.EsAvenida = false;
            }

            return esAvenida;
        }

        //Metodo para eliminar una manzana.
        public void DeleteManzana(DeleteManzanaInput manzanaEliminar)
        {
            _manzanaRepositorio.Delete(manzanaEliminar.Id);
        }


        /************************************************************************************
      *  DIRECCIONES
      * **********************************************************************************/
        public GetDireccionOutput GetDireccion(GetDireccionInput direccionInput)
        {
            return Mapper.Map<GetDireccionOutput>(_direccionRepositorio.Get(direccionInput.Id));
        }

        public SaveDireccionSinNomenclaturaOutput SaveDireccionSinNomenclatura(SaveDireccionSinNomenclaturaInput nuevoDireccionSinNomenclatura)
        {
            SaveDireccionSinNomenclaturaOutput saveSinNomenclatura = new SaveDireccionSinNomenclaturaOutput();

            Direccion existeDireccion = _direccionRepositorio.GetWithLocalidadAndDepartamento(nuevoDireccionSinNomenclatura.BarrioId.Value, nuevoDireccionSinNomenclatura.Nombre);

            if (existeDireccion == null)
            {
                nuevoDireccionSinNomenclatura.DireccionCompleta = nuevoDireccionSinNomenclatura.Nombre;

                int idGuardado = _direccionRepositorio.InsertAndGetId(Mapper.Map<Direccion>(nuevoDireccionSinNomenclatura));
                saveSinNomenclatura = Mapper.Map<SaveDireccionSinNomenclaturaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(idGuardado));
            }
            else
            {
                saveSinNomenclatura = Mapper.Map<SaveDireccionSinNomenclaturaOutput>(existeDireccion);
            }

            return saveSinNomenclatura;
        }

        public SaveDireccionConNomenclaturaOutput SaveDireccionConNomenclatura(SaveDireccionConNomenclaturaInput saveDireccionConNomenclatura)
        {
            var mensajeError = "";

            SaveDireccionConNomenclaturaOutput saveConNomenclatura = new SaveDireccionConNomenclaturaOutput();

            var torie1 = _torieLocalidadRepositorio.GetWithTipoOrientacion(Convert.ToInt32(saveDireccionConNomenclatura.TorieLocalidad1Id));

            var torie2 = _torieLocalidadRepositorio.GetWithTipoOrientacion(Convert.ToInt32(saveDireccionConNomenclatura.TorieLocalidad2Id));

            if (torie1.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA && saveDireccionConNomenclatura.Orientacion1 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaAvenidaRequerida");
            }
            else if (torie1.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && saveDireccionConNomenclatura.Orientacion1 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaNumeroRequerido");
            }
            else if (torie1.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && saveDireccionConNomenclatura.Orientacion1 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaPrimariaNumeroEnteroRequerido");
            }
            else if (torie2.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA && saveDireccionConNomenclatura.Orientacion2 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaAvenidaRequerida");
            }
            else if (torie2.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && saveDireccionConNomenclatura.Orientacion2 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaNumeroRequerido");
            }
            else if (torie2.TipoOrientacionTorieLocalidad.Nombre != BowConsts.TIPO_ORIENTACION_AVENIDA && saveDireccionConNomenclatura.Orientacion2 == null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_manzana_viaSecundariaNumeroEnteroRequerido");
            }
            else
            {
                var existeDireccion = _direccionRepositorio.FirstOrDefault(m => m.TorieLocalidad1Id == saveDireccionConNomenclatura.TorieLocalidad1Id && m.TorieLocalidad2Id == saveDireccionConNomenclatura.TorieLocalidad2Id && m.Orientacion1 == saveDireccionConNomenclatura.Orientacion1 && m.SufijoLocalidad1Id == saveDireccionConNomenclatura.SufijoLocalidad1Id && m.SufijoLocalidad2Id == saveDireccionConNomenclatura.SufijoLocalidad2Id
                              && m.Orientacion2 == saveDireccionConNomenclatura.Orientacion2 && m.BarrioId == saveDireccionConNomenclatura.BarrioId && m.Porton == saveDireccionConNomenclatura.Porton && m.Apartamento == saveDireccionConNomenclatura.Apartamento || m.TorieLocalidad1Id == saveDireccionConNomenclatura.TorieLocalidad2Id && m.TorieLocalidad2Id == saveDireccionConNomenclatura.TorieLocalidad1Id && m.Orientacion1 == saveDireccionConNomenclatura.Orientacion2 && m.Orientacion2 == saveDireccionConNomenclatura.Orientacion1 && m.SufijoLocalidad1Id == saveDireccionConNomenclatura.SufijoLocalidad2Id && m.SufijoLocalidad2Id == saveDireccionConNomenclatura.SufijoLocalidad1Id
                              && m.BarrioId == saveDireccionConNomenclatura.BarrioId && m.Porton == saveDireccionConNomenclatura.Porton && m.Apartamento == saveDireccionConNomenclatura.Apartamento);

                var existeManzana = _manzanaRepositorio.FirstOrDefault(m => m.TorieLocalidad1Id == saveDireccionConNomenclatura.TorieLocalidad1Id && m.TorieLocalidad2Id == saveDireccionConNomenclatura.TorieLocalidad2Id && m.Orientacion1 == saveDireccionConNomenclatura.Orientacion1 && m.SufijoLocalidad1Id == saveDireccionConNomenclatura.SufijoLocalidad1Id && m.SufijoLocalidad2Id == saveDireccionConNomenclatura.SufijoLocalidad2Id
                               && m.Orientacion2 == saveDireccionConNomenclatura.Orientacion2 && m.BarrioId == saveDireccionConNomenclatura.BarrioId || m.TorieLocalidad1Id == saveDireccionConNomenclatura.TorieLocalidad2Id && m.TorieLocalidad2Id == saveDireccionConNomenclatura.TorieLocalidad1Id && m.Orientacion1 == saveDireccionConNomenclatura.Orientacion2 && m.Orientacion2 == saveDireccionConNomenclatura.Orientacion1 && m.SufijoLocalidad1Id == saveDireccionConNomenclatura.SufijoLocalidad2Id && m.SufijoLocalidad2Id == saveDireccionConNomenclatura.SufijoLocalidad1Id
                               && m.BarrioId == saveDireccionConNomenclatura.BarrioId);

                if (existeManzana == null && existeDireccion == null)
                {
                    //Variable para obtener los datos de torieLocalidad 1 para consultar el nombre 
                    var torieLocalidad1 = _torieLocalidadRepositorio.GetWithTipoOrientacion(Convert.ToInt32(saveDireccionConNomenclatura.TorieLocalidad1Id));
                    var nombreTorie1 = torieLocalidad1.TipoOrientacionTorieLocalidad.Nombre;

                    //Variable para obtener los datos de torieLocalidad 2 para consultar el nombre
                    var torieLocalidad2 = _torieLocalidadRepositorio.GetWithTipoOrientacion(Convert.ToInt32(saveDireccionConNomenclatura.TorieLocalidad2Id));
                    var nombreTorie2 = torieLocalidad2.TipoOrientacionTorieLocalidad.Nombre;

                    var nombreSufijo1 = "";
                    var nombreSufijo2 = "";

                    if (saveDireccionConNomenclatura.SufijoLocalidad1Id != null)
                    {
                        var sufijo1Id = Convert.ToInt32(saveDireccionConNomenclatura.SufijoLocalidad1Id);
                        //Variable para obtener los datos de sufijoLocalidad 1 para consultar el nombre
                        var sufijoLocalidad1 = _sufijoLocalidadRepositorio.GetWithSufijo(sufijo1Id);
                        nombreSufijo1 = sufijoLocalidad1.SufijoSufijoLocalidad.Nombre;
                    }

                    if (saveDireccionConNomenclatura.SufijoLocalidad2Id != null)
                    {
                        var sufijo2Id = Convert.ToInt32(saveDireccionConNomenclatura.SufijoLocalidad2Id);
                        //Variable para obtener los datos de sufijoLocalidad 2 para consultar el nombre
                        var sufijoLocalidad2 = _sufijoLocalidadRepositorio.GetWithSufijo(sufijo2Id);
                        nombreSufijo2 = sufijoLocalidad2.SufijoSufijoLocalidad.Nombre;
                    }

                    //Proceso para consultar el nombre de la avenida para concatenarlo en el nombre
                    if (torie1.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA)
                    {
                        var nombreAvenida1 = _avenidaRepositorio.Get(Convert.ToInt32(saveDireccionConNomenclatura.Orientacion1));

                        if (saveDireccionConNomenclatura.Apartamento != "")
                        {
                            saveDireccionConNomenclatura.DireccionCompleta = nombreTorie1 + ' ' + nombreAvenida1.Nombre + " - " + nombreTorie2 + ' ' + saveDireccionConNomenclatura.Orientacion2 + ' ' + BowConsts.CAMPO_PORTON + ' ' + saveDireccionConNomenclatura.Porton + ' ' + BowConsts.CAMPO_APARTAMENTO + ' ' + saveDireccionConNomenclatura.Apartamento;
                        }
                        else
                        {
                            saveDireccionConNomenclatura.DireccionCompleta = nombreTorie1 + ' ' + nombreAvenida1.Nombre + " - " + nombreTorie2 + ' ' + saveDireccionConNomenclatura.Orientacion2 + ' ' + saveDireccionConNomenclatura.Porton;
                        }
                    }
                    else if (torie2.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA)
                    {
                        var nombreAvenida2 = _avenidaRepositorio.Get(Convert.ToInt32(saveDireccionConNomenclatura.Orientacion2));

                        if (saveDireccionConNomenclatura.Apartamento != "")
                        {
                            saveDireccionConNomenclatura.DireccionCompleta = nombreTorie2 + ' ' + nombreAvenida2.Nombre + " - " + nombreTorie1 + ' ' + saveDireccionConNomenclatura.Orientacion1 + ' ' + BowConsts.CAMPO_PORTON + ' ' + saveDireccionConNomenclatura.Porton + ' ' + BowConsts.CAMPO_APARTAMENTO + ' ' + saveDireccionConNomenclatura.Apartamento;
                        }
                        else
                        {
                            saveDireccionConNomenclatura.DireccionCompleta = nombreTorie2 + ' ' + nombreAvenida2.Nombre + " - " + nombreTorie1 + ' ' + saveDireccionConNomenclatura.Orientacion1;
                        }
                    }
                    else
                    {
                        if (saveDireccionConNomenclatura.Apartamento != "")
                        {
                            saveDireccionConNomenclatura.DireccionCompleta = nombreTorie1 + ' ' + saveDireccionConNomenclatura.Orientacion1 + ' ' + nombreSufijo1 + " - " + nombreTorie2 + ' ' + saveDireccionConNomenclatura.Orientacion2 + ' ' + nombreSufijo2 + ' ' + BowConsts.CAMPO_PORTON + ' ' + saveDireccionConNomenclatura.Porton + ' ' + BowConsts.CAMPO_APARTAMENTO + ' ' + saveDireccionConNomenclatura.Apartamento;
                        }
                        else
                        {
                            saveDireccionConNomenclatura.DireccionCompleta = nombreTorie1 + ' ' + saveDireccionConNomenclatura.Orientacion1 + ' ' + nombreSufijo1 + " - " + nombreTorie2 + ' ' + saveDireccionConNomenclatura.Orientacion2 + ' ' + nombreSufijo2;
                        }
                    }

                    int idGuardado = _direccionRepositorio.InsertAndGetId(Mapper.Map<Direccion>(saveDireccionConNomenclatura));
                    saveConNomenclatura = Mapper.Map<SaveDireccionConNomenclaturaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(idGuardado));

                }
                else if (existeManzana != null && existeDireccion == null)
                {
                    //Si existe la manzana se graba manzanaId y barrioId
                    int idManzana = existeManzana.Id;

                    if (existeManzana.BarrioId != null)
                    {
                        saveDireccionConNomenclatura.BarrioId = existeManzana.BarrioId;
                    }

                    saveDireccionConNomenclatura.ManzanaId = idManzana;

                    int idGuardado = _direccionRepositorio.InsertAndGetId(Mapper.Map<Direccion>(saveDireccionConNomenclatura));
                    saveConNomenclatura = Mapper.Map<SaveDireccionConNomenclaturaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(idGuardado));
                }
                else
                {
                    saveConNomenclatura = Mapper.Map<SaveDireccionConNomenclaturaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(existeDireccion.Id));
                }
            }

            if (mensajeError != "")
            {
                throw new UserFriendlyException(mensajeError);
            }

            return saveConNomenclatura;
        }

        public SaveDireccionSinNomenclaturaUsaOutput SaveDireccionSinNomenclaturaUsa(SaveDireccionSinNomenclaturaUsaInput nuevoDireccionSinNomenclaturaUsa)
        {
            SaveDireccionSinNomenclaturaUsaOutput saveSinNomenclaturaUsa = new SaveDireccionSinNomenclaturaUsaOutput();

            var barrioNoIdentificado = _barrioRepositorio.FirstOrDefault(b => b.LocalidadId == nuevoDireccionSinNomenclaturaUsa.LocalidadId && b.Nombre == BowConsts.BARRIO_NO_IDENTIFICADO);

            Direccion existeDireccion = _direccionRepositorio.GetWithLocalidadAndDepartamentoByNombreAndCodeZip(nuevoDireccionSinNomenclaturaUsa.Nombre, nuevoDireccionSinNomenclaturaUsa.ZipCode);

            if (existeDireccion == null)
            {
                nuevoDireccionSinNomenclaturaUsa.BarrioId = barrioNoIdentificado.Id;

                nuevoDireccionSinNomenclaturaUsa.DireccionCompleta = nuevoDireccionSinNomenclaturaUsa.Nombre + ' ' + nuevoDireccionSinNomenclaturaUsa.ZipCode;

                int idGuardado = _direccionRepositorio.InsertAndGetId(Mapper.Map<Direccion>(nuevoDireccionSinNomenclaturaUsa));
                saveSinNomenclaturaUsa = Mapper.Map<SaveDireccionSinNomenclaturaUsaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(idGuardado));
            }
            else
            {
                saveSinNomenclaturaUsa = Mapper.Map<SaveDireccionSinNomenclaturaUsaOutput>(existeDireccion);
            }

            return saveSinNomenclaturaUsa;
        }

        public SaveDireccionConNomenclaturaUsaOutput SaveDireccionConNomenclaturaUsa(SaveDireccionConNomenclaturaUsaInput nuevoDireccionConNomenclaturaUsa)
        {
            SaveDireccionConNomenclaturaUsaOutput saveConNomenclaturaUsa = new SaveDireccionConNomenclaturaUsaOutput();

            //Se consulta la Avenida de la localidad seleccionada y se consulta su torieLocalidadId para asignarlo al TorieLocalidad1Id                  
            TorieLocalidad torieLocalidadAvenidaUsa = _torieLocalidadRepositorio.FirstOrDefault(tl => tl.TipoOrientacionTorieLocalidad.Nombre == BowConsts.TIPO_ORIENTACION_AVENIDA && tl.LocalidadId == nuevoDireccionConNomenclaturaUsa.LocalidadId);
            nuevoDireccionConNomenclaturaUsa.TorieLocalidad1Id = torieLocalidadAvenidaUsa.Id;

            //Validar si la direccion ingresada ya existe.
            var existeDireccion = _direccionRepositorio.FirstOrDefault(m => m.TorieLocalidad1Id == nuevoDireccionConNomenclaturaUsa.TorieLocalidad1Id && m.Porton == nuevoDireccionConNomenclaturaUsa.Porton
                         && m.SufijoLocalidad1Id == nuevoDireccionConNomenclaturaUsa.SufijoLocalidad1Id && m.Orientacion1 == nuevoDireccionConNomenclaturaUsa.Orientacion1);

            if (existeDireccion == null)
            {
                //Se consulta el nombre del tipo de orientacion.
                var torieLocalidadUsa = _torieLocalidadRepositorio.GetWithTipoOrientacion(Convert.ToInt32(nuevoDireccionConNomenclaturaUsa.TorieLocalidad1Id));
                var nombreTorieUsa = torieLocalidadUsa.TipoOrientacionTorieLocalidad.Nombre;

                //Se consulta el nombre de la avenida
                var nombreAvenida = _avenidaRepositorio.Get(Convert.ToInt32(nuevoDireccionConNomenclaturaUsa.Orientacion1));

                //Se consulta el nombre del sufijo;
                var nombreSufijoLocalidad = _sufijoLocalidadRepositorio.FirstOrDefault(Convert.ToInt32(nuevoDireccionConNomenclaturaUsa.SufijoLocalidad1Id));
                var nombreSufijo = _sufijoRepositorio.Get(nombreSufijoLocalidad.SufijoId);

                nuevoDireccionConNomenclaturaUsa.DireccionCompleta = nuevoDireccionConNomenclaturaUsa.Porton + ' ' + nombreTorieUsa + ' ' + nombreAvenida.Nombre + " - " + nombreSufijo.Nombre;

                var barrioNoIdentificado = _barrioRepositorio.FirstOrDefault(b => b.LocalidadId == nuevoDireccionConNomenclaturaUsa.LocalidadId && b.Nombre == BowConsts.BARRIO_NO_IDENTIFICADO);
                nuevoDireccionConNomenclaturaUsa.BarrioId = barrioNoIdentificado.Id;

                int idGuardado = _direccionRepositorio.InsertAndGetId(Mapper.Map<Direccion>(nuevoDireccionConNomenclaturaUsa));
                saveConNomenclaturaUsa = Mapper.Map<SaveDireccionConNomenclaturaUsaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(idGuardado));
            }
            else
            {
                saveConNomenclaturaUsa = Mapper.Map<SaveDireccionConNomenclaturaUsaOutput>(_direccionRepositorio.GetWithLocalidadAndDepartamentoById(existeDireccion.Id));
            }

            return saveConNomenclaturaUsa;

        }

        //Metodo para verificar si el pais seleccionado en el dropdown es 'Pais' es pais 'USA' comparado con el archivo de las constantes
        public EsPaisUsaOutput EsPaisUsa(EsPaisUsaInput paisSeleccion)
        {
            var paisUsa = _paisRepositorio.Get(paisSeleccion.Id);

            EsPaisUsaOutput esUsa = new EsPaisUsaOutput();

            if (paisUsa.Nombre == BowConsts.PAIS_ESTADOSUNIDOS)
            {
                esUsa.EsUsa = true;
            }

            else
            {
                esUsa.EsUsa = false;
            }

            return esUsa;
        }

        //*********************** Zonas *************************

        //Metodo para guardar una zona
        public void SaveZona(SaveZonaInput nuevaZona)
        {
            nuevaZona.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevaZona.Nombre.ToLower());
            Zona existeZona = _zonaRepositorio.FirstOrDefault(b => b.Nombre.ToLower() == nuevaZona.Nombre.ToLower() && b.TipoId == nuevaZona.TipoId && b.LocalidadId == nuevaZona.LocalidadId);

            if (existeZona == null)
            {
                _zonaRepositorio.Insert(Mapper.Map<Zona>(nuevaZona));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_zona_validarNombreZona");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para cargar las zonas que pertenecen a una localidad
        public GetZonasByLocalidadOutput GetZonasByLocalidad(GetZonasByLocalidadInput localidadInput)
        {
            var estadoActivo = _parametrosService.GetEstadoActivoZonaEmpleado();

            List<Zona> listaZonas = _zonaRepositorio.GetAllListWithZonaByLocalidad(localidadInput.Id);

            GetZonasByLocalidadOutput zonas = new GetZonasByLocalidadOutput { Zonas = new List<ZonaOutput>() };
            ZonaOutput zonaout = new ZonaOutput();

            foreach (Zona zon in listaZonas)
            {
                zonaout = Mapper.Map<ZonaOutput>(zon);
                zonaout.CantidadEmpleados = _zonaEmpleadoRepositorio.GetAll().Where(ze => ze.EstadoId == estadoActivo.Id && ze.ZonaId == zon.Id).ToList().Count();
                zonas.Zonas.Add(zonaout);
            }

            return zonas;

        }

        //Metodo para consultar la zona a editar/eliminar
        public GetZonaOutput GetZona(GetZonaInput zonaInput)
        {
            var listaZonas = _zonaRepositorio.GetWithTipo(zonaInput.Id);
            return Mapper.Map<GetZonaOutput>(_zonaRepositorio.Get(zonaInput.Id));
        }

        //Metodos para actualizar una zona
        public void UpdateZona(UpdateZonaInput zonaUpdate)
        {
            zonaUpdate.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(zonaUpdate.Nombre.ToLower());
            Zona zonaConsultada = _zonaRepositorio.Get(zonaUpdate.Id);

            var localidadId = zonaConsultada.LocalidadId;

            Zona existeZona = _zonaRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == zonaUpdate.Nombre.ToLower() && p.Id != zonaUpdate.Id && p.LocalidadId == localidadId);

            if (existeZona == null)
            {
                zonaConsultada.Nombre = zonaUpdate.Nombre;
                zonaConsultada.Descripcion = zonaUpdate.Descripcion;

                var zonaActualizar = _zonaRepositorio.Update(Mapper.Map<Zona>(zonaConsultada));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "zonificacion_zona_validarNombreZona");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para eliminar una zona
        public void DeleteZona(DeleteZonaInput zonaEliminar)
        {
            _zonaRepositorio.Delete(zonaEliminar.Id);
        }

        //Metodo para filtrar las zonas que pertenecen a una localidad por tipo seleccionado en el dropdown tipos zona
        public GetZonasByLocalidadAndTipoZonaOutput GetZonasByLocalidadAndTipoZona(GetZonasByLocalidadAndTipoZonaInput localidadInput)
        {
            List<Zona> listaZonas = new List<Zona>();

            if (localidadInput.TipoId == 0)
            {
                listaZonas = _zonaRepositorio.GetAllListWithZonaByLocalidad(localidadInput.Id);
            }
            else
            {
                listaZonas = _zonaRepositorio.GetAllListWithZonaByLocalidadAndTipoZona(localidadInput.Id, localidadInput.TipoId);
            }
            return new GetZonasByLocalidadAndTipoZonaOutput { Zonas = Mapper.Map<List<ZonaOutput>>(listaZonas) };
        }

        ////Metodo para cargar los barrios por zona
        public GetBarriosByZonaOutput GetBarriosByZona(GetBarriosByZonaInput zonaInput)
        {
            var listaBarrios = _zonabarrioRepositorio.GetAllListWithBarriosByZona(zonaInput.ZonaId);
            return new GetBarriosByZonaOutput { Barrios = Mapper.Map<List<ZonaBarrioOutput>>(listaBarrios) };
        }

        //Metodo para eliminar barrio de la zona
        public void DeleteZonaBarrio(DeleteZonaBarrioInput zonabarrioEliminar)
        {
            _zonabarrioRepositorio.Delete(zonabarrioEliminar.Id);
        }

        //Metodo para cargar los barrios disponibles por zona
        public GetBarriosByZonaDisponiblesOutput GetBarriosByZonaDisponibles(GetBarriosByZonaDisponiblesInput zonaInput)
        {
            Zona listaZonas = _zonaRepositorio.Get(zonaInput.ZonaId);
            var localidadId = listaZonas.LocalidadId;
            var tipoZonaId = listaZonas.TipoId;

            List<ZonaBarrio> listaZonaBarrio = _zonabarrioRepositorio.GetAllListWithZonaBarrioByZonaAndLocalidad(localidadId, tipoZonaId).ToList();
            List<Barrio> listaBarrios = _barrioRepositorio.GetAllList().Where(b => b.LocalidadId == localidadId).ToList();

            var barriosDisponibles = listaBarrios.Where(ba => !listaZonaBarrio.Any(zb => zb.BarrioId == ba.Id) && ba.Nombre != BowConsts.BARRIO_NO_IDENTIFICADO);
            return new GetBarriosByZonaDisponiblesOutput { BarriosDisponibles = Mapper.Map<List<ZonaBarrioOutput>>(barriosDisponibles) };
        }

        //Metodo para guardar una zona
        public void AsignarBarrioZona(SaveZonaBarrioInput nuevaZonaBarrio)
        {
            _zonabarrioRepositorio.Insert(Mapper.Map<ZonaBarrio>(nuevaZonaBarrio));
        }

        //Método para guardar un teléfono
        public SaveTelefonoOutput SaveTelefono(SaveTelefonoInput nuevoTelefono)
        {
            string prueba = PruebaJsonAuditoria();
            SaveTelefonoOutput saveTelefonoOutput = new SaveTelefonoOutput();

            if (nuevoTelefono.TipoId == 0)
            {
                Tipo tipoTelefono = _tipoRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.TIPO_TELEFONO_CELULAR);
                nuevoTelefono.TipoId = tipoTelefono.Id;
            }

            ValidarTelefonoOutput validarTelefonoOutput = validarTelefono(Mapper.Map<ValidarTelefonoInput>(nuevoTelefono));

            if (validarTelefonoOutput.TelefonoValido)
            {
                Telefono existeTelefono = _telefonoRepositorio.GetWithLocalidadAndDepartamentoandTipo(nuevoTelefono.Numero, nuevoTelefono.Extension);

                Telefono existeTelefonoCelular = _telefonoRepositorio.GetWithLocalidadAndDepartamentoandTipo(nuevoTelefono.Numero, nuevoTelefono.Extension);

                if (existeTelefono == null)
                {
                    if (existeTelefonoCelular == null)
                    {
                        int idTel = _telefonoRepositorio.InsertAndGetId(Mapper.Map<Telefono>(nuevoTelefono));
                        saveTelefonoOutput = Mapper.Map<SaveTelefonoOutput>(_telefonoRepositorio.GetWithLocalidadAndDepartamentoandTipo(idTel));
                    }
                    else
                    {
                        saveTelefonoOutput = Mapper.Map<SaveTelefonoOutput>(existeTelefonoCelular);
                    }
                }
                else
                {
                    saveTelefonoOutput = Mapper.Map<SaveTelefonoOutput>(existeTelefono);
                }
            }
            else
            {
                throw new UserFriendlyException(validarTelefonoOutput.MensajeValidacion);
            }

            return saveTelefonoOutput;
        }

        public ValidarTelefonoOutput validarTelefono(ValidarTelefonoInput validarTelefonoInput)
        {
            ValidarTelefonoOutput validarTelefonoOutput = new ValidarTelefonoOutput();

            validarTelefonoOutput.TelefonoValido = true;

            Localidad localidadTelefono = _localidadRepositorio.GetWithDepartamentoAndPais(validarTelefonoInput.LocalidadId);

            int paisEstadosUnidos = _paisRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PAIS_ESTADOSUNIDOS).Id;
            int paisColombia = _paisRepositorio.FirstOrDefault(p => p.Nombre == BowConsts.PAIS_COLOMBIA).Id;

            Tipo tipo = _tipoRepositorio.FirstOrDefault(p => p.Id == validarTelefonoInput.TipoId);

            if (paisColombia == localidadTelefono.DepartamentoLocalidad.PaisId)
            {
                if (tipo.Nombre == BowConsts.TIPO_TELEFONO_FIJO)
                {
                    if (validarTelefonoInput.Numero.Length != BowConsts.TIPO_TELEFONO_FIJO_NUMERO_CARACTERES)
                    {
                        validarTelefonoOutput.TelefonoValido = false;
                        validarTelefonoOutput.MensajeValidacion = LocalizationHelper.GetString("Bow", "zonificacion_telefono_registrar_validarLongitudFijo_colombia");
                    }
                }
                else if (tipo.Nombre == BowConsts.TIPO_TELEFONO_CELULAR)
                {
                    if (validarTelefonoInput.Numero.Length != BowConsts.TIPO_TELEFONO_CELULAR_NUMERO_CARACTERES || !validarTelefonoInput.Numero[0].ToString().Equals(BowConsts.TIPO_TELEFONO_CELULAR_NUMERO_INICIAL))
                    {
                        validarTelefonoOutput.TelefonoValido = false;
                        validarTelefonoOutput.MensajeValidacion = LocalizationHelper.GetString("Bow", "zonificacion_telefono_registrar_validarLongitudCelular_colombia");
                    }
                }
            }
            else if (paisEstadosUnidos == localidadTelefono.DepartamentoLocalidad.PaisId)
            {
                if (validarTelefonoInput.Numero.Length != 10)
                {
                    validarTelefonoOutput.TelefonoValido = false;
                    validarTelefonoOutput.MensajeValidacion = LocalizationHelper.GetString("Bow", "zonificacion_telefono_registrar_validarLongitud_estadosUnidos");
                }
            }

            return validarTelefonoOutput;
        }

        public GetTelefonoOutput GetTelefono(GetTelefonoInput telefonoInput)
        {
            return Mapper.Map<GetTelefonoOutput>(_telefonoRepositorio.GetWithLocalidad(telefonoInput.Id));
        }

        public string PruebaJsonAuditoria()
        {

            IDictionary<string, Pais> datosAuditoria = new Dictionary<string, Pais>();
            datosAuditoria.Add("cedula", new Pais { Id = 1, Nombre = "Colombia" });
            datosAuditoria.Add("nombre", new Pais { Id = 1, Nombre = "Peru" });
            datosAuditoria.Add("Apellido", new Pais { Id = 1, Nombre = "Chile" });

            return JsonConvert.SerializeObject(datosAuditoria);
        }


        //****************** empleados zona ***************

        //Metodo para retornar los empleados activos de la zona indicada
        public GetEmpleadosActivosByZonaOutput GetEmpleadosActivosByZona(GetEmpleadosActivosByZonaInput zonaInput)
        {
            GetEstadoActivoZonaEmpleadoOutput estadoActivo = _parametrosService.GetEstadoActivoZonaEmpleado();

            var zonaEmpleado = _zonaEmpleadoRepositorio.GetAllWithPersonaAndZonaAndTipoAndEstado(zonaInput.ZonaId, estadoActivo.Id);
            return new GetEmpleadosActivosByZonaOutput { EmpleadosActivos = Mapper.Map<List<EmpleadoByZonaOutput>>(zonaEmpleado) };
        }

        public GetEmpleadosInactivosByZonaOutput GetEmpleadosInactivosByZona(GetEmpleadosInactivosByZonaInput zonaInput)
        {
            GetEstadoInactivoZonaEmpleadoOutput estadoActivo = _parametrosService.GetEstadoInactivoZonaEmpleado();

            var zonaEmpleado = _zonaEmpleadoRepositorio.GetAllWithPersonaAndZonaAndTipoAndEstado(zonaInput.ZonaId, estadoActivo.Id);
            return new GetEmpleadosInactivosByZonaOutput { EmpleadosInactivos = Mapper.Map<List<EmpleadoByZonaOutput>>(zonaEmpleado) };
        }

        //Metodo para guardar en ZonaEmpleado
        public void SaveZonaEmpleado(SaveZonaEmpleadoInput zonaEmpleadoInput)
        {
            //if (zonaEmpleadoInput.EmpleadoId == null)
            //{
            //    mensajeError = LocalizationHelper.GetString("Bow", "empleados_empleadozona_empleadoRequerido");
            //}
            if (zonaEmpleadoInput.FechaAsignacion == null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "empleados_empleadozona_fechaAsignacionRequerido");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                var existe = _zonaEmpleadoRepositorio.FirstOrDefault(zp => zp.EmpleadoId == zonaEmpleadoInput.EmpleadoId);

                if (existe == null)
                {
                    var estadoActivo = _parametrosService.GetEstadoActivoZonaEmpleado();
                    zonaEmpleadoInput.EstadoId = estadoActivo.Id;

                    ZonaEmpleado nueva = new ZonaEmpleado();
                    nueva = Mapper.Map<ZonaEmpleado>(zonaEmpleadoInput);

                    _zonaEmpleadoRepositorio.Insert(nueva);
                }
                else
                {
                    var mensajeError = LocalizationHelper.GetString("Bow", "empleados_empleadozona_existeEmpleadoZona");
                    throw new UserFriendlyException(mensajeError);

                }
            }

        }

        //Metodo para obtener la informacion de la zona empleado indicado
        public GetZonaEmpleadoOutput GetZonaEmpleado(GetZonaEmpleadoInput zonaempleadoInput)
        {
            var empleadoZona = _zonaEmpleadoRepositorio.GetWithEmpleadoAndZonaAndTipoAndEstado(zonaempleadoInput.Id);

            GetZonaEmpleadoOutput empleado = Mapper.Map<GetZonaEmpleadoOutput>(empleadoZona);

            DateTime fechaActual = DateTime.Now;
            DateTime fechaAsignacion = Convert.ToDateTime(empleado.FechaAsignacion);

            empleado.FechaRetiroMaxima = fechaActual;
            empleado.FechaRetiroMinima = fechaAsignacion;

            return empleado;
        }

        //metodo para actualizar el empleado indicado
        public void UpdateZonaEmpleado(UpdateZonaEmpleadoInput zonaEmpleadoInput)
        {
            ZonaEmpleado empleadoUpdate = _zonaEmpleadoRepositorio.FirstOrDefault(ze => ze.Id == zonaEmpleadoInput.Id);

            if (zonaEmpleadoInput.FechaRetiro != null)
            {
                var estadoInactivo = _parametrosService.GetEstadoInactivoZonaEmpleado();
                empleadoUpdate.EstadoId = estadoInactivo.Id;
                empleadoUpdate.FechaRetiro = zonaEmpleadoInput.FechaRetiro;
            }

            empleadoUpdate.TipoId = zonaEmpleadoInput.TipoId;
            _zonaEmpleadoRepositorio.Update(empleadoUpdate);
        }

        public PuedeEliminarZonaOutput PuedeEliminarZona(PuedeEliminarZonaInput zonaEliminar)
        {
            PuedeEliminarZonaOutput puede = new PuedeEliminarZonaOutput();

            var listaZonasBarrios = _zonabarrioRepositorio.GetAllList().Where(d => d.ZonaId == zonaEliminar.Id);

            if (listaZonasBarrios.Count() == 0)
            {
                var listaZonasEmpleados = _zonaEmpleadoRepositorio.GetAllList().Where(d => d.ZonaId == zonaEliminar.Id);

                if (listaZonasEmpleados.Count() == 0)
                {
                    puede.PuedeEliminar = true;
                }
                else
                {
                    puede.PuedeEliminar = false;
                }
            }
            else
            {
                puede.PuedeEliminar = false;
            }

            return puede;
        }


    }
}
