namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class TenantFilter : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "bow.organizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Logo = c.String(maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Organizacion_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_organizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        OrganizacionId = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaOrganizacion_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoNaturalezaId = c.Int(nullable: false),
                        TipoDocumentoId = c.Int(nullable: false),
                        Documento = c.String(nullable: false, maxLength: 30),
                        RazonSocial = c.String(nullable: false, maxLength: 100),
                        NombreComercial = c.String(maxLength: 100),
                        PersonaId = c.Int(),
                        ActividadEconomicaId = c.Int(nullable: false),
                        DireccionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Empresa_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.actividad_economica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ActividadEconomica_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.direccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 150),
                        Pista = c.String(maxLength: 300),
                        ManzanaId = c.Int(),
                        BarrioId = c.Int(),
                        TorieLocalidad1Id = c.Int(),
                        Orientacion1 = c.Int(),
                        SufijoLocalidad1Id = c.Int(),
                        TorieLocalidad2Id = c.Int(),
                        Orientacion2 = c.Int(),
                        SufijoLocalidad2Id = c.Int(),
                        Porton = c.String(maxLength: 20),
                        Apartamento = c.String(maxLength: 10),
                        DireccionCompleta = c.String(maxLength: 150),
                        ZipCode = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Direccion_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.barrio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 40),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Barrio_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        DepartamentoId = c.Int(nullable: false),
                        Habitantes = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Localidad_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.afiliado_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GestionProspectoId = c.Int(nullable: false),
                        ParentescoId = c.Int(nullable: false),
                        Nombre = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        Edad = c.Int(nullable: false),
                        CiudadResidenciaId = c.Int(nullable: false),
                        BebePorNacer = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AfiliadoProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.gestion_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProspectoId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        PersonaId = c.Int(),
                        EstadoNoAfiliacionId = c.Int(),
                        FunerariaAfiliadoId = c.Int(),
                        GrupoFamiliarId = c.Int(),
                        SucursalId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        FechaGestion = c.DateTime(nullable: false),
                        FechaBloqueo = c.DateTime(),
                        EmpresaAfiliada = c.String(maxLength: 50),
                        Observaciones = c.String(maxLength: 500),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GestionProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficios_gestion_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GestionProspectoId = c.Int(nullable: false),
                        BeneficioAdicionalPlanExequialId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BeneficiosGestionProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficio_adicional_plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        BeneficioId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        Asignables = c.Int(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        Valor = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        BeneficioPlanExequialId = c.Int(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BeneficioAdicionalPlanExequial_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Beneficio_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficio_plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        BeneficioId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        Asignables = c.Int(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BeneficioPlanExequial_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Motivo = c.String(nullable: false, maxLength: 50),
                        EstadoNombreId = c.Int(nullable: false),
                        ParametroId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Estado_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_informal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        PorcentajeDescuento = c.Int(nullable: false),
                        EncargadoExento = c.Boolean(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoInformal_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_informal_empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        GrupoInformalId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoInformalEmpleado_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Empleado_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieneDocumento = c.Boolean(nullable: false),
                        TipoDocumentoId = c.Int(),
                        NumeroDocumento = c.String(maxLength: 30),
                        FechaExpDocumento = c.DateTime(),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellido1 = c.String(nullable: false, maxLength: 50),
                        Apellido2 = c.String(maxLength: 50),
                        TieneFechaNacimiento = c.Boolean(),
                        FechaNacimiento = c.DateTime(),
                        Genero = c.String(nullable: false, maxLength: 1),
                        CorreoElectronico = c.String(maxLength: 80),
                        ContactarCorreo = c.Boolean(nullable: false),
                        ContactarSms = c.Boolean(nullable: false),
                        ContactarTelefono = c.Boolean(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        TipoProfesionId = c.Int(nullable: false),
                        TipoEstadoCivilId = c.Int(nullable: false),
                        Discapacitada = c.Boolean(nullable: false),
                        FechaFallecimiento = c.DateTime(),
                        PaisId = c.Int(nullable: false),
                        FechaUltActualizacion = c.DateTime(nullable: false),
                        Usuario = c.String(maxLength: 10),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Persona_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_auditoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(nullable: false),
                        Usuario = c.String(),
                        Cambios = c.String(nullable: false, maxLength: 3000),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaAuditoria_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_contacto_web",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Identificador = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaContactoWeb_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.tipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        ParametroId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 300),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Tipo_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_contacto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        TipoAreaEmpresaId = c.Int(nullable: false),
                        Cargo = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaContacto_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_contacto_web",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        TipoRedId = c.Int(nullable: false),
                        Identificador = c.String(nullable: false, maxLength: 100),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaContactoWeb_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.info_tributaria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TipoValorId = c.Int(),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_InfoTributaria_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.info_tributaria_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InfoTributariaId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_InfoTributariaLocalidad_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.info_tributaria_opcion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        InfoTributariaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_InfoTributariaOpcion_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_info_tributaria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        InfoTributariaOpcionId = c.Int(nullable: false),
                        Valor = c.String(nullable: false, maxLength: 20),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(),
                        FechaActualizacion = c.DateTime(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaInfoTributaria_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.parametro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 300),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Parametro_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_direccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        DireccionId = c.Int(nullable: false),
                        TipoUbicacionId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        UsuarioIngreso = c.String(),
                        FechaCancelacion = c.DateTime(),
                        UsuarioCancelacion = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaDireccion_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TipoUbicacionId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        UsuarioIngreso = c.String(),
                        FechaCancelacion = c.DateTime(),
                        UsuarioCancelacion = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaTelefono_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false, maxLength: 15),
                        Extension = c.String(maxLength: 5),
                        TipoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Telefono_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaTelefono_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DireccionId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Prospecto_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.sucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaOrganizacionId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TipoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        DireccionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Sucursal_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.plan_exequial_sucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PlanExequialSucursal_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        Descripcion = c.String(nullable: false, maxLength: 300),
                        PlanParaGrupo = c.Boolean(nullable: false),
                        PlanFamiliar = c.Boolean(nullable: false),
                        PlanEmpresarial = c.Boolean(nullable: false),
                        MonedaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        cantidadDiasMora = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PlanExequial_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_familiar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        CantidadMaximaAfiliados = c.Int(),
                        PermitirAfiliadosAdicionales = c.String(nullable: false, maxLength: 1),
                        ValorPlan = c.Int(nullable: false),
                        TieneCuotaInicial = c.String(nullable: false, maxLength: 1),
                        ValorCuotaInicial = c.Int(),
                        PlanExequialId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoFamiliar_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_familiar_parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentescoId = c.Int(nullable: false),
                        GrupoFamiliarId = c.Int(nullable: false),
                        ValidarSoloIngreso = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoFamiliarParentesco_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_parentesco_rango",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GrupoFamiliarParentescoId = c.Int(nullable: false),
                        EdadMinima = c.Int(nullable: false),
                        EdadMaxima = c.Int(nullable: false),
                        PeriodoCarencia = c.Int(nullable: false),
                        UnidadPeriodoCarencia = c.String(nullable: false, maxLength: 1),
                        TipoValorBasico = c.String(nullable: false, maxLength: 1),
                        ValorBasico = c.Int(nullable: false),
                        TipoValorAdicional = c.String(nullable: false, maxLength: 1),
                        ValorAdicional = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoParentescoRango_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Posicion = c.Int(nullable: false),
                        Genero = c.String(nullable: false, maxLength: 1),
                        Repeticiones = c.Int(nullable: false),
                        Limite = c.String(),
                        EdadDiferencia = c.Int(),
                        CoincidirApellidos = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Parentesco_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.moneda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Simbolo = c.String(nullable: false, maxLength: 10),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Moneda_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.plan_exequial_recaudo_masivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        RecaudoMasivoId = c.Int(nullable: false),
                        EsObligatorio = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PlanExequialRecaudoMasivo_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.recaudo_masivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Clave = c.String(nullable: false, maxLength: 30),
                        OrganizacionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_RecaudoMasivo_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.recaudo_masivo_cobertura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecaudoMasivoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_RecaudoMasivoCobertura_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.sucursal_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SucursalId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_SucursalTelefono_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.zona_empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaAsignacion = c.DateTime(),
                        FechaRetiro = c.DateTime(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ZonaEmpleado_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.zona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(),
                        LocalidadId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Zona_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.zona_barrio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        BarrioId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ZonaBarrio_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Indicativo = c.String(nullable: false, maxLength: 4),
                        PaisId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Departamento_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.tipo_documento_persona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        LongitudMinima = c.Int(nullable: false),
                        LongitudMaxima = c.Int(nullable: false),
                        ConjuntoCaracteres = c.String(nullable: false, maxLength: 1),
                        EdadMinima = c.Int(),
                        EdadMaxima = c.Int(),
                        Default = c.String(nullable: false, maxLength: 1),
                        AplicaEmpresa = c.String(nullable: false, maxLength: 1),
                        AplicaPersona = c.String(nullable: false, maxLength: 1),
                        PaisId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TipoDocumentoPersona_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        OpcionPreferenciaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaPreferencia_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.opcion_preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 80),
                        PreferenciaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_OpcionPreferencia_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 80),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Preferencia_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.nombre_estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Abreviacion = c.String(nullable: false, maxLength: 2),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_NombreEstado_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.funeraria_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_FunerariaProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.sufijo_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SufijoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_SufijoLocalidad_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.manzana",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50),
                        TorieLocalidad1Id = c.Int(),
                        Orientacion1 = c.Int(),
                        SufijoLocalidad1Id = c.Int(),
                        TorieLocalidad2Id = c.Int(),
                        Orientacion2 = c.Int(),
                        SufijoLocalidad2Id = c.Int(),
                        BarrioId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Manzana_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.torie_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoOrientacionId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TorieLocalidad_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.tipo_orientacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TipoOrientacion_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.sufijo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Sufijo_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.avenida",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Avenida_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "bow.periodo_venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        Nombre = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PeriodoVenta_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
                "bow.periodo_venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        Nombre = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PeriodoVenta_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.avenida",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Avenida_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.sufijo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Sufijo_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.tipo_orientacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TipoOrientacion_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.torie_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoOrientacionId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TorieLocalidad_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.manzana",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50),
                        TorieLocalidad1Id = c.Int(),
                        Orientacion1 = c.Int(),
                        SufijoLocalidad1Id = c.Int(),
                        TorieLocalidad2Id = c.Int(),
                        Orientacion2 = c.Int(),
                        SufijoLocalidad2Id = c.Int(),
                        BarrioId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Manzana_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.sufijo_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SufijoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_SufijoLocalidad_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.funeraria_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_FunerariaProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.nombre_estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Abreviacion = c.String(nullable: false, maxLength: 2),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_NombreEstado_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 80),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Preferencia_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.opcion_preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 80),
                        PreferenciaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_OpcionPreferencia_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        OpcionPreferenciaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaPreferencia_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.tipo_documento_persona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        LongitudMinima = c.Int(nullable: false),
                        LongitudMaxima = c.Int(nullable: false),
                        ConjuntoCaracteres = c.String(nullable: false, maxLength: 1),
                        EdadMinima = c.Int(),
                        EdadMaxima = c.Int(),
                        Default = c.String(nullable: false, maxLength: 1),
                        AplicaEmpresa = c.String(nullable: false, maxLength: 1),
                        AplicaPersona = c.String(nullable: false, maxLength: 1),
                        PaisId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TipoDocumentoPersona_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Indicativo = c.String(nullable: false, maxLength: 4),
                        PaisId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Departamento_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.zona_barrio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        BarrioId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ZonaBarrio_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.zona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(),
                        LocalidadId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Zona_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.zona_empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaAsignacion = c.DateTime(),
                        FechaRetiro = c.DateTime(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ZonaEmpleado_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.sucursal_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SucursalId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_SucursalTelefono_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.recaudo_masivo_cobertura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecaudoMasivoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_RecaudoMasivoCobertura_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.recaudo_masivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Clave = c.String(nullable: false, maxLength: 30),
                        OrganizacionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_RecaudoMasivo_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.plan_exequial_recaudo_masivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        RecaudoMasivoId = c.Int(nullable: false),
                        EsObligatorio = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PlanExequialRecaudoMasivo_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.moneda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Simbolo = c.String(nullable: false, maxLength: 10),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Moneda_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Posicion = c.Int(nullable: false),
                        Genero = c.String(nullable: false, maxLength: 1),
                        Repeticiones = c.Int(nullable: false),
                        Limite = c.String(),
                        EdadDiferencia = c.Int(),
                        CoincidirApellidos = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Parentesco_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_parentesco_rango",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GrupoFamiliarParentescoId = c.Int(nullable: false),
                        EdadMinima = c.Int(nullable: false),
                        EdadMaxima = c.Int(nullable: false),
                        PeriodoCarencia = c.Int(nullable: false),
                        UnidadPeriodoCarencia = c.String(nullable: false, maxLength: 1),
                        TipoValorBasico = c.String(nullable: false, maxLength: 1),
                        ValorBasico = c.Int(nullable: false),
                        TipoValorAdicional = c.String(nullable: false, maxLength: 1),
                        ValorAdicional = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoParentescoRango_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_familiar_parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentescoId = c.Int(nullable: false),
                        GrupoFamiliarId = c.Int(nullable: false),
                        ValidarSoloIngreso = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoFamiliarParentesco_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_familiar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(nullable: false, maxLength: 200),
                        CantidadMaximaAfiliados = c.Int(),
                        PermitirAfiliadosAdicionales = c.String(nullable: false, maxLength: 1),
                        ValorPlan = c.Int(nullable: false),
                        TieneCuotaInicial = c.String(nullable: false, maxLength: 1),
                        ValorCuotaInicial = c.Int(),
                        PlanExequialId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoFamiliar_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        Descripcion = c.String(nullable: false, maxLength: 300),
                        PlanParaGrupo = c.Boolean(nullable: false),
                        PlanFamiliar = c.Boolean(nullable: false),
                        PlanEmpresarial = c.Boolean(nullable: false),
                        MonedaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        cantidadDiasMora = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PlanExequial_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.plan_exequial_sucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PlanExequialSucursal_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.sucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaOrganizacionId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TipoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        DireccionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Sucursal_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DireccionId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Prospecto_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaTelefono_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false, maxLength: 15),
                        Extension = c.String(maxLength: 5),
                        TipoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Telefono_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TipoUbicacionId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        UsuarioIngreso = c.String(),
                        FechaCancelacion = c.DateTime(),
                        UsuarioCancelacion = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaTelefono_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_direccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        DireccionId = c.Int(nullable: false),
                        TipoUbicacionId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        UsuarioIngreso = c.String(),
                        FechaCancelacion = c.DateTime(),
                        UsuarioCancelacion = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaDireccion_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.parametro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 300),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Parametro_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_info_tributaria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        InfoTributariaOpcionId = c.Int(nullable: false),
                        Valor = c.String(nullable: false, maxLength: 20),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(),
                        FechaActualizacion = c.DateTime(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaInfoTributaria_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.info_tributaria_opcion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        InfoTributariaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_InfoTributariaOpcion_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.info_tributaria_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InfoTributariaId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_InfoTributariaLocalidad_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.info_tributaria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TipoValorId = c.Int(),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_InfoTributaria_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_contacto_web",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        TipoRedId = c.Int(nullable: false),
                        Identificador = c.String(nullable: false, maxLength: 100),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaContactoWeb_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_contacto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        TipoAreaEmpresaId = c.Int(nullable: false),
                        Cargo = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaContacto_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.tipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        ParametroId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 300),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Tipo_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_contacto_web",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Identificador = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaContactoWeb_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona_auditoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(nullable: false),
                        Usuario = c.String(),
                        Cambios = c.String(nullable: false, maxLength: 3000),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PersonaAuditoria_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.persona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TieneDocumento = c.Boolean(nullable: false),
                        TipoDocumentoId = c.Int(),
                        NumeroDocumento = c.String(maxLength: 30),
                        FechaExpDocumento = c.DateTime(),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellido1 = c.String(nullable: false, maxLength: 50),
                        Apellido2 = c.String(maxLength: 50),
                        TieneFechaNacimiento = c.Boolean(),
                        FechaNacimiento = c.DateTime(),
                        Genero = c.String(nullable: false, maxLength: 1),
                        CorreoElectronico = c.String(maxLength: 80),
                        ContactarCorreo = c.Boolean(nullable: false),
                        ContactarSms = c.Boolean(nullable: false),
                        ContactarTelefono = c.Boolean(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        TipoProfesionId = c.Int(nullable: false),
                        TipoEstadoCivilId = c.Int(nullable: false),
                        Discapacitada = c.Boolean(nullable: false),
                        FechaFallecimiento = c.DateTime(),
                        PaisId = c.Int(nullable: false),
                        FechaUltActualizacion = c.DateTime(nullable: false),
                        Usuario = c.String(maxLength: 10),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Persona_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Empleado_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_informal_empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        GrupoInformalId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoInformalEmpleado_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.grupo_informal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        FechaIngreso = c.DateTime(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        PorcentajeDescuento = c.Int(nullable: false),
                        EncargadoExento = c.Boolean(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GrupoInformal_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Motivo = c.String(nullable: false, maxLength: 50),
                        EstadoNombreId = c.Int(nullable: false),
                        ParametroId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Estado_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficio_plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        BeneficioId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        Asignables = c.Int(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BeneficioPlanExequial_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Beneficio_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficio_adicional_plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        BeneficioId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        Asignables = c.Int(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        Valor = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        BeneficioPlanExequialId = c.Int(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BeneficioAdicionalPlanExequial_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.beneficios_gestion_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GestionProspectoId = c.Int(nullable: false),
                        BeneficioAdicionalPlanExequialId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_BeneficiosGestionProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.gestion_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProspectoId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        PersonaId = c.Int(),
                        EstadoNoAfiliacionId = c.Int(),
                        FunerariaAfiliadoId = c.Int(),
                        GrupoFamiliarId = c.Int(),
                        SucursalId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        FechaGestion = c.DateTime(nullable: false),
                        FechaBloqueo = c.DateTime(),
                        EmpresaAfiliada = c.String(maxLength: 50),
                        Observaciones = c.String(maxLength: 500),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GestionProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.afiliado_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GestionProspectoId = c.Int(nullable: false),
                        ParentescoId = c.Int(nullable: false),
                        Nombre = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        Edad = c.Int(nullable: false),
                        CiudadResidenciaId = c.Int(nullable: false),
                        BebePorNacer = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_AfiliadoProspecto_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        DepartamentoId = c.Int(nullable: false),
                        Habitantes = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Localidad_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.barrio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 40),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Barrio_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.direccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 150),
                        Pista = c.String(maxLength: 300),
                        ManzanaId = c.Int(),
                        BarrioId = c.Int(),
                        TorieLocalidad1Id = c.Int(),
                        Orientacion1 = c.Int(),
                        SufijoLocalidad1Id = c.Int(),
                        TorieLocalidad2Id = c.Int(),
                        Orientacion2 = c.Int(),
                        SufijoLocalidad2Id = c.Int(),
                        Porton = c.String(maxLength: 20),
                        Apartamento = c.String(maxLength: 10),
                        DireccionCompleta = c.String(maxLength: 150),
                        ZipCode = c.String(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Direccion_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.actividad_economica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ActividadEconomica_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoNaturalezaId = c.Int(nullable: false),
                        TipoDocumentoId = c.Int(nullable: false),
                        Documento = c.String(nullable: false, maxLength: 30),
                        RazonSocial = c.String(nullable: false, maxLength: 100),
                        NombreComercial = c.String(maxLength: 100),
                        PersonaId = c.Int(),
                        ActividadEconomicaId = c.Int(nullable: false),
                        DireccionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Empresa_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.empresa_organizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        OrganizacionId = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_EmpresaOrganizacion_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "bow.organizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Logo = c.String(maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Organizacion_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
