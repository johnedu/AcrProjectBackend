using Abp.Application.Services;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Parametros
{
    /// <summary>
    /// Definición de los servicios ofrecidos por el módulo de Parametros
    /// </summary>
    public interface IParametrosService : IApplicationService
    {
        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetParametrosOutput"/> con toda la información de los parametros        
        /// </summary>
        GetParametrosOutput GetParametros();

        /// <summary>
        /// Se encarga de registrar el parametro indicado en el sistema. La información del parametro
        /// llega en un objeto de la clase <see cref="Bow.Application.Parametros.DTOs.InputModels.SaveParametroInput"/>
        /// </summary>
        /// <param name="nuevoParametro">Nuevo parametro a registrar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el parametro indicado ya existe</exception>
        void SaveParametro(SaveParametroInput nuevoParametro);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetParametroOutput"/> con la información del parametro
        /// solicitado para editar / eliminar - Un solo registro
        /// </summary>
        /// <param name="parametroInput">parametro a consultar <see cref="Bow.Application.Parametros.DTOs.InputModels.GetParametroInput"/></param>
        /// <returns>GetParametroOutput con el parametro para editar / eliminar</returns>
        GetParametroOutput GetParametro(GetParametroInput parametroInput);

        /// <summary>
        /// Se encarga de actualizar el parametro indicado en el objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.UpdateParametroInput"/>
        /// del sistema
        /// </summary>
        /// <param name="parametroUpdate">parametro a actualizar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el parametro indicado ya existe</exception>
        void UpdateParametro(UpdateParametroInput parametroUpdate);

        /// <summary>
        /// Se encarga de eliminar el parametro indicado en el objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.DeleteParametroInput"/>
        /// del sistema
        /// </summary>
        /// <param name="parametroEliminar">Parametro a eliminar</param>
        void DeleteParametro(DeleteParametroInput parametroEliminar);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.PuedeEliminarParametroOutput"/> con un bool si se puede o no eliminar el 
        /// parámetro indicado.
        /// </summary>
        /// <param name="parametroEliminar">parametro a consultar para verificar si tiene estados o tipos asociados<see cref="Bow.Application.Parametros.DTOs.InputModels.DeleteParametroInput"/></param>
        /// <returns>PuedeEliminarParametroOutput valor true o false si se puede o no eliminar el parametro indicado</returns>
        PuedeEliminarParametroOutput PuedeEliminarParametro(DeleteParametroInput parametroEliminar);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTiposOutput"/> con toda la información de los tipos
        /// que pertenecen al parametro solicitado
        /// </summary>
        /// <param name="parametroInput">Objeto Parametro a consultar <see cref="Bow.Application.Parametros.DTOs.InputModels.GetParametroInput"/></param>
        /// <returns>GetTiposOutput con la lista de tipos que pertenecen al parametro indicado</returns>
        GetTiposOutput GetTipos(GetParametroInput parametroInput);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTipoOutput"/> con la información del tipo de parametro
        /// solicitado para editar / eliminar - Un solo registro
        /// </summary>
        /// <param name="tipoInput">tipo de parametro a consultar <see cref="Bow.Application.Parametros.DTOs.InputModels.GetTipoInput"/></param>
        /// <returns>GetTipoOutput con el tipo de parametro para editar / eliminar</returns>
        GetTipoOutput GetTipo(GetTipoInput tipoInput);

        /// <summary>
        /// Se encarga de registrar el tipo de parametro indicado en el sistema. La información del tipo de parametro
        /// llega en un objeto de la clase <see cref="Bow.Application.Parametros.DTOs.InputModels.SaveTipoInput"/>
        /// </summary>
        /// <param name="nuevoTipo">Nuevo tipo de parametro a registrar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el nombre del tipo de parametro indicado ya existe</exception>
        void SaveTipo(SaveTipoInput nuevoTipo);

        /// <summary>
        /// Se encarga de eliminar el tipo de parametro indicado en el objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.DeleteTipoInput"/>
        /// del sistema
        /// </summary>
        /// <param name="tipoEliminar">Tipo de parametro a eliminar</param>
        void DeleteTipo(DeleteTipoInput tipoEliminar);

        /// <summary>
        /// Se encarga de actualizar el tipo de parametro indicado en el objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.UpdateTipoInput"/>
        /// del sistema
        /// </summary>
        /// <param name="tipoUpdate">tipo de parametro a actualizar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el tipo de parametro indicado ya existe</exception>
        void UpdateTipo(UpdateTipoInput tipoUpdate);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadosWithNombresEstadosOutput"/> con la información relacionada de los estados de un parametro
        /// </summary>
        /// <param name="parametroInput">Parametro a consultar <see cref="Bow.Application.Parametros.DTOs.InputModels.GetParametroInput"/></param>
        /// <returns>GetEstadosWithNombresEstadosOutput con el listado de los estados que pertenecen al parametro</returns>
        GetEstadosWithNombresEstadosOutput GetEstadosWithNombreEstado(GetParametroInput parametroInput);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetEstadosWithNombresEstadosOutput GetEstadosWithNombreEstadoPreferencia();

        /// <summary>
        /// Se encarga de registrar el estado indicado en el sistema. La información del estado
        /// llega en un objeto de la clase <see cref="Bow.Application.Parametros.DTOs.InputModels.SaveNombreEstadoInput"/>
        /// </summary>
        /// <param name="nuevoNombreEstado">Nuevo estado a registrar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el nombre del estado indicado ya existe</exception>
        void SaveNombreEstado(SaveNombreEstadoInput nuevoNombreEstado);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetNombresEstadosOutput"/> con la información de los estados para cargar en el dropdown
        /// </summary>
        GetNombresEstadosOutput GetNombresEstados();

        /// <summary>
        /// Se encarga de asignar un estado al parametro endicado. La información del estado y el parametro
        /// llega en un objeto de la clase <see cref="Bow.Application.Parametros.DTOs.InputModels.SaveEstadoInput"/>
        /// </summary>
        /// <param name="nuevoEstadoAsignado">Nuevo estado para asignar al parametro</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el nombre del estado indicado ya esta asignado</exception>
        void saveAsignarEstadoParametro(SaveEstadoInput nuevoEstadoAsignado);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.EstadoWithNombreEstadoOutput"/> con la información del estado del parametro
        /// solicitado para editar / eliminar - Un solo registro
        /// </summary>
        /// <param name="estadoInput">Estado de parametro a consultar <see cref="Bow.Application.Parametros.DTOs.InputModels.GetEstadoInput"/></param>
        /// <returns>EstadoWithNombreEstadoOutput con el estado de parametro para editar / eliminar</returns>
        EstadoWithNombreEstadoOutput GetEstado(GetEstadoInput estadoInput);

        /// <summary>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.EstadoWithNombreEstadoOutput"/> con la información del estado
        /// </summary>
        /// <param name="estadoAndParametroInput">Nombre del estado y del parametro a consultar <see cref="Bow.Application.Parametros.DTOs.InputModels.GetEstadoByNombreEstadoAndNombreParametroInput"/></param>
        /// <returns>EstadoWithNombreEstadoOutput con el estado de parametro</returns>
        EstadoWithNombreEstadoOutput GetEstadoByNombreEstadoAndNombreParametro(GetEstadoByNombreEstadoAndNombreParametroInput estadoAndParametroInput);

        /// <summary>
        /// Se encarga de eliminar la asignacion del estado del parametro indicado en el objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.DeleteEstadoInput"/>
        /// del sistema
        /// </summary>
        /// <param name="estadoEliminar">Estado del parametro a eliminar</param>
        void DeleteEstado(DeleteEstadoInput estadoEliminar);

        /// <summary>
        /// Se encarga de actualizar el motivo del estado del parametro indicado en el objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.UpdateEstadoParametroInput"/>
        /// del sistema
        /// </summary>
        /// <param name="estadoUpdate">Motivo del estado del parametro a actualizar</param>
        void UpdateEstadoParametro(UpdateEstadoParametroInput estadoUpdate);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTiposZonaOutput"/> con los tipos de zonas
        /// </summary>
        GetTiposZonaOutput GetTiposZona();

        /// <summary>
        /// Se encarga de listar todos los tipos de teléfono
        /// </summary>
        /// <returns>Retorna la lista de tipos de teléfono <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetAllTiposOutput"/></returns>
        GetAllTiposOutput GetAllTiposTelefono();

        /// <summary>
        /// Se encarga de consultar el tipo de telefono fijo
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTipoTelefonoFijoOutput"/> con la información del tipo de teléfono</returns>
        GetTipoTelefonoFijoOutput GetTipoTelefonoFijo();

        /// <summary>
        /// Se encarga de listar todos los tipos de parametro de Profesiones
        /// </summary>
        /// <returns>Retorna la lista de tipos parametro profesiones <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTiposByParametroProfesionesOutput"/></returns>
        GetTiposByParametroProfesionesOutput GetTiposByParametroProfesiones();

        /// <summary>
        /// Se encarga de listar todos los tipos de parametro de Estado Civil
        /// </summary>
        /// <returns>Retorna la lista de tipos parametro Estado Civil <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTiposByParametroEstadoCivilOutput"/></returns>
        GetTiposByParametroEstadoCivilOutput GetTiposByParametroEstadoCivil();

        /// <summary>
        ///     Se encarga de listar todos los tipos de valor de Información Tributaria
        /// </summary>
        /// <returns>Retorna la lista de tipos de valor de Información Tributaria <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTiposInfoTributariaOutput"/></returns>
        GetTiposInfoTributariaOutput GetTiposInfoTributaria();

        /// <summary>
        /// Se encarga de listar todos los tipos ubicación
        /// </summary>
        /// <returns>Retorna la lista de tipos de ubicación <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTiposByParametroUbicacionOutput"/></returns>
        GetTiposByParametroUbicacionOutput GetTiposByParametroUbicacion();

        /// <summary>
        ///     Se encarga de listar todos los tipos de valor de naturaleza empresa
        /// </summary>
        /// <returns>Retorna la lista de tipos de valor de Naturaleza Empresa <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTiposNaturalezaEmpresaOutput"/></returns>
        GetTiposNaturalezaEmpresaOutput GetTiposNaturalezaEmpresa();

        /// <summary>
        ///     Se encarga de consultar el tipo de naturaleza jurídica
        /// </summary>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetEsTipoJuridicaOutput"/> con la respuesta si es de tipo jurídica o no </returns>
        GetTipoJuridicaOutput GetTipoJuridica();

        /// <summary>
        /// Se encarga de consultar el id estado activo del parametro preferencia
        /// </summary>
        /// <returns></returns>
        GetEstadoPreferenciaOutput GetEstadoActivoPreferencia();

        /// <summary>
        /// Se encarga de consultar los id de los estados del parámetro tipoUbicacion
        /// </summary>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetEstadosTipoUbicacionOutput"/> con la información de los estados del parámetro </returns>
        GetEstadosTipoUbicacionOutput GetEstadosTipoUbicacion();

        /// <summary>
        ///     Se encarga de obtener toda la lista de contactos web de una persona sin los que ya tenga asignados
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.GetAllTiposContactoWebWithFilterPersonaInput"/> con la lista de contactos web ya asignados
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllTiposContactoWebWithFilterPersonaOutput"/> con la lista de contactos web
        /// </return>
        GetAllTiposContactoWebWithFilterPersonaOutput GetAllTiposContactoWebWithFilterPersona(GetAllTiposContactoWebWithFilterPersonaInput tiposContactoWebPersonaAsignados);

        /// <summary>
        ///     Se encarga de obtener toda la lista de tipos sin incluir los tipos que llegan como parámetro
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.GetAllTiposWithFilterInput"/> con la lista de tipos a excluir y el parámetro de los tipos a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllTiposWithFilterOutput"/> con la lista de tipos
        /// </return>
        GetAllTiposWithFilterOutput GetAllTiposWithFilter(GetAllTiposWithFilterInput tiposContactoWebEmpresaAsignados);

        /// <summary>
        /// Se encarga de listar los estados del empleado
        /// </summary>
        /// <returns>Retorna la lista de los estados posibles para el empleado<see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetEstadosEmpleadoOutput"/></returns>
        GetEstadosEmpleadoOutput GetEstadosEmpleado();

        /// <summary>
        /// Se encarga de retornar el id de estado activo del parametro rol empleado en zona para utilizar en zonificacionService
        /// </summary>
        /// <returns>Retorna el id del estado activo<see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetEstadoActivoZonaEmpleadoOutput"/></returns>
        GetEstadoActivoZonaEmpleadoOutput GetEstadoActivoZonaEmpleado();

        /// <summary>
        /// Se encarga de retornar el id de estado inactivo del parametro rol empleado en zona para utilizar en zonificacionService
        /// </summary>
        /// <returns>Retorna el id del estado inactivo<see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetEstadoInactivoZonaEmpleadoOutput"/></returns>
        GetEstadoInactivoZonaEmpleadoOutput GetEstadoInactivoZonaEmpleado();

        /// <summary>
        /// Se encarga de retornar los tipos de rol de empleado en zona
        /// </summary>
        /// <returns>Retorna los tipos de rol<see cref="Bow.Applicattion.Parametros.DTOs.OutputModels.GetTiposRolOutput"/></returns>
        GetTiposRolOutput GetTiposRol();

        //GetEstadosEmpleadoOutput GetEstadosEmpleado();

        /// <summary>
        ///     Se encarga de obtener toda la lista de tipos de sucursales empresa
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTiposSucursalEmpresaOutput"/> con la lista de tipos de sucursales
        /// </return>
        GetTiposSucursalEmpresaOutput GetTiposSucursalEmpresa();

        /// <summary>
        ///     Se encarga de obtener toda la lista de estados de sucursales empresa
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadosSucursalEmpresaOutput"/> con la lista de estados de sucursales
        /// </return>
        GetEstadosSucursalEmpresaOutput GetEstadosSucursalEmpresa();

        /// <summary>
        ///     Se encarga de obtener la lista de las profesiones con el id de "No identificada" para ubicarla por defecto
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTiposByParametroProfesionesWithIdNoIdentificadoOutput"/> con la lista de profesiones y el id de No Identificada
        /// </return>
        GetTiposByParametroProfesionesWithIdNoIdentificadoOutput GetTiposByParametroProfesionesWithIdNoIdentificado();

        /// <summary>
        ///     Se encarga de obtener la lista de los estados civiles con el id de "No identificada" para ubicarla por defecto
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTiposByParametroEstadoCivilWithIdNoIdentificadoOutput"/> con la lista de los estados civiles y el id de No Identificado
        /// </return>
        GetTiposByParametroEstadoCivilWithIdNoIdentificadoOutput GetTiposByParametroEstadoCivilWithIdNoIdentificado();

        /// <summary>
        ///     Se encarga de obtener el id del estado activo del plan exequial
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadoActivoPlanExequialOutput"/> con el id del estado activo del plan exequial
        /// </return>
        GetEstadoActivoPlanExequialOutput GetEstadoActivoPlanExequial();

        /// <summary>
        ///     Se encarga de obtener el id del estado inactivo del plan exequial
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadoInactivoPlanExequialOutput"/> con el id del estado inactivo del plan exequial
        /// </return>
        GetEstadoInactivoPlanExequialOutput GetEstadoInactivoPlanExequial();

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.SaveCategoriaInput"/> con la información de la categoria
        /// para almacenarlo o actualizarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void SaveCategoria(SaveCategoriaInput nuevaCategoria);

        /// <summary>
        ///     Se encarga de obtener la lista de las categorias existentes en el sistema (Categorias beneficios)
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetCategoriasOutput"/> con la lista de las categorias existentes
        /// </return>
        GetCategoriasOutput GetCategorias();

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.DeleteCategoriaInput"/> con el Id de la categoria
        /// para eliminar en la base de datos
        /// </summary>
        /// <returns></returns>
        void DeleteCategoria(DeleteCategoriaInput categoriaEliminar);

        /// <summary>
        ///     Se encarga de obtener la lista de las estados existentes del parámetro Grupo Familiar
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllEstadosGrupoFamiliarOutput"/> con la lista de los estados existentes
        /// </return>
        GetAllEstadosGrupoFamiliarOutput GetAllEstadosGrupoFamiliar();

        /// <summary>
        ///     Se encarga de obtener el id del estado activo del Grupo Familiar
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadoActivoGrupoFamiliarOutput"/> con el id del estado activo del Grupo Familiar
        /// </return>
        GetEstadoActivoGrupoFamiliarOutput GetEstadoActivoGrupoFamiliar();

        /// <summary>
        ///     Se encarga de obtener la lista de tipos de beneficios
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllTiposBeneficioOutput"/> con la información del tipo
        /// </return>
        GetAllTiposBeneficioOutput GetAllTiposBeneficio();

        /// <summary>
        ///     Se encarga de obtener la lista de las estados existentes del parámetro beneficios del plan exequial
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllEstadosBeneficiosPlanExequialOutput"/> con la lista de los estados existentes
        /// </return>
        GetAllEstadosBeneficiosPlanExequialOutput GetAllEstadosBeneficiosPlanExequial();

        /// <summary>
        ///     Se encarga de obtener el id del estado activo del parámetro beneficios del plan exequial
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadoActivoBeneficiosPlanExequialOutput"/> con el id del estado activo de beneficios del plan exequial
        /// </return>
        GetEstadoActivoBeneficiosPlanExequialOutput GetEstadoActivoBeneficiosPlanExequial();

        /// <summary>
        ///     Se encarga de obtener el id del estado inactivo del parámetro beneficios del plan exequial
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetEstadoInactivoBeneficiosPlanExequialOutput"/> con el id del estado inactivo de beneficios del plan exequial
        /// </return>
        GetEstadoInactivoBeneficiosPlanExequialOutput GetEstadoInactivoBeneficiosPlanExequial();

        /// <summary>
        ///     Se encarga de obtener el tipo de ubicadion residencial
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTipoUbicacionResidencialOutput"/> con el objeto de tipo de ubicacion residencial
        /// </return>
        GetTipoUbicacionResidencialOutput GetTipoUbicacionResidencial();

        /// <summary>
        ///     Se encarga de obtener todos los estados de motivo de no afiliación del cliente prospecto
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllEstadosClienteProspectoOutput"/> con el objeto de tipo de ubicacion residencial
        /// </return>
        GetAllEstadosClienteProspectoOutput GetAllEstadosClienteProspecto();

        /// <summary>
        ///     Se encarga de obtener el id del estado activo según un parametro Id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.GetAllTiposWithFilterInput"/> el parametroid para obtener su estado activo Id
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllTiposWithFilterOutput"/> el estado activo id del parametro indicado
        /// </return>
        GetEstadoActivoByNombreParametroOutput GetEstadoActivoByNombreParametro(GetEstadoActivoByNombreParametroInput parametro);

        /// <summary>
        ///     Se encarga de obtener el id del estado inactivo según un parametro Id
        /// </summary>
        /// <param>
        ///     Ingresa un objeto <see cref="Bow.Application.Parametros.DTOs.InputModels.GetAllTiposWithFilterInput"/> el parametroid para obtener su estado inactivo Id
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetAllTiposWithFilterOutput"/> el estado inactivo id del parametro indicado
        /// </return>
        GetEstadoInactivoByNombreParametroOutput GetEstadoInactivoByNombreParametro(GetEstadoInactivoByNombreParametroInput parametro);

        /// <summary>
        ///     Se encarga de obtener los id de los tipos de beneficio de plan exequial propios, adicionales y modificables
        /// </summary>
        /// <param></param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Parametros.DTOs.OutputModels.GetTipoUbicacionResidencialOutput"/> con los id de los beneficios de plan exequial propios, adicionales y modificables
        /// </return>
        GetTiposBeneficiosPlanExequialOutPut GetTiposBeneficiosPlanExequial();
    }
}
