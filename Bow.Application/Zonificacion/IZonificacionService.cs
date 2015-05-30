using Abp.Application.Services;
using Bow.Afiliaciones.DTOs.InputModels;
using Bow.Afiliaciones.DTOs.OutputModels;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Zonificacion
{
    /// <summary>
    /// Definición de los servicios ofrecidos por el módulo de Zonificación
    /// </summary>
    public interface IZonificacionService : IApplicationService
    {
        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Zonificacion.DTOs.GetPaisOutput"/> con toda la información del pais solicitado
        /// </summary>
        /// <param name="paisInput">Pais a consultar <see cref="Bow.Application.Zonificacion.DTOs.InputModels.GetPaisInput"/></param>
        /// <returns>GetPaisOutput con la información del Pais</returns>
        GetPaisOutput GetPais(EsPaisUsaInput paisInput);

        /// <summary>
        /// Retorna un objeto  <see cref="Bow.Application.Zonificacion.DTOs.GetLocalidadOutput"/> con toda la información de la localidad solicitada
        /// </summary>
        /// <param name="localidadInput"></param>
        /// <returns>GetLocalidadOutput con la información de la localidad</returns>
        GetLocalidadOutput GetLocalidad(GetLocalidadInput localidadInput);

        /// <summary>
        ///  Retorna un objeto  <see cref="Bow.Application.Zonificacion.DTOs.GetBarrioOutput"/> con toda la información de la localidad solicitada
        /// </summary>
        /// <param name="barrioInput"></param>
        /// <returns>GetBarrioOutpu tcon la información de barrio</returns>
        GetBarrioOutput GetBarrio(GetBarrioInput barrioInput);

        /// <summary>
        /// Retorna una  lista de todos los paises que estan registrados en el sistema
        /// </summary>
        /// <returns></returns>
        GetPaisesOutput GetPaises();

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Zonificacion.DTOs.GetLocalidadOutput"/> con toda la informacion de la localidad
        /// </summary>
        /// <param name="departamentoInput">id del departamento</param>
        /// <returns></returns>
        GetLocalidadesOutput GetLocalidades(GetLocalidadesInput departamentoInput);
        
        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Zonificacion.DTOs.GetLocalidadesByPaisOutput"/> con toda la informacion de las localidades
        /// </summary>
        /// <param name="paisInput">id del pais</param>
        GetLocalidadesByPaisOutput GetLocalidadesByPais(GetLocalidadesByPaisInput paisInput);

        /// <summary>
        /// Se encarga de recibir un objeto de la clase <see cref="Bow.Application.Zonificacion.DTOs.SavePaisInput"/> 
        /// para registrar un Pais en el sistema
        /// en el sistema
        /// </summary>
        /// <param name="nuevoPais">SavePaisInput a registrar en el sistema</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el pais indicado ya existe</exception>
        void SavePais(SavePaisInput nuevoPais);

        /// <summary>
        /// Se encarga de registrar un objeto de la clase <see cref="Bow.Application.Zonificacion.DTOs.SaveLocalidadInput"/> 
        /// para registrar una Localidad en el sistema
        /// </summary>
        /// <param name="nuevaLocalidad">SaveLocalidadInput a registrar en el sistema</param>
        void SaveLocalidad(SaveLocalidadInput nuevaLocalidad);

        /// <summary>
        /// Se encarga de registrar un objeto de la clase <see cref="Bow.Application.Zonificacion.DTOs.SaveBarrioInput"/> 
        /// </summary>
        /// <param name="nuevoBarrio"></param>
        void SaveBarrio(SaveBarrioInput nuevoBarrio);

        /// <summary>
        /// Se encarga de registrar un objeto de la clase <see cref="Bow.Application.Zonificacion.DTOs.SaveZonaInput"/> 
        /// </summary>
        /// <param name="nuevaZona"></param>
        void SaveZona(SaveZonaInput nuevaZona);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeletePaisInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="paisEliminar">id del pais que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarPaisOutput PuedeEliminarPais(PuedeEliminarPaisInput paisEliminar);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteBarrioInput"/>
        /// </summary>
        /// <param name="barrioEliminar"></param>
        /// <returns></returns>
        PuedeEliminarBarrioOutput PuedeEliminarBarrio(PuedeEliminarBarrioInput barrioEliminar);

        /// <summary>
        ///  Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteDepartamentoInput"/>
        /// </summary>
        /// <param name="departamentoEliminar"></param>
        /// <returns></returns>
        PuedeEliminarDepartamentoOutput PuedeEliminarDepartamento(PuedeEliminarDepartamentoInput departamentoEliminar);

        /// <summary>
        ///  Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteDepartamentoInput"/>
        /// </summary>
        /// <param name="departamentoEliminar"></param>
        /// <returns></returns>
        PuedeEliminarManzanaOutput PuedeEliminarManzana(PuedeEliminarManzanaInput manzanaEliminar);

        /// <summary>
        /// Se encarga de eliminar el pais indicado en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeletePaisInput"/>
        /// del sistema
        /// </summary>
        /// <param name="paisEliminar">Pais a Eliminar</param>
        void DeletePais(DeletePaisInput paisEliminar);

        /// <summary>
        /// Se encarga de eliminar el barrio indicado en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteBarrioInput"/>
        /// </summary>
        /// <param name="barrioEliminar"></param>
        void DeleteBarrio(DeleteBarrioInput barrioEliminar);

        /// <summary>
        /// Dado el código de una localidad retorna la información de la localidad y del departamento y el pais a los
        /// que pertenece.
        /// </summary>
        /// <param name="localidadInput">Contiene el Id de la localidad a consultar <see cref="Bow.Applicattion.Parametros.DTOs.InputModels.GetInfoLocalidadInput"/></param>
        /// <returns>Retorna un objeto de la clase <see cref="Bow.Applicattion.Parametros.DTOs.InputModels.GetInfoLocalidadInput"/> con la información 
        /// de la localidad y la zonificación a la que está asociada</returns>
        GetLocalidadWithDepartamentoAndPaisOutput GetLocalidadWithDepartamentoAndPais(GetLocalidadWithDepartamentoAndPaisInput localidadInput);

        /// <summary>
        ///     Dado el código de una localidad retorna la información de la localidad y del departamento y el pais a los que pertenece.
        /// </summary>
        /// <param name="localidad">
        ///     Contiene el Id de la localidad a consultar <see cref="Bow.Applicattion.Parametros.DTOs.InputModels.GetLocalidadByIdWithDepartamentoAndPaisInput"/>
        /// </param>
        /// <returns>
        ///     Retorna un objeto de la clase <see cref="Bow.Applicattion.Parametros.DTOs.InputModels.GetLocalidadByIdWithDepartamentoAndPaisOutput"/> con la información de la localidad
        /// </returns>
        GetLocalidadByIdWithDepartamentoAndPaisOutput GetLocalidadByIdWithDepartamentoAndPais(GetLocalidadByIdWithDepartamentoAndPaisInput localidad);

        /// <summary>
        /// Se encarga de eliminar el pais indicado en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteLocalidadInput"/>
        /// </summary>
        /// <param name="localidadEliminar">Localidad a eliminar</param>
        void DeleteLocalidad(DeleteLocalidadInput localidadEliminar);

        /// <summary>
        /// Se encarga de actualizar el pais indicado en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.UpdatePaisInput"/>
        /// del sistema
        /// </summary>
        /// <param name="paisUpdate">Pais a actualizar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el pais indicado ya existe</exception>
        void UpdatePais(UpdatePaisInput paisUpdate);

        /// <summary>
        /// Se encarga de registrar el departamento indicado en el sistema. La información del departamento
        /// llega en un objeto de la clase <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveDepartamentoInput"/>
        /// </summary>
        /// <param name="nuevoDepartamento">Nuevo departamento a registrar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el departamento indicado ya existe</exception>
        void SaveDepartamento(SaveDepartamentoInput nuevoDepartamento);     

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Zonificacion.DTOs.OutputModels.GetDepartamentoOutput"/> con toda la información de los departamentos
        /// que pertenecen al país solicitado
        /// </summary>
        /// <param name="paisInput">Pais a consultar <see cref="Bow.Application.Zonificacion.DTOs.InputModels.GetPaisInput"/></param>
        /// <returns>GetDepartamentosOutput con la lista de departamentos que pertenecen al país indicado</returns>
        GetDepartamentosOutput GetDepartamentos(GetDepartamentosInput paisInput);

        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Zonificacion.DTOs.OutputModels.GetAllDepartamentosOutput"/> con toda la información de los departamentos
        /// que pertenecen al país solicitado
        /// </summary>
        /// <returns>GetDepartamentosOutput con la lista de todos los departamentos</returns>
        GetAllDepartamentosOutput GetAllDepartamentos();
        
        /// <summary>
        /// Retorna un objeto <see cref="Bow.Application.Zonificacion.DTOs.OutputModels.GetDepartamentoOutput"/> con la información del departamento
        /// solicitado para editar - Un solo registro
        /// </summary>
        /// <param name="departamentoInput">Departamento a consultar <see cref="Bow.Application.Zonificacion.DTOs.InputModels.GetDepartamentoInput"/></param>
        /// <returns>GetDepartamentoOutput con el departamento para editar</returns>
        GetDepartamentoOutput GetDepartamento(GetDepartamentoInput departamentoInput);

        /// <summary>
        /// Se encarga de eliminar el departamento indicado en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteDepartamentoInput"/>
        /// del sistema
        /// </summary>
        /// <param name="departamentoEliminar">Departamento a eliminar</param>
        void DeleteDepartamento(DeleteDepartamentoInput departamentoEliminar);

        /// <summary>
        /// Se encarga de actualizar el departamento indicado en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.UpdateDepartamentoInput"/>
        /// del sistema
        /// </summary>
        /// <param name="departamentoUpdate">Departamento a actualizar</param>
        /// <exception cref="Abp.UI.UserFriendlyException">Si el departamento indicado ya existe</exception>
        void UpdateDepartamento(UpdateDepartamentoInput departamentoUpdate);

        /// <summary>
        /// Se encarga de registrar el tipo de orientación indicado en el sistema. Primero se verifica que no esté
        /// registrado para poder ingresarlo
        /// </summary>
        /// <param name="nuevoTipoOrientacion">Contiene el nombre del tipo de orientación a registrar</param>
        void SaveTipoOrientacion(SaveTipoOrientacionInput nuevoTipoOrientacion);

       /// <summary>
        /// Retorna una  lista de todos los tipos de orientacion que estan registrados en el sistema
        /// </summary>
        /// <returns></returns>
        GetTiposOrientacionOutput GetTiposOrientacion();

        /// <summary>
        /// Se encarga de registrar un tipo de orientación en una localidad en caso de que ya no este registrada en 
        /// el sistema
        /// </summary>
        /// <param name="tipoOrientacionLocalidad">Contiene el código del tipo de orientación y el código de la localidad. <see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveTipoOrientacionLocalidadInput"/></param>
        void SaveTipoOrientacionLocalidad(SaveTipoOrientacionLocalidadInput tipoOrientacionLocalidad);

        /// <summary>
        /// Retorna los tipos de orientación que están registrados en la localidad indicada
        /// Dado el código de una localidad retorna la información de la localidad y del departamento y el pais a los
        /// que pertenece.
        /// </summary>
        /// <param name="localidad">Cóntiene el identificador de la localidad a consultar <see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetToriesLocalidaByLocalidadInput"/></param>
        /// <returns>Retorna un objeto con los tipos de orientación asignados a la localidad <see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetToriesLocalidadByLocalidadOutput"/></returns>
        GetToriesLocalidadByLocalidadOutput GetToriesLocalidadByLocalidad(GetToriesLocalidaByLocalidadInput localidad);

        /// <summary>
        /// Se encarga de eliminar el torieLocalidad indicado en caso de que no esté siendo utilizado
        /// por manzanas o direcciones
        /// </summary>
        /// <param name="torieLocalidadEliminar"></param>
        void DeleteTorieLocalidad(DeleteTorieLocalidadInput torieLocalidadEliminar);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteTorieLocalidadInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="sufijoEliminar">id de torielocalidad que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarTorieLocalidadOutput PuedeEliminarTorieLocalidad(PuedeEliminarTorieLocalidadInput torieQuitar);

        /// <summary>
        /// Se encarga de actualizar la localidad indicada
        /// </summary>
        /// <param name="localidadUpdate"></param>
        void UpdateLocalidad(UpdateLocalidadInput localidadUpdate);

        /// <summary>
        /// Se encarga de actualizar el barrio indicado
        /// </summary>
        /// <param name="barrioUpdate"></param>
        void UpdateBarrio(UpdateBarrioInput barrioUpdate);

        /// <summary>
        /// Retorna los departamentos con el pais asociado con los datos del nombre e indicativo correspondiente
        /// </summary>
        /// <param name="departamento">Cóntiene el identificador del departamento a consultar <see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetDepartamentoWithPaisInput"/></param>
        /// <returns>Retorna un objeto con los datos del departamento y el pais con sus indicativos<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetDepartamentoWithPaisOutput"/></returns>
        GetDepartamentoWithPaisOutput GetDepartamentoWithPais(GetDepartamentoWithPaisInput departamento);

        /// <summary>
        /// Retorna el nombre del tipo de orientacion asociado a la localidad indicada 
        /// </summary>
        /// <param name="torieLocalidad">Cóntiene el identificador del tipo de orientacion asignado a la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetTorieLocalidadInput"/></param>
        /// <returns>Retorna un objeto con el nombre de tipo de orientacion según el id de tipoOrientacionLocalidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetTorieLocalidadOutput"/></returns>
        GetTorieLocalidadOutput GetTorieLocalidad(GetTorieLocalidadInput torieLocalidad);

        /// <summary>
        ///  Retorna una lista de los sufijos registrados en el sistema
        /// </summary>
        /// <returns></returns>
        GetSufijosOutput GetSufijos();

        /// <summary>
        /// Retorna una lista de los sufijos relacionados con una localidad
        /// </summary>
        /// <param name="localidad">Cóntiene el identificador de la localidad a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetSufijosLocalidadByLocalidadInput"/></param>
        /// <returns>Retorna una lista de sufijos asignados a la localidad indicada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetSufijosLocalidadByLocalidadOutput"/></returns>
        GetSufijosLocalidadByLocalidadOutput GetSufijosLocalidadByLocalidad(GetSufijosLocalidadByLocalidadInput localidad);

        /// <summary>
        /// Se encarga de registrar un sufijo en la localidad indicada
        /// </summary>
        /// <param name="sufijoLocalidad">Contiene el código del sufijo y la localidad para ser asignada. <see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveSufijoLocalidadInput"/></param>
        void SaveSufijoLocalidad(SaveSufijoLocalidadInput sufijoLocalidad);

        /// <summary>
        /// Se encarga de registrar un sufijo.
        /// </summary>
        /// <param name="nuevoSufijo">Contiene el nombre del sufijo para registrar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveSufijoInput"/></param>
        void SaveSufijo(SaveSufijoInput nuevoSufijo);

        /// <summary>
        /// Retorna el id y el nombre del sufijo para mostrarlo al eliminar.
        /// </summary>
        /// <param name="sufijolocalidadInput">Cóntiene el identificador del sufijoLocalidad para consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetSufijoLocalidadInput"/></param>
        /// <returns>Retorna un objeto el id y el nombre del sufijo relacionado con la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.SufijoLocalidadWithSufijoOutput"/></returns>
        GetSufijoLocalidadWithSufijoOutput GetSufijoLocalidadWithSufijo(GetSufijoLocalidadWithSufijoInput sufijolocalidadInput);

        /// <summary>
        /// Se encarga de eliminar la asociacón del sufijo con la localidad.
        /// </summary>
        /// <param name="sufijolocalidadInput">Codigo del SufijoLocalidad que se desea eliminar</param>
        void DeleteSufijoLocalidad(DeleteSufijoLocalidadInput sufijolocalidadInput);

        /// <summary>
        /// Se encarga de eliminar el sufijo asociado indicado.
        /// </summary>
        /// <param name="avenidaEliminar">Codigo del sufijo que se desea eliminar</param>
        void DeleteSufijo(DeleteSufijoInput sufijoEliminar);

          /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteSufijoInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="sufijoEliminar">id del sufijo que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarSufijoOutput PuedeEliminarSufijo(PuedeEliminarSufijoInput sufijoEliminar);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteSufijoLocalidadInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="sufijoQuitar">id del sufijolocalidad que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarSufijoLocalidadOutput PuedeEliminarSufijoLocalidad(PuedeEliminarSufijoLocalidadInput sufijoQuitar);

        /// <summary>
        /// Retorna una lista de las avenidas relacionadas con la localidad indicada.
        /// </summary>
        /// <param name="localidadInput">Cóntiene el identificador de la localidad para consultar sus avenidas<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetLocalidadInput"/></param>
        /// <returns>Retorna una lista con las avenidas pertenecientes a la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAvenidasOutput"/></returns>
        GetAvenidasByLocalidadOutput GetAvenidasByLocalidad(GetAvenidasByLocalidadInput localidadInput);

        /// <summary>
        /// Retorna una lista de los barrios relacionadas con la localidad indicada.
        /// </summary>
        /// <param name="localidadInput"></param>
        /// <returns></returns>
        GetBarriosByLocalidadOutput GetBarriosByLocalidad(GetBarriosByLocalidadInput localidadInput);

        /// <summary>
        /// Se encarga de registrar una avenida a la localidad indicada.
        /// </summary>
        /// <param name="nuevoAvenida">Contiene el código de la localidad y el nombre de la avenida para registrar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveAvenidaInput"/></param>
        void SaveAvenida(SaveAvenidaInput nuevoAvenida);

        /// <summary>
        /// Retorna los datos de la avenida para mostrarlos si se va a eliminar / editar
        /// </summary>
        /// <param name="avenidaInput">Cóntiene el identificador de la avenida a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetTorieLocalidadInput"/></param>
        /// <returns>Retorna un objeto con el nombre de la avenida y el id de la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAvenidaOutput"/></returns>
        GetAvenidaOutput GetAvenida(GetAvenidaInput avenidaInput);

        /// <summary>
        /// Se encarga de actualizar la avenida asignada a la localidad indicada
        /// </summary>
        /// <param name="avenidaUpdate">Datos de la avenida e identificador de la localidad para actualizar</param>
        void UpdateAvenida(UpdateAvenidaInput avenidaUpdate);

        /// <summary>
        /// Se encarga de eliminar la avenida asociada con la localidad indicada.
        /// </summary>
        /// <param name="avenidaEliminar">Codigo de la avenida que se desea eliminar</param>
        void DeleteAvenida(DeleteAvenidaInput avenidaEliminar);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteAvenidaLocalidadInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="avenidaEliminar">id de la avenidalocalidad que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarAvenidaOutput PuedeEliminarAvenida(PuedeEliminarAvenidaInput avenidaEliminar);

        /// <summary>
        /// Retorna una lista de los sufijos que no han sido asignados a la localidad indicada
        /// </summary>
        /// <param name="localidad">Cóntiene el identificador de la localidad a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetSufijosLocalidadByLocalidadInput"/></param>
        /// <returns>Retorna una lista de los sufijos que no han sido asignados a la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetSufijosDisponiblesOutput"/></returns>
        GetSufijosSinAsignarALocalidadOutput GetSufijosSinAsignarALocalidad(GetSufijosSinAsignarALocalidadInput localidad);

        /// <summary>
        /// Retorna una lista de los tipos de orientacion que no han sido asignados a la localidad indicada
        /// </summary>
        /// <param name="localidad">Cóntiene el identificador de la localidad a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetToriesLocalidaByLocalidadInput"/></param>
        /// <returns>Retorna una lista de los tipos de orientacion que no han sido asignados a la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetSufijosDisponiblesOutput"/></returns>
        GetTiposOrientacionSinAsignarALocalidadOutput GetTiposOrientacionSinAsignarALocalidad(GetTiposOrientacionSinAsignarALocalidadInput localidad);

        /// <summary>
        /// Retorna una lista de las manzanas que pertenecen al barrio indicado
        /// </summary>
        /// <param name="barrioInput">Cóntiene el identificador del barrio a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetBarrioInput"/></param>
        /// <returns>Retorna una lista de las manzanas que pertenecen al barrio indicado<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetManzanasOutput"/></returns>
        GetManzanasByBarrioOutput GetManzanasByBarrio(GetManzanasByBarrioInput barrioInput);

        /// <summary>
        /// Se encarga de registrar una manzana sin nomenclatura al barrio indicado
        /// </summary>
        /// <param name="nuevoManzana">Contiene el código del barrio y datos sin nomenclatura para registrar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveManzanaSinNomenclaturaInput"/></param>
        void SaveManzanaSinNomenclatura(SaveManzanaSinNomenclaturaInput nuevoManzana);

        /// <summary>
        /// Se encarga de registrar una manzana con nomenclatura al barrio indicado
        /// </summary>
        /// <param name="nuevoManzanaConNomenclatura">Contiene el código de la localidad y el nombre de la avenida para registrar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveManzanaConNomenclaturaInput"/></param>
        void SaveManzanaConNomenclatura(SaveManzanaConNomenclaturaInput nuevoManzanaConNomenclatura);

        /// <summary>
        /// Retorna booleano de si el valor seleccionado en el dropdown via principal es 'Avenida' o no.
        /// </summary>
        /// <param name="torieSeleccion">Cóntiene el identificador del tipo de orientacion localidad para validar si el id de tipo de orientacion es 'Avenida'<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetTorieLocalidadInput"/></param>
        /// <returns>Retorna un true o false de si el valor seleccionado es 'Avenida' o no<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAvenidasLocalidadManzanaOutput"/></returns>
        EsTipoOrientacionAvenidaOutput EsTipoOrientacionAvenida(EsTipoOrientacionAvenidaInput torieSeleccion);

        /// <summary>
        /// Retorna una lista de los tipos de orientacion de la localidad según la selección del dropdown vía principal para cargar los datos validados en el dropdown de vía secundaria.
        /// </summary>
        /// <param name="localidad">Cóntiene el identificador de la localidad del barrio a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetTorieInput"/></param>
        /// <returns>Retorna una lista de los tipos de orientacion de la localidad validados que contiene el barrio<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetToriesLocalidadManzanaOutput"/></returns>
        GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidadOutput GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidad(GetToriesLocalidadDisponiblesWithTipoOrientacionByTorieLocalidadInput localidad);

        /// <summary>
        /// Se encarga de eliminar la manzana indicada en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteManzanaInput"/>
        /// </summary>
        /// <param name="manzanaEliminar">Identificador de la manzana a eliminar</param>
        void DeleteManzana(DeleteManzanaInput manzanaEliminar);

        /// <summary>
        /// Se encarga de consultar la información de direccion 
        /// llega en un objeto de la clase 
        /// </summary>
        /// <param name="direccionInput">Ingresa un objeto de tipo <see cref="Bow.Application.Zonificacion.DTOs.InputModels.GetDireccionInput"/> con el id de la dirección </param>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Application.Zonificacion.DTOs.InputModels.GetDireccionOutput"/> con la información de la dirección </returns>
        GetDireccionOutput GetDireccion(GetDireccionInput direccionInput);

        /// <summary>
        /// Se encarga de registrar una direccion general sin nomenclatura y retorna un objeto con la direccion guardada
        /// </summary>
        /// <param name="nuevoDireccionSinNomenclatura">Contiene los datos de la direccion sin nomenclatura a guardar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveDireccionSinNomenclaturaInput"/></param>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveDireccionSinNomenclaturaOutput"/> con la información de la dirección guardada </returns>
        SaveDireccionSinNomenclaturaOutput SaveDireccionSinNomenclatura(SaveDireccionSinNomenclaturaInput nuevoDireccionSinNomenclatura);

        /// <summary>
        /// Se encarga de registrar una direccion general con nomenclatura y retorna un objeto con la direccion guardada
        /// </summary>
        /// <param name="saveDireccionConNomenclatura">Contiene los datos de la direccion con nomenclatura a guardar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveDireccionConNomenclaturaInput"/></param>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveDireccionConNomenclaturaOutput"/> con la información de la dirección guardada </returns>
        SaveDireccionConNomenclaturaOutput SaveDireccionConNomenclatura(SaveDireccionConNomenclaturaInput saveDireccionConNomenclatura);

        /// <summary>
        /// Se encarga de registrar una direccion de USA sin nomenclatura y retorna un objeto con la direccion guardada
        /// </summary>
        /// <param name="nuevoDireccionSinNomenclaturaUsa">Contiene los datos de la direccion de USA sin nomenclatura a guardar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveDireccionSinNomenclaturaUsaInput"/></param>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveDireccionSinNomenclaturaUsaOutput"/> con la información de la dirección guardada </returns>
        SaveDireccionSinNomenclaturaUsaOutput SaveDireccionSinNomenclaturaUsa(SaveDireccionSinNomenclaturaUsaInput nuevoDireccionSinNomenclaturaUsa);

        /// <summary>
        /// Se encarga de registrar una direccion de USA con nomenclatura y retorna un objeto con la direccion guardada
        /// </summary>
        /// <param name="nuevoDireccionConNomenclaturaUsa">Contiene los datos de la direccion de USA con nomenclatura a guardar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveDireccionConNomenclaturaUsaInput"/></param>
        /// <returns>Retorna un objeto de tipo <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveDireccionConNomenclaturaUsaOutput"/> con la información de la dirección guardada </returns>
        SaveDireccionConNomenclaturaUsaOutput SaveDireccionConNomenclaturaUsa(SaveDireccionConNomenclaturaUsaInput nuevoDireccionConNomenclaturaUsa);

        /// <summary>
        /// Retorna booleano de si el valor seleccionado en el dropdown pais es 'USA' o no.
        /// </summary>
        /// <param name="paisSeleccion">Cóntiene el identificador del pais para validar si el id es 'USA'<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.EsPaisUsaInput"/></param>
        /// <returns>Retorna un true o false si el valor seleccionado es 'USA' o no<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.EsPaisUsaOutput"/></returns>
        EsPaisUsaOutput EsPaisUsa(EsPaisUsaInput paisSeleccion);

        /// <summary>
        /// Retorna una lista de las zonas de la localidad indicada
        /// </summary>
        /// <param name="localidadInput">Cóntiene el identificador de la localidad a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetZonasByLocalidadInput"/></param>
        /// <returns>Retorna una lista con las zonas de la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetZonasByLocalidadOutput"/></returns>
        GetZonasByLocalidadOutput GetZonasByLocalidad(GetZonasByLocalidadInput localidadInput);

        /// <summary>
        /// Retorna la información de la zona indicada
        /// </summary>
        /// <param name="zonaInput">Cóntiene el identificador de la zona a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetZonaInput"/></param>
        /// <returns>Retorna la información de la zona indicada <see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetZonaOutput"/></returns>
        GetZonaOutput GetZona(GetZonaInput zonaInput);

        /// <summary>
        /// Se encarga de actualizar la zona indicada
        /// </summary>
        /// <param name="zonaUpdate">Datos de la zona para actualizar</param>
        void UpdateZona(UpdateZonaInput zonaUpdate);

        /// <summary>
        /// Se encarga de eliminar la zona indicada en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteZonaInput"/>
        /// </summary>
        /// <param name="zonaEliminar">Identificador de la zona para eliminar</param>
        void DeleteZona(DeleteZonaInput zonaEliminar);

        /// <summary>
        /// Retorna una lista de las zonas con el tipo de zona de la localidad indicada
        /// </summary>
        /// <param name="localidadInput">Cóntiene el identificador de la localidad y el tipo a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetZonasByLocalidadAndTipoZonaInput"/></param>
        /// <returns>Retorna una lista con las zonas con el nombre del tipo de la localidad indicada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetZonasByLocalidadAndTipoZonaOutput"/></returns>
        GetZonasByLocalidadAndTipoZonaOutput GetZonasByLocalidadAndTipoZona(GetZonasByLocalidadAndTipoZonaInput localidadInput);

        /// <summary>
        /// Retorna una lista de los barrios asignados a la zona indicada
        /// </summary>
        /// <param name="zonaInput">Cóntiene el identificador de la zona a consultar sus barrios asignados<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetBarriosByZonaInput"/></param>
        /// <returns>Retorna una lista con los barrios de la zona indicada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetBarriosByZonaOutput"/></returns>
        GetBarriosByZonaOutput GetBarriosByZona(GetBarriosByZonaInput zonaInput);

        /// <summary>
        /// Se encarga de eliminar un barrio de la zona indicada en el objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.DeleteZonaBarrioInput"/>
        /// </summary>
        /// <param name="zonabarrioEliminar">Identificador de la zonabarrio para eliminar</param>
        void DeleteZonaBarrio(DeleteZonaBarrioInput zonabarrioEliminar);

        /// <summary>
        /// Retorna una lista de los barrios disponibles para asignar a zona
        /// </summary>
        /// <param name="zonaInput">Cóntiene el identificador de la zona a consultar sus barrios disponibles<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetBarriosByZonaDisponiblesInput"/></param>
        /// <returns>Retorna una lista con los barrios disponibles de la zona indicada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetBarriosByZonaOutput"/></returns>
        GetBarriosByZonaDisponiblesOutput GetBarriosByZonaDisponibles(GetBarriosByZonaDisponiblesInput zonaInput);

        /// <summary>
        /// Se encarga de asignar un barrio a la zona indicada
        /// </summary>
        /// <param name="nuevaZonaBarrio">Contiene el código del barrio y codigo de la zona para asignar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.SaveZonaBarrioInput"/></param>
        void AsignarBarrioZona(SaveZonaBarrioInput nuevaZonaBarrio);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.PuedeEliminarLocalidadInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="localidadEliminar">id de la localidad que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarLocalidadOutput PuedeEliminarLocalidad(PuedeEliminarLocalidadInput localidadEliminar);

        /// <summary>
        /// Se encarga de listar todas las localidades con su respectivo departamento y pais
        /// </summary>
        /// <returns>Retorna la lista de localidades <see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAllLocalidadesOutput"/></returns>
        GetAllLocalidadesOutput GetAllLocalidades();

        /// <summary>
        ///     Se encarga de listar todas las localidades con su respectivo departamento y pais sin las localidades de la lista que ingresa como parametro
        /// </summary>
        /// <param name="listaLocalidadesAQuitar">Lista de localidades que no deben aparecer en la lista que se retorna</param>
        /// <returns>Retorna la lista de localidades <see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAllLocalidadesWithFilterOutput"/> </returns>
        GetAllLocalidadesWithFilterOutput GetAllLocalidadesWithFilter(GetAllLocalidadesWithFilterInput listaLocalidadesARemover);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveTelefonoInput"/>
        /// para guardarlo
        /// </summary>
        SaveTelefonoOutput SaveTelefono(SaveTelefonoInput nuevoTelefono);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveTelefonoInput"/>
        /// para guardarlo
        /// </summary>
        ValidarTelefonoOutput validarTelefono(ValidarTelefonoInput validarTelefonoInput);

        /// <summary>
        ///     Se encarga de listar todas las localidades con su respectivo departamento y pais de un pais específico sin las localidades de la lista que ingresa como parametro
        /// </summary>
        /// <param name="listaLocalidadesAQuitar">Lista de localidades que no deben aparecer en la lista que se retorna</param>
        /// <returns>Retorna la lista de localidades <see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAllLocalidadesByPaisWithFilterOutput"/> </returns>
        GetAllLocalidadesByPaisWithFilterOutput GetAllLocalidadesByPaisWithFilter(GetAllLocalidadesByPaisWithFilterInput listaLocalidadesAQuitar);

        /// <summary>
        ///     Se encarga de listar todas las localidades con su respectivo departamento y pais de un departamento específico sin las localidades de la lista que ingresa como parametro
        /// </summary>
        /// <param name="listaLocalidadesAQuitar">Lista de localidades que no deben aparecer en la lista que se retorna</param>
        /// <returns>Retorna la lista de localidades <see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetAllLocalidadesByDepartamentoWithFilterOutput"/> </returns>
        GetAllLocalidadesByDepartamentoWithFilterOutput GetAllLocalidadesByDepartamentoWithFilter(GetAllLocalidadesByDepartamentoWithFilterInput listaLocalidadesAQuitar);

        /// <summary>
        /// Retorna el telefono con la localidad del id del telefono indicado
        /// </summary>
        /// <param name="telefonoInput">Cóntiene el identificador del telefono a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetTelefonoInput"/></param>
        /// <returns>Retorna un telefono con el nombre de la localidad<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetTelefonoOutput"/></returns>
        GetTelefonoOutput GetTelefono(GetTelefonoInput telefonoInput);

        /// <summary>
        /// Retorna los empleados activos por zona
        /// </summary>
        /// <param name="zonaInput">Cóntiene el identificador de la zona a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetEmpleadosByZonaInput"/></param>
        /// <returns>Retorna una lista de empleados activos por la zona indicada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetEmpleadosByZonaOutput"/></returns>
        GetEmpleadosActivosByZonaOutput GetEmpleadosActivosByZona(GetEmpleadosActivosByZonaInput zonaInput);

        /// <summary>
        /// Retorna los empleados inactivos por zona
        /// </summary>
        /// <param name="zonaInput">Cóntiene el identificador de la zona a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetEmpleadosInactivosByZonaInput"/></param>
        /// <returns>Retorna una lista de empleados inactivos por la zona indicada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetEmpleadosInactivosByZonaOutput"/></returns>
        GetEmpleadosInactivosByZonaOutput GetEmpleadosInactivosByZona(GetEmpleadosInactivosByZonaInput zonaInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.SaveZonaEmpleadoInput"/> con la información de la zona empleado
        /// para almacenarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void SaveZonaEmpleado(SaveZonaEmpleadoInput zonaEmpleadoInput);

        /// <summary>
        /// Retorna la informacion de la zona empleado indicado
        /// </summary>
        /// <param name="zonaempleadoInput">Cóntiene el identificador de la zona empleado a consultar<see cref="Bow.Applicattion.Zonificacion.DTOs.InputModels.GetZonaEmpleadoInput"/></param>
        /// <returns>Retorna la información de la zona empleado consultada<see cref="Bow.Applicattion.Zonificacion.DTOs.OutputModels.GetZonaEmpleadoOutput"/></returns>
        GetZonaEmpleadoOutput GetZonaEmpleado(GetZonaEmpleadoInput zonaempleadoInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.UpdateZonaEmpleadoInput"/> con la información de la zona empleado
        /// para actualizar en la base de datos
        /// </summary>
        /// <returns></returns>
        void UpdateZonaEmpleado(UpdateZonaEmpleadoInput zonaEmpleadoInput);

        /// <summary>
        /// Se encarga de recibir un objeto <see cref="Bow.Application.Zonificacion.DTOs.InputModels.PuedeEliminarZonaInput"/>
        /// para saber si este se puede eliminar o no
        /// </summary>
        /// <param name="zonaEliminar">id de la zona que deseamos eliminar</param>
        /// <returns></returns>
        PuedeEliminarZonaOutput PuedeEliminarZona(PuedeEliminarZonaInput zonaEliminar);
     

        string PruebaJsonAuditoria();
    }
}


