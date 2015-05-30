using Abp.Localization;
using Abp.UI;
using AutoMapper;
using Bow.Empleados.DTOs.InputModels;
using Bow.Empleados.DTOs.OutputModels;
using Bow.Empleados.Entidades;
using Bow.Empleados.Repositorios;
using Bow.Parametros;
using Bow.Parametros.Repositorios;
using Bow.Personas.Repositorios;
using Bow.Zonificacion.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados
{
    public class EmpleadosService : IEmpleadosService
    {
        # region Repositorios

        private IEmpleadoRepositorio _empleadoRepositorio;

        //Los siguientes declaraciones se realizan a los repositorios y no a los servicios debido a la consulta linq
        //que se realiza en el método GetBuscadorEmpleado, ya que la lista que necesita el query linq debe realizarse dentro de la sentencia
        private IPersonaRepositorio _personaRepositorio;
        private IPersonaDireccionRepositorio _personaDireccionRepositorio;
        private IDireccionRepositorio _direccionRepositorio;
        private IBarrioRepositorio _barrioRepositorio;
        private ILocalidadRepositorio _localidadRepositorio;
        private IDepartamentoRepositorio _departamentoRepositorio;
        private IEstadoRepositorio _estadoRepositorio;
        private INombreEstadoRepositorio _nombreEstadoRepositorio;

        private IParametrosService _parametrosService;

        # endregion

        public EmpleadosService(IEmpleadoRepositorio empleadoRepositorio, IPersonaRepositorio personaRepositorio, IPersonaDireccionRepositorio personaDireccionRepositorio,
                                IDireccionRepositorio direccionRepositorio, IBarrioRepositorio barrioRepositorio, ILocalidadRepositorio localidadRepositorio, IDepartamentoRepositorio departamentoRepositorio,
                                IEstadoRepositorio estadoRepositorio, INombreEstadoRepositorio nombreEstadoRepositorio, IParametrosService parametrosService)
        {
            _empleadoRepositorio = empleadoRepositorio;

            _personaRepositorio = personaRepositorio;
            _personaDireccionRepositorio = personaDireccionRepositorio;
            _direccionRepositorio = direccionRepositorio;
            _barrioRepositorio = barrioRepositorio;
            _localidadRepositorio = localidadRepositorio;
            _departamentoRepositorio = departamentoRepositorio;
            _estadoRepositorio = estadoRepositorio;
            _nombreEstadoRepositorio = nombreEstadoRepositorio;

            _parametrosService = parametrosService;
        }

        public void SaveEmpleado(SaveEmpleadoInput empleadoInput)
        {
            var mensajeError = "";

            if (empleadoInput.Codigo == 0)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "empleados_nuevoempleado_codigoRequerido");
            }
            else if (empleadoInput.PersonaId == 0)
            {
                mensajeError = LocalizationHelper.GetString("Bow", "empleados_nuevoempleado_personaRequerido");
            }
            else
            {
                var existeEmpleado = _empleadoRepositorio.FirstOrDefault(e => e.PersonaId == empleadoInput.PersonaId);

                if (existeEmpleado == null)
                {
                    Empleado nuevo = Mapper.Map<Empleado>(empleadoInput);
                    _empleadoRepositorio.Insert(nuevo);
                }
                else
                {
                    mensajeError = LocalizationHelper.GetString("Bow", "empleados_nuevoempleado_empleadoExiste");
                }
            }

            if (mensajeError != "")
            {
                throw new UserFriendlyException(mensajeError);
            }
        }

        public GetEmpleadosWithSucursalAndLocalidadOutput GetEmpleadosWithSucursal()
        {
            return new GetEmpleadosWithSucursalAndLocalidadOutput { Empleados = Mapper.Map<List<EmpleadoWithSucursalAndLocalidadOutput>>(_empleadoRepositorio.GetWithSucursal()) };
        }

        public GetBuscadorEmpleadoOutput GetBuscadorEmpleado(GetBuscadorEmpleadoInput datosInput)
        {
            var tipoUbicacionResidencial = _parametrosService.GetTipoUbicacionResidencial().Id;

            var resultado = (from empleado in _empleadoRepositorio.GetAll()
                             join persona in _personaRepositorio.GetAll() on empleado.PersonaId equals persona.Id into pers
                             from p in pers.DefaultIfEmpty()
                             join personaDireccion in _personaDireccionRepositorio.GetAll() on p.Id equals personaDireccion.PersonaId into perdir
                             from pd in perdir.Where(s => s.TipoUbicacionId.Equals(tipoUbicacionResidencial)).DefaultIfEmpty()
                             join direccion in _direccionRepositorio.GetAll() on pd.DireccionId equals direccion.Id into dir
                             from d in dir.DefaultIfEmpty()
                             join barrio in _barrioRepositorio.GetAll() on d.BarrioId equals barrio.Id into barr
                             from b in barr.DefaultIfEmpty()
                             join localidad in _localidadRepositorio.GetAll() on b.LocalidadId equals localidad.Id into loc
                             from l in loc.DefaultIfEmpty()
                             join departamento in _departamentoRepositorio.GetAll() on l.DepartamentoId equals departamento.Id into dep
                             from de in dep.DefaultIfEmpty()
                             join estado in _estadoRepositorio.GetAll() on empleado.EstadoId equals estado.Id into estad
                             from e in estad.DefaultIfEmpty()
                             join nombreEstado in _nombreEstadoRepositorio.GetAll() on e.EstadoNombreId equals nombreEstado.Id into nomestad
                             from ne in nomestad.DefaultIfEmpty()

                             where ((string.IsNullOrEmpty(datosInput.Nombre) || (p.Nombre.Contains(datosInput.Nombre)))
                              && ((string.IsNullOrEmpty(datosInput.Apellido1)) || (p.Apellido1.Contains(datosInput.Apellido1)))
                              && ((string.IsNullOrEmpty(datosInput.Apellido2)) || (p.Apellido2.Contains(datosInput.Apellido2)))
                              && ((string.IsNullOrEmpty(datosInput.Documento)) || (p.NumeroDocumento.Contains(datosInput.Documento)))
                              && ((string.IsNullOrEmpty(datosInput.CorreoElectronico)) || (p.CorreoElectronico.Contains(datosInput.CorreoElectronico)))
                              && ((datosInput.SucursalId == 0) || (empleado.SucursalId.Equals(datosInput.SucursalId)))
                              && ((datosInput.EstadoId == 0) || (empleado.EstadoId.Equals(datosInput.EstadoId))))

                             select new BuscadorEmpleadoOutput
                              {
                                  Id = empleado.Id,
                                  Codigo = empleado.Codigo,
                                  PersonaId = p.Id,
                                  TieneDocumento = p.TieneDocumento,
                                  TipoDocumentoId = p.TipoDocumentoId.ToString(),
                                  NumeroDocumento = p.NumeroDocumento,
                                  FechaExpDocumento = p.FechaExpDocumento.ToString(),
                                  Nombre = p.Nombre,
                                  Apellido1 = p.Apellido1,
                                  Apellido2 = p.Apellido2,
                                  TieneFechaNacimiento = p.TieneFechaNacimiento,
                                  FechaNacimiento = p.FechaNacimiento.ToString(),
                                  Genero = p.Genero,
                                  CorreoElectronico = p.CorreoElectronico,
                                  ContactarCorreo = p.ContactarCorreo,
                                  ContactarSms = p.ContactarSms,
                                  ContactarTelefono = p.ContactarTelefono,
                                  FechaIngreso = p.FechaIngreso.ToString(),
                                  TipoProfesionId = p.TipoProfesionId,
                                  TipoEstadoCivilId = p.TipoEstadoCivilId,
                                  Discapacitada = p.Discapacitada,
                                  FechaFallecimiento = p.FechaFallecimiento.ToString(),
                                  PaisId = p.PaisId,
                                  NombreCompleto = p.Nombre + " " + p.Apellido1 + " " + p.Apellido2,
                                  LocalidadDepartamento = de.Nombre == null ? null : de.Nombre + " (" + l.Nombre + ") - ",
                                  Estado = ne.Nombre
                              }).Distinct().Take(50).ToList();

            return new GetBuscadorEmpleadoOutput { Empleados = Mapper.Map<List<BuscadorEmpleadoOutput>>(resultado) };

            //var tipoUbicacionResidencial = _parametrosService.GetTipoUbicacionResidencial().Id;

            ////Consulta linq con leftjoin y distinct
            //var resultado = (from persona in _personaRepositorio.GetAll()
            //                 join personaTelefono in _personaTelefonoRepositorio.GetAll() on persona.Id equals personaTelefono.PersonaId into pertel
            //                 from pt in pertel.DefaultIfEmpty().Distinct()
            //                 join telefono in _telefonoRepositorio.GetAll() on pt.TelefonoId equals telefono.Id into tel
            //                 from t in tel.DefaultIfEmpty()
            //                 join personaDireccion in _personaDireccionRepositorio.GetAll() on persona.Id equals personaDireccion.PersonaId into perdir
            //                 from pd in perdir.Where(s => s.TipoUbicacionId.Equals(tipoUbicacionResidencial)).DefaultIfEmpty()
            //                 join direccion in _direccionRepositorio.GetAll() on pd.DireccionId equals direccion.Id into dir
            //                 from d in dir.DefaultIfEmpty()
            //                 join barrio in _barrioRepositorio.GetAll() on d.BarrioId equals barrio.Id into barr
            //                 from b in barr.DefaultIfEmpty()
            //                 join localidad in _localidadRepositorio.GetAll() on b.LocalidadId equals localidad.Id into loc
            //                 from l in loc.DefaultIfEmpty()
            //                 join departamento in _departamentoRepositorio.GetAll() on l.DepartamentoId equals departamento.Id into dep
            //                 from de in dep.DefaultIfEmpty()
            //                 where ((string.IsNullOrEmpty(datosInput.Nombre) || (persona.Nombre.Contains(datosInput.Nombre)))
            //                 && ((string.IsNullOrEmpty(datosInput.Apellido1)) || (persona.Apellido1.Contains(datosInput.Apellido1)))
            //                 && ((string.IsNullOrEmpty(datosInput.Apellido2)) || (persona.Apellido2.Contains(datosInput.Apellido2)))
            //                 && ((string.IsNullOrEmpty(datosInput.Documento)) || (persona.NumeroDocumento.Contains(datosInput.Documento)))
            //                 && ((string.IsNullOrEmpty(datosInput.CorreoElectronico)) || (persona.CorreoElectronico.Contains(datosInput.CorreoElectronico)))
            //                 && ((string.IsNullOrEmpty(datosInput.Telefono)) || (t.Numero.Contains(datosInput.Telefono)))
            //                 && ((string.IsNullOrEmpty(datosInput.ZipCode)) || (d.ZipCode.Contains(datosInput.ZipCode))))

            //                 select new BuscadorPersonaOutput
            //                 {
            //                     Id = persona.Id,
            //                     TieneDocumento = persona.TieneDocumento,
            //                     TipoDocumentoId = persona.TipoDocumentoId.ToString(),
            //                     NumeroDocumento = persona.NumeroDocumento,
            //                     FechaExpDocumento = persona.FechaExpDocumento.ToString(),
            //                     Nombre = persona.Nombre,
            //                     Apellido1 = persona.Apellido1,
            //                     Apellido2 = persona.Apellido2,
            //                     TieneFechaNacimiento = persona.TieneFechaNacimiento,
            //                     FechaNacimiento = persona.FechaNacimiento.ToString(),
            //                     Genero = persona.Genero,
            //                     CorreoElectronico = persona.CorreoElectronico,
            //                     ContactarCorreo = persona.ContactarCorreo,
            //                     ContactarSms = persona.ContactarSms,
            //                     ContactarTelefono = persona.ContactarTelefono,
            //                     FechaIngreso = persona.FechaIngreso.ToString(),
            //                     TipoProfesionId = persona.TipoProfesionId,
            //                     TipoEstadoCivilId = persona.TipoEstadoCivilId,
            //                     Discapacitada = persona.Discapacitada,
            //                     FechaFallecimiento = persona.FechaFallecimiento.ToString(),
            //                     PaisId = persona.PaisId,
            //                     NombreCompleto = persona.Nombre + " " + persona.Apellido1 + " " + persona.Apellido2,
            //                     LocalidadDepartamento = de.Nombre == null ? null : de.Nombre + " - " + l.Nombre,
            //                 }).Distinct().Take(50).ToList();

            //return new GetBuscadorPersonaOutput { Personas = Mapper.Map<List<BuscadorPersonaOutput>>(resultado) };
        }

        public GetEmpleadosByIdOutput GetEmpleadosById(GetEmpleadosByIdInput empleado)
        {
            return Mapper.Map<GetEmpleadosByIdOutput>(_empleadoRepositorio.GetByIdWithSucursal(empleado.Id));
        }

    }
}
