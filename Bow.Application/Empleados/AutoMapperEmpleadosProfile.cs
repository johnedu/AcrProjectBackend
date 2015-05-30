using Bow.Empleados.DTOs.InputModels;
using Bow.Empleados.DTOs.OutputModels;
using Bow.Empleados.Entidades;
using Bow.Personas.DTOs.InputModels;
using Bow.Utilidades.AutoMapper;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empleados
{
    public class AutoMapperEmpleadosProfile: AutoMapperBaseProfile
    {
        public AutoMapperEmpleadosProfile()
            : base("AutoMapperEmpleadosProfile")
        {
        }

        protected override void CrearMappings()
        {
            CreateMap<SaveEmpleadoInput, Empleado>();

            CreateMap<Empleado, EmpleadoWithSucursalAndLocalidadOutput>()
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.PersonaEmpleado.Nombre))
                 .ForMember(dest => dest.Apellido1, opt => opt.MapFrom(src => src.PersonaEmpleado.Apellido1))
                 .ForMember(dest => dest.Apellido2, opt => opt.MapFrom(src => src.PersonaEmpleado.Apellido2))
                 .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.PersonaEmpleado.nombreCompleto))
                 .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.SucursalId))
                 .ForMember(dest => dest.SucursalNombre, opt => opt.MapFrom(src => src.SucursalEmpleado.Nombre))
                 .ForMember(dest => dest.EmpresaNombre, opt => opt.MapFrom(src => src.SucursalEmpleado.EmpresaOrganizacion.EmpresaEmpresaOrganizacion.NombreComercial))
                 .ForMember(dest => dest.OrganizacionNombre, opt => opt.MapFrom(src => src.SucursalEmpleado.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion.Nombre))
                 .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.LocalidadId))
                 .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.Nombre))
                 .ForMember(dest => dest.DepartamentoIndicativo, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.Indicativo))
                 .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoId))
                 .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre))
                 .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisDepartamento.Indicativo))
                 .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisId))
                 .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisDepartamento.Nombre));

            CreateMap<Empleado, GetEmpleadosByIdOutput>()
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.PersonaEmpleado.Nombre))
                 .ForMember(dest => dest.Apellido1, opt => opt.MapFrom(src => src.PersonaEmpleado.Apellido1))
                 .ForMember(dest => dest.Apellido2, opt => opt.MapFrom(src => src.PersonaEmpleado.Apellido2))
                 .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.PersonaEmpleado.nombreCompleto))
                 .ForMember(dest => dest.SucursalId, opt => opt.MapFrom(src => src.SucursalId))
                 .ForMember(dest => dest.SucursalNombre, opt => opt.MapFrom(src => src.SucursalEmpleado.Nombre))
                 .ForMember(dest => dest.EmpresaNombre, opt => opt.MapFrom(src => src.SucursalEmpleado.EmpresaOrganizacion.EmpresaEmpresaOrganizacion.NombreComercial))
                 .ForMember(dest => dest.OrganizacionNombre, opt => opt.MapFrom(src => src.SucursalEmpleado.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion.Nombre))
                 .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.LocalidadId))
                 .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.Nombre))
                 .ForMember(dest => dest.DepartamentoIndicativo, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.Indicativo))
                 .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoId))
                 .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.Nombre))
                 .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisDepartamento.Indicativo))
                 .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisId))
                 .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.DepartamentoLocalidad.PaisDepartamento.Nombre));
        }
    }
}
