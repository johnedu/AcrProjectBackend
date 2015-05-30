using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using AutoMapper;
using Bow.Afiliaciones.DTOs.InputModels;
using Bow.Afiliaciones.DTOs.OutputModels;
using Bow.Afiliaciones.Entidades;
using Bow.Afiliaciones.Repositorios;
using Bow.Empresas;
using Bow.Empresas.Entidades;
using Bow.Parametros;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Zonificacion;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones
{
    public class AfiliacionesService : IAfiliacionesService
    {

        # region Repositorios

        private IPlanExequialRepositorio _planExequialRepositorio;
        private IBeneficioRepositorio _beneficioRepositorio;
        private IGrupoFamiliarRepositorio _grupoFamiliarRepositorio;
        private IGrupoFamiliarParentescoRepositorio _grupoFamiliarParentescoRepositorio;
        private IParentescoRepositorio _parentescoRepositorio;
        private IGrupoParentescoRangoRepositorio _grupoParentescoRangoRepositorio;
        private IPeriodoVentaRepositorio _periodoVentaRepositorio;
        private IBeneficioPlanExequialRepositorio _beneficioPlanExequialRepositorio;
        private IBeneficioAdicionalPlanExequialRepositorio _beneficioAdicionalPlanExequialRepositorio;
        private IPlanExequialSucursalRepositorio _planExequialSucursalRepositorio;
        private IRecaudoMasivoCoberturaRepositorio _recaudoMasivoCoberturaRepositorio;
        private IPlanExequialRecaudoMasivoRepositorio _planExequialRecaudoMasivoRepositorio;
        private IProspectoRepositorio _prospectoRepositorio;
        private IGestionProspectoRepositorio _gestionProspectoRepositorio;
        private IFunerariaProspectoRepositorio _funerariaProspectoRepositorio;
        private IAfiliadoProspectoRepositorio _afiliadoProspectoRepositorio;
        private IGrupoInformalRepositorio _grupoInformalRepositorio;
        private IGrupoInformalEmpleadoRepositorio _grupoInformalEmpleadoRepositorio;
        private IBeneficiosGestionProspectoRepositorio _beneficiosGestionProspectoRepositorio;

        private IParametrosService _parametrosService;
        private IEmpresasService _empresasService;
        private IZonificacionService _zonificacionService;

        public IAbpSession AbpSession { get; set; }

        # endregion

        public AfiliacionesService(IPlanExequialRepositorio planExequialRepositorio,
                                IBeneficioRepositorio beneficioRepositorio,
                                IPeriodoVentaRepositorio periodoVentaRepositorio,
                                IGrupoFamiliarRepositorio grupoFamiliarRepositorio,
                                IGrupoFamiliarParentescoRepositorio grupoFamiliarParentescoRepositorio,
                                IParentescoRepositorio parentescoRepositorio,
                                IGrupoParentescoRangoRepositorio grupoParentescoRangoRepositorio,
                                IBeneficioPlanExequialRepositorio beneficioPlanExequialRepositorio,
                                IBeneficioAdicionalPlanExequialRepositorio beneficioAdicionalPlanExequialRepositorio,
                                IPlanExequialSucursalRepositorio planExequialSucursalRepositorio,
                                IRecaudoMasivoCoberturaRepositorio recaudoMasivoCoberturaRepositorio,
                                IPlanExequialRecaudoMasivoRepositorio planExequialRecaudoMasivoRepositorio,
                                IProspectoRepositorio prospectoRepositorio,
                                IGestionProspectoRepositorio gestionProspectoRepositorio,
                                IFunerariaProspectoRepositorio funerariaProspectoRepositorio,
                                IAfiliadoProspectoRepositorio afiliadoProspectoRepositorio,
                                IGrupoInformalRepositorio grupoInformalRepositorio,
                                IGrupoInformalEmpleadoRepositorio grupoInformalEmpleadoRepositorio,
                                IBeneficiosGestionProspectoRepositorio beneficiosGestionProspectoRepositorio,
                                IZonificacionService zonificacionService,
                                IParametrosService parametrosService,
                                IEmpresasService empresasService)
        {
            _planExequialRepositorio = planExequialRepositorio;
            _beneficioRepositorio = beneficioRepositorio;
            _grupoFamiliarRepositorio = grupoFamiliarRepositorio;
            _grupoFamiliarParentescoRepositorio = grupoFamiliarParentescoRepositorio;
            _parentescoRepositorio = parentescoRepositorio;
            _grupoParentescoRangoRepositorio = grupoParentescoRangoRepositorio;
            _periodoVentaRepositorio = periodoVentaRepositorio;
            _beneficioPlanExequialRepositorio = beneficioPlanExequialRepositorio;
            _beneficioAdicionalPlanExequialRepositorio = beneficioAdicionalPlanExequialRepositorio;
            _planExequialSucursalRepositorio = planExequialSucursalRepositorio;
            _recaudoMasivoCoberturaRepositorio = recaudoMasivoCoberturaRepositorio;
            _planExequialRecaudoMasivoRepositorio = planExequialRecaudoMasivoRepositorio;
            _prospectoRepositorio = prospectoRepositorio;
            _gestionProspectoRepositorio = gestionProspectoRepositorio;
            _funerariaProspectoRepositorio = funerariaProspectoRepositorio;
            _afiliadoProspectoRepositorio = afiliadoProspectoRepositorio;
            _grupoInformalRepositorio = grupoInformalRepositorio;
            _grupoInformalEmpleadoRepositorio = grupoInformalEmpleadoRepositorio;
            _beneficiosGestionProspectoRepositorio = beneficiosGestionProspectoRepositorio;

            _parametrosService = parametrosService;
            _empresasService = empresasService;
            _zonificacionService = zonificacionService;
            AbpSession = NullAbpSession.Instance;

        }

        /***************************************************************************************************
         * Plan Exequial
         * ************************************************************************************************/

        //  Método para obtener la lista de planes exequiales
        public GetAllPlanesExequialesOutput GetAllPlanesExequiales()
        {
            var listaPlanesExequiales = _planExequialRepositorio.GetAllList();
            return new GetAllPlanesExequialesOutput { PlanesExequiales = Mapper.Map<List<PlanExequialOutput>>(listaPlanesExequiales) };
        }

        //  Método para obtener un plan exequial según el id
        public GetPlanExequialOutput GetPlanExequial(GetPlanExequialInput planExequial)
        {
            return Mapper.Map<GetPlanExequialOutput>(_planExequialRepositorio.Get(planExequial.Id));
        }

        //  Método para obtener un plan exequial según el id con la moneda
        public GetPlanExequialWithMonedaOutput GetPlanExequialWithMoneda(GetPlanExequialWithMonedaInput planExequial)
        {
            return Mapper.Map<GetPlanExequialWithMonedaOutput>(_planExequialRepositorio.GetWithMoneda(planExequial.Id));
        }

        //  Método para guardar un plan exequial
        public void SavePlanExequial(SavePlanExequialInput nuevoPlanExequial)
        {
            PlanExequial existePlanExequial = _planExequialRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoPlanExequial.Nombre.ToLower());
            if (existePlanExequial != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_planExequial_form_plan_yaExiste");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                nuevoPlanExequial.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoPlanExequial.Nombre.ToLower());
                nuevoPlanExequial.FechaIngreso = DateTime.Now.ToString();
                nuevoPlanExequial.EstadoId = _parametrosService.GetEstadoActivoPlanExequial().Id;
                PlanExequial planExequial = Mapper.Map<PlanExequial>(nuevoPlanExequial);
                planExequial.TenantId = AbpSession.GetTenantId();
                _planExequialRepositorio.Insert(planExequial);
            }
        }

        //  Metodo para eliminar un plan exequial
        public void DeletePlanExequial(DeletePlanExequialInput planExequialEliminar)
        {
            _planExequialRepositorio.Delete(planExequialEliminar.Id);
        }

        //  Metodo para actualizar un plan exequial
        public void UpdatePlanExequial(UpdatePlanExequialInput editarPlanExequial)
        {
            PlanExequial existePlanExequial = _planExequialRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == editarPlanExequial.Nombre.ToLower() && d.Id != editarPlanExequial.Id);
            if (existePlanExequial != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_planExequial_form_plan_yaExiste");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                editarPlanExequial.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(editarPlanExequial.Nombre.ToLower());
                _planExequialRepositorio.Update(Mapper.Map<PlanExequial>(editarPlanExequial));
            }
        }

        //  Metodo para actualizar un plan exequial
        public void CambiarEstadoPlanExequial(CambiarEstadoPlanExequialInput estadoPlanExequial)
        {
            PlanExequial planExequialUpdate = _planExequialRepositorio.FirstOrDefault(d => d.Id == estadoPlanExequial.Id);
            if (planExequialUpdate != null)
            {
                if (estadoPlanExequial.Estado)
                {
                    planExequialUpdate.FechaCancelacion = null;
                    planExequialUpdate.EstadoId = _parametrosService.GetEstadoActivoPlanExequial().Id;
                }
                else
                {
                    planExequialUpdate.FechaCancelacion = DateTime.Now;
                    planExequialUpdate.EstadoId = _parametrosService.GetEstadoInactivoPlanExequial().Id;
                }
                _planExequialRepositorio.Update(Mapper.Map<PlanExequial>(planExequialUpdate));
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_planExequial_form_update_plan_noExiste");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //  Metodo para validar si es posible eliminar un plan exequial
        public PuedeEliminarPlanExequialOutput PuedeEliminarPlanExequial(PuedeEliminarPlanExequialInput planExequialEliminar)
        {
            //int countListaPlanExequial = _empresaInfoTributariaRepositorio.GetAllList().Where(d => d.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId == infoTributariaEliminar.Id).Count();
            int countListaPlanExequial = 0;
            bool puedeEliminar = false;
            if (countListaPlanExequial == 0)
                puedeEliminar = true;
            return new PuedeEliminarPlanExequialOutput { PuedeEliminar = puedeEliminar };
        }

        // Metodo para obtener los beneficios de la categoria indicada
        public GetBeneficiosByCategoriaOutput GetBeneficiosByCategoria(GetBeneficiosByCategoriaInput categoriaInput)
        {
            GetTipoInput tipoCategoria = new GetTipoInput();
            tipoCategoria.Id = categoriaInput.TipoCategoriaId;

            var tipo = _parametrosService.GetTipo(tipoCategoria);

            GetBeneficiosByCategoriaOutput lista = new GetBeneficiosByCategoriaOutput();
            lista.TipoCategoria = tipo.Nombre;
            lista.Beneficios = Mapper.Map<List<BeneficioOutPut>>(_beneficioRepositorio.GetWithTipoCategoria(categoriaInput.TipoCategoriaId));

            return lista;
        }

        //Metodo para obtener la información del beneficio indicado por el id
        public GetBeneficioOutput GetBeneficio(GetBeneficioInput beneficioInput)
        {
            return Mapper.Map<GetBeneficioOutput>(_beneficioRepositorio.Get(beneficioInput.Id));
        }

        //Metodo para guardar el beneficio de una categoria
        public void SaveBeneficio(SaveBeneficioInput beneficioInput)
        {
            beneficioInput.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(beneficioInput.Nombre.ToLower());

            Beneficio existeBeneficio = _beneficioRepositorio.FirstOrDefault(b => b.TipoId == beneficioInput.TipoId && b.Nombre == beneficioInput.Nombre);

            if (existeBeneficio == null)
            {
                if (beneficioInput.EsNuevo == true)
                {
                    Beneficio beneficio = Mapper.Map<Beneficio>(beneficioInput);
                    beneficio.TenantId = AbpSession.GetTenantId();
                    _beneficioRepositorio.Insert(beneficio);
                }
                else
                {
                    _beneficioRepositorio.Update(Mapper.Map<Beneficio>(beneficioInput));

                }
            }
        }

        //Metodo para eliminar el beneficio indicado
        public void DeleteBeneficio(DeleteBeneficioInput beneficioInput)
        {
            _beneficioRepositorio.Delete(beneficioInput.Id);
        }

        /***************************************************************************************************
         * Detalle Plan Exequial
         * ************************************************************************************************/

        //  Método para obtener la lista de grupos familiares
        public GetAllGruposFamiliaresByPlanOutput GetAllGruposFamiliaresByPlan(GetAllGruposFamiliaresByPlanInput planExequial)
        {
            var listaGruposFamiliares = _grupoFamiliarRepositorio.GetAllList().Where(g => g.PlanExequialId == planExequial.PlanExequialId);
            return new GetAllGruposFamiliaresByPlanOutput { GruposFamiliares = Mapper.Map<List<GrupoFamiliarOutput>>(listaGruposFamiliares) };
        }

        //  Método para obtener un grupo familiar según el id
        public GetGrupoFamiliarOutput GetGrupoFamiliar(GetGrupoFamiliarInput grupoFamiliar)
        {
            return Mapper.Map<GetGrupoFamiliarOutput>(_grupoFamiliarRepositorio.Get(grupoFamiliar.Id));
        }

        //  Método para guardar un grupo familiar
        public SaveGrupoFamiliarOutput SaveGrupoFamiliar(SaveGrupoFamiliarInput nuevoGrupoFamiliar)
        {
            GrupoFamiliar existeGrupoFamiliar = _grupoFamiliarRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == nuevoGrupoFamiliar.Nombre.ToLower() && p.PlanExequialId == nuevoGrupoFamiliar.PlanExequialId);
            if (existeGrupoFamiliar != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_detallePlanExequial_save_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                nuevoGrupoFamiliar.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(nuevoGrupoFamiliar.Nombre.ToLower());
                nuevoGrupoFamiliar.EstadoId = _parametrosService.GetEstadoActivoGrupoFamiliar().Id;
                GrupoFamiliar grupoFamiliar = Mapper.Map<GrupoFamiliar>(nuevoGrupoFamiliar);
                grupoFamiliar.TenantId = AbpSession.GetTenantId();
                return new SaveGrupoFamiliarOutput { Id = _grupoFamiliarRepositorio.InsertAndGetId(grupoFamiliar) };
            }
        }

        //  Metodo para eliminar un grupo familiar
        public void DeleteGrupoFamiliar(DeleteGrupoFamiliarInput grupoFamiliarEliminar)
        {
            _grupoFamiliarRepositorio.Delete(grupoFamiliarEliminar.Id);
        }

        //  Metodo para actualizar un grupo familiar
        public void UpdateGrupoFamiliar(UpdateGrupoFamiliarInput editarGrupoFamiliar)
        {
            GrupoFamiliar existeGrupoFamiliar = _grupoFamiliarRepositorio.FirstOrDefault(d => d.Nombre.ToLower() == editarGrupoFamiliar.Nombre.ToLower() && d.PlanExequialId == editarGrupoFamiliar.PlanExequialId && d.Id != editarGrupoFamiliar.Id);
            if (existeGrupoFamiliar != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_grupoFamiliar_update_nombre_existente");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                editarGrupoFamiliar.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(editarGrupoFamiliar.Nombre.ToLower());
                _grupoFamiliarRepositorio.Update(Mapper.Map<GrupoFamiliar>(editarGrupoFamiliar));
            }
        }

        //  Metodo para validar si es posible eliminar un plan exequial
        public PuedeEliminarGrupoFamiliarOutput PuedeEliminarGrupoFamiliar(PuedeEliminarGrupoFamiliarInput grupoFamiliarEliminar)
        {
            //int countListaEmpresaInfoTributaria = _empresaInfoTributariaRepositorio.GetAllList().Where(d => d.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId == infoTributariaEliminar.Id).Count();
            int countListaGrupoFamiliar = 0;
            bool puedeEliminar = false;
            if (countListaGrupoFamiliar == 0)
                puedeEliminar = true;
            return new PuedeEliminarGrupoFamiliarOutput { PuedeEliminar = puedeEliminar };
        }

        /***************************************************************************************************
         * Parentescos Grupo Familiar
         * ************************************************************************************************/

        //  Método para obtener la lista de rangos de un parentesco
        public GetAllRangosParentescoByGrupoOutput GetAllRangosParentescoByGrupo(GetAllRangosParentescoByGrupoInput grupoFamiliar)
        {
            var listaRangosParentesco = _grupoParentescoRangoRepositorio.GetWithParentescoByGrupo(grupoFamiliar.GrupoFamiliarId);
            return new GetAllRangosParentescoByGrupoOutput { RangosParentesco = Mapper.Map<List<RangoParentescoOutput>>(listaRangosParentesco) };
        }

        //  Método para obtener la lista de rangos de un parentesco
        public GetAllRangosParentescoByGrupoAndParentescoOutput GetAllRangosParentescoByGrupoAndParentesco(GetAllRangosParentescoByGrupoAndParentescoInput grupoAndParentesco)
        {
            var listaRangosParentesco = _grupoParentescoRangoRepositorio.GetWithParentescoByGrupoAndParentesco(grupoAndParentesco.ParentescoId, grupoAndParentesco.GrupoFamiliarId);
            return new GetAllRangosParentescoByGrupoAndParentescoOutput { RangosParentesco = Mapper.Map<List<RangoParentescoOutput>>(listaRangosParentesco) };
        }

        //  Método para obtener un rango de un parentesco según el id
        public GetRangoParentescoOutput GetRangoParentesco(GetRangoParentescoInput rangoParentesco)
        {
            return Mapper.Map<GetRangoParentescoOutput>(_grupoParentescoRangoRepositorio.Get(rangoParentesco.Id));
        }

        //  Método para guardar un rango de parentesco
        public void SaveRangoParentesco(SaveRangoParentescoInput nuevoRangoParentesco)
        {
            GrupoParentescoRango parentescoRangoMinimo = _grupoParentescoRangoRepositorio.GetAll().Where(p => p.EdadMinima <= nuevoRangoParentesco.EdadMinima && p.EdadMaxima >= nuevoRangoParentesco.EdadMinima && p.GrupoFamiliarParentescoId == nuevoRangoParentesco.GrupoFamiliarParentescoId).FirstOrDefault();
            var mensajeError = "";
            if (parentescoRangoMinimo != null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_parentesco_save_edadMinima_seTraslapa");
            }

            GrupoParentescoRango parentescoRangoMaximo = _grupoParentescoRangoRepositorio.GetAll().Where(p => p.EdadMinima <= nuevoRangoParentesco.EdadMaxima && p.EdadMaxima >= nuevoRangoParentesco.EdadMaxima && p.GrupoFamiliarParentescoId == nuevoRangoParentesco.GrupoFamiliarParentescoId).FirstOrDefault();
            if (parentescoRangoMaximo != null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_parentesco_save_edadMaxima_seTraslapa");
            }

            if (mensajeError.Equals(""))
            {
                GrupoParentescoRango grupoParentescoRango = Mapper.Map<GrupoParentescoRango>(nuevoRangoParentesco);
                grupoParentescoRango.TenantId = AbpSession.GetTenantId();
                _grupoParentescoRangoRepositorio.Insert(grupoParentescoRango);
            }
            else
            {
                throw new UserFriendlyException(mensajeError);
            }
        }

        //  Metodo para eliminar un rango de parentesco
        public void DeleteRangoParentesco(DeleteRangoParentescoInput rangoParentescoEliminar)
        {
            int grupoFamiliarParentescoId = _grupoParentescoRangoRepositorio.GetAll().Where(r => r.Id == rangoParentescoEliminar.Id).FirstOrDefault().GrupoFamiliarParentescoId;
            _grupoParentescoRangoRepositorio.Delete(rangoParentescoEliminar.Id);
            GrupoParentescoRango parentescoRango = _grupoParentescoRangoRepositorio.GetAll().Where(g => g.GrupoFamiliarParentescoId == grupoFamiliarParentescoId && g.Id != rangoParentescoEliminar.Id).FirstOrDefault();
            if (parentescoRango == null)
            {
                _grupoFamiliarParentescoRepositorio.Delete(grupoFamiliarParentescoId);
            }
        }

        //  Metodo para actualizar un rango de parentesco
        public void UpdateRangoParentesco(UpdateRangoParentescoInput editarRangoParentesco)
        {
            GrupoParentescoRango parentescoRangoMinimo = _grupoParentescoRangoRepositorio.GetAll().Where(p => p.EdadMinima <= editarRangoParentesco.EdadMinima && p.EdadMaxima >= editarRangoParentesco.EdadMinima && p.GrupoFamiliarParentescoId == editarRangoParentesco.GrupoFamiliarParentescoId && p.Id != editarRangoParentesco.Id).FirstOrDefault();
            var mensajeError = "";
            if (parentescoRangoMinimo != null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_parentesco_update_edadMinima_seTraslapa");
            }

            GrupoParentescoRango parentescoRangoMaximo = _grupoParentescoRangoRepositorio.GetAll().Where(p => p.EdadMinima <= editarRangoParentesco.EdadMaxima && p.EdadMaxima >= editarRangoParentesco.EdadMaxima && p.GrupoFamiliarParentescoId == editarRangoParentesco.GrupoFamiliarParentescoId && p.Id != editarRangoParentesco.Id).FirstOrDefault();
            if (parentescoRangoMaximo != null)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_parentesco_update_edadMaxima_seTraslapa");
            }

            if (mensajeError.Equals(""))
            {
                _grupoParentescoRangoRepositorio.Update(Mapper.Map<GrupoParentescoRango>(editarRangoParentesco));
            }
            else
            {
                throw new UserFriendlyException(mensajeError);
            }
        }

        //  Metodo para validar si es posible eliminar un rango de parentesco
        public PuedeEliminarRangoParentescoOutput PuedeEliminarRangoParentesco(PuedeEliminarRangoParentescoInput rangoParentescoEliminar)
        {
            //int countListaEmpresaInfoTributaria = _empresaInfoTributariaRepositorio.GetAllList().Where(d => d.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId == infoTributariaEliminar.Id).Count();
            int countListaRangoParentesco = 0;
            bool puedeEliminar = false;
            if (countListaRangoParentesco == 0)
                puedeEliminar = true;
            return new PuedeEliminarRangoParentescoOutput { PuedeEliminar = puedeEliminar };
        }

        //  Método para obtener la lista de parentescos
        public GetAllParentescosOutput GetAllParentescos()
        {
            var listaParentescos = _parentescoRepositorio.GetAllList().OrderBy(p => p.Posicion);
            return new GetAllParentescosOutput { Parentescos = Mapper.Map<List<ParentescoOutput>>(listaParentescos) };
        }

        //  Método para obtener información del grupo familiar parentesco
        public GetGrupoFamiliarParentescoOutput GetGrupoFamiliarParentesco(GetGrupoFamiliarParentescoInput parentesco)
        {
            GrupoFamiliarParentesco grupoParentesco = _grupoFamiliarParentescoRepositorio.GetAll().Where(p => p.ParentescoId == parentesco.ParentescoId && p.GrupoFamiliarId == parentesco.GrupoFamiliarId).FirstOrDefault();
            if (grupoParentesco == null)
            {
                grupoParentesco = new GrupoFamiliarParentesco { Id = 0, ValidarSoloIngreso = BowConsts.PARENTESCO_SI_VALIDA_SOLO_INGRESO };
            }
            return Mapper.Map<GetGrupoFamiliarParentescoOutput>(grupoParentesco);
        }

        //  Método para guardar un parentesco asociado a un grupo familiar
        public SaveOrUpdateGrupoFamiliarParentescoOutput SaveOrUpdateGrupoFamiliarParentesco(SaveOrUpdateGrupoFamiliarParentescoInput grupoFamiliarParentescoInpunt)
        {
            GrupoFamiliarParentesco existeGrupoFamiliarParentesco = _grupoFamiliarParentescoRepositorio.FirstOrDefault(p => p.ParentescoId == grupoFamiliarParentescoInpunt.ParentescoId && p.GrupoFamiliarId == grupoFamiliarParentescoInpunt.GrupoFamiliarId);
            SaveOrUpdateGrupoFamiliarParentescoOutput saveOrUpdateGrupoFamiliarParentesco = new SaveOrUpdateGrupoFamiliarParentescoOutput();
            if (existeGrupoFamiliarParentesco != null)
            {
                existeGrupoFamiliarParentesco.ValidarSoloIngreso = grupoFamiliarParentescoInpunt.ValidarSoloIngreso;
                _grupoFamiliarParentescoRepositorio.Update(existeGrupoFamiliarParentesco);
                saveOrUpdateGrupoFamiliarParentesco.Id = existeGrupoFamiliarParentesco.Id;
            }
            else
            {
                GrupoFamiliarParentesco grupoFamiliarParentesco = Mapper.Map<GrupoFamiliarParentesco>(grupoFamiliarParentescoInpunt);
                grupoFamiliarParentesco.TenantId = AbpSession.GetTenantId();
                saveOrUpdateGrupoFamiliarParentesco.Id = _grupoFamiliarParentescoRepositorio.InsertAndGetId(grupoFamiliarParentesco);
            }
            return saveOrUpdateGrupoFamiliarParentesco;
        }

        //  Método para obtener la lista de parentesco con la lista de rangos
        public GetAllParentescosAllRangosOutput GetAllParentescosAllRangos(GetAllParentescosAllRangosInput grupoFamiliar)
        {
            var listaGrupoFamiliarParentesco = _grupoFamiliarParentescoRepositorio.GetWithRangos(grupoFamiliar.PlanExequialId, grupoFamiliar.GrupoFamiliarId);
            return new GetAllParentescosAllRangosOutput { Parentescos = Mapper.Map<List<ParentescoAllRangosOutput>>(listaGrupoFamiliarParentesco) };
        }

        //Metodo consultar los periodos de ventas registradas en el sistema, se calcula el intervalo en días de la diferencia entre fecha inicio y fin
        public GetPeriodosVentasRegistradosOutput GetPeriodosVentasRegistrados()
        {
            List<PeriodoVenta> periodosVentas = _periodoVentaRepositorio.GetAllList();

            GetPeriodosVentasRegistradosOutput listaPeriodosVentas = new GetPeriodosVentasRegistradosOutput { PeriodosRegistrados = Mapper.Map<List<PeriodoVentaOutPut>>(periodosVentas) };

            //Se identifica la fecha inicial para el proximo periodo de venta, se recaulcula el rango para el datepicker de fecha Inicio
            if (periodosVentas.Count() != 0)
            {
                //Se verifica la fecha final del periodo anterior y se coloca la fecha minima y maxima esa fecha mas un día
                DateTime fechaMinima = _periodoVentaRepositorio.GetAll().Max(p => p.FechaFin);
                fechaMinima = fechaMinima.AddDays(1);

                listaPeriodosVentas.FechaInicioMinima = fechaMinima;
                listaPeriodosVentas.FechaInicioMaxima = fechaMinima;
                listaPeriodosVentas.FechaInicioProxima = fechaMinima;
            }
            else
            {
                DateTime fechaMaxima = DateTime.Now.AddYears(BowConsts.AÑOS_FECHA_INICIO_MAXIMA_PERIODO_VENTA);
                DateTime fechaMinima = DateTime.Now.AddYears(-BowConsts.AÑOS_FECHA_INICIO_MINIMA_PERIODO_VENTA);

                listaPeriodosVentas.FechaInicioMinima = fechaMinima;
                listaPeriodosVentas.FechaInicioMaxima = fechaMaxima;
                listaPeriodosVentas.FechaInicioProxima = null;
            }

            for (int i = 0; i < listaPeriodosVentas.PeriodosRegistrados.Count(); i++)
            {
                DateTime fechaInicio = Convert.ToDateTime(listaPeriodosVentas.PeriodosRegistrados[i].FechaInicio);
                DateTime fechaFin = Convert.ToDateTime(listaPeriodosVentas.PeriodosRegistrados[i].FechaFin);

                TimeSpan diferencia = fechaFin - fechaInicio;

                listaPeriodosVentas.PeriodosRegistrados[i].Intervalo = (diferencia.Days + 1) + " " + LocalizationHelper.GetString("Bow", "afiliaciones_periodosventas_intervaloDias");
            }

            return listaPeriodosVentas;
        }

        //Metodo para verificar los periodos de ventas de acuerdo al periodo ingresado
        public GetVerificarPeriodosVentasOutput GetVerificarPeriodosVentas(GetVerificarPeriodosVentasInput periodoInput)
        {
            var mensajeError = "";

            GetVerificarPeriodosVentasOutput periodosCalculados = new GetVerificarPeriodosVentasOutput();

            DateTime fechaInicio = Convert.ToDateTime(periodoInput.FechaInicio);
            DateTime fechaFin = Convert.ToDateTime(periodoInput.FechaFin);

            //Se verifica que el nombre no exista en la base de datos
            var periodoNombre = periodoInput.Prefijo + periodoInput.PeriodoInicial;
            var existePeriodo = _periodoVentaRepositorio.FirstOrDefault(p => p.Nombre == periodoNombre);

            //Se verifica que no se traslapalen las fechas, consultando el rango de fechas ingresadas con las de la base de datos
            var existeFecha = _periodoVentaRepositorio.FirstOrDefault(p => p.FechaInicio >= fechaInicio && p.FechaFin <= fechaFin || p.FechaInicio <= fechaFin && p.FechaFin >= fechaInicio);

            //Se consulta si la base de datos tiene registros
            List<PeriodoVenta> periodosVentas = _periodoVentaRepositorio.GetAllList();
            DateTime fechaMaxima = new DateTime();

            if (periodosVentas.Count() != 0)
            {
                //Se consulta la fecha minima y maxima que esta en la base de datos
                DateTime fechaMinima = _periodoVentaRepositorio.GetAll().Min(p => p.FechaInicio);
                fechaMaxima = _periodoVentaRepositorio.GetAll().Max(p => p.FechaFin);

                //Se le agrega un día a la fecha máxima para obtener la fecha correcta para el inicio del nuevo periodo.
                fechaMaxima = fechaMaxima.AddDays(1);
            }

            if (existePeriodo == null)
            {
                if (existeFecha == null)
                {
                    if (periodosVentas.Count() != 0 && fechaInicio != fechaMaxima)
                    {
                        mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_periodosventas_fechaInicioIncorrecta");
                        throw new UserFriendlyException(mensajeError);
                    }
                    else
                    {
                        List<PeriodoVentaOutPut> lista = new List<PeriodoVentaOutPut>();

                        periodoInput.PeriodoInicial = periodoInput.PeriodoInicial - 1;

                        while (fechaInicio < fechaFin)
                        {
                            PeriodoVentaOutPut periodo = new PeriodoVentaOutPut();

                            if (periodoInput.Tiempo == LocalizationHelper.GetString("Bow", "afiliaciones_periodosventas_intervaloDias"))
                            {
                                periodo.FechaInicio = fechaInicio;
                                fechaInicio = fechaInicio.AddDays(periodoInput.Intervalo);
                                periodo.FechaFin = fechaInicio.AddDays(-1);
                                periodoInput.PeriodoInicial = periodoInput.PeriodoInicial + 1;
                                periodo.Nombre = periodoInput.Prefijo + periodoInput.PeriodoInicial;
                                periodo.Intervalo = periodoInput.Intervalo + " " + periodoInput.Tiempo;

                                if (fechaInicio.AddDays(-1) <= fechaFin)
                                {
                                    lista.Add(periodo);
                                }
                            }
                            else if (periodoInput.Tiempo == LocalizationHelper.GetString("Bow", "afiliaciones_periodosventas_intervaloMeses"))
                            {
                                periodo.FechaInicio = fechaInicio;
                                fechaInicio = fechaInicio.AddMonths(periodoInput.Intervalo);
                                periodo.FechaFin = fechaInicio.AddDays(-1);

                                periodoInput.PeriodoInicial = periodoInput.PeriodoInicial + 1;
                                periodo.Nombre = periodoInput.Prefijo + periodoInput.PeriodoInicial;

                                periodo.Intervalo = periodoInput.Intervalo + " " + periodoInput.Tiempo;

                                if (fechaInicio.AddDays(-1) <= fechaFin)
                                {
                                    lista.Add(periodo);
                                }
                            }

                        }
                        periodosCalculados.PeriodosCalculados = lista;
                    }
                }
                else
                {
                    mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_periodosventas_existeFecha");
                    throw new UserFriendlyException(mensajeError);
                }
            }
            else
            {
                mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_periodosventas_existePeriodoVentaNombre");
                throw new UserFriendlyException(mensajeError);
            }

            return periodosCalculados;
        }

        public void SavePeriodosVentas(SavePeriodosVentasInput periodosInput)
        {
            GetVerificarPeriodosVentasInput periodo = Mapper.Map<GetVerificarPeriodosVentasInput>(periodosInput);

            GetVerificarPeriodosVentasOutput verificarPeriodos = GetVerificarPeriodosVentas(periodo);

            for (int i = 0; i < verificarPeriodos.PeriodosCalculados.Count(); i++)
            {
                PeriodoVenta periodoVenta = Mapper.Map<PeriodoVenta>(verificarPeriodos.PeriodosCalculados[i]);
                periodoVenta.TenantId = AbpSession.GetTenantId();
                _periodoVentaRepositorio.Insert(periodoVenta);
            }
        }

        public bool DeletePeriodosVentas(DeletePeriodosVentasInput periodosEliminarInput)
        {
            bool puedeEliminar = true;

            if (periodosEliminarInput.PeriodosEliminar.Count() != 0)
            {
                //Se verifica si cada elemento de la lista tiene fechas superiores, si es asi no se puede eliminar
                for (int i = 0; i < periodosEliminarInput.PeriodosEliminar.Count(); i++)
                {
                    DateTime fechaEliminar = Convert.ToDateTime(periodosEliminarInput.PeriodosEliminar[i].FechaFin);
                    DeletePeriodosVentasInput listaSuperiores = new DeletePeriodosVentasInput { PeriodosEliminar = Mapper.Map<List<PeriodoVentaInput>>(_periodoVentaRepositorio.GetAllList().Where(p => p.FechaFin >= fechaEliminar).ToList()) };

                    //Se comparan las dos listas para verificar si son iguales, lo que significaría que estan en secuencia y al final de la grilla de lo contrario no se puede eliminar
                    HashSet<int> diffids = new HashSet<int>(periodosEliminarInput.PeriodosEliminar.Select(s => s.Id));
                    var lista = listaSuperiores.PeriodosEliminar.Where(m => !diffids.Contains(m.Id)).ToList();

                    //Si las listas son diferentes(lista tendría registros) no se puede eliminar
                    if (lista.Count != 0)
                    {
                        puedeEliminar = false;
                        break;
                    }
                    else
                    {
                        _periodoVentaRepositorio.Delete(periodosEliminarInput.PeriodosEliminar[i].Id);
                    }
                }
            }

            return puedeEliminar;

        }

        //Método para calcular el rango de la fecha de inicio del periodo de venta (Carga al iniciar el formulario)
        public FechaFinPeriodoVentaOutput FechaFinPeriodoVenta(FechaFinPeriodoVentaInput fechainicioInput)
        {
            DateTime fechaMinima = fechainicioInput.FechaInicioMinima;
            DateTime fechaMaxima = DateTime.Now.AddYears(BowConsts.AÑOS_FECHA_FIN_MAXIMA_PERIODO_VENTA);

            FechaFinPeriodoVentaOutput fechasFin = new FechaFinPeriodoVentaOutput();

            fechasFin.FechaFinMinima = fechaMinima;
            fechasFin.FechaFinMaxima = fechaMaxima;

            return fechasFin;
        }

        /***************************************************************************************************
         * Beneficios Plan Exequial
         * ************************************************************************************************/

        //  Método para obtener la lista beneficios del plan exequial
        public GetAllBeneficiosPlanExequialOutput GetAllBeneficiosPlanExequial(GetAllBeneficiosPlanExequialInput planExequial)
        {
            var listaBeneficios = _beneficioPlanExequialRepositorio.GetWithTipo(planExequial.PlanExequialId);
            return new GetAllBeneficiosPlanExequialOutput { Beneficios = Mapper.Map<List<BeneficioPlanExequialOutPut>>(listaBeneficios) };
        }

        //  Método para obtener la lista beneficios del plan exequial
        public GetAllBeneficiosAdicionalesPlanExequialOutput GetAllBeneficiosAdicionalesPlanExequial(GetAllBeneficiosAdicionalesPlanExequialInput planExequial)
        {
            var listaBeneficios = _beneficioAdicionalPlanExequialRepositorio.GetWithTipo(planExequial.PlanExequialId);
            return new GetAllBeneficiosAdicionalesPlanExequialOutput { Beneficios = Mapper.Map<List<BeneficioAdicionalPlanExequialOutPut>>(listaBeneficios) };
        }

        //  Método para obtener un beneficios del plan exequial según el id
        public GetBeneficioPlanExequialOutput GetBeneficioPlanExequial(GetBeneficioPlanExequialInput beneficioPlanExequial)
        {
            return Mapper.Map<GetBeneficioPlanExequialOutput>(_beneficioPlanExequialRepositorio.GetBeneficioWithTipo(beneficioPlanExequial.Id));
        }

        //  Método para obtener beneficios del plan exequial según el id
        public GetBeneficioAdicionalPlanExequialOutput GetBeneficioAdicionalPlanExequial(GetBeneficioAdicionalPlanExequialInput beneficioAdicionalPlanExequial)
        {
            return Mapper.Map<GetBeneficioAdicionalPlanExequialOutput>(_beneficioAdicionalPlanExequialRepositorio.GetBeneficioWithTipo(beneficioAdicionalPlanExequial.Id));
        }

        //  Método para guardar un beneficio del plan exequial
        public void SaveBeneficioPlanExequial(SaveBeneficioPlanExequialInput nuevoBeneficioPlanExequial)
        {
            nuevoBeneficioPlanExequial.FechaIngreso = DateTime.Now.ToString();
            nuevoBeneficioPlanExequial.EstadoId = _parametrosService.GetEstadoActivoBeneficiosPlanExequial().Id;
            BeneficioPlanExequial beneficioPlanExequial = Mapper.Map<BeneficioPlanExequial>(nuevoBeneficioPlanExequial);
            beneficioPlanExequial.TenantId = AbpSession.GetTenantId();
            _beneficioPlanExequialRepositorio.Insert(beneficioPlanExequial);
        }

        //  Método para guardar un beneficio del plan exequial
        public void SaveBeneficioAdicionalPlanExequial(SaveBeneficioAdicionalPlanExequialInput nuevoBeneficioAdicionalPlanExequial)
        {
            nuevoBeneficioAdicionalPlanExequial.FechaIngreso = DateTime.Now.ToString();
            nuevoBeneficioAdicionalPlanExequial.EstadoId = _parametrosService.GetEstadoActivoBeneficiosPlanExequial().Id;
            BeneficioAdicionalPlanExequial beneficioAdicionalPlanExequial = Mapper.Map<BeneficioAdicionalPlanExequial>(nuevoBeneficioAdicionalPlanExequial);
            beneficioAdicionalPlanExequial.TenantId = AbpSession.GetTenantId();
            _beneficioAdicionalPlanExequialRepositorio.Insert(beneficioAdicionalPlanExequial);
        }

        //  Metodo para eliminar un beneficio del plan exequial
        public void DeleteBeneficioPlanExequial(DeleteBeneficioPlanExequialInput beneficioPlanExequialEliminar)
        {
            _beneficioPlanExequialRepositorio.Delete(beneficioPlanExequialEliminar.Id);
        }

        //  Metodo para eliminar un beneficio adicional del plan exequial
        public void DeleteBeneficioAdicionalPlanExequial(DeleteBeneficioAdicionalPlanExequialInput beneficioAdicionalPlanExequialEliminar)
        {
            _beneficioAdicionalPlanExequialRepositorio.Delete(beneficioAdicionalPlanExequialEliminar.Id);
        }

        //  Metodo para actualizar un beneficio del plan exequial
        public void UpdateBeneficioPlanExequial(UpdateBeneficioPlanExequialInput editarBeneficioPlanExequial)
        {
            if (editarBeneficioPlanExequial.EstadoId == _parametrosService.GetEstadoInactivoBeneficiosPlanExequial().Id)
            {
                editarBeneficioPlanExequial.FechaCancelacion = DateTime.Now.ToString();
            }
            _beneficioPlanExequialRepositorio.Update(Mapper.Map<BeneficioPlanExequial>(editarBeneficioPlanExequial));
        }

        //  Metodo para actualizar un beneficio del plan exequial
        public void UpdateBeneficioAdicionalPlanExequial(UpdateBeneficioAdicionalPlanExequialInput editarBeneficioAdicionalPlanExequial)
        {
            if (editarBeneficioAdicionalPlanExequial.EstadoId == _parametrosService.GetEstadoInactivoBeneficiosPlanExequial().Id)
            {
                editarBeneficioAdicionalPlanExequial.FechaCancelacion = DateTime.Now.ToString();
            }
            _beneficioAdicionalPlanExequialRepositorio.Update(Mapper.Map<BeneficioAdicionalPlanExequial>(editarBeneficioAdicionalPlanExequial));
        }

        //  Metodo para validar si es posible eliminar un beneficio del plan exequial
        public PuedeEliminarBeneficioPlanExequialOutput PuedeEliminarBeneficioPlanExequial(PuedeEliminarBeneficioPlanExequialInput beneficioPlanExequialEliminar)
        {
            //int countListaBeneficios = _empresaInfoTributariaRepositorio.GetAllList().Where(d => d.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId == infoTributariaEliminar.Id).Count();
            int countListaBeneficios = 0;
            bool puedeEliminar = false;
            if (countListaBeneficios == 0)
                puedeEliminar = true;
            return new PuedeEliminarBeneficioPlanExequialOutput { PuedeEliminar = puedeEliminar };
        }

        //  Metodo para obtener los tipos de beneficios con el número de beneficios de cada uno
        public GetAllTiposBeneficioPlanExequialOutput GetAllTiposBeneficioPlanExequial(GetAllTiposBeneficioPlanExequialInput planExequial)
        {
            var listaTiposTipos = _parametrosService.GetAllTiposBeneficio().TiposGruposBeneficio;
            List<TipoBeneficioPlanExequial> listaTipos = new List<TipoBeneficioPlanExequial>();
            foreach (var tipo in listaTiposTipos)
            {
                if (tipo.Nombre.Equals(BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_PROPIO))
                {
                    listaTipos.Add(new TipoBeneficioPlanExequial { Id = tipo.Id, Nombre = tipo.Nombre, NumeroBeneficiosPlanExequial = _beneficioPlanExequialRepositorio.GetAll().Where(p => p.PlanExequialId == planExequial.PlanExequialId).Count() });
                }
                else if (tipo.Nombre.Equals(BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_ADICIONAL))
                {
                    listaTipos.Add(new TipoBeneficioPlanExequial { Id = tipo.Id, Nombre = tipo.Nombre, NumeroBeneficiosPlanExequial = _beneficioAdicionalPlanExequialRepositorio.GetAll().Where(p => p.PlanExequialId == planExequial.PlanExequialId).Count() });
                }
            }
            return new GetAllTiposBeneficioPlanExequialOutput { TiposBeneficioPlanExequial = listaTipos };
        }

        // Metodo para obtener los beneficios de la categoria indicada
        public GetBeneficiosPlanExequialByCategoriaOutput GetBeneficiosPlanExequialByCategoria(GetBeneficiosPlanExequialByCategoriaInput categoriaInput)
        {
            GetBeneficiosPlanExequialByCategoriaOutput lista = new GetBeneficiosPlanExequialByCategoriaOutput();

            List<Beneficio> listaBeneficiosYaAsignados = new List<Beneficio>();
            if (categoriaInput.GrupoBeneficio == BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_PROPIO)
            {
                listaBeneficiosYaAsignados = _beneficioPlanExequialRepositorio.GetAll().Where(b => b.PlanExequialId == categoriaInput.PlanExequialId).Select(b => b.BeneficioBeneficioPlanExequial).ToList();
            }
            else if (categoriaInput.GrupoBeneficio == BowConsts.TIPO_BENEFICIO_PLAN_EXEQUIAL_ADICIONAL)
            {
                listaBeneficiosYaAsignados = _beneficioAdicionalPlanExequialRepositorio.GetAll().Where(b => b.PlanExequialId == categoriaInput.PlanExequialId).Select(b => b.BeneficioBeneficioAdicionalPlanExequial).ToList();
            }
            lista.Beneficios = Mapper.Map<List<BeneficioOutPut>>(_beneficioRepositorio.GetWithTipoCategoria(categoriaInput.TipoCategoriaId).Except(listaBeneficiosYaAsignados));
            return lista;
        }

        //  Método para obtener la lista beneficios del plan exequial
        public GetAllBeneficiosPlanExequialByTipoOutput GetAllBeneficiosPlanExequialByTipo(GetAllBeneficiosPlanExequialByTipoInput planExequialAndTipo)
        {
            var listaBeneficios = _beneficioPlanExequialRepositorio.GetAll().Where(b => b.PlanExequialId == planExequialAndTipo.PlanExequialId && b.BeneficioBeneficioPlanExequial.TipoId == planExequialAndTipo.TipoId);
            return new GetAllBeneficiosPlanExequialByTipoOutput { Beneficios = Mapper.Map<List<BeneficioPlanExequialByTipoOutput>>(listaBeneficios) };
        }

        //  Método para obtener la lista beneficios del plan exequial
        public GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicional(GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalInput planExequialAndTipoAndBeneficioAdicional)
        {
            List<BeneficioPlanExequial> listaBeneficios = _beneficioPlanExequialRepositorio.GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput(planExequialAndTipoAndBeneficioAdicional.PlanExequialId, planExequialAndTipoAndBeneficioAdicional.TipoId, planExequialAndTipoAndBeneficioAdicional.BeneficioAdicionalId);
            List<BeneficioPlanExequial> listaFinalBeneficios = new List<BeneficioPlanExequial>();
            foreach(BeneficioPlanExequial item in listaBeneficios) {
                List<BeneficioAdicionalPlanExequial> listaBeneficiosYaAsignados = _beneficioAdicionalPlanExequialRepositorio.GetAll().Where(ba => ba.BeneficioId == planExequialAndTipoAndBeneficioAdicional.BeneficioAdicionalId && ba.BeneficioPlanExequialAdicional.BeneficioId == item.BeneficioId).ToList();
                if (listaBeneficiosYaAsignados.Count() == 0)
                {
                    listaFinalBeneficios.Add(item);
                }
            }
            return new GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput { Beneficios = Mapper.Map<List<BeneficioPlanExequialByTipoOutput>>(listaFinalBeneficios) };
        }

        /***************************************************************************************************
         * Sucursales Plan Exequial
         * ************************************************************************************************/

        // Metodo para obtener todas las empresas con las sucursales asignadas al plan exequial
        public GetAllEmpresasPlanExequialOutput GetAllEmpresasPlanExequial(GetAllEmpresasPlanExequialInput empresasPlanExequial)
        {
            var listaEmpresas = _empresasService.GetAllEmpresasWithSucursalesByOrganizacion(new Empresas.DTOs.InputModels.GetAllEmpresasWithSucursalesByOrganizacionInput { OrganizacionId = empresasPlanExequial.OrganizacionId });
            GetAllEmpresasPlanExequialOutput listaEmpresasPlanExequial = new GetAllEmpresasPlanExequialOutput { EmpresasPlanExequial = new List<EmpresaPlanExequialOutput>() };
            foreach (var item in listaEmpresas.Empresas)
            {
                int sucursalesAsignadas = _planExequialSucursalRepositorio.GetAll().Where(s => s.SucursalPlanExequialSucursal.EmpresaOrganizacion.EmpresaId == item.Id && s.PlanExequialId == empresasPlanExequial.PlanExequialId).Count();
                listaEmpresasPlanExequial.EmpresasPlanExequial.Add(new EmpresaPlanExequialOutput
                {
                    EmpresaOrganizacionId = item.Id,
                    EmpresaNombre = item.Empresa,
                    SucursalesAsignadas = sucursalesAsignadas,
                    SucursalesNoAsignadas = item.NumeroSucursales - sucursalesAsignadas
                });
            }
            return listaEmpresasPlanExequial;
        }

        // Metodo para obtener todas las sucursales de una empresa indicando las que están asignadas al plan exequial
        public GetAllSucursalesPlanExequialOutput GetAllSucursalesPlanExequial(GetAllSucursalesPlanExequialInput empresasPlanExequial)
        {
            var listaSucursales = _empresasService.GetAllSucursalesEmpresa(new Empresas.DTOs.InputModels.GetAllSucursalesEmpresaInput { EmpresaOrganizacionId = empresasPlanExequial.EmpresaOrganizacionId });
            GetAllSucursalesPlanExequialOutput listaSucursalesPlanExequial = new GetAllSucursalesPlanExequialOutput { SucursalesPlanExequial = new List<SucursalPlanExequialOutput>() };
            foreach (var item in listaSucursales.Sucursales)
            {
                listaSucursalesPlanExequial.SucursalesPlanExequial.Add(new SucursalPlanExequialOutput
                {
                    SucursalId = item.Id,
                    SucursalNombre = item.Nombre,
                    SucursalesAsignada = _planExequialSucursalRepositorio.FirstOrDefault(s => s.PlanExequialId == empresasPlanExequial.PlanExequialId && s.SucursalId == item.Id) == null ? false : true
                });
            }
            return listaSucursalesPlanExequial;
        }

        //  Metodo para actualizar la información sucursales asignadas al plan exequial
        public void UpdateSucursalesPlanExequial(UpdateSucursalesPlanExequialInput sucursalPlanExequial)
        {
            foreach (SucursalPlanExequialInput item in sucursalPlanExequial.ListaSucursales)
            {
                if (item.AsignadoAlPlan)
                {
                    PlanExequialSucursal planExequialSucursal = Mapper.Map<PlanExequialSucursal>(item);
                    planExequialSucursal.TenantId = AbpSession.GetTenantId();
                    _planExequialSucursalRepositorio.Insert(planExequialSucursal);
                }
                else
                {
                    _planExequialSucursalRepositorio.Delete(s => s.SucursalId == item.SucursalId && s.PlanExequialId == item.PlanExequialId);
                }
            }
        }

        /***************************************************************************************************
         * Localidades de Convenios de Recaudo Masivo
         * ************************************************************************************************/

        public GetAllLocalidadesByConvenioOutput GetAllLocalidadesByConvenio(GetAllLocalidadesByConvenioInput convenioInput)
        {
            var listaConvenioLocalidades = _recaudoMasivoCoberturaRepositorio.GetAllLocalidadAndDepartamentoAndPaisByConvenio(convenioInput.RecaudoMasivoId);
            return new GetAllLocalidadesByConvenioOutput { RecaudoMasivoLocalidades = Mapper.Map<List<LocalidadesByConvenioOutput>>(listaConvenioLocalidades) };
        }

        public GetAllLocalidadesByConvenioAndPaisOutput GetAllLocalidadesByConvenioAndPais(GetAllLocalidadesByConvenioAndPaisInput convenioAndPaisInput)
        {
            var listaConvenioLocalidades = _recaudoMasivoCoberturaRepositorio.GetAllLocalidadByConvenioAndPais(convenioAndPaisInput.RecaudoMasivoId, convenioAndPaisInput.PaisId);
            return new GetAllLocalidadesByConvenioAndPaisOutput { RecaudoMasivoLocalidades = Mapper.Map<List<LocalidadesByConvenioAndPaisOutput>>(listaConvenioLocalidades) };
        }

        public GetAllLocalidadesByConvenioAndDepartamentoOutput GetAllLocalidadesByConvenioAndDepartamento(GetAllLocalidadesByConvenioAndDepartamentoInput convenioAndPaisAndDepartamentoInput)
        {
            var listaConvenioLocalidades = _recaudoMasivoCoberturaRepositorio.GetAllLocalidadByConvenioAndDepartamento(convenioAndPaisAndDepartamentoInput.RecaudoMasivoId, convenioAndPaisAndDepartamentoInput.DepartamentoId);
            return new GetAllLocalidadesByConvenioAndDepartamentoOutput { RecaudoMasivoLocalidades = Mapper.Map<List<LocalidadesByConvenioAndPaisOutput>>(listaConvenioLocalidades) };
        }

        public GetAllLocalidadesByNotConvenioOutput GetAllLocalidadesByNotConvenio(GetAllLocalidadesByNotConvenioInput localidadesByNotConvenio)
        {
            GetAllLocalidadesWithFilterInput listaRecaudoMasivoLocalidades = new GetAllLocalidadesWithFilterInput();
            listaRecaudoMasivoLocalidades.listaLocalidadesQueNoSeMuestra = Mapper.Map<List<LocalidadInput>>(_recaudoMasivoCoberturaRepositorio.GetAllLocalidadAndDepartamentoAndPaisByConvenio(localidadesByNotConvenio.RecaudoMasivoId));

            GetAllLocalidadesWithFilterOutput listaLocalidadesRecaudoMasivoFiltradas = _zonificacionService.GetAllLocalidadesWithFilter(listaRecaudoMasivoLocalidades);

            return new GetAllLocalidadesByNotConvenioOutput { RecaudoMasivoLocalidades = Mapper.Map<List<LocalidadByNotConvenioOutput>>(listaLocalidadesRecaudoMasivoFiltradas.Localidades) };
        }

        public GetAllLocalidadesByNotConvenioAndPaisOutput GetAllLocalidadesByNotConvenioAndPais(GetAllLocalidadesByNotConvenioAndPaisInput localidadesByNotConvenioAndPais)
        {
            GetAllLocalidadesByPaisWithFilterInput listaRecaudoMasivoLocalidades = new GetAllLocalidadesByPaisWithFilterInput();
            listaRecaudoMasivoLocalidades.listaLocalidadesQueNoSeMuestra = _recaudoMasivoCoberturaRepositorio.GetAllLocalidadByConvenioAndPais(localidadesByNotConvenioAndPais.RecaudoMasivoId, localidadesByNotConvenioAndPais.PaisId);
            listaRecaudoMasivoLocalidades.PaisId = localidadesByNotConvenioAndPais.PaisId;

            GetAllLocalidadesByPaisWithFilterOutput listaInfoTributariaLocalidadesFiltradas = _zonificacionService.GetAllLocalidadesByPaisWithFilter(listaRecaudoMasivoLocalidades);

            return new GetAllLocalidadesByNotConvenioAndPaisOutput { RecaudoMasivoLocalidades = Mapper.Map<List<LocalidadByNotConvenioAndPaisOutput>>(listaInfoTributariaLocalidadesFiltradas.Localidades) };
        }

        public GetAllLocalidadesByNotConvenioAndDepartamentoOutput GetAllLocalidadesByNotConvenioAndDepartamento(GetAllLocalidadesByNotConvenioAndDepartamentoInput localidadesByNotConvenioAndDepartamento)
        {
            GetAllLocalidadesByDepartamentoWithFilterInput listaRecaudoMasivoLocalidades = new GetAllLocalidadesByDepartamentoWithFilterInput();
            listaRecaudoMasivoLocalidades.listaLocalidadesQueNoSeMuestra = _recaudoMasivoCoberturaRepositorio.GetAllLocalidadByConvenioAndDepartamento(localidadesByNotConvenioAndDepartamento.RecaudoMasivoId, localidadesByNotConvenioAndDepartamento.DepartamentoId);
            listaRecaudoMasivoLocalidades.DepartamentoId = localidadesByNotConvenioAndDepartamento.DepartamentoId;

            GetAllLocalidadesByDepartamentoWithFilterOutput listaInfoTributariaLocalidadesFiltradas = _zonificacionService.GetAllLocalidadesByDepartamentoWithFilter(listaRecaudoMasivoLocalidades);

            return new GetAllLocalidadesByNotConvenioAndDepartamentoOutput { RecaudoMasivoLocalidades = Mapper.Map<List<LocalidadesByNotConvenioAndDepartamentoOutput>>(listaInfoTributariaLocalidadesFiltradas.Localidades) };
        }

        public GetAllPaisesByConvenioOutput GetAllPaisesByConvenio(GetAllPaisesByConvenioInput convenioInput)
        {
            var listaRecaudoMasivoPaises = _recaudoMasivoCoberturaRepositorio.GetAllPaisesByConvenio(convenioInput.RecaudoMasivoId);
            return new GetAllPaisesByConvenioOutput { RecaudoMasivoPaises = Mapper.Map<List<PaisByConvenioOutput>>(listaRecaudoMasivoPaises) };
        }

        //Método para guardar un prospecto
        public SaveProspectoOutput SaveProspecto(SaveProspectoInput nuevoProspecto)
        {
            SaveProspectoOutput prospectoSave = new SaveProspectoOutput();
            prospectoSave = Mapper.Map<SaveProspectoOutput>(nuevoProspecto);

            //Paso para guardar el tenantId
            Prospecto prosp = new Prospecto();
            prosp = Mapper.Map<Prospecto>(nuevoProspecto);
            prosp.TenantId = AbpSession.TenantId.Value;
            Prospecto prospecto = Mapper.Map<Prospecto>(prosp);
            prospecto.TenantId = AbpSession.GetTenantId();
            prospectoSave.Id = _prospectoRepositorio.InsertAndGetId(prospecto);
            //prospecto.Id = _prospectoRepositorio.InsertAndGetId(Mapper.Map<Prospecto>(nuevoProspecto));

            return prospectoSave;
        }

        //Método para guardar gestionProspecto
        public SaveGestionProspectoOutput SaveGestionProspecto(SaveGestionProspectoInput gestionProspectoInput)
        {
            SaveGestionProspectoOutput gestionProspectoSave = new SaveGestionProspectoOutput();

            if (gestionProspectoInput.Id == 0)
            {
                gestionProspectoSave = Mapper.Map<SaveGestionProspectoOutput>(gestionProspectoInput);
                gestionProspectoInput.FechaGestion = DateTime.Now;

                //Paso para guardar el tenantId
                GestionProspecto gest = new GestionProspecto();
                gest = Mapper.Map<GestionProspecto>(gestionProspectoInput);
                gest.TenantId = AbpSession.TenantId.Value;

                GestionProspecto gestionProspecto = Mapper.Map<GestionProspecto>(gest);
                gestionProspecto.TenantId = AbpSession.GetTenantId();
                gestionProspectoSave.Id = _gestionProspectoRepositorio.InsertAndGetId(gestionProspecto);
                //gestionProspecto.Id = _gestionProspectoRepositorio.InsertAndGetId(Mapper.Map<GestionProspecto>(gestionProspectoInput));
            }
            else
            {
                var gestionEditar = _gestionProspectoRepositorio.Get(gestionProspectoInput.Id);

                gestionEditar.EmpresaAfiliada = gestionProspectoInput.EmpresaAfiliada;
                gestionEditar.Observaciones = gestionProspectoInput.Observaciones;
                gestionEditar.FunerariaAfiliadoId = gestionProspectoInput.FunerariaAfiliadoId;
                gestionEditar.EstadoNoAfiliacionId = gestionProspectoInput.EstadoNoAfiliacionId;
                gestionEditar.GrupoFamiliarId = gestionProspectoInput.GrupoFamiliarId;

                gestionProspectoSave = Mapper.Map<SaveGestionProspectoOutput>(gestionProspectoInput);
                gestionProspectoSave.Id = gestionProspectoInput.Id;

                _gestionProspectoRepositorio.Update(gestionEditar);
            }

            return gestionProspectoSave;
        }

        //Método para obtener todas la funerarias existentes
        public GetAllFunerariasOutput GetAllFunerarias()
        {
            var listaFunerarias = _funerariaProspectoRepositorio.GetAll();
            return new GetAllFunerariasOutput { Funerarias = Mapper.Map<List<FunerariaOutput>>(listaFunerarias) };
        }

        //Método para guardar un beneficiario (AfiliadoProspecto)
        public void SaveAfiliadoProspecto(SaveAfiliadoProspectoInput afiliadoProspecto)
        {
            var parentescoGuardar = _parentescoRepositorio.Get(afiliadoProspecto.ParentescoId);

            ValidarCantidadParentescoInput validarCantidad = new ValidarCantidadParentescoInput();
            validarCantidad.Id = afiliadoProspecto.ParentescoId;
            validarCantidad.GestionProspectoId = afiliadoProspecto.GestionProspectoId;
            validarCantidad.AfiliadoProspectoId = afiliadoProspecto.Id;
            validarCantidad.Repeticiones = parentescoGuardar.Repeticiones;

            //var parentescoGuardar = _parentescoRepositorio.Get(afiliadoProspecto.ParentescoId);

            if (parentescoGuardar.Limite != "" && parentescoGuardar.EdadDiferencia != null)
            {
                ValidarEdadParentescoInput validarEdad = new ValidarEdadParentescoInput();
                validarEdad.Operacion = parentescoGuardar.Limite;
                validarEdad.Edad1 = afiliadoProspecto.Edad;
                validarEdad.Edad2 = parentescoGuardar.EdadDiferencia.Value;

                var validarEdadParentesco = ValidarEdadParentesco(validarEdad);

                if (validarEdadParentesco == false)
                {
                    var mensajeRango = "";
                    if (parentescoGuardar.Limite == ">")
                    {
                        mensajeRango = LocalizationHelper.GetString("Bow", "afiliaciones_clienteprospecto_edadMayor") + parentescoGuardar.EdadDiferencia + " " + LocalizationHelper.GetString("Bow", "afiliaciones_clienteProspecto_años");
                    }
                    else
                    {
                        mensajeRango = LocalizationHelper.GetString("Bow", "afiliaciones_clienteprospecto_edadMenor") + parentescoGuardar.EdadDiferencia + " " + LocalizationHelper.GetString("Bow", "afiliaciones_clienteProspecto_años");
                    }

                    var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_clienteprospecto_limiteEdad") + mensajeRango;
                    throw new UserFriendlyException(mensajeError);
                }
            }

            if (afiliadoProspecto.Apellido1 != "" || afiliadoProspecto.Apellido2 != "")
            {
                if (afiliadoProspecto.Apellido1 != null || afiliadoProspecto.Apellido2 != null)
                {
                    if (parentescoGuardar.CoincidirApellidos == true)
                    {
                        ValidarApellidosParentescoInput validarApellidos = new ValidarApellidosParentescoInput();
                        validarApellidos.GestionProspectoId = afiliadoProspecto.GestionProspectoId;
                        validarApellidos.Apellido1 = afiliadoProspecto.Apellido1;
                        validarApellidos.Apellido2 = afiliadoProspecto.Apellido2;

                        var apellidosCoincidir = ValidarApellidosParentesco(validarApellidos);

                        if (apellidosCoincidir == false)
                        {
                            var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_clienteprospecto_apellidosDiferentes");
                            throw new UserFriendlyException(mensajeError);
                        }
                    }
                }
            }

            var validarCantidadparentesco = ValidarCantidadParentesco(validarCantidad);

            if (validarCantidadparentesco == true)
            {
                if (afiliadoProspecto.Id == 0)
                {
                    AfiliadoProspecto afili = new AfiliadoProspecto();
                    afili = Mapper.Map<AfiliadoProspecto>(afiliadoProspecto);
                    afili.TenantId = AbpSession.TenantId.Value;

                    _afiliadoProspectoRepositorio.Insert(afili);
                    //_afiliadoProspectoRepositorio.Insert(Mapper.Map<AfiliadoProspecto>(afiliadoProspecto));
                }
                else
                {
                    var updateAfiliado = _afiliadoProspectoRepositorio.Get(afiliadoProspecto.Id);

                    updateAfiliado.ParentescoId = afiliadoProspecto.ParentescoId;
                    updateAfiliado.Nombre = afiliadoProspecto.Nombre;
                    updateAfiliado.Apellido1 = afiliadoProspecto.Apellido1;
                    updateAfiliado.Apellido2 = afiliadoProspecto.Apellido2;
                    updateAfiliado.Edad = afiliadoProspecto.Edad;
                    updateAfiliado.CiudadResidenciaId = afiliadoProspecto.CiudadResidenciaId;
                    updateAfiliado.BebePorNacer = afiliadoProspecto.BebePorNacer;

                    _afiliadoProspectoRepositorio.Update(updateAfiliado);
                }
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_clienteprospecto_limiteParentescos");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Método para validar si el apellido del titular debe coincidir con el apellido del parentesco
        public bool ValidarApellidosParentesco(ValidarApellidosParentescoInput parentescoInput)
        {
            var gestionProspecto = _gestionProspectoRepositorio.Get(parentescoInput.GestionProspectoId);

            var primerApellidoTitular = gestionProspecto.Persona.Apellido1;
            var segundoApellidoTitular = gestionProspecto.Persona.Apellido2;

            var xx = parentescoInput.Apellido2.IsNormalized();

            if (parentescoInput.Apellido1.ToLower() == primerApellidoTitular.ToLower() || parentescoInput.Apellido2.ToLower() == primerApellidoTitular.ToLower())
            {
                return true;
            }
            else
            {
                if (segundoApellidoTitular != "")
                {
                    if (parentescoInput.Apellido1.ToLower() == segundoApellidoTitular.ToLower() || parentescoInput.Apellido2.ToLower() == segundoApellidoTitular.ToLower())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        //Método para verificar la cantidad posible de tipos parentescos para agregar
        public bool ValidarCantidadParentesco(ValidarCantidadParentescoInput parentescoInput)
        {
            var afiliadoParentescoIdEditar = 0;
            if (parentescoInput.AfiliadoProspectoId != 0)
            {
                afiliadoParentescoIdEditar = _afiliadoProspectoRepositorio.Get(parentescoInput.AfiliadoProspectoId).ParentescoId;
            }

            var afiliadoProspecto = _afiliadoProspectoRepositorio.GetAll().Where(p => p.GestionProspectoId == parentescoInput.GestionProspectoId && p.ParentescoId == parentescoInput.Id);

            if (afiliadoProspecto.Count() >= parentescoInput.Repeticiones)// && parentescoInput.AfiliadoProspectoId == 0)
            {
                if (afiliadoParentescoIdEditar == parentescoInput.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        //Método para validar que la edad del parentesco este en el rango permitido
        public bool ValidarEdadParentesco(ValidarEdadParentescoInput parentescoInput)
        {
            if (parentescoInput.Operacion == ">")
            {
                return parentescoInput.Edad1 >= parentescoInput.Edad2;
            }
            else
            {
                return parentescoInput.Edad1 <= parentescoInput.Edad2;
            }
        }

        //Método para obtener los afiliados prospecto de un cliente prospecto.
        public GetAfiliadosProspectoOutput GetAfiliadosProspecto(GetAfiliadosProspectoInput gestionProspectoId)
        {
            var listaAfiliadosProspecto = _afiliadoProspectoRepositorio.GetWithParentescoAndLocalidadByGestionProspecto(gestionProspectoId.Id);
            return new GetAfiliadosProspectoOutput { AfiliadosProspecto = Mapper.Map<List<AfiliadoProspectoOutput>>(listaAfiliadosProspecto) };
        }

        public void DeleteAfiliadoProspecto(DeleteAfiliadoProspectoInput afiliadoProspectoInput)
        {
            _afiliadoProspectoRepositorio.Delete(afiliadoProspectoInput.Id);
        }

        public GetAllDepartamentosByConvenioOutput GetAllDepartamentosByConvenio(GetAllDepartamentosByConvenioInput convenioInput)
        {
            var listaRecaudoMasivoDepartamentos = _recaudoMasivoCoberturaRepositorio.GetAllDepartamentosByConvenio(convenioInput.RecaudoMasivoId);
            return new GetAllDepartamentosByConvenioOutput { RecaudoMasivoDepartamentos = Mapper.Map<List<DepartamentoByConvenioOutput>>(listaRecaudoMasivoDepartamentos) };
        }

        //  Método para guardar las localidades de Información tributaria por Pais
        public void SaveConveniosLocalidadByPais(SaveConveniosLocalidadByPaisInput asignarConveniosLocalidadPorPais)
        {
            GetLocalidadesByPaisInput pais = new GetLocalidadesByPaisInput();
            pais.Id = asignarConveniosLocalidadPorPais.PaisId;
            GetLocalidadesByPaisOutput listaLocalidad = _zonificacionService.GetLocalidadesByPais(pais);
            RecaudoMasivoCobertura itemConvenioLocalidad;
            foreach (LocalidadOutput localidad in listaLocalidad.Localidades)
            {
                itemConvenioLocalidad = _recaudoMasivoCoberturaRepositorio.FirstOrDefault(loc => loc.LocalidadId == localidad.Id && loc.RecaudoMasivoId == asignarConveniosLocalidadPorPais.RecaudoMasivoId);
                if (itemConvenioLocalidad == null)
                {
                    itemConvenioLocalidad = new RecaudoMasivoCobertura();
                    itemConvenioLocalidad.RecaudoMasivoId = asignarConveniosLocalidadPorPais.RecaudoMasivoId;
                    itemConvenioLocalidad.LocalidadId = localidad.Id;
                    itemConvenioLocalidad.TenantId = AbpSession.GetTenantId();
                    _recaudoMasivoCoberturaRepositorio.Insert(itemConvenioLocalidad);
                }
            }
        }

        //  Método para guardar las localidades de Información tributaria por Departamento
        public void SaveConveniosLocalidadByDepartamento(SaveConveniosLocalidadByDepartamentoInput asignarConveniosLocalidadPorDepartamento)
        {
            GetLocalidadesInput departamento = new GetLocalidadesInput();
            departamento.Id = asignarConveniosLocalidadPorDepartamento.DepartamentoId;
            GetLocalidadesOutput listaLocalidad = _zonificacionService.GetLocalidades(departamento);
            RecaudoMasivoCobertura itemConvenioLocalidad;
            foreach (LocalidadOutput localidad in listaLocalidad.Localidades)
            {
                itemConvenioLocalidad = _recaudoMasivoCoberturaRepositorio.FirstOrDefault(loc => loc.LocalidadId == localidad.Id && loc.RecaudoMasivoId == asignarConveniosLocalidadPorDepartamento.RecaudoMasivoId);
                if (itemConvenioLocalidad == null)
                {
                    itemConvenioLocalidad = new RecaudoMasivoCobertura();
                    itemConvenioLocalidad.RecaudoMasivoId = asignarConveniosLocalidadPorDepartamento.RecaudoMasivoId;
                    itemConvenioLocalidad.LocalidadId = localidad.Id;
                    itemConvenioLocalidad.TenantId = AbpSession.GetTenantId();
                    _recaudoMasivoCoberturaRepositorio.Insert(itemConvenioLocalidad);
                }
            }
        }

        //  Método para guardar la localidad de Información tributaria
        public void SaveConvenioLocalidad(SaveConvenioLocalidadInput asignarConvenioLocalidad)
        {
            RecaudoMasivoCobertura itemConvenioLocalidad = _recaudoMasivoCoberturaRepositorio.FirstOrDefault(loc => loc.LocalidadId == asignarConvenioLocalidad.LocalidadId && loc.RecaudoMasivoId == asignarConvenioLocalidad.RecaudoMasivoId);
            if (itemConvenioLocalidad == null)
            {
                RecaudoMasivoCobertura recaudoMasivoCobertura = Mapper.Map<RecaudoMasivoCobertura>(asignarConvenioLocalidad);
                recaudoMasivoCobertura.TenantId = AbpSession.GetTenantId();
                _recaudoMasivoCoberturaRepositorio.Insert(recaudoMasivoCobertura);
            }
        }

        //  Método para eliminar las localidades de Información tributaria por Pais
        public void DeleteConveniosLocalidadByPais(DeleteConveniosLocalidadByPaisInput eliminarConveniosLocalidadPorPais)
        {
            _recaudoMasivoCoberturaRepositorio.Delete(loc => loc.LocalidadRecaudoMasivo.DepartamentoLocalidad.PaisId == eliminarConveniosLocalidadPorPais.PaisId && loc.RecaudoMasivoId == eliminarConveniosLocalidadPorPais.RecaudoMasivoId);
        }

        //  Método para eliminar las localidades de Información tributaria por Departamento
        public void DeleteConveniosLocalidadByDepartamento(DeleteConveniosLocalidadByDepartamentoInput eliminarConveniosLocalidadPorDepartamento)
        {
            _recaudoMasivoCoberturaRepositorio.Delete(loc => loc.LocalidadRecaudoMasivo.DepartamentoId == eliminarConveniosLocalidadPorDepartamento.DepartamentoId && loc.RecaudoMasivoId == eliminarConveniosLocalidadPorDepartamento.RecaudoMasivoId);
        }

        //  Método para eliminar las localidades de Información tributaria
        public void DeleteConvenioLocalidad(DeleteConvenioLocalidadInput eliminarConvenioLocalidad)
        {
            _recaudoMasivoCoberturaRepositorio.Delete(loc => loc.LocalidadId == eliminarConvenioLocalidad.LocalidadId && loc.RecaudoMasivoId == eliminarConvenioLocalidad.RecaudoMasivoId);
        }

        /***************************************************************************************************
         * Recaudo Masivo Plan Exequial
         * ************************************************************************************************/

        //  Metodo para obtener los convenios de recaudo masivo asociados al plan exequial
        public GetAllPlanExequialRecaudoMasivoOutput GetAllPlanExequialRecaudoMasivo(GetAllPlanExequialRecaudoMasivoInput planExequial)
        {
            var listaPlanExequialRecaudos = _planExequialRecaudoMasivoRepositorio.GetAll().Where(p => p.PlanExequialId == planExequial.PlanExequialId).ToList();
            return new GetAllPlanExequialRecaudoMasivoOutput { PlanExequialRecaudos = Mapper.Map<List<PlanExequialRecaudoOutput>>(listaPlanExequialRecaudos) };
        }

        //  Método para obtener la información del convenio de recaudo asociado al plan exequial segun el id
        public GetPlanExequialRecaudoMasivoOutput GetPlanExequialRecaudoMasivo(GetPlanExequialRecaudoMasivoInput planExequialRecaudoMasivo)
        {
            return Mapper.Map<GetPlanExequialRecaudoMasivoOutput>(_planExequialRecaudoMasivoRepositorio.Get(planExequialRecaudoMasivo.Id));
        }

        //  Método para guardar la asociación de un recaudo masivo a un plan exequial
        public void SavePlanExequialRecaudoMasivo(SavePlanExequialRecaudoMasivoInput nuevoPlanExequialRecaudoMasivo)
        {
            PlanExequialRecaudoMasivo planExequialRecaudoMasivo = Mapper.Map<PlanExequialRecaudoMasivo>(nuevoPlanExequialRecaudoMasivo);
            planExequialRecaudoMasivo.TenantId = AbpSession.GetTenantId();
            _planExequialRecaudoMasivoRepositorio.Insert(planExequialRecaudoMasivo);
        }

        //  Metodo para eliminar la asociación de un recaudo masivo a un plan exequial
        public void DeletePlanExequialRecaudoMasivo(DeletePlanExequialRecaudoMasivoInput eliminarPlanExequialRecaudoMasivo)
        {
            _planExequialRecaudoMasivoRepositorio.Delete(eliminarPlanExequialRecaudoMasivo.Id);
        }

        //  Metodo para actualizar la asociación de un recaudo masivo a un plan exequial
        public void UpdatePlanExequialRecaudoMasivo(UpdatePlanExequialRecaudoMasivoInput editarPlanExequialRecaudoMasivo)
        {
            _planExequialRecaudoMasivoRepositorio.Update(Mapper.Map<PlanExequialRecaudoMasivo>(editarPlanExequialRecaudoMasivo));
        }

        //  Metodo para validar si es posible eliminar una asociación de un recaudo masivo a un plan exequial
        public PuedeEliminarPlanExequialRecaudoMasivoOutput PuedeEliminarPlanExequialRecaudoMasivo(PuedeEliminarPlanExequialRecaudoMasivoInput planExequialRecaudoMasivoEliminar)
        {
            //int countListaBeneficios = _planExequialRecaudoMasivoRepositorio.GetAllList().Where(d => d.Id == planExequialRecaudoMasivoEliminar.Id).Count();
            int countListaBeneficios = 0;
            bool puedeEliminar = false;
            if (countListaBeneficios == 0)
                puedeEliminar = true;
            return new PuedeEliminarPlanExequialRecaudoMasivoOutput { PuedeEliminar = puedeEliminar };
        }

        public GetAllConveniosRecaudoMasivoNoAsignadosOutput GetAllConveniosRecaudoMasivoNoAsignados(GetAllConveniosRecaudoMasivoNoAsignadosInput planExequial)
        {
            List<RecaudoMasivo> listAllRecaudoMasivo = Mapper.Map<List<RecaudoMasivo>>(_empresasService.GetAllConveniosRecaudoMasivo().Convenios);
            List<RecaudoMasivo> listaRecaudosYaAsignados = _planExequialRecaudoMasivoRepositorio.GetAllRecaudoMasivoByPlanExequial(planExequial.PlanExequialId);

            listAllRecaudoMasivo = listAllRecaudoMasivo.Except(listaRecaudosYaAsignados).ToList();

            return new GetAllConveniosRecaudoMasivoNoAsignadosOutput { ConveniosRecaudo = Mapper.Map<List<ConvenioRecaudoMasivoOutput>>(listAllRecaudoMasivo) };
        }

        public GetAfiliadoProspectoOutput GetAfiliadoProspecto(GetAfiliadoProspectoInput afiliadoProspectoInput)
        {
            var afiliado = _afiliadoProspectoRepositorio.GetWithParentescoAndLocalidadByAfiliadoProspecto(afiliadoProspectoInput.Id);
            return Mapper.Map<GetAfiliadoProspectoOutput>(afiliado);
        }

        /***************************************************************************************************
         * Grupo Informal
         * ************************************************************************************************/

        //  Metodo para obtener el listado de grupos informales
        public GetAllGrupoInformalOutput GetAllGrupoInformal()
        {
            var listaGruposInformales = _grupoInformalRepositorio.GetAllList();
            return new GetAllGrupoInformalOutput { GruposInformales = Mapper.Map<List<GrupoInformalOutput>>(listaGruposInformales) };
        }

        //  Método para obtener la información del grupo informal segun el id
        public GetGrupoInformalOutput GetGrupoInformal(GetGrupoInformalInput grupoInformal)
        {
            return Mapper.Map<GetGrupoInformalOutput>(_grupoInformalRepositorio.Get(grupoInformal.Id));
        }

        //  Método para guardar el grupo informal
        public SaveGrupoInformalOutput SaveGrupoInformal(SaveGrupoInformalInput grupoInformal)
        {
            SaveGrupoInformalOutput saveGrupoInformal = new SaveGrupoInformalOutput();
            GrupoInformal existeGrupoInformal = _grupoInformalRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == grupoInformal.Nombre.ToLower());
            if (existeGrupoInformal != null)
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_grupoInformal_nombre_yaExiste");
                throw new UserFriendlyException(mensajeError);
            }
            else
            {
                grupoInformal.FechaIngreso = DateTime.Now.ToString();
                grupoInformal.EstadoId = _parametrosService.GetEstadoActivoByNombreParametro(new GetEstadoActivoByNombreParametroInput { NombreParametro = BowConsts.PARAMETRO_GRUPO_INFORMAL }).EstadoId;
                saveGrupoInformal = new SaveGrupoInformalOutput { Id = _grupoInformalRepositorio.InsertAndGetId(Mapper.Map<GrupoInformal>(grupoInformal)) };
            }
            return saveGrupoInformal;
        }

        //  Método para guardar empleados al grupo informal
        public void SaveGrupoInformalEmpleado(SaveGrupoInformalEmpleadoInput empleadosGrupoInformal)
        {
            foreach (EmpleadoInput empleado in empleadosGrupoInformal.Empleados)
            {
                empleado.FechaIngreso = DateTime.Now.ToString();
                empleado.GrupoInformalId = empleadosGrupoInformal.GrupoInformalId;
                empleado.EstadoId = _parametrosService.GetEstadoActivoByNombreParametro(new GetEstadoActivoByNombreParametroInput { NombreParametro = BowConsts.PARAMETRO_GRUPO_INFORMAL_EMPLEADO }).EstadoId;

                GrupoInformalEmpleado grupoInformalEmpleado = Mapper.Map<GrupoInformalEmpleado>(empleado);
                grupoInformalEmpleado.TenantId = AbpSession.GetTenantId();
                _grupoInformalEmpleadoRepositorio.Insert(grupoInformalEmpleado);
            }
        }

        //  Metodo para obtener el listado de grupos informales
        public GetAllGrupoInformalByContactoOutput GetAllGrupoInformalByContacto(GetAllGrupoInformalByContactoInput contacto)
        {
            var listaGruposInformales = _grupoInformalRepositorio.GetAll().Where(g => g.PersonaId == contacto.PersonaId).ToList();
            return new GetAllGrupoInformalByContactoOutput { GruposInformales = Mapper.Map<List<GrupoInformalByContactoOutput>>(listaGruposInformales) };
        }

        //  Metodo para obtener el listado de empleados de un grupo informal
        public GetAllEmpleadosByGrupoInformalOutput GetAllEmpleadosByGrupoInformal(GetAllEmpleadosByGrupoInformalInput grupoInformal)
        {
            var listaEmpleados = _grupoInformalEmpleadoRepositorio.GetAllEmpleadosByGrupoInformal(grupoInformal.GrupoInformalId);
            return new GetAllEmpleadosByGrupoInformalOutput { Empleados = Mapper.Map<List<EmpleadoByGrupoInformalOutput>>(listaEmpleados) };
        }

        /***************************************************************************************************
        * Gestionar Parentescos
        * ************************************************************************************************/

        //Metodo para guardar un parentesco y organizar el orden de la lista
        public void SaveParentesco(SaveParentescoInput parentescosInput)
        {
            Parentesco existeParentesco = _parentescoRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == parentescosInput.Nombre.ToLower());

            if (existeParentesco == null)
            {
                parentescosInput.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(parentescosInput.Nombre.ToLower());
                int cantidadMoverPos = 1;
                _parentescoRepositorio.MoverPosicionParentesco(parentescosInput.Posicion, cantidadMoverPos);

                Parentesco parentesco = Mapper.Map<Parentesco>(parentescosInput);
                parentesco.TenantId = AbpSession.GetTenantId();
                _parentescoRepositorio.Insert(parentesco);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_gestionarparentescos_existeParentesco");
                throw new UserFriendlyException(mensajeError);
            }

        }

        //Metodo para guardar un parentesco y organizar el orden de la lista
        public void UpdateParentesco(SaveParentescoInput parentescosInput)
        {
            Parentesco existeParentesco = _parentescoRepositorio.FirstOrDefault(p => p.Nombre.ToLower() == parentescosInput.Nombre.ToLower() && p.Id != parentescosInput.Id);

            if (existeParentesco == null)
            {
                Parentesco parentescoEditar = _parentescoRepositorio.FirstOrDefault(parentescosInput.Id);

                int cantidadMoverNeg = -1;

                _parentescoRepositorio.MoverPosicionParentesco(parentescoEditar.Posicion, cantidadMoverNeg);

                //parentescoEditar.Id = parentescosInput.Id;
                parentescoEditar.Nombre = CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(parentescosInput.Nombre.ToLower());
                parentescoEditar.Posicion = parentescosInput.Posicion;
                parentescoEditar.Genero = parentescosInput.Genero;
                parentescoEditar.Repeticiones = parentescosInput.Repeticiones;
                parentescoEditar.Limite = parentescosInput.Limite;
                parentescoEditar.EdadDiferencia = parentescosInput.EdadDiferencia;
                parentescoEditar.CoincidirApellidos = parentescosInput.CoincidirApellidos;

                int cantidadMoverPos = 1;
                _parentescoRepositorio.MoverPosicionParentesco(parentescosInput.Posicion, cantidadMoverPos);

                _parentescoRepositorio.Update(parentescoEditar);
            }
            else
            {
                var mensajeError = LocalizationHelper.GetString("Bow", "afiliaciones_gestionarparentescos_existeParentesco");
                throw new UserFriendlyException(mensajeError);
            }
        }

        //Metodo para obtener un parentesco por el id, para editarlo.
        public GetParentescoOutput GetParentesco(GetParentescoInput parentescoInput)
        {
            var parentesco = _parentescoRepositorio.FirstOrDefault(parentescoInput.Id);
            return Mapper.Map<GetParentescoOutput>(parentesco);
        }

        //Metodo para identificar si el parentesco a eliminar esta relacionado con otras tablas.
        public PuedeEliminarParentescoOutput PuedeEliminarParentesco(PuedeEliminarParentescoInput parentescoInput)
        {
            PuedeEliminarParentescoOutput puedeEliminar = new PuedeEliminarParentescoOutput();

            List<AfiliadoProspecto> existeAfiliado = _afiliadoProspectoRepositorio.GetAll().Where(p => p.ParentescoId == parentescoInput.Id).ToList();

            List<GrupoFamiliarParentesco> existeGrupoFamiliar = _grupoFamiliarParentescoRepositorio.GetAll().Where(p => p.ParentescoId == parentescoInput.Id).ToList();

            if (existeAfiliado.Count() != 0 || existeGrupoFamiliar.Count() != 0)
            {
                puedeEliminar.PuedeEliminar = false;
            }
            else
            {
                puedeEliminar.PuedeEliminar = true;
            }

            return puedeEliminar;
        }

        //Metodo para eliminar un parentesco y organizar el orden de la lista
        public void DeleteParentesco(DeleteParentescoInput parentescoInput)
        {
            _parentescoRepositorio.Delete(parentescoInput.Id);
            var cantidadMoverNeg = -1;
            _parentescoRepositorio.MoverPosicionParentesco(parentescoInput.Posicion, cantidadMoverNeg);
        }


        /***************************************************************************************************
        * Clientes Prospecto
        * ************************************************************************************************/

        public GetPlanesProspectoOutput GetPlanesProspecto(GetPlanesProspectoInput parentescosInput)
        {
            //Se verifica si la lista de parentescos viene con datos para realizar la consulta de planes que puede tener
            if (parentescosInput.Parentescos.Count() != 0)
            {
                //Variable para almacenar la lista de grupo familiar parentesco según parentescos indicados
                List<GrupoFamiliarParentesco> grupoPosible = new List<GrupoFamiliarParentesco>();

                //Se consulta todos los grupos familiares parentesco y se agrupa por el grupo familiar
                var grupoFamiliarParentesco = _grupoFamiliarParentescoRepositorio.GetAll().GroupBy(s => s.GrupoFamiliarId).ToList();

                //Se convierte a una lista de grupo familiar parentesco los parentescos ingresados
                List<GrupoFamiliarParentesco> listaAfiliados = Mapper.Map<List<GrupoFamiliarParentesco>>(parentescosInput.Parentescos);

                //Se recorre la agrupación hecha anteriormente
                foreach (var item in grupoFamiliarParentesco)
                {
                    //Se compara los parentescos ingresados con cada agrupación de grupo familiar parentesco
                    var lst = (from gfp in item
                               join la in listaAfiliados on gfp.ParentescoId equals la.ParentescoId
                               select gfp).Distinct().ToList();

                    //Si el resultado obtenido anteriormente es igual a la cantidad de registros de la agrupación, 
                    //es porque el grupo familiar parentesco pertenece a un plan posible para el cliente prospecto (se agrega solo el primer registro para obtener el grupofamiliarId)
                    if (item.Count() == lst.Count() && lst.Count() == listaAfiliados.Count())
                    {
                        grupoPosible.Add(item.First());
                    }
                }

                //Se consulta los planes exequiales según el grupo familiar parentesco obtenido anteriormente
                var listaPlanes = (from gfparent in grupoPosible
                                   join gfamil in _grupoFamiliarRepositorio.GetAll() on gfparent.GrupoFamiliarId equals gfamil.Id
                                   join pexeq in _planExequialRepositorio.GetAll() on gfamil.PlanExequialId equals pexeq.Id
                                   join planExeSucu in _planExequialSucursalRepositorio.GetAll() on pexeq.Id equals planExeSucu.PlanExequialId
                                   where planExeSucu.SucursalId.Equals(parentescosInput.SucursalId)
                                   select pexeq).Distinct().ToList();

                var listaGrupos = (from gfparent in grupoPosible
                                   join gfamil in _grupoFamiliarRepositorio.GetAll() on gfparent.GrupoFamiliarId equals gfamil.Id
                                   join pexeq in _planExequialRepositorio.GetAll() on gfamil.PlanExequialId equals pexeq.Id
                                   join planExeSucu in _planExequialSucursalRepositorio.GetAll() on pexeq.Id equals planExeSucu.PlanExequialId
                                   where planExeSucu.SucursalId.Equals(parentescosInput.SucursalId)
                                   select gfamil).Distinct().ToList();

                GetPlanesProspectoOutput resultadoPlanesGrupos = new GetPlanesProspectoOutput();

                resultadoPlanesGrupos.Planes = Mapper.Map<List<PlanProspectoOutput>>(listaPlanes);
                resultadoPlanesGrupos.GruposFamiliares = Mapper.Map<List<GruposFamiliaresPlanOutput>>(listaGrupos);

                return resultadoPlanesGrupos;
            }
            else
            {
                //Se obtiene la lista de planes
                var listaPlanes = (from pexeq in _planExequialRepositorio.GetAll()
                                   join planExeSucu in _planExequialSucursalRepositorio.GetAll() on pexeq.Id equals planExeSucu.PlanExequialId
                                   where planExeSucu.SucursalId.Equals(parentescosInput.SucursalId)
                                   select pexeq).ToList();

                //Se obtiene la lista de los grupos familiares asociados al plan anterior
                var listaGrupos = (from pexeq in _planExequialRepositorio.GetAll()
                                   join planExeSucu in _planExequialSucursalRepositorio.GetAll() on pexeq.Id equals planExeSucu.PlanExequialId
                                   join grupofam in _grupoFamiliarRepositorio.GetAll() on pexeq.Id equals grupofam.PlanExequialId
                                   where planExeSucu.SucursalId.Equals(parentescosInput.SucursalId)
                                   select grupofam).ToList();

                GetPlanesProspectoOutput resultadoPlanesGrupos = new GetPlanesProspectoOutput();

                resultadoPlanesGrupos.Planes = Mapper.Map<List<PlanProspectoOutput>>(listaPlanes);
                resultadoPlanesGrupos.GruposFamiliares = Mapper.Map<List<GruposFamiliaresPlanOutput>>(listaGrupos);

                return resultadoPlanesGrupos;
            }
        }

        //Metodo para obtener el valor y beneficios del plan indicado
        public GetBeneficiosPlanOutput GetBeneficiosPlan(GetBeneficiosPlanInput planInput)
        {
            var valorPlan = _grupoFamiliarRepositorio.FirstOrDefault(s => s.PlanExequialId == planInput.PlanExequialId && s.Id == planInput.Id).ValorPlan;
            var tiposBeneficio = _parametrosService.GetTiposBeneficiosPlanExequial();

            List<TipoBeneficioPropioPlanExequialOutput> listaPropios = Mapper.Map<List<TipoBeneficioPropioPlanExequialOutput>>(_beneficioPlanExequialRepositorio.GetWithBeneficio(planInput.PlanExequialId, tiposBeneficio.EstadoActivoId));
            List<TipoBeneficioAdicionalPlanExequialOutput> listaAdicionales = Mapper.Map<List<TipoBeneficioAdicionalPlanExequialOutput>>(_beneficioAdicionalPlanExequialRepositorio.GetWithBeneficio(planInput.PlanExequialId, tiposBeneficio.EstadoActivoId));

            GetBeneficiosPlanOutput DatosBeneficios = new GetBeneficiosPlanOutput();
            DatosBeneficios.ValorPlan = valorPlan;
            DatosBeneficios.Propios = listaPropios;
            DatosBeneficios.Adicionales = listaAdicionales;

            return DatosBeneficios;
        }

        //Método para guardar la afiliacion indicada (botón deseo afiliarme)
        public void SaveAfiliacion(SaveAfiliacionInput afiliacionInput)
        {
            if (afiliacionInput.GrupoFamiliarId != 0 && afiliacionInput.GestionProspectoId != 0)
            {
                GestionProspecto gestion = _gestionProspectoRepositorio.Get(afiliacionInput.GestionProspectoId);

                gestion.GrupoFamiliarId = afiliacionInput.GrupoFamiliarId;
                gestion.EstadoNoAfiliacionId = null;
                gestion.FunerariaAfiliadoId = null;
                gestion.Observaciones = "";

                _gestionProspectoRepositorio.Update(gestion);

                if (afiliacionInput.BeneficiosAdicionales.Count != 0)
                {
                    foreach (var item in afiliacionInput.BeneficiosAdicionales)
                    {
                        if (item.Seleccionado == true)
                        {
                            BeneficiosGestionProspecto beneficioGestionAdicionales = new BeneficiosGestionProspecto();

                            beneficioGestionAdicionales.GestionProspectoId = afiliacionInput.GestionProspectoId;
                            beneficioGestionAdicionales.BeneficioAdicionalPlanExequialId = item.Id;

                            //Paso para agregar el tenantId
                            beneficioGestionAdicionales.TenantId = AbpSession.TenantId.Value;
                            _beneficiosGestionProspectoRepositorio.Insert(beneficioGestionAdicionales);
                        }
                    }
                }
            }
        }

        //Metodo para obtener una lista de gestion prospecto por telefonoId o direccionId del prospecto
        public GetGestionProspectoOutput GetProspecto(GetProspectoProspectoInput direccionTelefonoInput)
        {
            var gestionProspecto = _gestionProspectoRepositorio.GetAll().Where(a => a.Prospecto.DireccionId == direccionTelefonoInput.DireccionId || a.Prospecto.TelefonoId == direccionTelefonoInput.TelefonoId).OrderByDescending(f => f.FechaGestion);
            return new GetGestionProspectoOutput { GestionesProspecto = Mapper.Map<List<GestionProspectoOutput>>(gestionProspecto) };
        }
       

        //Metodo para cargar la información del cliente prospecto seleccionado para ver sus detalles
        public DetalleClienteProspectoOutput DetalleClienteProspecto(DetalleClienteProspectoInput gestionInput)
        {
            DetalleClienteProspectoOutput detalle = new DetalleClienteProspectoOutput();

            detalle.PlanExequialEnSucursal = false;

            //Se obtienen los afiliados de la gestion prospecto seleccionada
            detalle.Afiliados = Mapper.Map<List<AfiliadoProspectoOutput>>(_afiliadoProspectoRepositorio.GetWithParentescoAndLocalidadByGestionProspecto(gestionInput.GestionProspectoId));

            //Se obtienen el plan exequial segun el grupo familiar de la gestion prospecto seleccionada
            var planExequial = _grupoFamiliarRepositorio.GetWithPlanExequial(gestionInput.GrupoFamiliarId);

            //Se obtienen los planes exequiales segun la sucursal indicada
            var planExequialSucursal = _planExequialSucursalRepositorio.GetWithPlanExequial(gestionInput.SucursalId);

            if (planExequial != null)
            {
                if (planExequial.PlanExequialGrupoFamiliar != null)
                {
                    foreach (var plan in planExequialSucursal)
                    {
                        //Se compara si el plan exequial de la gestion prospecto pertenece a el plan exequial de la sucursal ingresada inicialmente en el formulario.
                        //Se retorna true o false para pintar el plan exequial de la gestion prospecto
                        if (plan.PlanExequialId == planExequial.PlanExequialGrupoFamiliar.Id)
                        {
                            detalle.PlanExequialEnSucursal = true;
                            break;
                        }
                    }
                }
            }

            //Se agregan los datos del plan exequial si en la gestion prospecto indicada se especifico el plan.
            if (planExequial != null)
            {
                detalle.PlanExequialId = planExequial.PlanExequialGrupoFamiliar.Id;
                detalle.PlanExequialNombre = planExequial.PlanExequialGrupoFamiliar.Nombre;
            }

            return detalle;
        }

        //Metodo para obtener la gestion prospecto indicada con sus datos relacionados para cargar en el formulario completo
        public GetGestionProspectoIniciarContactoOutput GetGestionProspectoIniciarContacto(GetGestionProspectoIniciarContactoInput gestionInput)
        {
            var gestionProspecto = Mapper.Map<GetGestionProspectoIniciarContactoOutput>(_gestionProspectoRepositorio.GetAllGestionProspecto(gestionInput.Id));

            //Se obtienen el plan exequial segun el grupo familiar de la gestion prospecto seleccionada
            var planExequial = _grupoFamiliarRepositorio.GetWithPlanExequial(gestionProspecto.GrupoFamiliarId);

            //Se obtienen los planes exequiales segun la sucursal indicada
            var planExequialSucursal = _planExequialSucursalRepositorio.GetWithPlanExequial(gestionInput.SucursalId);

            if (planExequial != null)
            {
                if (planExequial.PlanExequialGrupoFamiliar != null)
                {
                    foreach (var plan in planExequialSucursal)
                    {
                        //Se compara si el plan exequial de la gestion prospecto pertenece a el plan exequial de la sucursal ingresada inicialmente en el formulario.
                        //Se retorna true o false para pintar el plan exequial de la gestion prospecto
                        if (plan.PlanExequialId == planExequial.PlanExequialGrupoFamiliar.Id)
                        {
                            gestionProspecto.PlanExequialEnSucursal = true;
                            break;
                        }
                    }
                }
            }

            gestionProspecto.Afiliados = Mapper.Map<List<AfiliadoProspectoOutput>>(_afiliadoProspectoRepositorio.GetWithParentescoAndLocalidadByGestionProspecto(gestionInput.Id));

            return gestionProspecto;
        }

        //Método para copiar los afiliados de la gestion prospecto seleccionada (al iniciar contacto)
        public void CopiarAfiliadosProspecto(CopiarAfiliadosProspectoInput afiliadosInput)
        {
            if (afiliadosInput.Afiliados.Count() != 0)
            {
                foreach (var item in afiliadosInput.Afiliados)
                {
                    AfiliadoProspecto afiliado = new AfiliadoProspecto();

                    afiliado.GestionProspectoId = afiliadosInput.GestionProspectoId;
                    afiliado.ParentescoId = item.ParentescoId;
                    afiliado.Nombre = item.Nombre;
                    afiliado.Apellido1 = item.Apellido1;
                    afiliado.Apellido2 = item.Apellido2;
                    afiliado.Edad = item.Edad;
                    afiliado.CiudadResidenciaId = item.CiudadResidenciaId;
                    afiliado.BebePorNacer = item.BebePorNacer;

                    //Paso para agregar el tenantId
                    afiliado.TenantId = AbpSession.TenantId.Value;
                    _afiliadoProspectoRepositorio.Insert(afiliado);
                }
            }
        }

        //  Método para obtener la lista de planes exequiales
        public GetAllPlanesExequialesBySucursalAndTipoOutput GetAllPlanesExequialesBySucursalAndTipo(GetAllPlanesExequialesBySucursalAndTipoInput sucursalAndTipo)
        {
            List<PlanExequial> listaPlanesExequiales = new List<PlanExequial>();
            if (sucursalAndTipo.TipoPlan.Equals(BowConsts.PLAN_EXEQUIAL_TIPO_EMPRESARIAL))
            {
                listaPlanesExequiales = _planExequialRepositorio.GetAllPlanesExequialesEmpresarialesBySucursalAndEmpresaAndRecuado(sucursalAndTipo.SucursalId, sucursalAndTipo.EmpresaOrGrupoId, sucursalAndTipo.RecaudoMasivoId);
            }
            else if (sucursalAndTipo.TipoPlan.Equals(BowConsts.PLAN_EXEQUIAL_TIPO_GRUPO))
            {
                listaPlanesExequiales = _planExequialRepositorio.GetAllPlanesExequialesGruposBySucursalAndGrupoAndRecuado(sucursalAndTipo.SucursalId, sucursalAndTipo.EmpresaOrGrupoId, sucursalAndTipo.RecaudoMasivoId);
            }
            else if (sucursalAndTipo.TipoPlan.Equals(BowConsts.PLAN_EXEQUIAL_TIPO_FAMILIAR))
            {
                listaPlanesExequiales = _planExequialRepositorio.GetAllPlanesExequialesFamiliaresBySucursalAndRecuado(sucursalAndTipo.SucursalId, sucursalAndTipo.RecaudoMasivoId);
            }
            return new GetAllPlanesExequialesBySucursalAndTipoOutput { PlanesExequiales = Mapper.Map<List<PlanExequialBySucursalAndTipoOutput>>(listaPlanesExequiales) };
        }

        //  Metodo para obtener los convenios de recaudo masivo asociados a una localidad
        public GetAllRecaudosMasivosByLocalidadOutput GetAllRecaudosMasivosByLocalidad(GetAllRecaudosMasivosByLocalidadInput localidad)
        {
            var listaPlanExequialRecaudos = _recaudoMasivoCoberturaRepositorio.GetAllRecaudosMasivosByLocalidad(localidad.LocalidadId);
            return new GetAllRecaudosMasivosByLocalidadOutput { RecaudosMasivo = Mapper.Map<List<RecaudosMasivosByLocalidadOutput>>(listaPlanExequialRecaudos) };
        }

        ////  Metodo para obtener los recaudos masivos por localidad id
        //public GetRecaudosMasivosCoberturaByLocalidadOutput GetRecaudosMasivosByLocalidad(GetRecaudosMasivosCoberturaByLocalidadInput localidad)
        //{
        //    var listaPlanExequialRecaudos = _recaudoMasivoCoberturaRepositorio.GetAllRecaudosMasivosByLocalidad(localidad.LocalidadId);
        //    return new GetRecaudosMasivosCoberturaByLocalidadOutput { RecaudosByLocalidad = Mapper.Map<List<RecaudoMasivoCoberturaByLocalidadOutput>>(listaPlanExequialRecaudos) };
        //}

        ////  Metodo para obtener los recaudos masivos por localidad id
        //public GetAfiliadosProspectoByLocalidadOutput GetAfiliadosProspectoByLocalidad(GetAfiliadosProspectoByLocalidadInput localidad)
        //{
        //    var listaAfiliadosProspecto = _afiliadoProspectoRepositorio.GetAll().Where(a => a.CiudadResidenciaId == localidad.LocalidadId);
        //    return new GetAfiliadosProspectoByLocalidadOutput { AfiliadosProspecto = Mapper.Map<List<AfiliadoProspectoByLocalidadOutput>>(listaAfiliadosProspecto) };
        //}

        //public GetGestionesProspectoByLocalidadOutput GetGestionesProspectoByLocalidad(GetGestionesProspectoByLocalidadInput localidad)
        //{
        //    var listaGestionesProspecto = _gestionProspectoRepositorio.GetAll().Where(a => a.LocalidadId == localidad.LocalidadId);
        //    return new GetGestionesProspectoByLocalidadOutput { GestionesProspecto = Mapper.Map<List<GestionProspectoByLocalidadOutput>>(listaGestionesProspecto) };
        //}

    }
}
