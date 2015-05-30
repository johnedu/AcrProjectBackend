namespace Bow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion_marzo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("bow.info_tributaria_localidad", "LocalidadId", "bow.localidad");
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.gestion_prospecto", t => t.GestionProspectoId)
                .ForeignKey("bow.parentesco", t => t.ParentescoId)
                .ForeignKey("bow.localidad", t => t.CiudadResidenciaId)
                .Index(t => t.GestionProspectoId)
                .Index(t => t.ParentescoId)
                .Index(t => t.CiudadResidenciaId);
            
            CreateTable(
                "bow.gestion_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProspectoId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        PersonaId = c.Int(),
                        EstadoNoAfiliacionId = c.Int(),
                        FunerariaAfiliadoId = c.Int(),
                        FechaGestion = c.DateTime(nullable: false),
                        FechaBloqueo = c.DateTime(),
                        EmpresaAfiliada = c.String(maxLength: 50),
                        Observaciones = c.String(maxLength: 500),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.empleado", t => t.EmpleadoId)
                .ForeignKey("bow.estado", t => t.EstadoNoAfiliacionId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .ForeignKey("bow.prospecto", t => t.ProspectoId)
                .ForeignKey("bow.funeraria_prospecto", t => t.FunerariaAfiliadoId)
                .Index(t => t.ProspectoId)
                .Index(t => t.EmpleadoId)
                .Index(t => t.PersonaId)
                .Index(t => t.EstadoNoAfiliacionId)
                .Index(t => t.FunerariaAfiliadoId);
            
            CreateTable(
                "bow.beneficios_gestion_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GestionProspectoId = c.Int(nullable: false),
                        BeneficioPlanExequialId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.beneficio_plan_exequial", t => t.BeneficioPlanExequialId)
                .ForeignKey("bow.gestion_prospecto", t => t.GestionProspectoId)
                .Index(t => t.GestionProspectoId)
                .Index(t => t.BeneficioPlanExequialId);
            
            CreateTable(
                "bow.beneficio_plan_exequial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        BeneficioId = c.Int(nullable: false),
                        TipoId = c.Int(nullable: false),
                        FechaIngreso = c.DateTime(nullable: false),
                        EsAsignable = c.String(nullable: false, maxLength: 1),
                        Valor = c.Int(nullable: false),
                        FechaCancelacion = c.DateTime(),
                        CantidadAsignables = c.Int(nullable: false),
                        TipoModificable = c.String(maxLength: 1),
                        EstadoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.beneficio", t => t.BeneficioId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.plan_exequial", t => t.PlanExequialId)
                .ForeignKey("bow.tipo", t => t.TipoId)
                .Index(t => t.PlanExequialId)
                .Index(t => t.BeneficioId)
                .Index(t => t.TipoId)
                .Index(t => t.EstadoId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.plan_exequial", t => t.PlanExequialId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .Index(t => t.PlanExequialId)
                .Index(t => t.EstadoId);
            
            CreateTable(
                "bow.grupo_familiar_parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentescoId = c.Int(nullable: false),
                        GrupoFamiliarId = c.Int(nullable: false),
                        ValidarSoloIngreso = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.grupo_familiar", t => t.GrupoFamiliarId, cascadeDelete: true)
                .ForeignKey("bow.parentesco", t => t.ParentescoId)
                .Index(t => t.ParentescoId)
                .Index(t => t.GrupoFamiliarId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.grupo_familiar_parentesco", t => t.GrupoFamiliarParentescoId)
                .Index(t => t.GrupoFamiliarParentescoId);
            
            CreateTable(
                "bow.parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Orden = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "bow.plan_exequial_recaudo_masivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        RecaudoMasivoId = c.Int(nullable: false),
                        EsObligatorio = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.plan_exequial", t => t.PlanExequialId, cascadeDelete: true)
                .ForeignKey("bow.recaudo_masivo", t => t.RecaudoMasivoId, cascadeDelete: true)
                .Index(t => t.PlanExequialId)
                .Index(t => t.RecaudoMasivoId);
            
            CreateTable(
                "bow.recaudo_masivo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Clave = c.String(nullable: false, maxLength: 30),
                        OrganizacionId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.organizacion", t => t.OrganizacionId)
                .Index(t => t.OrganizacionId);
            
            CreateTable(
                "bow.plan_exequial_sucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanExequialId = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.sucursal", t => t.SucursalId)
                .ForeignKey("bow.plan_exequial", t => t.PlanExequialId)
                .Index(t => t.PlanExequialId)
                .Index(t => t.SucursalId);
            
            CreateTable(
                "bow.prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DireccionId = c.Int(nullable: false),
                        TelefonoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.direccion", t => t.DireccionId, cascadeDelete: true)
                .ForeignKey("bow.telefono", t => t.TelefonoId, cascadeDelete: true)
                .Index(t => t.DireccionId)
                .Index(t => t.TelefonoId);
            
            CreateTable(
                "bow.recaudo_masivo_cobertura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecaudoMasivoId = c.Int(nullable: false),
                        LocalidadId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.recaudo_masivo", t => t.RecaudoMasivoId)
                .ForeignKey("bow.localidad", t => t.LocalidadId)
                .Index(t => t.RecaudoMasivoId)
                .Index(t => t.LocalidadId);
            
            CreateTable(
                "bow.funeraria_prospecto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        TenantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("bow.plan_exequial", "cantidadDiasMora", c => c.Int(nullable: false));
            AlterColumn("bow.persona", "Apellido2", c => c.String(maxLength: 50));
            CreateIndex("bow.empleado", "SucursalId");
            AddForeignKey("bow.empleado", "SucursalId", "bow.sucursal", "Id", cascadeDelete: true);
            AddForeignKey("bow.info_tributaria_localidad", "LocalidadId", "bow.localidad", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("bow.info_tributaria_localidad", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.recaudo_masivo_cobertura", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.afiliado_prospecto", "CiudadResidenciaId", "bow.localidad");
            DropForeignKey("bow.gestion_prospecto", "FunerariaAfiliadoId", "bow.funeraria_prospecto");
            DropForeignKey("bow.beneficios_gestion_prospecto", "GestionProspectoId", "bow.gestion_prospecto");
            DropForeignKey("bow.beneficios_gestion_prospecto", "BeneficioPlanExequialId", "bow.beneficio_plan_exequial");
            DropForeignKey("bow.beneficio_plan_exequial", "TipoId", "bow.tipo");
            DropForeignKey("bow.empleado", "SucursalId", "bow.sucursal");
            DropForeignKey("bow.grupo_familiar", "EstadoId", "bow.estado");
            DropForeignKey("bow.plan_exequial_sucursal", "PlanExequialId", "bow.plan_exequial");
            DropForeignKey("bow.recaudo_masivo_cobertura", "RecaudoMasivoId", "bow.recaudo_masivo");
            DropForeignKey("bow.plan_exequial_recaudo_masivo", "RecaudoMasivoId", "bow.recaudo_masivo");
            DropForeignKey("bow.recaudo_masivo", "OrganizacionId", "bow.organizacion");
            DropForeignKey("bow.prospecto", "TelefonoId", "bow.telefono");
            DropForeignKey("bow.gestion_prospecto", "ProspectoId", "bow.prospecto");
            DropForeignKey("bow.prospecto", "DireccionId", "bow.direccion");
            DropForeignKey("bow.gestion_prospecto", "PersonaId", "bow.persona");
            DropForeignKey("bow.plan_exequial_sucursal", "SucursalId", "bow.sucursal");
            DropForeignKey("bow.plan_exequial_recaudo_masivo", "PlanExequialId", "bow.plan_exequial");
            DropForeignKey("bow.grupo_familiar", "PlanExequialId", "bow.plan_exequial");
            DropForeignKey("bow.beneficio_plan_exequial", "PlanExequialId", "bow.plan_exequial");
            DropForeignKey("bow.grupo_familiar_parentesco", "ParentescoId", "bow.parentesco");
            DropForeignKey("bow.afiliado_prospecto", "ParentescoId", "bow.parentesco");
            DropForeignKey("bow.grupo_parentesco_rango", "GrupoFamiliarParentescoId", "bow.grupo_familiar_parentesco");
            DropForeignKey("bow.grupo_familiar_parentesco", "GrupoFamiliarId", "bow.grupo_familiar");
            DropForeignKey("bow.gestion_prospecto", "EstadoNoAfiliacionId", "bow.estado");
            DropForeignKey("bow.beneficio_plan_exequial", "EstadoId", "bow.estado");
            DropForeignKey("bow.gestion_prospecto", "EmpleadoId", "bow.empleado");
            DropForeignKey("bow.beneficio_plan_exequial", "BeneficioId", "bow.beneficio");
            DropForeignKey("bow.afiliado_prospecto", "GestionProspectoId", "bow.gestion_prospecto");
            DropIndex("bow.recaudo_masivo_cobertura", new[] { "LocalidadId" });
            DropIndex("bow.recaudo_masivo_cobertura", new[] { "RecaudoMasivoId" });
            DropIndex("bow.prospecto", new[] { "TelefonoId" });
            DropIndex("bow.prospecto", new[] { "DireccionId" });
            DropIndex("bow.plan_exequial_sucursal", new[] { "SucursalId" });
            DropIndex("bow.plan_exequial_sucursal", new[] { "PlanExequialId" });
            DropIndex("bow.recaudo_masivo", new[] { "OrganizacionId" });
            DropIndex("bow.plan_exequial_recaudo_masivo", new[] { "RecaudoMasivoId" });
            DropIndex("bow.plan_exequial_recaudo_masivo", new[] { "PlanExequialId" });
            DropIndex("bow.grupo_parentesco_rango", new[] { "GrupoFamiliarParentescoId" });
            DropIndex("bow.grupo_familiar_parentesco", new[] { "GrupoFamiliarId" });
            DropIndex("bow.grupo_familiar_parentesco", new[] { "ParentescoId" });
            DropIndex("bow.grupo_familiar", new[] { "EstadoId" });
            DropIndex("bow.grupo_familiar", new[] { "PlanExequialId" });
            DropIndex("bow.empleado", new[] { "SucursalId" });
            DropIndex("bow.beneficio_plan_exequial", new[] { "EstadoId" });
            DropIndex("bow.beneficio_plan_exequial", new[] { "TipoId" });
            DropIndex("bow.beneficio_plan_exequial", new[] { "BeneficioId" });
            DropIndex("bow.beneficio_plan_exequial", new[] { "PlanExequialId" });
            DropIndex("bow.beneficios_gestion_prospecto", new[] { "BeneficioPlanExequialId" });
            DropIndex("bow.beneficios_gestion_prospecto", new[] { "GestionProspectoId" });
            DropIndex("bow.gestion_prospecto", new[] { "FunerariaAfiliadoId" });
            DropIndex("bow.gestion_prospecto", new[] { "EstadoNoAfiliacionId" });
            DropIndex("bow.gestion_prospecto", new[] { "PersonaId" });
            DropIndex("bow.gestion_prospecto", new[] { "EmpleadoId" });
            DropIndex("bow.gestion_prospecto", new[] { "ProspectoId" });
            DropIndex("bow.afiliado_prospecto", new[] { "CiudadResidenciaId" });
            DropIndex("bow.afiliado_prospecto", new[] { "ParentescoId" });
            DropIndex("bow.afiliado_prospecto", new[] { "GestionProspectoId" });
            AlterColumn("bow.persona", "Apellido2", c => c.String(nullable: false, maxLength: 50));
            DropColumn("bow.plan_exequial", "cantidadDiasMora");
            DropTable("bow.funeraria_prospecto");
            DropTable("bow.recaudo_masivo_cobertura");
            DropTable("bow.prospecto");
            DropTable("bow.plan_exequial_sucursal");
            DropTable("bow.recaudo_masivo");
            DropTable("bow.plan_exequial_recaudo_masivo");
            DropTable("bow.parentesco");
            DropTable("bow.grupo_parentesco_rango");
            DropTable("bow.grupo_familiar_parentesco");
            DropTable("bow.grupo_familiar");
            DropTable("bow.beneficio_plan_exequial");
            DropTable("bow.beneficios_gestion_prospecto");
            DropTable("bow.gestion_prospecto");
            DropTable("bow.afiliado_prospecto");
            AddForeignKey("bow.info_tributaria_localidad", "LocalidadId", "bow.localidad", "Id", cascadeDelete: true);
        }
    }
}
