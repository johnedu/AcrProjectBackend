using Bow.Empresas.DTOs.InputModels;
using Bow.Empresas.DTOs.OutputModels;
using Bow.Empresas.Entidades;
using Bow.Parametros.DTOs.InputModels;
using Bow.Parametros.DTOs.OutputModels;
using Bow.Utilidades.AutoMapper;
using Bow.Zonificacion.DTOs.InputModels;
using Bow.Zonificacion.DTOs.OutputModels;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas
{
    public class AutoMapperEmpresasProfile : AutoMapperBaseProfile
    {
        public AutoMapperEmpresasProfile()
            : base("AutoMapperEmpresasProfile")
        {
        }

        protected override void CrearMappings()
        {
            //Mapping de Info Tributaria
            CreateMap<SaveInfoTributariaInput, InfoTributaria>();
            CreateMap<UpdateInfoTributariaInput, InfoTributaria>();
            CreateMap<UpdateActividadEconomicaInput, ActividadEconomica>();
            CreateMap<ActividadEconomica,ActividadEconomicaOutput>();
            CreateMap<ActividadEconomica, GetActividadEconomicaOutput>();
            CreateMap<SaveActividadEconomicaInput, ActividadEconomica>();
            

            CreateMap<InfoTributaria, GetInfoTributariaOutput>()
                .ForMember(dest => dest.TipoValorId, opt => opt.MapFrom(src => src.TipoValor.Id))
                .ForMember(dest => dest.TipoValor, opt => opt.MapFrom(src => src.TipoValor.Nombre))
                .ForMember(dest => dest.EstadoId, opt => opt.MapFrom(src => src.Estado.EstadoNombreEstado.Nombre.Equals(BowConsts.NOMBREESTADO_NOMBRE_VIGENTE) ? true : false));

            CreateMap<InfoTributaria, InfoTributariaOutput>()
                .ForMember(dest => dest.TipoValor, opt => opt.MapFrom(src => src.TipoValor.Nombre))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.EstadoNombreEstado.Nombre.Equals(BowConsts.NOMBREESTADO_NOMBRE_VIGENTE) ? true : false ))

                .ForMember(dest => dest.CantidadOpciones, opt => opt.MapFrom(src => src.InfoTributariaOpciones.Count))
                .ForMember(dest => dest.CantidadLocalidades, opt => opt.MapFrom(src => src.InfoTributariaLocalidades.Count));

            CreateMap<InfoTributaria, InfoTributariaByLocalidadOutput>()
                .ForMember(dest => dest.TipoValor, opt => opt.MapFrom(src => src.TipoValor.Nombre));

            //Mapping de Opciones Info Tributaria

            CreateMap<SaveInfoTributariaOpcionInput, InfoTributariaOpcion>();
            CreateMap<UpdateInfoTributariaOpcionInput, InfoTributariaOpcion>();

            CreateMap<InfoTributariaOpcion, GetInfoTributariaOpcionOutput>();
            CreateMap<InfoTributariaOpcion, InfoTributariaOpcionOutput>();

            //Mapping de Localidades de Info Tributaria

            CreateMap<Localidad, LocalidadesByInfoTributariaOutput>()
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Id))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Nombre))
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Id))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Nombre));

            CreateMap<Localidad, LocalidadesByInfoTributariaAndPaisOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre + " (" + src.DepartamentoLocalidad.Nombre + ", " + src.DepartamentoLocalidad.PaisDepartamento.Nombre + ")" ));

            CreateMap<LocalidadDepartamentoPaisWithFilterOutput, LocalidadesByNotInfoTributariaOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Localidad + " (" + src.Departamento + ", " + src.Pais + ")"));

            CreateMap<LocalidadDepartamentoPaisWithFilterOutput, LocalidadesByNotInfoTributariaPaisOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Localidad + " (" + src.Departamento + ")"));

            CreateMap<LocalidadDepartamentoPaisWithFilterOutput, LocalidadesByNotInfoTributariaDepartamentoOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Localidad));

            CreateMap<SaveInfoTributariaLocalidadnput, InfoTributariaLocalidad>();

            CreateMap<Pais, PaisesByInfoTributariaOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<Departamento, DepartamentosByInfoTributariaOutput>()
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.PaisDepartamento.Id))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.PaisDepartamento.Nombre));

            CreateMap<Localidad, LocalidadInput>();
            

            //Actividades Económicas

            CreateMap<ActividadEconomica, ActividadEconomicaOutput>();
            
            //Mapping de Organización

            CreateMap<Organizacion, GetOrganizacionOutput>();
            CreateMap<UpdateOrganizacionInput, Organizacion>();

            //Mapping de Empresa

            CreateMap<Empresa, GetEmpresaOutput>();
            
            CreateMap<SaveEmpresaInput, Empresa>();
            CreateMap<UpdateEmpresaInput, Empresa>();

            //Mapping de Empresa Organización

            CreateMap<EmpresaOrganizacion, EmpresaOutput>()
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.Id));

            CreateMap<SaveEmpresaOrganizacionInput, SaveEmpresaInput>();
            CreateMap<UpdateEmpresaOrganizacionInput, UpdateEmpresaInput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmpresaId));
            CreateMap<SaveEmpresaOrganizacionInput, EmpresaOrganizacion>();
            CreateMap<UpdateEmpresaOrganizacionInput, EmpresaOrganizacion>();

            CreateMap<EmpresaOrganizacion, GetEmpresaOrganizacionOutput>()
                .ForMember(dest => dest.TipoNaturalezaId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.TipoNaturalezaId))
                .ForMember(dest => dest.PaisTipoDocumentoId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.TipoDocumento.PaisId))
                .ForMember(dest => dest.PaisTipoDocumentoNombre, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.TipoDocumento.TipoDocumentoPersonaPais.Nombre))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.TipoDocumentoId))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.Documento))
                .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.RazonSocial))
                .ForMember(dest => dest.NombreComercial, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.NombreComercial))
                .ForMember(dest => dest.PersonaId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.PersonaId))
                .ForMember(dest => dest.ActividadEconomicaId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.ActividadEconomicaId))
                .ForMember(dest => dest.ActividadEconomicaCodigo, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.ActividadEconomica.Codigo))
                .ForMember(dest => dest.ActividadEconomicaNombre, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.ActividadEconomica.Nombre))
                .ForMember(dest => dest.DireccionId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.DireccionId))
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.Direccion.BarrioDireccion.LocalidadId))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.Direccion.DireccionCompleta))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EstadoEmpresaOrganizacion.EstadoNombreEstado.Nombre));

            //Mapping de Empresa Teléfono

            CreateMap<EmpresaTelefonoInput, EmpresaTelefono>();

            CreateMap<EmpresaTelefono, TelefonoEmpresaOutput>()
                .ForMember(dest => dest.TelefonoId, opt => opt.MapFrom(src => src.TelefonoEmpresaTelefono.Id))
                .ForMember(dest => dest.TelefonoNumero, opt => opt.MapFrom(src => (src.TelefonoEmpresaTelefono.TipoTelefono.Nombre == BowConsts.TIPO_TELEFONO_FIJO ?
                    ("(" + src.TelefonoEmpresaTelefono.LocalidadTelefono.DepartamentoLocalidad.Indicativo + ") ") : "") +
                    ((src.TelefonoEmpresaTelefono.Extension == null || src.TelefonoEmpresaTelefono.Extension == "") ? src.TelefonoEmpresaTelefono.Numero : src.TelefonoEmpresaTelefono.Numero + " - Ext: " + src.TelefonoEmpresaTelefono.Extension)))
                .ForMember(dest => dest.NombreLocalidad, opt => opt.MapFrom(src => src.TelefonoEmpresaTelefono.LocalidadTelefono.Nombre + " (" + src.TelefonoEmpresaTelefono.LocalidadTelefono.DepartamentoLocalidad.Nombre + ")"))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "C"));

            //Mapping de Empresa Contacto Web

            CreateMap<EmpresaContactoWebInput, EmpresaContactoWeb>();

            CreateMap<EmpresaContactoWeb, ContactoWebEmpresaOutput>()
                .ForMember(dest => dest.IdMedioContacto, opt => opt.MapFrom(src => src.TipoRedEmpresaContactoWeb.Id))
                .ForMember(dest => dest.MedioContacto, opt => opt.MapFrom(src => src.TipoRedEmpresaContactoWeb.Nombre))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "C"));

            CreateMap<TipoOutput, TipoContactoWebEmpresaOutput>();

            CreateMap<EmpresaContactoWeb, TipoInput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TipoRedEmpresaContactoWeb.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.TipoRedEmpresaContactoWeb.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.TipoRedEmpresaContactoWeb.Descripcion));

            //Mapping de Empresa Contacto

            CreateMap<EmpresaContactoInput, EmpresaContacto>();

            CreateMap<EmpresaContacto, ContactoEmpresaOutput>()
                .ForMember(dest => dest.DocumentoPersona, opt => opt.MapFrom(src => src.PersonaEmpresaContacto.NumeroDocumento))    
                .ForMember(dest => dest.IdPersona, opt => opt.MapFrom(src => src.PersonaId))
                .ForMember(dest => dest.NombrePersona, opt => opt.MapFrom(src => src.PersonaEmpresaContacto.nombreCompleto))
                .ForMember(dest => dest.TelefonosContacto, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.IdTipoAreaEmpresa, opt => opt.MapFrom(src => src.TipoAreaEmpresaId))
                .ForMember(dest => dest.TipoAreaEmpresa, opt => opt.MapFrom(src => src.TipoAreaEmpresaContacto.Nombre))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "C"));

            CreateMap<TipoOutput, TiposAreaEmpresaOutput>();

            CreateMap<EmpresaContacto, TipoInput>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TipoAreaEmpresaContacto.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.TipoAreaEmpresaContacto.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.TipoAreaEmpresaContacto.Descripcion));

            //Mapping de Empresa Info Tributaria

            CreateMap<EmpresaInfoTributaria, OpcionesInfoTributariaEmpresaOutput>()
                .ForMember(dest => dest.InfoTributariaId, opt => opt.MapFrom(src => src.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributariaId))
                .ForMember(dest => dest.InfoTributaria, opt => opt.MapFrom(src => src.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributaria.Nombre))
                .ForMember(dest => dest.InfoTributariaOpcion, opt => opt.MapFrom(src => src.InfoTributariaOpcionEmpresaInfoTributaria.Nombre))
                .ForMember(dest => dest.TipoValor, opt => opt.MapFrom(src => src.InfoTributariaOpcionEmpresaInfoTributaria.InfoTributaria.TipoValor.Nombre))
                .ForMember(dest => dest.FechaInicio, opt => opt.MapFrom(src => src.FechaInicio.ToString("yyyy/MM/dd")))
                .ForMember(dest => dest.FechaFin, opt => opt.MapFrom(src => src.FechaFin == null ? null : src.FechaFin.Value.ToString("yyyy/MM/dd")))
                .ForMember(dest => dest.FechaActualizacion, opt => opt.MapFrom(src => src.FechaActualizacion == null ? null : src.FechaActualizacion.Value.ToString("yyyy/MM/dd H:mm:ss")))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "C"))
                .ForMember(dest => dest.IdEliminar, opt => opt.MapFrom(src => "C" + src.InfoTributariaOpcionEmpresaInfoTributaria.Id.ToString()))
                .ForMember(dest => dest.EstadoActiva, opt => opt.MapFrom(src => src.FechaFin == null ? true : false));

            CreateMap<EmpresaInfoTributariaInput, EmpresaInfoTributaria>();

            //Mapping de Sucursal Empresa

            CreateMap<SaveSucursalEmpresaInput, Sucursal>();
            CreateMap<UpdateSucursalEmpresaInput, Sucursal>();

            CreateMap<Sucursal, SucursalOutput>();
            CreateMap<Sucursal, GetSucursalEmpresaOrganizacionOutput>()
                .ForMember(dest => dest.TipoSucursal, opt => opt.MapFrom(src => src.SucursalTipo.Nombre))
                .ForMember(dest => dest.EstadoSucursal, opt => opt.MapFrom(src => src.SucursalEstado.EstadoNombreEstado.Nombre))
                .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.SucursalDireccion.DireccionCompleta))
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.SucursalDireccion.BarrioDireccion.LocalidadId))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.SucursalDireccion.BarrioDireccion.Localidad.Nombre));

            CreateMap<SucursalTelefonoInput, SucursalTelefono>();
            CreateMap<SucursalTelefono, TelefonoSucursalOutput>()
                .ForMember(dest => dest.TelefonoNumero, opt => opt.MapFrom(src => (src.TelefonoSucursalTelefono.TipoTelefono.Nombre == BowConsts.TIPO_TELEFONO_FIJO ?
                    ("(" + src.TelefonoSucursalTelefono.LocalidadTelefono.DepartamentoLocalidad.Indicativo + ") ") : "") +
                    ((src.TelefonoSucursalTelefono.Extension == null || src.TelefonoSucursalTelefono.Extension == "") ? src.TelefonoSucursalTelefono.Numero : src.TelefonoSucursalTelefono.Numero + " - Ext: " + src.TelefonoSucursalTelefono.Extension)))
                .ForMember(dest => dest.NombreLocalidad, opt => opt.MapFrom(src => "(" + src.TelefonoSucursalTelefono.LocalidadTelefono.Nombre + " - " + src.TelefonoSucursalTelefono.LocalidadTelefono.DepartamentoLocalidad.Nombre + ")"))
                .ForMember(dest => dest.Accion, opt => opt.MapFrom(src => "C"));

            CreateMap<Sucursal, SucursalesOutput>()
                .ForMember(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.EmpresaOrganizacion.EmpresaEmpresaOrganizacion.NombreComercial))
                .ForMember(dest => dest.NombreOrganizacion, opt => opt.MapFrom(src => src.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion.Nombre))
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.SucursalDireccion.BarrioDireccion.LocalidadId));

            CreateMap<EmpresaOrganizacion, EmpresaWithSucursalesOutput>()
                .ForMember(dest => dest.Empresa, opt => opt.MapFrom(src => src.EmpresaEmpresaOrganizacion.NombreComercial))
                .ForMember(dest => dest.NumeroSucursales, opt => opt.MapFrom(src => src.EmpresaOrganizacionSucursal.Count()));

            CreateMap<RecaudoMasivo, RecaudoMasivoOutput>()
                .ForMember(dest => dest.NumeroLocalidades, opt => opt.MapFrom(src => src.RecaudoMasivoCobertura.Count()));

            CreateMap<Sucursal, GetSucursalByIdWithEmpresaAndOrganizacionOutput>()
                .ForMember(dest => dest.NombreEmpresa, opt => opt.MapFrom(src => src.EmpresaOrganizacion.EmpresaEmpresaOrganizacion.NombreComercial))
                .ForMember(dest => dest.NombreOrganizacion, opt => opt.MapFrom(src => src.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion.Nombre));

        }
    }
}
