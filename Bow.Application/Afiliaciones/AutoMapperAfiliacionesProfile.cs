using Bow.Afiliaciones.DTOs.InputModels;
using Bow.Afiliaciones.DTOs.OutputModels;
using Bow.Afiliaciones.Entidades;
using Bow.Cartera.DTOs.OutputModels;
using Bow.Cartera.Entidades;
using Bow.Empresas.DTOs.OutputModels;
using Bow.Empresas.Entidades;
using Bow.Utilidades.AutoMapper;
using Bow.Zonificacion.DTOs.OutputModels;
using Bow.Zonificacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bow.Empresas
{
    public class AutoMapperAfiliacionesProfile : AutoMapperBaseProfile
    {
        public AutoMapperAfiliacionesProfile()
            : base("AutoMapperAfiliacionesProfile")
        {
        }

        protected override void CrearMappings()
        {
            //Mapping de Plan Exequial
            CreateMap<PlanExequial, PlanExequialOutput>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EstadoPlanExequial.EstadoNombreEstado.Nombre))
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.MonedaPlanExequial.Nombre))
                .ForMember(dest => dest.EstadoActivo, opt => opt.MapFrom(src => src.EstadoPlanExequial.EstadoNombreEstado.Nombre == BowConsts.NOMBREESTADO_NOMBRE_ACTIVO ? true : false));
            CreateMap<PlanExequial, GetPlanExequialOutput>();
            CreateMap<PlanExequial, GetPlanExequialWithMonedaOutput>()
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.MonedaPlanExequial.Nombre))
                .ForMember(dest => dest.MonedaSimbolo, opt => opt.MapFrom(src => src.MonedaPlanExequial.Simbolo));

            CreateMap<SavePlanExequialInput, PlanExequial>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)));
            CreateMap<UpdatePlanExequialInput, PlanExequial>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));

            //Mapping de Beneficios
            CreateMap<SaveBeneficioInput, Beneficio>();

            CreateMap<Beneficio, BeneficioOutPut>()
                 .ForMember(dest => dest.TipoNombre, opt => opt.MapFrom(src => src.TipoBeneficio.Nombre));
            CreateMap<BeneficioPlanExequial, BeneficioOutPut>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Id))
                 .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Nombre))
                 .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Descripcion))
                 .ForMember(dest => dest.TipoId, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.TipoId))
                 .ForMember(dest => dest.TipoNombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.TipoBeneficio.Nombre));

            CreateMap<Beneficio, GetBeneficioOutput>();

            //Mapping de Grupo Familiar
            CreateMap<GrupoFamiliar, GrupoFamiliarOutput>();
            CreateMap<GrupoFamiliar, GetGrupoFamiliarOutput>();

            CreateMap<SaveGrupoFamiliarInput, GrupoFamiliar>();
            CreateMap<UpdateGrupoFamiliarInput, GrupoFamiliar>();

            //Mapping de Grupo Parentesco Rango
            CreateMap<Parentesco, ParentescoOutput>();

            CreateMap<GrupoParentescoRango, RangoParentescoOutput>()
                .ForMember(dest => dest.ParentescoNombre, opt => opt.MapFrom(src => src.GrupoFamiliarParentesco.Parentesco.Nombre))
                .ForMember(dest => dest.ParentescoValidaIngreso, opt => opt.MapFrom(src => src.GrupoFamiliarParentesco.ValidarSoloIngreso))
                .ForMember(dest => dest.ParentescoId, opt => opt.MapFrom(src => src.GrupoFamiliarParentesco.ParentescoId));
            CreateMap<GrupoParentescoRango, GetRangoParentescoOutput>()
                .ForMember(dest => dest.ParentescoId, opt => opt.MapFrom(src => src.GrupoFamiliarParentesco.ParentescoId));

            CreateMap<SaveRangoParentescoInput, GrupoParentescoRango>();
            CreateMap<UpdateRangoParentescoInput, GrupoParentescoRango>();

            CreateMap<GrupoFamiliarParentesco, GetGrupoFamiliarParentescoOutput>();

            CreateMap<SaveOrUpdateGrupoFamiliarParentescoInput, GrupoFamiliarParentesco>();

            CreateMap<GrupoFamiliarParentesco, ParentescoAllRangosOutput>()
                .ForMember(dest => dest.NombreParentesco, opt => opt.MapFrom(src => src.Parentesco.Nombre))
                .ForMember(dest => dest.Rangos, opt => opt.MapFrom(src => src.GruposParentescoRango));


            CreateMap<PeriodoVenta, PeriodoVentaOutPut>();

            CreateMap<PeriodoVentaOutPut, PeriodoVenta>();

            CreateMap<SavePeriodosVentasInput, GetVerificarPeriodosVentasInput>();

            CreateMap<PeriodoVenta, PeriodoVentaInput>();

            //Mapping de Beneficios Plan Exequial
            CreateMap<BeneficioPlanExequial, BeneficioPlanExequialOutPut>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Descripcion))
                .ForMember(dest => dest.EsAsignable, opt => opt.MapFrom(src => src.Asignables == 0 ? false : true))
                .ForMember(dest => dest.CategoriaBeneficioId, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.TipoId))
                .ForMember(dest => dest.CategoriaBeneficioNombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.TipoBeneficio.Nombre))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EstadoBeneficioPlanExequial.EstadoNombreEstado.Nombre));
            CreateMap<BeneficioPlanExequial, GetBeneficioPlanExequialOutput>()
                .ForMember(dest => dest.NombreBeneficio, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Nombre))
                .ForMember(dest => dest.DescripcionBeneficio, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Descripcion))
                .ForMember(dest => dest.CategoriaBeneficioId, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.TipoId))
                .ForMember(dest => dest.CategoriaBeneficioNombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.TipoBeneficio.Nombre))
                .ForMember(dest => dest.EsAsignable, opt => opt.MapFrom(src => src.Asignables == 0 ? false : true));
            CreateMap<BeneficioPlanExequial, BeneficioPlanExequialByTipoOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Nombre));

            CreateMap<BeneficioAdicionalPlanExequial, BeneficioAdicionalPlanExequialOutPut>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.Nombre))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.Descripcion))
                .ForMember(dest => dest.CategoriaBeneficioNombre, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.TipoBeneficio.Nombre))
                .ForMember(dest => dest.EsAsignable, opt => opt.MapFrom(src => src.Asignables == 0 ? false : true))
                .ForMember(dest => dest.CategoriaBeneficioId, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.TipoId))
                .ForMember(dest => dest.CategoriaBeneficioNombre, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.TipoBeneficio.Nombre))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EstadoBeneficioAdicionalPlanExequial.EstadoNombreEstado.Nombre))
                .ForMember(dest => dest.BeneficioPlanExequialNombre, opt => opt.MapFrom(src => src.BeneficioPlanExequialAdicional.BeneficioBeneficioPlanExequial.Nombre));
            CreateMap<BeneficioAdicionalPlanExequial, GetBeneficioAdicionalPlanExequialOutput>()
                .ForMember(dest => dest.NombreBeneficio, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.Nombre))
                .ForMember(dest => dest.DescripcionBeneficio, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.Descripcion))
                .ForMember(dest => dest.CategoriaBeneficioId, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.TipoId))
                .ForMember(dest => dest.CategoriaBeneficioNombre, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.TipoBeneficio.Nombre))
                .ForMember(dest => dest.EsAsignable, opt => opt.MapFrom(src => src.Asignables == 0 ? false : true))
                .ForMember(dest => dest.BeneficioPlanExequialId, opt => opt.MapFrom(src => src.BeneficioPlanExequialId))
                .ForMember(dest => dest.BeneficioPlanExequialNombre, opt => opt.MapFrom(src => src.BeneficioPlanExequialAdicional.BeneficioBeneficioPlanExequial.Nombre));

            CreateMap<SaveBeneficioPlanExequialInput, BeneficioPlanExequial>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null || src.FechaCancelacion == "" ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));
            CreateMap<SaveBeneficioAdicionalPlanExequialInput, BeneficioAdicionalPlanExequial>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null || src.FechaCancelacion == "" ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));

            CreateMap<UpdateBeneficioPlanExequialInput, BeneficioPlanExequial>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null || src.FechaCancelacion == "" ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));
            CreateMap<UpdateBeneficioAdicionalPlanExequialInput, BeneficioAdicionalPlanExequial>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null || src.FechaCancelacion == "" ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));

            //Mapping de Sucursales del Plan Exequial
            CreateMap<SucursalPlanExequialInput, PlanExequialSucursal>();

            //Mapping cliente prospecto
            CreateMap<SaveProspectoInput, SaveProspectoOutput>();

            CreateMap<SaveProspectoInput, Prospecto>();

            CreateMap<SaveGestionProspectoInput, SaveGestionProspectoOutput>();

            CreateMap<SaveGestionProspectoInput, GestionProspecto>();

            CreateMap<FunerariaProspecto, FunerariaOutput>();

            CreateMap<SaveAfiliadoProspectoInput, AfiliadoProspecto>();

            CreateMap<AfiliadoProspecto, AfiliadoProspectoOutput>()
                .ForMember(dest => dest.ParentescoNombre, opt => opt.MapFrom(src => src.Parentesco.Nombre))
                .ForMember(dest => dest.CiudadResidenciaNombre, opt => opt.MapFrom(src => src.CiudadResidencia.Nombre));

            CreateMap<AfiliadoProspecto, GetAfiliadoProspectoOutput>()
                .ForMember(dest => dest.ParentescoNombre, opt => opt.MapFrom(src => src.Parentesco.Nombre))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.CiudadResidencia.Nombre))
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.CiudadResidencia.Id))
                .ForMember(dest => dest.DepartamentoIndicativo, opt => opt.MapFrom(src => src.CiudadResidencia.DepartamentoLocalidad.Indicativo))
                .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.CiudadResidencia.DepartamentoId))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.CiudadResidencia.DepartamentoLocalidad.Nombre))
                .ForMember(dest => dest.PaisIndicativo, opt => opt.MapFrom(src => src.CiudadResidencia.DepartamentoLocalidad.PaisDepartamento.Indicativo))
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.CiudadResidencia.DepartamentoLocalidad.PaisId))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.CiudadResidencia.DepartamentoLocalidad.PaisDepartamento.Nombre));

            CreateMap<PlanExequial, PlanProspectoOutput>();

            CreateMap<GrupoFamiliar, GruposFamiliaresPlanOutput>();

            CreateMap<PlanProspectoInput, GrupoFamiliarParentesco>();

            CreateMap<BeneficioPlanExequial, TipoBeneficioPropioPlanExequialOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.BeneficioBeneficioPlanExequial.Nombre));

            CreateMap<BeneficioAdicionalPlanExequial, TipoBeneficioAdicionalPlanExequialOutput>()
               .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.BeneficioBeneficioAdicionalPlanExequial.Nombre));

            CreateMap<GestionProspecto, GestionProspectoOutput>()
                .ForMember(dest => dest.PersonaNombre, opt => opt.MapFrom(src => src.Persona.nombreCompleto))
                .ForMember(dest => dest.EstadoNoAfiliacionMotivo, opt => opt.MapFrom(src => src.EstadoNoAfiliacion.Motivo));

            CreateMap<GestionProspecto, GetGestionProspectoIniciarContactoOutput>()
                .ForMember(dest => dest.GrupoFamiliarNombre, opt => opt.MapFrom(src => src.GrupoFamiliar.Nombre))
                .ForMember(dest => dest.PlanExequialId, opt => opt.MapFrom(src => src.GrupoFamiliar.PlanExequialGrupoFamiliar.Id))
                .ForMember(dest => dest.PlanExequialNombre, opt => opt.MapFrom(src => src.GrupoFamiliar.PlanExequialGrupoFamiliar.Nombre))
                .ForMember(dest => dest.GrupoFamiliarNombre, opt => opt.MapFrom(src => src.GrupoFamiliar.Nombre))
                .ForMember(dest => dest.FunerariaAfiliadoNombre, opt => opt.MapFrom(src => src.FunenariaAfiliado.Nombre))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Persona.Nombre))
                .ForMember(dest => dest.Apellido1, opt => opt.MapFrom(src => src.Persona.Apellido1))
                .ForMember(dest => dest.Apellido2, opt => opt.MapFrom(src => src.Persona.Apellido2))
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.Persona.PaisId))
                .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.Persona.FechaNacimiento))
                .ForMember(dest => dest.Afiliados, opt => opt.MapFrom(src => src.AfiliadoProspecto));

            //Mapping de Convenio de Recaudo
            CreateMap<Localidad, LocalidadesByConvenioOutput>()
                .ForMember(dest => dest.LocalidadId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.DepartamentoId, opt => opt.MapFrom(src => src.DepartamentoId))
                .ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.DepartamentoLocalidad.Nombre))
                .ForMember(dest => dest.PaisId, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisId))
                .ForMember(dest => dest.Pais, opt => opt.MapFrom(src => src.DepartamentoLocalidad.PaisDepartamento.Nombre));

            CreateMap<Localidad, LocalidadesByConvenioAndPaisOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre + " (" + src.DepartamentoLocalidad.Nombre + ", " + src.DepartamentoLocalidad.PaisDepartamento.Nombre + ")"));

            CreateMap<LocalidadDepartamentoPaisWithFilterOutput, LocalidadByNotConvenioOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Localidad + " (" + src.Departamento + ", " + src.Pais + ")"));

            CreateMap<LocalidadDepartamentoPaisWithFilterOutput, LocalidadByNotConvenioAndPaisOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Localidad + " (" + src.Departamento + ")"));

            CreateMap<LocalidadDepartamentoPaisWithFilterOutput, LocalidadesByNotConvenioAndDepartamentoOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Localidad));

            CreateMap<Pais, PaisByConvenioOutput>();
            CreateMap<Departamento, DepartamentoByConvenioOutput>();

            CreateMap<SaveConvenioLocalidadInput, RecaudoMasivoCobertura>();

            CreateMap<PlanExequialRecaudoMasivo, PlanExequialRecaudoOutput>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.RecaudoMasivoPlanExequialRecaudoMasivo.Nombre))
                .ForMember(dest => dest.NumeroLocalidades, opt => opt.MapFrom(src => src.RecaudoMasivoPlanExequialRecaudoMasivo.RecaudoMasivoCobertura.Count()));

            CreateMap<PlanExequialRecaudoMasivo, GetPlanExequialRecaudoMasivoOutput>()
                .ForMember(dest => dest.RecaudoMasivoNombre, opt => opt.MapFrom(src => src.RecaudoMasivoPlanExequialRecaudoMasivo.Nombre))
                .ForMember(dest => dest.RecaudoMasivoClave, opt => opt.MapFrom(src => src.RecaudoMasivoPlanExequialRecaudoMasivo.Clave));

            CreateMap<SavePlanExequialRecaudoMasivoInput, PlanExequialRecaudoMasivo>();
            CreateMap<UpdatePlanExequialRecaudoMasivoInput, PlanExequialRecaudoMasivo>();

            CreateMap<RecaudoMasivoOutput, RecaudoMasivo>();
            CreateMap<RecaudoMasivo, ConvenioRecaudoMasivoOutput>();

            //  Mapping Grupo Informal
            CreateMap<GrupoInformal, GrupoInformalOutput>();
            CreateMap<GrupoInformal, GetGrupoInformalOutput>()
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.PersonaGrupoInformal.NumeroDocumento))
                .ForMember(dest => dest.PersonaNombre, opt => opt.MapFrom(src => src.PersonaGrupoInformal.nombreCompleto))
                .ForMember(dest => dest.SucursalNombre, opt => opt.MapFrom(src => src.SucursalGrupoInformal.Nombre))
                .ForMember(dest => dest.SucursalNombreEmpresa, opt => opt.MapFrom(src => src.SucursalGrupoInformal.EmpresaOrganizacion.EmpresaEmpresaOrganizacion.NombreComercial))
                .ForMember(dest => dest.SucursalNombreOrganizacion, opt => opt.MapFrom(src => src.SucursalGrupoInformal.EmpresaOrganizacion.OrganizacionEmpresaOrganizacion.Nombre));
            CreateMap<GrupoInformal, GrupoInformalByContactoOutput>()
                .ForMember(dest => dest.NombreContacto, opt => opt.MapFrom(src => src.PersonaGrupoInformal.nombreCompleto));


            CreateMap<SaveGrupoInformalInput, GrupoInformal>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));
            CreateMap<EmpleadoInput, GrupoInformalEmpleado>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Parse(src.FechaIngreso)))
                .ForMember(dest => dest.FechaCancelacion, opt => opt.MapFrom(src => src.FechaCancelacion == null ? new DateTime?() : DateTime.Parse(src.FechaCancelacion)));
            CreateMap<GrupoInformalEmpleado, EmpleadoByGrupoInformalOutput>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.GrupoInformalEmpleadoEmpleado.Codigo))
                .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.GrupoInformalEmpleadoEmpleado.PersonaEmpleado.nombreCompleto))
                .ForMember(dest => dest.Localidad, opt => opt.MapFrom(src => src.GrupoInformalEmpleadoEmpleado.SucursalEmpleado.SucursalDireccion.BarrioDireccion.Localidad.Nombre));


            //Mapping Gestionar Parentescos
            CreateMap<SaveParentescoInput, Parentesco>();

            CreateMap<Parentesco, GetParentescoOutput>();

            CreateMap<PlanExequial, PlanExequialBySucursalAndTipoOutput>()
                .ForMember(dest => dest.Moneda, opt => opt.MapFrom(src => src.MonedaPlanExequial.Nombre));

            CreateMap<RecaudoMasivo, RecaudosMasivosByLocalidadOutput>();
            

        }
    }
}