namespace Bow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "bow.pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Indicativo = c.String(nullable: false, maxLength: 4),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Indicativo = c.String(nullable: false, maxLength: 4),
                        PaisId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.pais", t => t.PaisId)
                .Index(t => t.PaisId);
            
            CreateTable(
                "bow.localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        DepartamentoId = c.Int(nullable: false),
                        Habitantes = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.departamento", t => t.DepartamentoId)
                .Index(t => t.DepartamentoId);
            
            CreateTable(
                "bow.barrio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 40),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.localidad", t => t.LocalidadId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.manzana", t => t.ManzanaId)
                .ForeignKey("bow.sufijo_localidad", t => t.SufijoLocalidad1Id)
                .ForeignKey("bow.sufijo_localidad", t => t.SufijoLocalidad2Id)
                .ForeignKey("bow.torie_localidad", t => t.TorieLocalidad1Id)
                .ForeignKey("bow.torie_localidad", t => t.TorieLocalidad2Id)
                .ForeignKey("bow.barrio", t => t.BarrioId)
                .Index(t => t.ManzanaId)
                .Index(t => t.BarrioId)
                .Index(t => t.TorieLocalidad1Id)
                .Index(t => t.SufijoLocalidad1Id)
                .Index(t => t.TorieLocalidad2Id)
                .Index(t => t.SufijoLocalidad2Id);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.actividad_economica", t => t.ActividadEconomicaId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .ForeignKey("bow.tipo", t => t.TipoNaturalezaId, cascadeDelete: true)
                .ForeignKey("bow.tipo_documento_persona", t => t.TipoDocumentoId)
                .ForeignKey("bow.direccion", t => t.DireccionId)
                .Index(t => t.TipoNaturalezaId)
                .Index(t => t.TipoDocumentoId)
                .Index(t => t.PersonaId)
                .Index(t => t.ActividadEconomicaId)
                .Index(t => t.DireccionId);
            
            CreateTable(
                "bow.actividad_economica",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.empresa_contacto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        TipoAreaEmpresaId = c.Int(nullable: false),
                        Cargo = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoAreaEmpresaId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .ForeignKey("bow.empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId)
                .Index(t => t.PersonaId)
                .Index(t => t.TipoAreaEmpresaId);
            
            CreateTable(
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
                        Apellido2 = c.String(nullable: false, maxLength: 50),
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoEstadoCivilId)
                .ForeignKey("bow.tipo", t => t.TipoProfesionId)
                .ForeignKey("bow.tipo_documento_persona", t => t.TipoDocumentoId)
                .ForeignKey("bow.pais", t => t.PaisId)
                .Index(t => t.TipoDocumentoId)
                .Index(t => t.TipoProfesionId)
                .Index(t => t.TipoEstadoCivilId)
                .Index(t => t.PaisId);
            
            CreateTable(
                "bow.persona_auditoria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        FechaCambio = c.DateTime(nullable: false),
                        Usuario = c.String(),
                        Cambios = c.String(nullable: false, maxLength: 3000),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .Index(t => t.PersonaId);
            
            CreateTable(
                "bow.persona_contacto_web",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        Identificador = c.String(),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .Index(t => t.PersonaId)
                .Index(t => t.TipoId);
            
            CreateTable(
                "bow.tipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        ParametroId = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 300),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.parametro", t => t.ParametroId, cascadeDelete: true)
                .Index(t => t.ParametroId);
            
            CreateTable(
                "bow.empresa_contacto_web",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        TipoRedId = c.Int(nullable: false),
                        Identificador = c.String(nullable: false, maxLength: 100),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoRedId, cascadeDelete: true)
                .ForeignKey("bow.empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId)
                .Index(t => t.TipoRedId);
            
            CreateTable(
                "bow.info_tributaria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TipoValorId = c.Int(),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.estado", t => t.EstadoId, cascadeDelete: true)
                .ForeignKey("bow.tipo", t => t.TipoValorId)
                .Index(t => t.TipoValorId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Motivo = c.String(nullable: false, maxLength: 50),
                        EstadoNombreId = c.Int(nullable: false),
                        ParametroId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.nombre_estado", t => t.EstadoNombreId, cascadeDelete: true)
                .ForeignKey("bow.parametro", t => t.ParametroId, cascadeDelete: true)
                .Index(t => t.EstadoNombreId)
                .Index(t => t.ParametroId);
            
            CreateTable(
                "bow.nombre_estado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Abreviacion = c.String(nullable: false, maxLength: 2),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
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
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.moneda", t => t.MonedaId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .Index(t => t.MonedaId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.moneda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Simbolo = c.String(nullable: false, maxLength: 10),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .Index(t => t.PersonaId)
                .Index(t => t.EstadoId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.zona", t => t.ZonaId, cascadeDelete: true)
                .ForeignKey("bow.empleado", t => t.EmpleadoId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.tipo", t => t.TipoId)
                .Index(t => t.ZonaId)
                .Index(t => t.EmpleadoId)
                .Index(t => t.TipoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.zona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(),
                        LocalidadId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoId, cascadeDelete: true)
                .ForeignKey("bow.localidad", t => t.LocalidadId)
                .Index(t => t.LocalidadId)
                .Index(t => t.TipoId);
            
            CreateTable(
                "bow.zona_barrio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZonaId = c.Int(nullable: false),
                        BarrioId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.zona", t => t.ZonaId, cascadeDelete: true)
                .ForeignKey("bow.barrio", t => t.BarrioId, cascadeDelete: true)
                .Index(t => t.ZonaId)
                .Index(t => t.BarrioId);
            
            CreateTable(
                "bow.empresa_organizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        OrganizacionId = c.Int(nullable: false),
                        EmpresaId = c.Int(nullable: false),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.organizacion", t => t.OrganizacionId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.empresa", t => t.EmpresaId)
                .Index(t => t.OrganizacionId)
                .Index(t => t.EmpresaId)
                .Index(t => t.EstadoId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.empresa_organizacion", t => t.EmpresaOrganizacionId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.tipo", t => t.TipoId)
                .ForeignKey("bow.direccion", t => t.DireccionId)
                .Index(t => t.EmpresaOrganizacionId)
                .Index(t => t.TipoId)
                .Index(t => t.EstadoId)
                .Index(t => t.DireccionId);
            
            CreateTable(
                "bow.sucursal_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SucursalId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.telefono", t => t.TelefonoId, cascadeDelete: true)
                .ForeignKey("bow.sucursal", t => t.SucursalId)
                .Index(t => t.SucursalId)
                .Index(t => t.TelefonoId);
            
            CreateTable(
                "bow.telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false, maxLength: 15),
                        Extension = c.String(maxLength: 5),
                        TipoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoId, cascadeDelete: true)
                .ForeignKey("bow.localidad", t => t.LocalidadId)
                .Index(t => t.TipoId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "bow.empresa_telefono",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpresaId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.telefono", t => t.TelefonoId, cascadeDelete: true)
                .ForeignKey("bow.empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId)
                .Index(t => t.TelefonoId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.telefono", t => t.TelefonoId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.tipo", t => t.TipoUbicacionId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .Index(t => t.PersonaId)
                .Index(t => t.TelefonoId)
                .Index(t => t.TipoUbicacionId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.organizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Logo = c.String(maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.tipo", t => t.TipoUbicacionId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .ForeignKey("bow.direccion", t => t.DireccionId)
                .Index(t => t.PersonaId)
                .Index(t => t.DireccionId)
                .Index(t => t.TipoUbicacionId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.parametro",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 80),
                        Descripcion = c.String(maxLength: 300),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 80),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.opcion_preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 80),
                        PreferenciaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.preferencia", t => t.PreferenciaId)
                .Index(t => t.PreferenciaId);
            
            CreateTable(
                "bow.persona_preferencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonaId = c.Int(nullable: false),
                        OpcionPreferenciaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.opcion_preferencia", t => t.OpcionPreferenciaId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .Index(t => t.PersonaId)
                .Index(t => t.OpcionPreferenciaId);
            
            CreateTable(
                "bow.info_tributaria_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InfoTributariaId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.localidad", t => t.LocalidadId, cascadeDelete: true)
                .ForeignKey("bow.info_tributaria", t => t.InfoTributariaId)
                .Index(t => t.InfoTributariaId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "bow.info_tributaria_opcion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        InfoTributariaId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.info_tributaria", t => t.InfoTributariaId)
                .Index(t => t.InfoTributariaId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.info_tributaria_opcion", t => t.InfoTributariaOpcionId)
                .ForeignKey("bow.empresa", t => t.EmpresaId)
                .Index(t => t.EmpresaId)
                .Index(t => t.InfoTributariaOpcionId);
            
            CreateTable(
                "bow.beneficio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoId = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo", t => t.TipoId, cascadeDelete: true)
                .Index(t => t.TipoId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.pais", t => t.PaisId)
                .Index(t => t.PaisId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.sufijo_localidad", t => t.SufijoLocalidad1Id)
                .ForeignKey("bow.sufijo_localidad", t => t.SufijoLocalidad2Id)
                .ForeignKey("bow.torie_localidad", t => t.TorieLocalidad1Id)
                .ForeignKey("bow.torie_localidad", t => t.TorieLocalidad2Id)
                .ForeignKey("bow.barrio", t => t.BarrioId)
                .Index(t => t.TorieLocalidad1Id)
                .Index(t => t.SufijoLocalidad1Id)
                .Index(t => t.TorieLocalidad2Id)
                .Index(t => t.SufijoLocalidad2Id)
                .Index(t => t.BarrioId);
            
            CreateTable(
                "bow.sufijo_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SufijoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.sufijo", t => t.SufijoId)
                .ForeignKey("bow.localidad", t => t.LocalidadId, cascadeDelete: true)
                .Index(t => t.SufijoId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "bow.sufijo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.torie_localidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoOrientacionId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.tipo_orientacion", t => t.TipoOrientacionId)
                .ForeignKey("bow.localidad", t => t.LocalidadId, cascadeDelete: true)
                .Index(t => t.TipoOrientacionId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "bow.tipo_orientacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.avenida",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.localidad", t => t.LocalidadId, cascadeDelete: true)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "bow.periodo_venta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        Nombre = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("bow.avenida", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.tipo_documento_persona", "PaisId", "bow.pais");
            DropForeignKey("bow.persona", "PaisId", "bow.pais");
            DropForeignKey("bow.departamento", "PaisId", "bow.pais");
            DropForeignKey("bow.localidad", "DepartamentoId", "bow.departamento");
            DropForeignKey("bow.zona", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.torie_localidad", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.telefono", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.sufijo_localidad", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.barrio", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.zona_barrio", "BarrioId", "bow.barrio");
            DropForeignKey("bow.manzana", "BarrioId", "bow.barrio");
            DropForeignKey("bow.direccion", "BarrioId", "bow.barrio");
            DropForeignKey("bow.sucursal", "DireccionId", "bow.direccion");
            DropForeignKey("bow.persona_direccion", "DireccionId", "bow.direccion");
            DropForeignKey("bow.torie_localidad", "TipoOrientacionId", "bow.tipo_orientacion");
            DropForeignKey("bow.manzana", "TorieLocalidad2Id", "bow.torie_localidad");
            DropForeignKey("bow.manzana", "TorieLocalidad1Id", "bow.torie_localidad");
            DropForeignKey("bow.direccion", "TorieLocalidad2Id", "bow.torie_localidad");
            DropForeignKey("bow.direccion", "TorieLocalidad1Id", "bow.torie_localidad");
            DropForeignKey("bow.sufijo_localidad", "SufijoId", "bow.sufijo");
            DropForeignKey("bow.manzana", "SufijoLocalidad2Id", "bow.sufijo_localidad");
            DropForeignKey("bow.manzana", "SufijoLocalidad1Id", "bow.sufijo_localidad");
            DropForeignKey("bow.direccion", "SufijoLocalidad2Id", "bow.sufijo_localidad");
            DropForeignKey("bow.direccion", "SufijoLocalidad1Id", "bow.sufijo_localidad");
            DropForeignKey("bow.direccion", "ManzanaId", "bow.manzana");
            DropForeignKey("bow.empresa", "DireccionId", "bow.direccion");
            DropForeignKey("bow.empresa_telefono", "EmpresaId", "bow.empresa");
            DropForeignKey("bow.empresa_organizacion", "EmpresaId", "bow.empresa");
            DropForeignKey("bow.empresa_info_tributaria", "EmpresaId", "bow.empresa");
            DropForeignKey("bow.empresa_contacto_web", "EmpresaId", "bow.empresa");
            DropForeignKey("bow.empresa_contacto", "EmpresaId", "bow.empresa");
            DropForeignKey("bow.persona", "TipoDocumentoId", "bow.tipo_documento_persona");
            DropForeignKey("bow.empresa", "TipoDocumentoId", "bow.tipo_documento_persona");
            DropForeignKey("bow.persona_telefono", "PersonaId", "bow.persona");
            DropForeignKey("bow.persona_preferencia", "PersonaId", "bow.persona");
            DropForeignKey("bow.empresa_contacto", "PersonaId", "bow.persona");
            DropForeignKey("bow.empleado", "PersonaId", "bow.persona");
            DropForeignKey("bow.persona_direccion", "PersonaId", "bow.persona");
            DropForeignKey("bow.persona_contacto_web", "PersonaId", "bow.persona");
            DropForeignKey("bow.zona", "TipoId", "bow.tipo");
            DropForeignKey("bow.zona_empleado", "TipoId", "bow.tipo");
            DropForeignKey("bow.beneficio", "TipoId", "bow.tipo");
            DropForeignKey("bow.telefono", "TipoId", "bow.tipo");
            DropForeignKey("bow.sucursal", "TipoId", "bow.tipo");
            DropForeignKey("bow.persona_telefono", "TipoUbicacionId", "bow.tipo");
            DropForeignKey("bow.persona", "TipoProfesionId", "bow.tipo");
            DropForeignKey("bow.persona", "TipoEstadoCivilId", "bow.tipo");
            DropForeignKey("bow.persona_direccion", "TipoUbicacionId", "bow.tipo");
            DropForeignKey("bow.persona_contacto_web", "TipoId", "bow.tipo");
            DropForeignKey("bow.info_tributaria", "TipoValorId", "bow.tipo");
            DropForeignKey("bow.info_tributaria_opcion", "InfoTributariaId", "bow.info_tributaria");
            DropForeignKey("bow.empresa_info_tributaria", "InfoTributariaOpcionId", "bow.info_tributaria_opcion");
            DropForeignKey("bow.info_tributaria_localidad", "InfoTributariaId", "bow.info_tributaria");
            DropForeignKey("bow.info_tributaria_localidad", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.sucursal", "EstadoId", "bow.estado");
            DropForeignKey("bow.preferencia", "EstadoId", "bow.estado");
            DropForeignKey("bow.opcion_preferencia", "PreferenciaId", "bow.preferencia");
            DropForeignKey("bow.persona_preferencia", "OpcionPreferenciaId", "bow.opcion_preferencia");
            DropForeignKey("bow.tipo", "ParametroId", "bow.parametro");
            DropForeignKey("bow.estado", "ParametroId", "bow.parametro");
            DropForeignKey("bow.info_tributaria", "EstadoId", "bow.estado");
            DropForeignKey("bow.zona_empleado", "EstadoId", "bow.estado");
            DropForeignKey("bow.persona_telefono", "EstadoId", "bow.estado");
            DropForeignKey("bow.persona_direccion", "EstadoId", "bow.estado");
            DropForeignKey("bow.empresa_organizacion", "EstadoId", "bow.estado");
            DropForeignKey("bow.empresa_organizacion", "OrganizacionId", "bow.organizacion");
            DropForeignKey("bow.sucursal", "EmpresaOrganizacionId", "bow.empresa_organizacion");
            DropForeignKey("bow.sucursal_telefono", "SucursalId", "bow.sucursal");
            DropForeignKey("bow.sucursal_telefono", "TelefonoId", "bow.telefono");
            DropForeignKey("bow.persona_telefono", "TelefonoId", "bow.telefono");
            DropForeignKey("bow.empresa_telefono", "TelefonoId", "bow.telefono");
            DropForeignKey("bow.empleado", "EstadoId", "bow.estado");
            DropForeignKey("bow.zona_empleado", "EmpleadoId", "bow.empleado");
            DropForeignKey("bow.zona_empleado", "ZonaId", "bow.zona");
            DropForeignKey("bow.zona_barrio", "ZonaId", "bow.zona");
            DropForeignKey("bow.plan_exequial", "EstadoId", "bow.estado");
            DropForeignKey("bow.plan_exequial", "MonedaId", "bow.moneda");
            DropForeignKey("bow.estado", "EstadoNombreId", "bow.nombre_estado");
            DropForeignKey("bow.empresa", "TipoNaturalezaId", "bow.tipo");
            DropForeignKey("bow.empresa_contacto_web", "TipoRedId", "bow.tipo");
            DropForeignKey("bow.empresa_contacto", "TipoAreaEmpresaId", "bow.tipo");
            DropForeignKey("bow.persona_auditoria", "PersonaId", "bow.persona");
            DropForeignKey("bow.empresa", "PersonaId", "bow.persona");
            DropForeignKey("bow.empresa", "ActividadEconomicaId", "bow.actividad_economica");
            DropIndex("bow.avenida", new[] { "LocalidadId" });
            DropIndex("bow.torie_localidad", new[] { "LocalidadId" });
            DropIndex("bow.torie_localidad", new[] { "TipoOrientacionId" });
            DropIndex("bow.sufijo_localidad", new[] { "LocalidadId" });
            DropIndex("bow.sufijo_localidad", new[] { "SufijoId" });
            DropIndex("bow.manzana", new[] { "BarrioId" });
            DropIndex("bow.manzana", new[] { "SufijoLocalidad2Id" });
            DropIndex("bow.manzana", new[] { "TorieLocalidad2Id" });
            DropIndex("bow.manzana", new[] { "SufijoLocalidad1Id" });
            DropIndex("bow.manzana", new[] { "TorieLocalidad1Id" });
            DropIndex("bow.tipo_documento_persona", new[] { "PaisId" });
            DropIndex("bow.beneficio", new[] { "TipoId" });
            DropIndex("bow.empresa_info_tributaria", new[] { "InfoTributariaOpcionId" });
            DropIndex("bow.empresa_info_tributaria", new[] { "EmpresaId" });
            DropIndex("bow.info_tributaria_opcion", new[] { "InfoTributariaId" });
            DropIndex("bow.info_tributaria_localidad", new[] { "LocalidadId" });
            DropIndex("bow.info_tributaria_localidad", new[] { "InfoTributariaId" });
            DropIndex("bow.persona_preferencia", new[] { "OpcionPreferenciaId" });
            DropIndex("bow.persona_preferencia", new[] { "PersonaId" });
            DropIndex("bow.opcion_preferencia", new[] { "PreferenciaId" });
            DropIndex("bow.preferencia", new[] { "EstadoId" });
            DropIndex("bow.persona_direccion", new[] { "EstadoId" });
            DropIndex("bow.persona_direccion", new[] { "TipoUbicacionId" });
            DropIndex("bow.persona_direccion", new[] { "DireccionId" });
            DropIndex("bow.persona_direccion", new[] { "PersonaId" });
            DropIndex("bow.persona_telefono", new[] { "EstadoId" });
            DropIndex("bow.persona_telefono", new[] { "TipoUbicacionId" });
            DropIndex("bow.persona_telefono", new[] { "TelefonoId" });
            DropIndex("bow.persona_telefono", new[] { "PersonaId" });
            DropIndex("bow.empresa_telefono", new[] { "TelefonoId" });
            DropIndex("bow.empresa_telefono", new[] { "EmpresaId" });
            DropIndex("bow.telefono", new[] { "LocalidadId" });
            DropIndex("bow.telefono", new[] { "TipoId" });
            DropIndex("bow.sucursal_telefono", new[] { "TelefonoId" });
            DropIndex("bow.sucursal_telefono", new[] { "SucursalId" });
            DropIndex("bow.sucursal", new[] { "DireccionId" });
            DropIndex("bow.sucursal", new[] { "EstadoId" });
            DropIndex("bow.sucursal", new[] { "TipoId" });
            DropIndex("bow.sucursal", new[] { "EmpresaOrganizacionId" });
            DropIndex("bow.empresa_organizacion", new[] { "EstadoId" });
            DropIndex("bow.empresa_organizacion", new[] { "EmpresaId" });
            DropIndex("bow.empresa_organizacion", new[] { "OrganizacionId" });
            DropIndex("bow.zona_barrio", new[] { "BarrioId" });
            DropIndex("bow.zona_barrio", new[] { "ZonaId" });
            DropIndex("bow.zona", new[] { "TipoId" });
            DropIndex("bow.zona", new[] { "LocalidadId" });
            DropIndex("bow.zona_empleado", new[] { "EstadoId" });
            DropIndex("bow.zona_empleado", new[] { "TipoId" });
            DropIndex("bow.zona_empleado", new[] { "EmpleadoId" });
            DropIndex("bow.zona_empleado", new[] { "ZonaId" });
            DropIndex("bow.empleado", new[] { "EstadoId" });
            DropIndex("bow.empleado", new[] { "PersonaId" });
            DropIndex("bow.plan_exequial", new[] { "EstadoId" });
            DropIndex("bow.plan_exequial", new[] { "MonedaId" });
            DropIndex("bow.estado", new[] { "ParametroId" });
            DropIndex("bow.estado", new[] { "EstadoNombreId" });
            DropIndex("bow.info_tributaria", new[] { "EstadoId" });
            DropIndex("bow.info_tributaria", new[] { "TipoValorId" });
            DropIndex("bow.empresa_contacto_web", new[] { "TipoRedId" });
            DropIndex("bow.empresa_contacto_web", new[] { "EmpresaId" });
            DropIndex("bow.tipo", new[] { "ParametroId" });
            DropIndex("bow.persona_contacto_web", new[] { "TipoId" });
            DropIndex("bow.persona_contacto_web", new[] { "PersonaId" });
            DropIndex("bow.persona_auditoria", new[] { "PersonaId" });
            DropIndex("bow.persona", new[] { "PaisId" });
            DropIndex("bow.persona", new[] { "TipoEstadoCivilId" });
            DropIndex("bow.persona", new[] { "TipoProfesionId" });
            DropIndex("bow.persona", new[] { "TipoDocumentoId" });
            DropIndex("bow.empresa_contacto", new[] { "TipoAreaEmpresaId" });
            DropIndex("bow.empresa_contacto", new[] { "PersonaId" });
            DropIndex("bow.empresa_contacto", new[] { "EmpresaId" });
            DropIndex("bow.empresa", new[] { "DireccionId" });
            DropIndex("bow.empresa", new[] { "ActividadEconomicaId" });
            DropIndex("bow.empresa", new[] { "PersonaId" });
            DropIndex("bow.empresa", new[] { "TipoDocumentoId" });
            DropIndex("bow.empresa", new[] { "TipoNaturalezaId" });
            DropIndex("bow.direccion", new[] { "SufijoLocalidad2Id" });
            DropIndex("bow.direccion", new[] { "TorieLocalidad2Id" });
            DropIndex("bow.direccion", new[] { "SufijoLocalidad1Id" });
            DropIndex("bow.direccion", new[] { "TorieLocalidad1Id" });
            DropIndex("bow.direccion", new[] { "BarrioId" });
            DropIndex("bow.direccion", new[] { "ManzanaId" });
            DropIndex("bow.barrio", new[] { "LocalidadId" });
            DropIndex("bow.localidad", new[] { "DepartamentoId" });
            DropIndex("bow.departamento", new[] { "PaisId" });
            DropTable("bow.periodo_venta");
            DropTable("bow.avenida");
            DropTable("bow.tipo_orientacion");
            DropTable("bow.torie_localidad");
            DropTable("bow.sufijo");
            DropTable("bow.sufijo_localidad");
            DropTable("bow.manzana");
            DropTable("bow.tipo_documento_persona");
            DropTable("bow.beneficio");
            DropTable("bow.empresa_info_tributaria");
            DropTable("bow.info_tributaria_opcion");
            DropTable("bow.info_tributaria_localidad");
            DropTable("bow.persona_preferencia");
            DropTable("bow.opcion_preferencia");
            DropTable("bow.preferencia");
            DropTable("bow.parametro");
            DropTable("bow.persona_direccion");
            DropTable("bow.organizacion");
            DropTable("bow.persona_telefono");
            DropTable("bow.empresa_telefono");
            DropTable("bow.telefono");
            DropTable("bow.sucursal_telefono");
            DropTable("bow.sucursal");
            DropTable("bow.empresa_organizacion");
            DropTable("bow.zona_barrio");
            DropTable("bow.zona");
            DropTable("bow.zona_empleado");
            DropTable("bow.empleado");
            DropTable("bow.moneda");
            DropTable("bow.plan_exequial");
            DropTable("bow.nombre_estado");
            DropTable("bow.estado");
            DropTable("bow.info_tributaria");
            DropTable("bow.empresa_contacto_web");
            DropTable("bow.tipo");
            DropTable("bow.persona_contacto_web");
            DropTable("bow.persona_auditoria");
            DropTable("bow.persona");
            DropTable("bow.empresa_contacto");
            DropTable("bow.actividad_economica");
            DropTable("bow.empresa");
            DropTable("bow.direccion");
            DropTable("bow.barrio");
            DropTable("bow.localidad");
            DropTable("bow.departamento");
            DropTable("bow.pais");
        }
    }
}
