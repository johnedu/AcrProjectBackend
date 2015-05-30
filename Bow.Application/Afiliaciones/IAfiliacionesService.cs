using Abp.Application.Services;
using Bow.Afiliaciones.DTOs.InputModels;
using Bow.Afiliaciones.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Afiliaciones
{
    public interface IAfiliacionesService : IApplicationService
    {
        /// <summary>
        ///     Se encarga de obtener la lista de planes exequiales
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllPlanesExequialesOutput"/> con el listado de planes exequiales
        /// </return>
        GetAllPlanesExequialesOutput GetAllPlanesExequiales();

        /// <summary>
        ///     Se encarga de obtener el plan exequial a partir del id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetPlanExequialInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllPlanesExequialesOutput"/> con la información del plan exequial
        /// </return>
        GetPlanExequialOutput GetPlanExequial(GetPlanExequialInput planExequial);

        /// <summary>
        ///     Se encarga de obtener el plan exequial a partir del id con la información de la moneda
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetPlanExequialWithMonedaInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetPlanExequialWithMonedaOutput"/> con la información del plan exequial y la moneda
        /// </return>
        GetPlanExequialWithMonedaOutput GetPlanExequialWithMoneda(GetPlanExequialWithMonedaInput planExequial);

        /// <summary>
        ///     Se encarga de almacenar un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SavePlanExequialInput"/> con la información del plan exequial
        /// </param>
        /// <return></return>
        void SavePlanExequial(SavePlanExequialInput nuevoPlanExequial);

        /// <summary>
        ///     Se encarga de eliminar un plan exequial a partir del id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeletePlanExequialInput"/> con el id del plan exequial
        /// </param>
        /// <return></return>
        void DeletePlanExequial(DeletePlanExequialInput planExequialEliminar);

        /// <summary>
        ///     Se encarga de actualizar un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdatePlanExequialInput"/> con la información del plan exequial
        /// </param>
        /// <return></return>
        void UpdatePlanExequial(UpdatePlanExequialInput editarPlanExequial);

        /// <summary>
        ///     Se encarga de actualizar el estado de un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.CambiarEstadoPlanExequialInput"/> con el id del plan exequial y el nuevo estado
        /// </param>
        /// <return></return>
        void CambiarEstadoPlanExequial(CambiarEstadoPlanExequialInput estadoPlanExequial);

        /// <summary>
        ///     Se encarga de validar si se puede o no eliminar un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarPlanExequialInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarPlanExequialOutput"/> con la respuesta si se puede eliminar o no el plan exequial
        /// </return>
        PuedeEliminarPlanExequialOutput PuedeEliminarPlanExequial(PuedeEliminarPlanExequialInput planExequialEliminar);

        /// <summary>
        /// Se encarga de obtener la lista de los beneficios por categoria segun el Id indicado
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetBeneficiosByCategoriaInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetBeneficiosByCategoriaOutput"/>
        /// </return>
        GetBeneficiosByCategoriaOutput GetBeneficiosByCategoria(GetBeneficiosByCategoriaInput categoriaInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveBeneficioInput"/> con la información del beneficio
        /// para almacenarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void SaveBeneficio(SaveBeneficioInput beneficioInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteBeneficioInput"/> con el id del beneficio
        /// para eliminarlo de la base de datos
        /// </summary>
        /// <returns></returns>
        void DeleteBeneficio(DeleteBeneficioInput beneficioInput);

        /// <summary>
        /// Se encarga de obtener la información del beneficio segun el Id indicado
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetBeneficioInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetBeneficioOutput"/>
        /// </return>
        GetBeneficioOutput GetBeneficio(GetBeneficioInput beneficioInput);

        /// <summary>
        ///     Se encarga de obtener la lista de grupos familiares de un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllGruposFamiliaresByPlanInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllGruposFamiliaresByPlanOutput"/> con el listado de grupos familiares
        /// </return>
        GetAllGruposFamiliaresByPlanOutput GetAllGruposFamiliaresByPlan(GetAllGruposFamiliaresByPlanInput planExequial);

        /// <summary>
        ///     Se encarga de obtener el grupo familiar a partir del id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetGrupoFamiliarInput"/> con el id del grupo familiar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetGrupoFamiliarOutput"/> con la información del grupo familiar
        /// </return>
        GetGrupoFamiliarOutput GetGrupoFamiliar(GetGrupoFamiliarInput grupoFamiliar);

        /// <summary>
        ///     Se encarga de almacenar un grupo familiar
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveGrupoFamiliarInput"/> con la información del grupo familiar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.SaveGrupoFamiliarOutput"/> con la lista de parentescos y cada parentesco con la lista de rangos
        /// </return>
        SaveGrupoFamiliarOutput SaveGrupoFamiliar(SaveGrupoFamiliarInput nuevoGrupoFamiliar);

        /// <summary>
        ///     Se encarga de eliminar un grupo familiar a partir del id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteGrupoFamiliarInput"/> con el id del grupo familiar
        /// </param>
        /// <return></return>
        void DeleteGrupoFamiliar(DeleteGrupoFamiliarInput grupoFamiliarEliminar);

        /// <summary>
        ///     Se encarga de actualizar un grupo familiar
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdateGrupoFamiliarInput"/> con la información del grupo familiar
        /// </param>
        /// <return></return>
        void UpdateGrupoFamiliar(UpdateGrupoFamiliarInput editarGrupoFamiliar);

        /// <summary>
        ///     Se encarga de validar si se puede o no eliminar un grupo familiar
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarGrupoFamiliarInput"/> con el id del grupo familiar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarGrupoFamiliarOutput"/> con la respuesta si se puede eliminar o no el grupo familiar
        /// </return>
        PuedeEliminarGrupoFamiliarOutput PuedeEliminarGrupoFamiliar(PuedeEliminarGrupoFamiliarInput grupoFamiliarEliminar);

        /// <summary>
        ///     Se encarga de obtener la lista de rangos de un parentesco a partir del grupo familiar
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllRangosParentescoByGrupoInput"/> con el id del grupo familiar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllRangosParentescoByGrupoOutput"/> con el listado de rangos de parentesco
        /// </return>
        GetAllRangosParentescoByGrupoOutput GetAllRangosParentescoByGrupo(GetAllRangosParentescoByGrupoInput parentesco);

        /// <summary>
        ///     Se encarga de obtener la lista de rangos de un parentesco a partir del grupo familiar y el parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllRangosParentescoByGrupoAndParentescoInput"/> con el id del grupo familiar y el id del parentesco
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllRangosParentescoByGrupoAndParentescoOutput"/> con el listado de rangos de parentesco
        /// </return>
        GetAllRangosParentescoByGrupoAndParentescoOutput GetAllRangosParentescoByGrupoAndParentesco(GetAllRangosParentescoByGrupoAndParentescoInput grupoAndParentesco);

        /// <summary>
        ///     Se encarga de obtener el rango de un parentesco a partir del id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetRangoParentescoInput"/> con el id del rango de un parentesco
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetRangoParentescoOutput"/> con la información del rango de un parentesco
        /// </return>
        GetRangoParentescoOutput GetRangoParentesco(GetRangoParentescoInput rangoParentesco);

        /// <summary>
        ///     Se encarga de almacenar un rango de un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveRangoParentescoInput"/> con la información del rango de un parentesco
        /// </param>
        /// <return></return>
        void SaveRangoParentesco(SaveRangoParentescoInput nuevoRangoParentesco);

        /// <summary>
        ///     Se encarga de eliminar un rango de un parentesco a partir del id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteRangoParentescoInput"/> con el id del rango de un parentesco
        /// </param>
        /// <return></return>
        void DeleteRangoParentesco(DeleteRangoParentescoInput rangoParentescoEliminar);

        /// <summary>
        ///     Se encarga de actualizar un rango de un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdateRangoParentescoInput"/> con la información del rango de un parentesco
        /// </param>
        /// <return></return>
        void UpdateRangoParentesco(UpdateRangoParentescoInput editarRangoParentesco);

        /// <summary>
        ///     Se encarga de validar si se puede o no eliminar un rango de un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarRangoParentescoInput"/> con el id del rango de un parentesco
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarRangoParentescoOutput"/> con la respuesta si se puede eliminar o no el rango de un parentesco
        /// </return>
        PuedeEliminarRangoParentescoOutput PuedeEliminarRangoParentesco(PuedeEliminarRangoParentescoInput rangoParentescoEliminar);

        /// <summary>
        ///     Se encarga de obtener la lista de parentescos
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllParentescosOutput"/> con el listado de parentescos
        /// </return>
        GetAllParentescosOutput GetAllParentescos();

        /// <summary>
        ///     Se encarga de obtener la información de un parentesco asociado a un grupo familiar
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetGrupoFamiliarParentescoInput"/> con el id del parentesco
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetGrupoFamiliarParentescoOutput"/> con la información de la relación entre parentesco y el grupo familiar
        /// </return>
        GetGrupoFamiliarParentescoOutput GetGrupoFamiliarParentesco(GetGrupoFamiliarParentescoInput parentesco);

        /// <summary>
        ///     Se encarga de almacenar o modificar un rango de un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveOrUpdateGrupoFamiliarParentescoInput"/> con la información del grupo familiar parentesco
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.SaveOrUpdateGrupoFamiliarParentescoOutput"/> con el id del grupo familiar parentesco almacenado o actualizado
        /// </return>
        SaveOrUpdateGrupoFamiliarParentescoOutput SaveOrUpdateGrupoFamiliarParentesco(SaveOrUpdateGrupoFamiliarParentescoInput nuevoGrupoFamiliarParentesco);

        /// <summary>
        ///     Se encarga de obtener la lista de parentescos de un grupo familiar con la lista de rangos de cada parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllParentescosAllRangosInput"/> con la información del grupo familiar parentesco
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllParentescosAllRangosOutput"/> con la lista de parentescos y cada parentesco con la lista de rangos
        /// </return>
        GetAllParentescosAllRangosOutput GetAllParentescosAllRangos(GetAllParentescosAllRangosInput grupoFamiliar);

        /// <summary>
        /// Se encarga de obtener la información de los periodos de ventas registrados en el sistema
        /// </summary>
         /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetPeriodosVentasRegistradosOutput"/>
        /// </return>
        GetPeriodosVentasRegistradosOutput GetPeriodosVentasRegistrados();

        /// <summary>
        /// Se encarga de hacer la vericiación del periodo de venta indicado, si cumple con los requisitos retorna una lista de fechas y descripción del periodo de venta
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetVerificarPeriodosVentasInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetVerificarPeriodosVentasOutput"/>
        /// </return>
        GetVerificarPeriodosVentasOutput GetVerificarPeriodosVentas(GetVerificarPeriodosVentasInput periodoInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SavePeriodosVentasInput"/> con la información del periodo de venta a guardar
        /// se realiza de nuevo la verificación y se almacena en el sistema
        /// </summary>
        /// <returns></returns>
        void SavePeriodosVentas(SavePeriodosVentasInput periodosInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeletePeriodosVentasInput"/> con la lista de periodos de venta a eliminar
        /// </summary>
        /// <returns>
        /// Retorna un bool con true si fue posible eliminar la lista o false si no se puede eliminar la lista por que generan huecos de fechas
        /// </returns>
        bool DeletePeriodosVentas(DeletePeriodosVentasInput periodosEliminarInput);

        /// <summary>
        /// Se encarga de hacer el calculo del rango de la fecha fin según la fecha de inicio indicada como parámetro de entrada
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.FechaFinPeriodoVentaInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.FechaFinPeriodoVentaOutput"/> con el rango del datepicker para la fecha fin
        /// </return>
        FechaFinPeriodoVentaOutput FechaFinPeriodoVenta(FechaFinPeriodoVentaInput fechainicioInput);

        /// <summary>
        ///     Se encarga de obtener la lista de beneficios del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllBeneficiosPlanExequialInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllBeneficiosPlanExequialOutput"/> con la lista de beneficios del plan exequial
        /// </return>
        GetAllBeneficiosPlanExequialOutput GetAllBeneficiosPlanExequial(GetAllBeneficiosPlanExequialInput planExequial);

        /// <summary>
        ///     Se encarga de obtener la lista de beneficios adicionales del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllBeneficiosAdicionalesPlanExequialInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllBeneficiosAdicionalesPlanExequialOutput"/> con la lista de beneficios adicionales del plan exequial
        /// </return>
        GetAllBeneficiosAdicionalesPlanExequialOutput GetAllBeneficiosAdicionalesPlanExequial(GetAllBeneficiosAdicionalesPlanExequialInput planExequial);

        /// <summary>
        ///     Se encarga de obtener un beneficios del plan exequial según el id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetBeneficioPlanExequialInput"/> con el id del beneficio del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetBeneficioPlanExequialOutput"/> con la información del beneficio del plan exequial
        /// </return>
        GetBeneficioPlanExequialOutput GetBeneficioPlanExequial(GetBeneficioPlanExequialInput beneficioPlanExequial);

        /// <summary>
        ///     Se encarga de obtener un beneficio adicional del plan exequial según el id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetBeneficioAdicionalPlanExequialInput"/> con el id del beneficio adicional del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetBeneficioAdicionalPlanExequialOutput"/> con la información del beneficio adicional del plan exequial
        /// </return>
        GetBeneficioAdicionalPlanExequialOutput GetBeneficioAdicionalPlanExequial(GetBeneficioAdicionalPlanExequialInput beneficioAdicionalPlanExequial);

        /// <summary>
        ///     Se encarga de guardar un beneficio del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllBeneficiosPlanExequialInput"/> con la información del beneficio del plan exequial
        /// </param>
        /// <return></return>
        void SaveBeneficioPlanExequial(SaveBeneficioPlanExequialInput nuevoBeneficioPlanExequial);

        /// <summary>
        ///     Se encarga de guardar un beneficio adicional del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveBeneficioAdicionalPlanExequialInput"/> con la información del beneficio adicional del plan exequial
        /// </param>
        /// <return></return>
        void SaveBeneficioAdicionalPlanExequial(SaveBeneficioAdicionalPlanExequialInput nuevoBeneficioAdicionalPlanExequial);

        /// <summary>
        ///     Se encarga de eliminar un beneficio del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllBeneficiosPlanExequialInput"/> con el id del beneficio del plan exequial
        /// </param>
        /// <return></return>
        void DeleteBeneficioPlanExequial(DeleteBeneficioPlanExequialInput beneficioPlanExequialEliminar);

        /// <summary>
        ///     Se encarga de eliminar un beneficio adicional del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteBeneficioAdicionalPlanExequialInput"/> con el id del beneficio adicional del plan exequial
        /// </param>
        /// <return></return>
        void DeleteBeneficioAdicionalPlanExequial(DeleteBeneficioAdicionalPlanExequialInput beneficioAdicionalPlanExequialEliminar);

        /// <summary>
        ///     Se encarga de actualizar un beneficio del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdateBeneficioPlanExequialInput"/> con la información del beneficio del plan exequial
        /// </param>
        /// <return></return>
        void UpdateBeneficioPlanExequial(UpdateBeneficioPlanExequialInput editarBeneficioPlanExequial);

        /// <summary>
        ///     Se encarga de actualizar un beneficio adicional del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdateBeneficioAdicionalPlanExequialInput"/> con la información del beneficio adicional del plan exequial
        /// </param>
        /// <return></return>
        void UpdateBeneficioAdicionalPlanExequial(UpdateBeneficioAdicionalPlanExequialInput editarBeneficioAdicionalPlanExequial);

        /// <summary>
        ///     Se encarga de validar si es posible eliminar o no un beneficios del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarBeneficioPlanExequialInput"/> con el id del beneficio del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.PuedeEliminarBeneficioPlanExequialOutput"/> con la respuesta si se puede eliminar o no el beneficio
        /// </return>
        PuedeEliminarBeneficioPlanExequialOutput PuedeEliminarBeneficioPlanExequial(PuedeEliminarBeneficioPlanExequialInput beneficioPlanExequialEliminar);

        /// <summary>
        ///     Se encarga consultar todos los tipos de beneficio del plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllTiposBeneficioPlanExequialInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna una lista con objetos <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllTiposBeneficioPlanExequialOutput"/> con la información del tipo de beneficio del plan exequial
        /// </return>
        GetAllTiposBeneficioPlanExequialOutput GetAllTiposBeneficioPlanExequial(GetAllTiposBeneficioPlanExequialInput planExequial);

        /// <summary>
        ///     Se encarga consultar todos los beneficios segun la categoría y que no estén ya asignados al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetBeneficiosPlanExequialByCategoriaInput"/> con el id del plan exequial, el id de la categoria y el tipo de grupo de beneficio
        /// </param>
        /// <return>
        ///     Retorna una lista con objetos <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetBeneficiosPlanExequialByCategoriaOutput"/> con la información del beneficio
        /// </return>
        GetBeneficiosPlanExequialByCategoriaOutput GetBeneficiosPlanExequialByCategoria(GetBeneficiosPlanExequialByCategoriaInput categoriaInput);

        /// <summary>
        ///     Se encarga de obtener la lista de beneficios del plan exequial según la categoría
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllBeneficiosPlanExequialByTipoInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllBeneficiosPlanExequialByTipoOutput"/> con la lista de beneficios del plan exequial
        /// </return>
        GetAllBeneficiosPlanExequialByTipoOutput GetAllBeneficiosPlanExequialByTipo(GetAllBeneficiosPlanExequialByTipoInput planExequialAndTipo);

        /// <summary>
        ///     Se encarga de obtener la lista de beneficios del plan exequial según la categoría y el plan exequial, además no toma en cuenta los que ya estén registrados con el beneficio adicional.
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalInput"/> con el id del plan exequial, el id del tipo y el id del beneficio adicional
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput"/> con la lista de beneficios del plan exequial
        /// </return>
        GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalOutput GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicional(GetAllBeneficiosPlanExequialByTipoAndBeneficioAdicionalInput planExequialAndTipoAndBeneficioAdicional);

        /// <summary>
        ///     Se encarga consultar todas las empresas de la organización y las sucursales asignadas a un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllEmpresasPlanExequialInput"/> con el id del plan exequial y el id de la organización
        /// </param>
        /// <return>
        ///     Retorna una lista con objetos <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllEmpresasPlanExequialOutput"/> con la información de cada empresa
        /// </return>
        GetAllEmpresasPlanExequialOutput GetAllEmpresasPlanExequial(GetAllEmpresasPlanExequialInput empresasPlanExequial);

        /// <summary>
        ///     Se encarga consultar todas las sucursales de la empresa y las que están asignadas a un plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllSucursalesPlanExequialInput"/> con el id del plan exequial y el id de la empresa organización
        /// </param>
        /// <return>
        ///     Retorna una lista con objetos <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllSucursalesPlanExequialOutput"/> con la información de cada sucursal
        /// </return>
        GetAllSucursalesPlanExequialOutput GetAllSucursalesPlanExequial(GetAllSucursalesPlanExequialInput empresasPlanExequial);

        /// <summary>
        ///     Se encarga agregar o eliminar asociaciones de una sucursal al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdateSucursalesPlanExequialInput"/> con el listado de sucursales y planes exequiales
        /// </param>
        /// <return></return>
        void UpdateSucursalesPlanExequial(UpdateSucursalesPlanExequialInput sucursalPlanExequial);

        /// <summary>
        ///     Se encarga obtener todas las localidades asociadas a un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllLocalidadesByConvenioInput"/> con el id del recaudo masivo
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllLocalidadesByConvenioOutput"/> con el listada de las localidades
        /// </return>
        GetAllLocalidadesByConvenioOutput GetAllLocalidadesByConvenio(GetAllLocalidadesByConvenioInput convenioInput);

        /// <summary>
        ///     Se encarga obtener todas las localidades asociadas a un convenio de recaudo masivo y de un pais específico
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllLocalidadesByConvenioAndPaisInput"/> con el id del recaudo masivo y el id del pais
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllLocalidadesByConvenioAndPaisOutput"/> con el listada de las localidades
        /// </return>
        GetAllLocalidadesByConvenioAndPaisOutput GetAllLocalidadesByConvenioAndPais(GetAllLocalidadesByConvenioAndPaisInput convenioAndPaisInput);

        /// <summary>
        ///     Se encarga obtener todas las localidades asociadas a un convenio de recaudo masivo y de un departamento específico
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllLocalidadesByConvenioAndDepartamentoInput"/> con el id del recaudo masivo y el id del departamento
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllLocalidadesByConvenioAndDepartamentoOutput"/> con el listada de las localidades
        /// </return>
        GetAllLocalidadesByConvenioAndDepartamentoOutput GetAllLocalidadesByConvenioAndDepartamento(GetAllLocalidadesByConvenioAndDepartamentoInput convenioAndPaisAndDepartamentoInput);

        /// <summary>
        ///     Se encarga obtener todas las localidades que no estén asociadas a un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllLocalidadesByNotConvenioInput"/> con el id del recaudo masivo
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllLocalidadesByNotConvenioOutput"/> con el listada de las localidades
        /// </return>
        GetAllLocalidadesByNotConvenioOutput GetAllLocalidadesByNotConvenio(GetAllLocalidadesByNotConvenioInput localidadesByNotConvenio);

        /// <summary>
        ///     Se encarga obtener todas las localidades que no estén asociadas a un convenio de recaudo masivo y que sean de un pais específico
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllLocalidadesByNotConvenioAndPaisInput"/> con el id del recaudo masivo y el id del pais
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllLocalidadesByNotConvenioAndPaisOutput"/> con el listada de las localidades
        /// </return>
        GetAllLocalidadesByNotConvenioAndPaisOutput GetAllLocalidadesByNotConvenioAndPais(GetAllLocalidadesByNotConvenioAndPaisInput localidadesByNotConvenioAndPais);

        /// <summary>
        ///     Se encarga obtener todas las localidades que no estén asociadas a un convenio de recaudo masivo y que sean de un departamento específico
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllLocalidadesByNotConvenioAndDepartamentoInput"/> con el id del recaudo masivo y el id del departamento
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllLocalidadesByNotConvenioAndDepartamentoOutput"/> con el listada de las localidades
        /// </return>
        GetAllLocalidadesByNotConvenioAndDepartamentoOutput GetAllLocalidadesByNotConvenioAndDepartamento(GetAllLocalidadesByNotConvenioAndDepartamentoInput localidadesByNotConvenioAndDepartamento);

        /// <summary>
        ///     Se encarga obtener todos los paises donde una o varias de sus localidades estén asociadas a un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllPaisesByConvenioInput"/> con el id del recaudo masivo
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllPaisesByConvenioOutput"/> con el listada de los paises
        /// </return>
        GetAllPaisesByConvenioOutput GetAllPaisesByConvenio(GetAllPaisesByConvenioInput convenioInput);

        /// <summary>
        ///     Se encarga obtener todos los departamentos donde una o varias de sus localidades estén asociadas a un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllDepartamentosByConvenioInput"/> con el id del recaudo masivo
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllDepartamentosByConvenioOutput"/> con el listada de los departamentos
        /// </return>
        GetAllDepartamentosByConvenioOutput GetAllDepartamentosByConvenio(GetAllDepartamentosByConvenioInput convenioInput);

        /// <summary>
        ///     Se encarga de almacenar la asociación de todas las localidades partenecientes a un pais con un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveConveniosLocalidadByPaisInput"/> con el id del recaudo masivo y el id del pais
        /// </param>
        /// <return></return>
        void SaveConveniosLocalidadByPais(SaveConveniosLocalidadByPaisInput asignarConveniosLocalidadPorPais);

        /// <summary>
        ///     Se encarga de almacenar la asociación de todas las localidades partenecientes a un departamento con un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveConveniosLocalidadByDepartamentoInput"/> con el id del recaudo masivo y el id del departamento
        /// </param>
        /// <return></return>
        void SaveConveniosLocalidadByDepartamento(SaveConveniosLocalidadByDepartamentoInput asignarConveniosLocalidadPorDepartamento);

        /// <summary>
        ///     Se encarga de almacenar la asociación de una localidad con un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveConvenioLocalidadnput"/> con el id del recaudo masivo y el id de la localidad
        /// </param>
        /// <return></return>
        void SaveConvenioLocalidad(SaveConvenioLocalidadInput asignarConvenioLocalidad);

        /// <summary>
        ///     Se encarga de eliminar la asociación de todas las localidades partenecientes a un pais con un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteConveniosLocalidadByPaisInput"/> con el id del recaudo masivo y el id del pais
        /// </param>
        /// <return></return>
        void DeleteConveniosLocalidadByPais(DeleteConveniosLocalidadByPaisInput eliminarConveniosLocalidadPorPais);
        
        /// <summary>
        ///     Se encarga de eliminar la asociación de todas las localidades partenecientes a un departamento con un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteConveniosLocalidadByDepartamentoInput"/> con el id del recaudo masivo y el id del departamento
        /// </param>
        /// <return></return>
        void DeleteConveniosLocalidadByDepartamento(DeleteConveniosLocalidadByDepartamentoInput eliminarConveniosLocalidadPorDepartamento);

        /// <summary>
        ///     Se encarga de eliminar la asociación de una localidad con un convenio de recaudo masivo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteConvenioLocalidadInput"/> con el id del recaudo masivo y el id de la localidad
        /// </param>
        /// <return></return>
        void DeleteConvenioLocalidad(DeleteConvenioLocalidadInput eliminarConvenioLocalidad);

        /// <summary>
        ///     Se encarga de listar los convenios de recaudo asociados al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteConvenioLocalidadInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllPlanExequialRecaudoMasivoOutput"/> con el listado de convenios de recaudo asignados al plan exequial
        /// </return>
        GetAllPlanExequialRecaudoMasivoOutput GetAllPlanExequialRecaudoMasivo(GetAllPlanExequialRecaudoMasivoInput planExequial);

        /// <summary>
        ///     Se encarga de obtener la información del convenio de recaudo asociado al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetPlanExequialRecaudoMasivoInput"/> con el id del convenio de recaudo asignado al plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetPlanExequialRecaudoMasivoOutput"/> con la información del convenio de recaudo asignado al plan exequial
        /// </return>
        GetPlanExequialRecaudoMasivoOutput GetPlanExequialRecaudoMasivo(GetPlanExequialRecaudoMasivoInput planExequialRecaudoMasivo);

        /// <summary>
        ///     Se encarga de almacenar la asignación de un convenio de recaudo al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SavePlanExequialRecaudoMasivoInput"/> con la información de la asignación del convenio de recaudo al plan exequial
        /// </param>
        /// <return></return>
        void SavePlanExequialRecaudoMasivo(SavePlanExequialRecaudoMasivoInput nuevoPlanExequialRecaudoMasivo);

        /// <summary>
        ///     Se encarga de eliminar la asignación de un convenio de recaudo al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeletePlanExequialRecaudoMasivoInput"/> con el id de la asignación del convenio de recaudo al plan exequial
        /// </param>
        /// <return></return>
        void DeletePlanExequialRecaudoMasivo(DeletePlanExequialRecaudoMasivoInput eliminarPlanExequialRecaudoMasivo);

        /// <summary>
        ///     Se encarga de modificar la asignación de un convenio de recaudo al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.UpdatePlanExequialRecaudoMasivoInput"/> con la información de la asignación del convenio de recaudo al plan exequial
        /// </param>
        /// <return></return>
        void UpdatePlanExequialRecaudoMasivo(UpdatePlanExequialRecaudoMasivoInput editarPlanExequialRecaudoMasivo);

        /// <summary>
        ///     Se encarga de validar si es posible o no eliminar la asignación del convenio de recaudo al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.PuedeEliminarPlanExequialRecaudoMasivoInput"/> con el id de la asignación del convenio de recaudo al plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.PuedeEliminarPlanExequialRecaudoMasivoOutput"/> con un valor booleano si es posible eliminar la asignación o no
        /// </return>
        PuedeEliminarPlanExequialRecaudoMasivoOutput PuedeEliminarPlanExequialRecaudoMasivo(PuedeEliminarPlanExequialRecaudoMasivoInput planExequialRecaudoMasivoEliminar);

        /// <summary>
        ///     Se encarga de listar los convenios de recaudo que no estén asignados al plan exequial
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllConveniosRecaudoMasivoNoAsignadosInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllPlanExequialRecaudoMasivoOutput"/> con el listado de convenios de recaudo no asignados al plan exequial
        /// </return>
        GetAllConveniosRecaudoMasivoNoAsignadosOutput GetAllConveniosRecaudoMasivoNoAsignados(GetAllConveniosRecaudoMasivoNoAsignadosInput planExequial);

        /// <summary>
        ///     Se encarga guardar un prospecto en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveProspectoInput"/> con la información del prospecto
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.SaveProspectoOutput"/> con la información del prospecto almacenado
        /// </return>
        SaveProspectoOutput SaveProspecto(SaveProspectoInput nuevoProspecto);

        /// <summary>
        ///     Se encarga guardar una gestion prospecto en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveGestionProspectoInput"/> con la información de la gestion prospecto
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.SaveGestionProspectoOutput"/> con la información de la gestion prospecto almacenado
        /// </return>
        SaveGestionProspectoOutput SaveGestionProspecto(SaveGestionProspectoInput nuevoGestionProspecto);


        /// <summary>
        ///     Se encarga retornar las funerarias prospecto almacenadas en la base de datos
        /// </summary>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllFunerariasOutput"/> con las funerarias almacenadas
        /// </return>
        GetAllFunerariasOutput GetAllFunerarias();

        /// <summary>
        ///     Se encarga de guardar un afiliado prospecto
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveAfiliadoProspectoInput"/> con la información del afiliado prospecto
        /// </param>
        /// <return></return>
        void SaveAfiliadoProspecto(SaveAfiliadoProspectoInput afiliadoProspecto);

        /// <summary>
        ///     Se encarga de retornar una lista de los afiliados prospecto almacenados segun la gestión prospecto indicada
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAfiliadosProspectoInput"/> con el id de la gestión prospecto a consultar
        /// </param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAfiliadosProspectoOutput"/> con la información de los afiliados prospecto
        /// </return>
        GetAfiliadosProspectoOutput GetAfiliadosProspecto(GetAfiliadosProspectoInput gestionProspectoId);

        /// <summary>
        ///     Se encarga de eliminar un afiliado prospecto
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DeleteAfiliadoProspectoInput"/> con el id del afiliado prospecto a eliminar
        /// </param>
        /// <return></return>
        void DeleteAfiliadoProspecto(DeleteAfiliadoProspectoInput afiliadoProspectoInput);

        /// <summary>
        ///     Se encarga de retornar la informacion de un afiliado prospecto para editarlo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAfiliadoProspectoInput"/> con el id del afiliado prospecto a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAfiliadoProspectoOutput"/> con la información del afiliado prospecto
        /// </return>
        GetAfiliadoProspectoOutput GetAfiliadoProspecto(GetAfiliadoProspectoInput afiliadoProspectoInput);

        GetAllGrupoInformalOutput GetAllGrupoInformal();

        GetGrupoInformalOutput GetGrupoInformal(GetGrupoInformalInput grupoInformal);

        /// <summary>
        ///     Se encarga de almacenar un grupo informal
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveGrupoInformalInput"/> con la información del grupo informal
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.SaveGrupoInformalOutput"/> con el Id del grupo informal almacenado
        /// </return>
        SaveGrupoInformalOutput SaveGrupoInformal(SaveGrupoInformalInput grupoInformal);

        /// <summary>
        ///     Se encarga de guardar los empleados encargados de un grupo informal
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveGrupoInformalEmpleadoInput"/> con el id del grupo informal y el listado de empleados a almacenar
        /// </param>
        /// <return></return>
        void SaveGrupoInformalEmpleado(SaveGrupoInformalEmpleadoInput empleadosGrupoInformal);

        GetAllGrupoInformalByContactoOutput GetAllGrupoInformalByContacto(GetAllGrupoInformalByContactoInput contacto);

        GetAllEmpleadosByGrupoInformalOutput GetAllEmpleadosByGrupoInformal(GetAllEmpleadosByGrupoInformalInput grupoInformal);

        /// <summary>
        ///     Se encarga de obtener los planes posibles segun datos del prospecto
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetPlanesProspectoInput"/> con la información del cliente prospecto
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetPlanesProspectoOutput"/> con los planes posibles para el cliente prospecto
        /// </return>
        GetPlanesProspectoOutput GetPlanesProspecto(GetPlanesProspectoInput parentescosInput);

        /// <summary>
        ///     Se encarga de obtener los tipos de beneficio propios, modificables y adicionales
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetBeneficiosPlanInput"/> con el id del plan exequial
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetBeneficiosPlanOutput"/> con la lista de los tipos de beneficios propios, modificables y adicionales
        /// </return>
        GetBeneficiosPlanOutput GetBeneficiosPlan(GetBeneficiosPlanInput planInput);


        /// <summary>
        ///     Se encarga de guardar la afiliacion del cliente prospecto
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveAfiliacionInput"/> con la información relacionada y asignada del cliente prospecto
        /// </param>
        /// <return></return>
        void SaveAfiliacion(SaveAfiliacionInput afiliacionInput);

        /// <summary>
        ///     Se encarga de guardar un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveParentescosInput"/> con la información del parentesco a guardar
        /// </param>
        /// <return></return>
        void SaveParentesco(SaveParentescoInput parentescosInput);

        /// <summary>
        ///     Se encarga de actualizar un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveParentescosInput"/> con la información del parentesco a actualizar
        /// </param>
        /// <return></return>
        void UpdateParentesco(SaveParentescoInput parentescosInput);

        /// <summary>
        ///     Se encarga de obtener los datos de un parentesco especifico
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetParentescoInput"/> con el id del parentesco a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetParentescoOutput"/> con la información del parentesco solicitado
        /// </return>
        GetParentescoOutput GetParentesco(GetParentescoInput parntescoInput);

        /// <summary>
        ///     Se encarga de verificar si un parentesco se puede o no eliminar
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetParentescoInput"/> con el id del parentesco a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetParentescoOutput"/> con un valor true o false si el parentesco ingresado se puede eliminar
        /// </return>
        PuedeEliminarParentescoOutput PuedeEliminarParentesco(PuedeEliminarParentescoInput parentescoInput);

        /// <summary>
        ///     Se encarga de eliminar un parentesco
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.SaveParentescosInput"/> con el id del parentesco a eliminar
        /// </param>
        /// <return></return>
        void DeleteParentesco(DeleteParentescoInput parentescoInput);

        /// <summary>
        ///     Se encarga de obtener la lista de gestiones prospecto que se realizaron en la direccion o telefono indicados (botón validar prospecto)
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetProspectoProspectoInput"/> con el id de la direccion o el id del telefono a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetGestionProspectoOutput"/> con un valor true o false si el parentesco ingresado se puede eliminar
        /// </return>
        GetGestionProspectoOutput GetProspecto(GetProspectoProspectoInput direccionTelefonoInput);

        /// <summary>
        ///     Se encarga de obtener el detalle de la gestion prospecto indicada (boton ver detalle)
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.DetalleClienteProspectoInput"/> con los datos de la gestion prospecto, para obtener sus detalles
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.DetalleClienteProspectoOutput"/> con la informacion detallada del plan prospecto con sus afiliados (beneficiarios)
        /// </return>
        DetalleClienteProspectoOutput DetalleClienteProspecto(DetalleClienteProspectoInput gestionInput);

        /// <summary>
        ///     Se encarga de obtener toda la información de la gestion prospecto indicada (botón Iniciar Contacto)
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetGestionProspectoIniciarContactoInput"/> con los datos de la gestion prospecto, para obtener sus detalles
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetGestionProspectoIniciarContactoOutput"/> con la informacion detallada del plan prospecto con sus afiliados (beneficiarios)
        /// </return>
        GetGestionProspectoIniciarContactoOutput GetGestionProspectoIniciarContacto(GetGestionProspectoIniciarContactoInput gestionInput);

        /// <summary>
        ///     Se encarga de copiar los parentescos de la gestion prospecto indicada para gestionar uno nuevo
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.CopiarAfiliadosProspectoInput"/> con la información de la gestion prospecto Id y los parentescos a copiar
        /// </param>
        /// <return></return>
        void CopiarAfiliadosProspecto(CopiarAfiliadosProspectoInput afiliadosInput);
        /// <summary>
        ///     Se encarga de obtener los planes exequiales a partir de la sucursal y el tipo de plan
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllPlanesExequialesBySucursalAndTipoInput"/> con el id de la sucursal,
        ///     el tipo de plan, y opcionalmente según el tipo de plan el id de la empresa para tipo empresarial o el id del grupo para tipo grupo
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllPlanesExequialesBySucursalAndTipoOutput"/> con el listado de planes exequiales
        /// </return>
        GetAllPlanesExequialesBySucursalAndTipoOutput GetAllPlanesExequialesBySucursalAndTipo(GetAllPlanesExequialesBySucursalAndTipoInput sucursalAndTipo);

        /// <summary>
        ///     Se encarga de obtener el listado de recaudos masivos asociados a una localidad
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Afiliaciones.DTOs.InputModels.GetAllRecaudosMasivosByLocalidadInput"/> con el id de la localidad
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Afiliaciones.DTOs.OutputModels.GetAllRecaudosMasivosByLocalidadOutput"/> con el listado de recaudos masivos
        /// </return>
        GetAllRecaudosMasivosByLocalidadOutput GetAllRecaudosMasivosByLocalidad(GetAllRecaudosMasivosByLocalidadInput localidad);

       
    }
}
