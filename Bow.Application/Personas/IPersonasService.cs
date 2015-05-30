using Abp.Application.Services;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Personas.DTOs.InputModels;
using Bow.Personas.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Personas
{
    public interface IPersonasService : IApplicationService
    {
        void ProbarFechas(ProbarFechasInput probarFechasInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nuevaPreferencia"></param>
        void SavePreferencia(SavePreferenciaInput nuevaPreferencia);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.SavePersonaInput"/> con la información de la persona
        /// para almacenarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        SavePersonaOutput SavePersona(SavePersonaInput nuevaPersona);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTipoDocumentoInput"/> con el id del tipo de documento solicitado
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.GetTipoDocumentoOutput"/> con la información del tipo de documento</returns>
        GetTipoDocumentoOutput GetTipoDocumento(GetTipoDocumentoInput tiposDocumentosInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTiposDocumentoInput"/> con el id del pais
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetTiposDocumentoOutput"/> con la información del tipo de documento</returns>
        GetTiposDocumentoOutput GetTiposDocumento(GetTiposDocumentoInput tiposDocumentoInput);

        /// <summary>
        ///     Se encarga de listar todos los tipos de documento
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetAllTiposDocumentoWithPaisOutput"/> con el listado de tipos de documento</returns>
        GetAllTiposDocumentoWithPaisOutput GetAllTiposDocumentoWithPais();

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.SaveTipoDocumentoInput"/> con la información del tipo de documento
        /// para almacenarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void SaveTipoDocumento(SaveTipoDocumentoInput nuevoTipoDocumento);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.DeleteTipoDocumentoInput"/> con el id del tipo de documento
        /// para eliminarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void DeleteTipoDocumento(DeleteTipoDocumentoInput tipoDocumentoEliminar);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.UpdateTipoDocumentoInput"/> con la información del tipo de documento
        /// para modificarlo en la base de datos
        /// </summary>
        /// <returns></returns>
        void UpdateTipoDocumento(UpdateTipoDocumentoInput tipoDocumentoUpdate);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.PuedeEliminarTipoDocumentoInput"/> con el id del tipo de documento
        /// para eliminarlo en la base de datos
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.PuedeEliminarTipoDocumentoOutput"/> con la respuesta si es posible eliminar el tipo de documento</returns>
        PuedeEliminarTipoDocumentoOutput PuedeEliminarTipoDocumento(PuedeEliminarTipoDocumentoInput tipoDocumentoEliminar);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTiposDocumentoPersonaInput"/> con el id del pais para obtener los tipos de documentos relacionados
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.GetTiposDocumentoPersonaOutput"/> con la información de lo tipos de documentos</returns>
        GetTiposDocumentoPersonaOutput GetTiposDocumentoPersona(GetTiposDocumentoPersonaInput paisInput);

        /// <summary>
        ///     Se encarga de obtener el id del tipo de documento por defecto de un pais
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTipoDocumentoPorDefectoInput"/>
        ///     con el id del pais
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTipoDocumentoPorDefectoOutput"/> con el id del tipo de documento por defecto
        /// </return>
        GetTipoDocumentoPorDefectoOutput GetTipoDocumentoPorDefecto(GetTipoDocumentoPorDefectoInput paisInput);

        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.FechaExpedicionOutput"/> con la fecha de expedición del documento minima y maxima permitida</returns>
        FechaExpedicionOutput FechaExpedicionDocumento();

        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.FechaNacimientoOutput"/> con la fecha de nacimiento minima y maxima permitida para la persona</returns>
        FechaNacimientoOutput FechaNacimiento();

        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.FechaFallecimientoOutput"/> con la fecha de fallecimiento minima</returns>
        FechaFallecimientoOutput FechaFallecimiento(FechaFallecimientoInput personaInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nuevaOpcionPreferencia"></param>
        /// <param name="preferenciaInput"></param>
        void SaveOpcionPreferencia(SaveOpcionPreferenciaInput nuevaOpcionPreferencia);

        /// <summary>
        /// Retorna una  lista de todas las preferencias que estan registradas en el sistema
        /// </summary>
        /// <returns></returns>
        GetPreferenciasOutput GetPreferenciasWithOpcionesPreferencia();

        GetPreferenciasWithEstadoBoolOutput GetPreferenciasWithEstadoBool();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenciaInput"></param>
        /// <returns></returns>
        GetPreferenciaOutput GetPreferencia(GetPreferenciaInput preferenciaInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcionPreferenciaInput"></param>
        /// <returns></returns>
        GetOpcionPreferenciaOutput GetOpcionPreferencia(GetOpcionPreferenciaInput opcionPreferenciaInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenciaInput"></param>
        /// <returns></returns>
        GetOpcionesPreferenciaOutput GetOpcionesPreferenciaByPreferencia(GetOpcionesPreferenciasInput preferenciaInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenciaUpdate"></param>
        void UpdatePreferencia(UpdatePreferenciaInput preferenciaUpdate);

        void UpdatePreferenciaWithEstadoBool(UpdatePreferenciaWithEstadoBoolInput preferenciaUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcionPreferenciaUpdate"></param>
        void UpdateOpcionPreferencia(UpdateOpcionPreferenciaInput opcionPreferenciaUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenciaEliminar"></param>
        /// <returns></returns>
        PuedeEliminarPreferenciaOutput PuedeEliminarPreferencia(PuedeEliminarPreferenciaInput preferenciaEliminar);

        /// <summary>
        /// Retorna Verdadero si la opción se puede eliminar porque no está registrada en una persona, Falso en caso contrario
        /// </summary>
        /// <param name="opcionEliminar"></param>
        /// <returns></returns>
        PuedeEliminarOpcionPreferenciaOutput puedeEliminarOpcionPreferencia(PuedeEliminarOpcionPreferenciaInput opcionEliminar);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferenciaEliminar"></param>
        void DeletePreferencia(DeletePreferenciaInput preferenciaEliminar);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opcionPreferenciaEliminar"></param>
        void DeleteOpcionPreferencia(DeleteOpcionPreferenciaInput opcionPreferenciaEliminar);

        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetPersonaOutput"/> con los datos de la persona consultada si existe en el sistema o no</returns>
        GetPersonasOutput GetPersonas(GetPersonasInput personaInput);

        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetPersonaEditarOutput"/> con los datos de la persona para editar</returns>
        GetPersonaEditarOutput GetPersonaEditar(GetPersonaEditarInput personaInput);

        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetTelefonosPersonaOutput"/> con el id de la persona para obtener los telefonos asociados</returns>
        GetTelefonosPersonaOutput GetTelefonosPersona(GetTelefonosPersonaInput personaInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.SavePersonaTelefonoInput"/> con la información de los telefonos de la persona
        /// para almacenarlo en la base de datos
        /// </summary>
        void SavePersonaTelefono(SavePersonaTelefonoInput personatelefonoInput);

        /// <summary>
        ///     Se encarga de consultar los tipos de documento para la empresa organización validando si es naturaleza jurídica o natural
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTiposDocumentoOrganizacionInput"/>
        ///     con el id del pais y el id de la naturaleza de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetTiposDocumentoOrganizacionOutput"/> con el listado de los tipos de documento
        /// </return>
        GetTiposDocumentoOrganizacionOutput GetTiposDocumentoOrganizacion(GetTiposDocumentoOrganizacionInput tiposDocumentoOrganizacionInput);

        /// <summary>
        ///     Se encarga de validar si un documento es válido según los parámetros del tipo de documento
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.validarTipoDocumentoInput"/>
        ///     con el id del tipo de documento y el número de documento
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.validarTipoDocumentoOutput"/> con la respuesta si es válido o no y un mensaje en caso de que no se válido
        /// </return>
        ValidarTipoDocumentoOutput ValidarTipoDocumentoPersona(ValidarTipoDocumentoInput datosDocumento);

        /// <summary>
        ///     Se encarga de validar si un documento es válido según los parámetros del tipo de documento, además si está en el rango de edad
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.validarTipoDocumentoConEdadInput"/>
        ///     con el id del tipo de documento, el número de documento y la edad
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.validarTipoDocumentoConEdadOutput"/> con la respuesta si es válido o no y un mensaje en caso de que no se válido
        /// </return>
        ValidarTipoDocumentoConEdadOutput ValidarTipoDocumentoPersonaConEdad(ValidarTipoDocumentoConEdadInput datosDocumento);

        /// <summary>
        ///     Se encarga de validar si un documento ya existe para una persona
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.validarDocumentoExisteInput"/>
        ///     con el número de documento
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.validarDocumentoExisteOutput"/> con la respuesta si existe o no
        /// </return>
        ValidarDocumentoExisteOutput ValidarDocumentoExiste(ValidarDocumentoExisteInput datosDocumento);


        /// <summary>
        ///     Se encarga de consultar las direcciones de una persona
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetDireccionesPersonaInput"/>
        ///     con el id de la persona a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetDireccionesPersonaOutput"/> con el listado de las direcciones de la persona
        /// </return>
        GetDireccionesPersonaOutput GetDireccionesPersona(GetDireccionesPersonaInput personaInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.SavePersonaDireccionInput"/> con la información de las direcciones de la persona
        /// para almacenarlo en la base de datos
        /// </summary>
        void SavePersonaDireccion(SavePersonaDireccionInput personadireccionInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetPersonaContactoWebInput"/> con el id de la persona para consultar sus medios de contacto asociadas
        /// </summary>
        /// <returns>Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetPersonaContactoWebOutput"/> con los medios de contacto asociados a la persona indicada</returns>
        GetPersonaContactoWebOutput GetPersonaContactoWeb(GetPersonaContactoWebInput personaInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.SavePersonaContactoWebInput"/> con la lista de personaContactoWeb
        /// para agregarlos en la base de datos
        /// </summary>        
        void SavePersonaContactoWeb(SavePersonaContactoWebInput personacontactowebInput);

        /// <summary>
        ///     Se encarga de consultar los medios de contacto de la persona indicada
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetContactosWebFilterByPersonaInput"/>
        ///     con el id de la persona a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetContactosWebFilterByPersonaOutput"/> con el listado de los medios de contacto web de la persona
        /// </return>
        GetContactosWebFilterByPersonaOutput GetContactosWebFilterByPersona(GetContactosWebFilterByPersonaInput personaInput);

        /// <summary>
        ///     Se encarga de consultar las preferencias con las opciones de la preferencia
        /// </summary>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetPreferenciaPersonaOutput"/> con el listado de las preferencias y sus opciones de preferencia
        /// </return>
        GetPreferenciaPersonaOutput GetPreferenciaPersona();

        /// <summary>
        ///     Se encarga de consultar las opciones de preferencia de una persona
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetOpcionPreferenciaPersonaInput"/>
        ///     con el id de la persona a consultar
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetOpcionPreferenciaPersonaOutput"/> con el listado de las opciones de preferencia de la persona indicada
        /// </return>
        GetOpcionPreferenciaPersonaOutput GetOpcionPreferenciaPersona(GetOpcionPreferenciaPersonaInput personaInput);

        /// <summary>
        /// Recibe un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.SavePersonaPreferenciaInput"/> con la lista de las opciones de las preferencias de la persona
        /// para agregarlos en la base de datos
        /// </summary>
        /// <returns></returns>
        void SavePersonaPreferencia(SavePersonaPreferenciaInput preferenciasPersona);

        /// <summary>
        ///     Se encarga de consultar la persona con sus telefonos a partir del número de documento
        /// </summary>
        /// <param name="personaDocumentoInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetPersonaWithTelefonoInput"/> con el documento de la persona
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutModels.GetPersonaWithTelefonoOutput"/> con la información de la persona
        /// </returns>
        GetPersonaWithTelefonoOutput GetPersonaWithTelefono(GetPersonaWithTelefonoInput personaDocumentoInput);

        /// <summary>
        ///     Se encarga de consultar la auditoria de la persona indicada
        /// </summary>
        /// <param name="personaInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetAuditoriaPersonaInput"/> con el id de la persona para consultar
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetAuditoriaPersonaOutput"/> con la información de la auditoria de la persona
        /// </returns>
        GetAuditoriaPersonaOutput GetAuditoriaPersona(GetAuditoriaPersonaInput personaInput);

        /// <summary>
        ///     Se encarga de consultar la fecha maxima y minima para el datapicker fechaAsignacion en ZonaEmpleado
        /// </summary>
        /// <param name="personaInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.FechaAsignacionInput"/> con el id de la persona para consultar su fecha de ingreso
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.FechaAsignacionOutput"/> con la información del rango de fecha disponible en el datapicker
        /// </returns>
        FechaAsignacionOutput FechaAsignacion(FechaAsignacionInput personaInput);

        /// <summary>
        ///     Se encarga de consultar la informacion de la persona indicada
        /// </summary>
        /// <param name="personaInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetPersonaInput"/> con el número del documento de la persona para consultar
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetPersonaOutput"/> con la información de la persona
        /// </returns>
        GetPersonaOutput GetPersona(GetPersonaInput personaInput);

         /// <summary>
        ///     Se encarga de hacer un filtro de las personas segun parámetros de entrada
        /// </summary>
        /// <param name="datosInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.GetBuscadorPersonaInput"/> con la información por las cuales quiere filtrar
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.GetBuscadorPersonaOutput"/> con la lista de las personas que cumplen los datos del filtro
        /// </returns>
        GetBuscadorPersonaOutput GetBuscadorPersona(GetBuscadorPersonaInput datosInput);

        /// <summary>
        ///     Se encarga de guardar una persona prospecto
        /// </summary>
        /// <param name="datosInput">
        ///     Ingresa un objeto <see cref="Bow.Application.Personas.DTOs.InputModels.SavePersonaProspectoInput"/> con la información de la persona prospecto con los datos básicos requeriods
        /// </param>
        /// <returns>
        ///     Retorna un objeto <see cref="Bow.Application.Personas.DTOs.OutputModels.SavePersonaProspectoOutput"/> con la información de la persona con que se almaceno
        /// </returns>
        SavePersonaProspectoOutput SavePersonaProspecto(SavePersonaProspectoInput nuevaPersonaProspecto);


        GetBuscadorPersonaGrupoInformalOutput GetBuscadorPersonaGrupoInformal(GetBuscadorPersonaGrupoInformalInput datosInput);
    }
}
