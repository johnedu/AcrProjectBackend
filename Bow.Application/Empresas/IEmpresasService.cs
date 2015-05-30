using Abp.Application.Services;
using Bow.Empresas.DTOs.InputModels;
using Bow.Empresas.DTOs.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas
{
    public interface IEmpresasService : IApplicationService
    {
        /// <summary>
        /// Se encarga de obtener la información tributaria a partir de un Id
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetInfoTributariaInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetInfoTributariaOutput"/>
        /// </return>
        GetInfoTributariaOutput GetInfoTributaria(GetInfoTributariaInput infoTributariaInput);

        /// <summary>
        /// Se encarga de obtener el listado completo de información tributaria
        /// </summary>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetInfoTributariasOutput"/>
        /// </return>
        GetInfoTributariasOutput GetInfoTributarias();

        /// <summary>
        /// Se encarga de almacenar la información tributaria en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveInfoTributariaInput"/>
        /// </param>
        void SaveInfoTributaria(SaveInfoTributariaInput nuevaInfoTributaria);

        /// <summary>
        /// Se encarga de eliminar un registro de información tributaria en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.DeleteInfoTributariaInput"/>
        /// </param>
        void DeleteInfoTributaria(DeleteInfoTributariaInput infoTributariaEliminar);

        /// <summary>
        /// Se encarga de actualizar un registro de información tributaria en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateInfoTributariaInput"/>
        /// </param>
        void UpdateInfoTributaria(UpdateInfoTributariaInput infoTributariaUpdate);

        /// <summary>
        /// Se encarga de validar si es posible eliminar un registro de información tributaria
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.PuedeEliminarInfoTributariaInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.PuedeEliminarInfoTributariaOutput"/>
        /// </return>
        PuedeEliminarInfoTributariaOutput PuedeEliminarInfoTributaria(PuedeEliminarInfoTributariaInput infoTributariaEliminar);

        /// <summary>
        ///     Se encarga de obtener la opción de información tributaria a partir de un Id
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetInfoTributariaOpcionInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetInfoTributariaOpcionOutput"/>
        /// </return>
        GetInfoTributariaOpcionOutput GetInfoTributariaOpcion(GetInfoTributariaOpcionInput infoOpcionesTributariaInput);

        /// <summary>
        ///     Se encarga de obtener el listado completo de opciones de información tributaria
        /// </summary>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetInfoTributariaOpcionesOutput"/>
        /// </return>
        GetInfoTributariaOpcionesOutput GetInfoTributariaOpciones(GetInfoTributariaOpcionesInput infoOpcionesTributariaInput);

        /// <summary>
        ///     Se encarga de almacenar la opción de información tributaria en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveInfoTributariaOpcionInput"/>
        /// </param>
        void SaveInfoTributariaOpcion(SaveInfoTributariaOpcionInput nuevaInfoTributariaOpcion);

        /// <summary>
        ///     Se encarga de eliminar un registro de opción de información tributaria en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.DeleteInfoTributariaOpcionInput"/>
        /// </param>
        void DeleteInfoTributariaOpcion(DeleteInfoTributariaOpcionInput infoTributariaOpcionEliminar);

        /// <summary>
        ///     Se encarga de actualizar un registro de opción de información tributaria en la base de datos
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateInfoTributariaOpcionInput"/>
        /// </param>
        void UpdateInfoTributariaOpcion(UpdateInfoTributariaOpcionInput infoTributariaOpcionUpdate);

        /// <summary>
        ///     Se encarga de validar si es posible eliminar un registro de opción de información tributaria
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.PuedeEliminarInfoTributariaOpcionInput"/>
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.PuedeEliminarInfoTributariaOpcionOutput"/>
        /// </return>
        PuedeEliminarInfoTributariaOpcionOutput PuedeEliminarInfoTributariaOpcion(PuedeEliminarInfoTributariaOpcionInput infoTributariaOpcionEliminar);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GetActividadesEconomicasOutput GetActividadesEconomicas();
    
        /// <summary>
        ///     Se encarga de consultar el listado de localidades a partir del codigo de una información tributaria
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllLocalidadesByInfoTributariaInput"/> con el código de información tributaria
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllLocalidadesByInfoTributariaOutput"/> con el listado de localidades
        /// </return>
        GetAllLocalidadesByInfoTributariaOutput GetAllLocalidadesByInfoTributaria(GetAllLocalidadesByInfoTributariaInput localidadInput);

        /// <summary>
        ///     Se encarga de consultar el listado de localidades a partir del codigo de una información tributaria y el código de un pais
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllLocalidadesByInfoTributariaAndPaisInput"/>
        ///     con el código de información tributaria y el código del pais
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllLocalidadesByInfoTributariaAndPaisOutput"/> con el listado de localidades
        /// </return>
        GetAllLocalidadesByInfoTributariaAndPaisOutput GetAllLocalidadesByInfoTributariaAndPais(GetAllLocalidadesByInfoTributariaAndPaisInput infoTributariaAndPaisInput);

        /// <summary>
        ///     Se encarga de consultar el listado de localidades a partir del codigo de una información tributaria y el código de un departamento
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllLocalidadesByInfoTributariaAndDepartamentoInput"/>
        ///     con el código de información tributaria y el código del departamento
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllLocalidadesByInfoTributariaAndDepartamentoOutput"/> con el listado de localidades
        /// </return>
        GetAllLocalidadesByInfoTributariaAndDepartamentoOutput GetAllLocalidadesByInfoTributariaAndDepartamento(GetAllLocalidadesByInfoTributariaAndDepartamentoInput infoTributariaAndPaisAndDepartamentoInput);

        /// <summary>
        ///     Se encarga de consultar el listado de localidades sin incluir las localidades que pertenecen a una información tributaria
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllLocalidadesByNotInfoTributariaInput"/>
        ///     con el código de información tributaria
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllLocalidadesByNotInfoTributariaOutput"/> con el listado de localidades
        /// </return>
        GetAllLocalidadesByNotInfoTributariaOutput GetAllLocalidadesByNotInfoTributaria(GetAllLocalidadesByNotInfoTributariaInput localidadesByNotInfoTributaria);

        /// <summary>
        ///     Se encarga de asociar todas las localidades de un pais a una información tributaria a partir del código de info tributaria y el código del pais
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveInfoTributariaLocalidadByPaisInput"/>
        ///     con el código de información tributaria y el código del pais
        /// </param>
        /// <return></return>
        void SaveInfoTributariaLocalidadByPais(SaveInfoTributariaLocalidadByPaisInput asignarInfoTributariaLocalidadPorPais);

        /// <summary>
        ///     Se encarga de asociar todas las localidades de un departamento a una información tributaria a partir del código de info tributaria y el código del departamento
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveInfoTributariaLocalidadByDepartamentoInput"/>
        ///     con el código de información tributaria y el código del departamento
        /// </param>
        /// <return></return>
        void SaveInfoTributariaLocalidadByDepartamento(SaveInfoTributariaLocalidadByDepartamentoInput asignarInfoTributariaLocalidadPorDepartamento);

        /// <summary>
        ///     Se encarga de asociar la localidad a una información tributaria a partir del código de info tributaria y el código de la localidad
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveInfoTributariaLocalidadnput"/>
        ///     con el código de información tributaria y el código del departamento
        /// </param>
        /// <return></return>
        void SaveInfoTributariaLocalidad(SaveInfoTributariaLocalidadnput asignarInfoTributariaLocalidad);

        /// <summary>
        ///     Se encarga de eliminar las localidades de un pais asociadas a una información tributaria a partir del código de info tributaria y el código del pais
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.DeleteInfoTributariaLocalidadByPaisInput"/>
        ///     con el código de información tributaria y el código del pais
        /// </param>
        /// <return></return>
        void DeleteInfoTributariaLocalidadByPais(DeleteInfoTributariaLocalidadByPaisInput eliminarInfoTributariaLocalidadPorPais);

        /// <summary>
        ///     Se encarga de eliminar las localidades de un departamento asociadas a una información tributaria a partir del código de info tributaria y el código del departamento
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.DeleteInfoTributariaLocalidadByDepartamentoInput"/>
        ///     con el código de información tributaria y el código del departamento
        /// </param>
        /// <return></return>
        void DeleteInfoTributariaLocalidadByDepartamento(DeleteInfoTributariaLocalidadByDepartamentoInput eliminarInfoTributariaLocalidadPorDepartamento);

        /// <summary>
        ///     Se encarga de eliminar la localidad asociada a una información tributaria a partir del código de info tributaria y el código de la localidad
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.DeleteInfoTributariaLocalidadInput"/>
        ///     con el código de información tributaria y el código de la localidad
        /// </param>
        /// <return></return>
        void DeleteInfoTributariaLocalidad(DeleteInfoTributariaLocalidadInput eliminarInfoTributariaLocalidad);

        /// <summary>
        ///     Se encarga de consultar el listado de paises a partir de las localidades asociadas a una información tributaria
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllPaisesByInfoTributariaInput"/>
        ///     con el código de información tributaria
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllPaisesByInfoTributariaOutput"/> con el listado de paises
        /// </return>
        GetAllPaisesByInfoTributariaOutput GetAllPaisesByInfoTributaria(GetAllPaisesByInfoTributariaInput infoTributariaInput);

        /// <summary>
        ///     Se encarga de consultar el listado de departamentos a partir de las localidades asociadas a una información tributaria
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllDepartamentosByInfoTributariaInput"/>
        ///     con el código de información tributaria
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllDepartamentosByInfoTributariaOutput"/> con el listado de departamentos
        /// </return>
        GetAllDepartamentosByInfoTributariaOutput GetAllDepartamentosByInfoTributaria(GetAllDepartamentosByInfoTributariaInput infoTributariaInput);

        /// <summary>
        ///     Se encarga de consultar la información de la Organización por el id
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetOrganizacionInput"/>
        ///     con el id de la Organización
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetOrganizacionOutput"/> con la información de la organización
        /// </return>
        GetOrganizacionOutput GetOrganizacion(GetOrganizacionInput organizacionInput);

        /// <summary>
        ///     Se encarga de actualizar la información de la Organización
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateOrganizacionInput"/>
        ///     con la información de la Organización
        /// </param>
        /// <return></return>
        void UpdateOrganizacion(UpdateOrganizacionInput organizacionUpdate);

        /// <summary>
        ///     Se encarga de consultar el listado de empresas de la una organización
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllEmpresasByOrganizacionInput"/>
        ///     con el id de la Organización
        /// </param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllEmpresasByOrganizacionOutput"/> con la información de la empresa
        /// </return>
        GetAllEmpresasByOrganizacionOutput GetAllEmpresasByOrganizacion(GetAllEmpresasByOrganizacionInput organizacionInput);

        /// <summary>
        ///     Se encarga de consultar el listado de empresas de la una organización
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllEmpresasWithSucursalesByOrganizacionInput"/>
        ///     con el id de la Organización
        /// </param>
        /// <return>
        ///     Retorna una lista de objetos <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllEmpresasWithSucursalesByOrganizacionOutput"/> con la información de la empresa
        /// </return>
        GetAllEmpresasWithSucursalesByOrganizacionOutput GetAllEmpresasWithSucursalesByOrganizacion(GetAllEmpresasWithSucursalesByOrganizacionInput organizacionInput);

        /// <summary>
        ///     Se encarga de consultar la información de una empresa por el ID
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetEmpresaInput"/>
        ///     con el id de la Empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetEmpresaOutput"/> con la información de la empresa
        /// </return>
        GetEmpresaOutput GetEmpresa(GetEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de almacenar la información de la Empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveEmpresaInput"/>
        ///     con la información de la Empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.SaveEmpresaOutput"/> con el id de la empresa almacenada
        /// </return>
        SaveEmpresaOutput SaveEmpresa(SaveEmpresaInput nuevaEmpresa);

        /// <summary>
        ///     Se encarga de actualizar la información de la Empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaInput"/>
        ///     con la información de la Empresa
        /// </param>
        /// <return></return>
        void UpdateEmpresa(UpdateEmpresaInput empresaUpdate);

        /// <summary>
        ///     Se encarga de almacenar la información de la Empresa y Organización
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveEmpresaOrganizacionInput"/>
        ///     con la información de la Empresa y la Organización
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.SaveEmpresaOrganizacionOutput"/> con el id de la organización
        /// </return>
        SaveEmpresaOrganizacionOutput SaveEmpresaOrganizacion(SaveEmpresaOrganizacionInput empresaOrganizacionSave);

        /// <summary>
        ///     Se encarga de actualizar la información de la Empresa y Organización
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaOrganizacionInput"/>
        ///     con la información de la Empresa y la Organización
        /// </param>
        /// <return></return>
        void UpdateEmpresaOrganizacion(UpdateEmpresaOrganizacionInput empresaOrganizacionUpdate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nuevaActividadEconomica"></param>
        void SaveActividadEconomica(SaveActividadEconomicaInput nuevaActividadEconomica);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actividadEconomicaInput"></param>
        /// <returns></returns>
        GetActividadEconomicaOutput GetActividadEconomica(GetActividadEconomicaInput actividadEconomicaInput);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actividadEconomicaUpdate"></param>
        void UpdateActividadEconomica(UpdateActividadEconomicaInput actividadEconomicaUpdate);

        /// <summary>
        ///     Se encarga de obtener la información de la Organización con la información de la Empresa asociada
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetEmpresaOrganizacionInput"/>
        ///     con el id de la Organización y de la Empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetEmpresaOrganizacionOutput"/> con la información de la organización y de la empresa
        /// </return>
        GetEmpresaOrganizacionOutput GetEmpresaOrganizacion(GetEmpresaOrganizacionInput empresaOrganizacionInput);

        /// <summary>
        ///     Se encarga de consultar el listado de localidades con departamento y pais sin incluir las localidades que pertenecen a una información tributaria y un pais
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllLocalidadesByNotInfoTributariaPaisInput"/>
        ///     con el id de información tributaria y el id del pais
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllLocalidadesByNotInfoTributariaPaisOutput"/> con el listado de localidades con el departamento y el pais
        /// </return>
        GetAllLocalidadesByNotInfoTributariaPaisOutput GetAllLocalidadesByNotInfoTributariaPais(GetAllLocalidadesByNotInfoTributariaPaisInput infoTributariaAndPais);

        /// <summary>
        ///     Se encarga de consultar el listado de localidades con departamento y pais sin incluir las localidades que pertenecen a una información tributaria y un departamento
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllLocalidadesByNotInfoTributariaDepartamentoInput"/>
        ///     con el id de información tributaria y el id del departamento
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllLocalidadesByNotInfoTributariaDepartamentoOutput"/> con el listado de localidades con el departamento y el pais
        /// </return>
        GetAllLocalidadesByNotInfoTributariaDepartamentoOutput GetAllLocalidadesByNotInfoTributariaDepartamento(GetAllLocalidadesByNotInfoTributariaDepartamentoInput infoTributariaAndDepartamento);

        /// <summary>
        ///     Se encarga de consultar el listado de telefonos con localidad de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllTelefonosEmpresaInput"/>
        ///     con el id de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllTelefonosEmpresaOutput"/> con el listado de teléfonos
        /// </return>
        GetAllTelefonosEmpresaOutput GetAllTelefonosEmpresa(GetAllTelefonosEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de actualizar el listado de telefonos de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaTelefonoInput"/>
        ///     con el id de la empresa y el listado de teléfonos a actualizar
        /// </param>
        /// <return></return>
        void UpdateEmpresaTelefono(UpdateEmpresaTelefonoInput empresaTelefonoUpdate);

        /// <summary>
        ///     Se encarga de obtener el listado de contactos web de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetContactosWebEmpresaInput"/>
        ///     con el id de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetContactosWebEmpresaOutput"/> con la información del contacto web
        /// </return>
        GetContactosWebEmpresaOutput GetContactosWebEmpresa(GetContactosWebEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de obtener el listado total de contactos web sin incluir los de una empresa 
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetContactosWebFilterByEmpresaInput"/>
        ///     con el id de la empresa
        /// </param>
        /// <return>
        ///     Retorna una lista de <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetContactosWebFilterByEmpresaOutput"/> con la información del contacto web
        /// </return>
        GetContactosWebFilterByEmpresaOutput GetContactosWebFilterByEmpresa(GetContactosWebFilterByEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de actualizar el listado de contactos web de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaContactosWebInput"/>
        ///     con el id de la empresa y el listado de contactos web a actualizar
        /// </param>
        /// <return></return>
        void UpdateEmpresaContactosWeb(UpdateEmpresaContactosWebInput empresaContactoWebUpdate);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actividadEliminar"></param>
        void DeleteActividadEconomica(DeleteActividadEconomicaInput actividadEliminar);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="actividadEliminar"></param>
        /// <returns></returns>
        PuedeEliminarActividadEconomicaOutput PuedeEliminarActividadEconomica(PuedeEliminarActividadEconomicaInput actividadEliminar);

        /// <summary>
        ///     Se encarga de obtener el listado de contactos de una empresa 
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetContactosEmpresaInput"/>
        ///     con el id de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetContactosEmpresaOutput"/> con la lista de contactos
        /// </return>
        GetContactosEmpresaOutput GetContactosEmpresa(GetContactosEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de obtener el listado de tipos de área de una empresa sin incluir los que ya tiene asignados
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetTiposAreaFilterByEmpresaInput"/>
        ///     con el id de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objecto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetTiposAreaFilterByEmpresaOutput"/> con la lista de tipos de área
        /// </return>
        GetTiposAreaFilterByEmpresaOutput GetTiposAreaFilterByEmpresa(GetTiposAreaFilterByEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de actualizar el listado de contactos de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaContactosInput"/>
        ///     con el id de la empresa y el listado de contactos a actualizar
        /// </param>
        /// <return></return>
        void UpdateEmpresaContactos(UpdateEmpresaContactosInput empresaContactoUpdate);

        /// <summary>
        ///     Se encarga de obtener el listado de información tributaria de una empresa sin las que ya tiene asignadas y según la localidad
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaInfoTributariaInput"/>
        ///     con el id de la empresa y el id de la localidad
        /// </param>
        /// <return>
        ///     Retorna un objeto de <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllInfoTributariaByLocalidadOutput"/> con la lista de información tributaria
        /// </return>
        GetAllInfoTributariaByLocalidadOutput GetAllInfoTributariaByLocalidad(GetAllInfoTributariaByLocalidadInput localidadInput);

        /// <summary>
        ///     Se encarga de obtener el listado de opciones de información tributaria de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllOpcionesInfoTributariaEmpresaInput"/>
        ///     con el id de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto de <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllOpcionesInfoTributariaEmpresaOutput"/> con la lista de opciones de información tributaria
        /// </return>
        GetAllOpcionesInfoTributariaEmpresaOutput GetAllOpcionesInfoTributariaEmpresa(GetAllOpcionesInfoTributariaEmpresaInput empresaInput);

        /// <summary>
        ///     Se encarga de actualizar el listado de opciones de información tributaria de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateEmpresaInfoTributariaInput"/>
        ///     con el id de la empresa y el listado de opciones de información tributaria a actualizar
        /// </param>
        /// <return></return>
        void UpdateEmpresaInfoTributaria(UpdateEmpresaInfoTributariaInput empresaInfoTributariaUpdate);

        /// <summary>
        ///     Se encarga de guardar la información de la sucursal de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.SaveSucursalEmpresaInput"/>
        ///     con la información de la sucursal de la empresa
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.SaveSucursalEmpresaOutput"/> con el id de la sucursal almacenada
        /// </return>
        SaveSucursalEmpresaOutput SaveSucursalEmpresa(SaveSucursalEmpresaInput empresaSucursalSave);

        /// <summary>
        ///     Se encarga de actualizar la información de la sucursal de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateSucursalEmpresaInput"/>
        ///     con la información de la sucursal de la empresa
        /// </param>
        /// <return></return>
        void UpdateSucursalEmpresa(UpdateSucursalEmpresaInput empresaSucursalUpdate);

        /// <summary>
        ///     Se encarga de actualizar la lista de teléfonos de la sucursal de una empresa
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.UpdateSucursalEmpresaTelefonoInput"/>
        ///     con la lista de teléfonos de la sucursal a actualizar
        /// </param>
        /// <return></return>
        void UpdateSucursalEmpresaTelefono(UpdateSucursalEmpresaTelefonoInput sucursalTelefonoUpdate);

        /// <summary>
        ///     Se encarga de obtener la lista de sucursales de una empresa asociada a una organización
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetAllSucursalesEmpresaInput"/>
        ///     con el id de la empresa organización
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllSucursalesEmpresaOutput"/> con la lista de sucursales de la empresa organización
        /// </return>
        GetAllSucursalesEmpresaOutput GetAllSucursalesEmpresa(GetAllSucursalesEmpresaInput empresaOrganizacionInput);

        /// <summary>
        ///     Se encarga de obtener la información de una sucursal de una empresa con el listado de teléfonos asociados
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetSucursalEmpresaOrganizacionInput"/>
        ///     con el id de la empresa organización y el id de la sucursal
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetSucursalEmpresaOrganizacionOutput"/> con la información de la sucursal
        /// </return>
        GetSucursalEmpresaOrganizacionOutput GetSucursalEmpresaOrganizacion(GetSucursalEmpresaOrganizacionInput sucursalEmpresaOrganizacionInput);

        /// <summary>
        /// Se encarga de obtener el listado completo de las sucursales con nombre de la empresa y organizacion a la que pertenecen
        /// </summary>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllSucursalesOutput"/>
        /// </return>
        GetAllSucursalesOutput GetAllSucursales();

        /// <summary>
        ///     Se encarga de obtener el listado de convenios de recaudo con el numero de localidades asociadas
        /// </summary>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetAllConveniosRecaudoMasivoOutput"/> con la información de cada convenio
        /// </return>
        GetAllConveniosRecaudoMasivoOutput GetAllConveniosRecaudoMasivo();

        /// <summary>
        ///     Se encarga de obtener la información de la sucursal con la empresa y la organización a partir del Id
        /// </summary>
        /// <param>
        ///     Ingresa como parámetro un objeto <see cref="Bow.Application.Empresas.DTOs.InputModels.GetSucursalByIdWithEmpresaAndOrganizacionInput"/> con el id de la sucursal
        /// </param>
        /// <return>
        ///     Retorna un objeto <see cref="Bow.Application.Empresas.DTOs.OutputModels.GetSucursalByIdWithEmpresaAndOrganizacionOutput"/> con la información de la sucursal
        /// </return>
        GetSucursalByIdWithEmpresaAndOrganizacionOutput GetSucursalByIdWithEmpresaAndOrganizacion(GetSucursalByIdWithEmpresaAndOrganizacionInput sucursal);

    }
}
