namespace Bow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion_abril : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("bow.beneficio_plan_exequial", "TipoId", "bow.tipo");
            DropForeignKey("bow.beneficios_gestion_prospecto", "BeneficioPlanExequialId", "bow.beneficio_plan_exequial");
            DropForeignKey("bow.grupo_familiar_parentesco", "GrupoFamiliarId", "bow.grupo_familiar");
            DropForeignKey("bow.plan_exequial_recaudo_masivo", "PlanExequialId", "bow.plan_exequial");
            DropIndex("bow.beneficios_gestion_prospecto", new[] { "BeneficioPlanExequialId" });
            DropIndex("bow.beneficio_plan_exequial", new[] { "TipoId" });
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.beneficio_plan_exequial", t => t.BeneficioPlanExequialId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .ForeignKey("bow.plan_exequial", t => t.PlanExequialId)
                .ForeignKey("bow.beneficio", t => t.BeneficioId)
                .Index(t => t.PlanExequialId)
                .Index(t => t.BeneficioId)
                .Index(t => t.EstadoId)
                .Index(t => t.BeneficioPlanExequialId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.sucursal", t => t.SucursalId)
                .ForeignKey("bow.persona", t => t.PersonaId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .Index(t => t.PersonaId)
                .Index(t => t.EstadoId)
                .Index(t => t.SucursalId);
            
            CreateTable(
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.empleado", t => t.EmpleadoId)
                .ForeignKey("bow.grupo_informal", t => t.GrupoInformalId)
                .ForeignKey("bow.estado", t => t.EstadoId)
                .Index(t => t.GrupoInformalId)
                .Index(t => t.EmpleadoId)
                .Index(t => t.EstadoId);
            
            AddColumn("bow.gestion_prospecto", "GrupoFamiliarId", c => c.Int());
            AddColumn("bow.gestion_prospecto", "SucursalId", c => c.Int(nullable: false));
            AddColumn("bow.gestion_prospecto", "LocalidadId", c => c.Int(nullable: false));
            AddColumn("bow.beneficios_gestion_prospecto", "BeneficioAdicionalPlanExequialId", c => c.Int(nullable: false));
            AddColumn("bow.beneficio_plan_exequial", "Asignables", c => c.Int(nullable: false));
            AddColumn("bow.parentesco", "Posicion", c => c.Int(nullable: false));
            AddColumn("bow.parentesco", "Genero", c => c.String(nullable: false, maxLength: 1));
            AddColumn("bow.parentesco", "Repeticiones", c => c.Int(nullable: false));
            AddColumn("bow.parentesco", "Limite", c => c.String());
            AddColumn("bow.parentesco", "EdadDiferencia", c => c.Int());
            AddColumn("bow.parentesco", "CoincidirApellidos", c => c.Boolean(nullable: false));
            CreateIndex("bow.gestion_prospecto", "GrupoFamiliarId");
            CreateIndex("bow.gestion_prospecto", "SucursalId");
            CreateIndex("bow.gestion_prospecto", "LocalidadId");
            CreateIndex("bow.beneficios_gestion_prospecto", "BeneficioAdicionalPlanExequialId");
            AddForeignKey("bow.gestion_prospecto", "GrupoFamiliarId", "bow.grupo_familiar", "Id");
            AddForeignKey("bow.gestion_prospecto", "SucursalId", "bow.sucursal", "Id");
            AddForeignKey("bow.beneficios_gestion_prospecto", "BeneficioAdicionalPlanExequialId", "bow.beneficio_adicional_plan_exequial", "Id");
            AddForeignKey("bow.gestion_prospecto", "LocalidadId", "bow.localidad", "Id");
            AddForeignKey("bow.grupo_familiar_parentesco", "GrupoFamiliarId", "bow.grupo_familiar", "Id");
            AddForeignKey("bow.plan_exequial_recaudo_masivo", "PlanExequialId", "bow.plan_exequial", "Id");
            DropColumn("bow.beneficios_gestion_prospecto", "BeneficioPlanExequialId");
            DropColumn("bow.beneficio_plan_exequial", "TipoId");
            DropColumn("bow.beneficio_plan_exequial", "EsAsignable");
            DropColumn("bow.beneficio_plan_exequial", "Valor");
            DropColumn("bow.beneficio_plan_exequial", "CantidadAsignables");
            DropColumn("bow.beneficio_plan_exequial", "TipoModificable");
            DropColumn("bow.parentesco", "Orden");
        }
        
        public override void Down()
        {
            AddColumn("bow.parentesco", "Orden", c => c.Int(nullable: false));
            AddColumn("bow.beneficio_plan_exequial", "TipoModificable", c => c.String(maxLength: 1));
            AddColumn("bow.beneficio_plan_exequial", "CantidadAsignables", c => c.Int(nullable: false));
            AddColumn("bow.beneficio_plan_exequial", "Valor", c => c.Int(nullable: false));
            AddColumn("bow.beneficio_plan_exequial", "EsAsignable", c => c.String(nullable: false, maxLength: 1));
            AddColumn("bow.beneficio_plan_exequial", "TipoId", c => c.Int(nullable: false));
            AddColumn("bow.beneficios_gestion_prospecto", "BeneficioPlanExequialId", c => c.Int(nullable: false));
            DropForeignKey("bow.plan_exequial_recaudo_masivo", "PlanExequialId", "bow.plan_exequial");
            DropForeignKey("bow.grupo_familiar_parentesco", "GrupoFamiliarId", "bow.grupo_familiar");
            DropForeignKey("bow.gestion_prospecto", "LocalidadId", "bow.localidad");
            DropForeignKey("bow.beneficios_gestion_prospecto", "BeneficioAdicionalPlanExequialId", "bow.beneficio_adicional_plan_exequial");
            DropForeignKey("bow.beneficio_adicional_plan_exequial", "BeneficioId", "bow.beneficio");
            DropForeignKey("bow.grupo_informal_empleado", "EstadoId", "bow.estado");
            DropForeignKey("bow.grupo_informal", "EstadoId", "bow.estado");
            DropForeignKey("bow.grupo_informal_empleado", "GrupoInformalId", "bow.grupo_informal");
            DropForeignKey("bow.grupo_informal", "PersonaId", "bow.persona");
            DropForeignKey("bow.grupo_informal", "SucursalId", "bow.sucursal");
            DropForeignKey("bow.gestion_prospecto", "SucursalId", "bow.sucursal");
            DropForeignKey("bow.gestion_prospecto", "GrupoFamiliarId", "bow.grupo_familiar");
            DropForeignKey("bow.beneficio_adicional_plan_exequial", "PlanExequialId", "bow.plan_exequial");
            DropForeignKey("bow.grupo_informal_empleado", "EmpleadoId", "bow.empleado");
            DropForeignKey("bow.beneficio_adicional_plan_exequial", "EstadoId", "bow.estado");
            DropForeignKey("bow.beneficio_adicional_plan_exequial", "BeneficioPlanExequialId", "bow.beneficio_plan_exequial");
            DropIndex("bow.grupo_informal_empleado", new[] { "EstadoId" });
            DropIndex("bow.grupo_informal_empleado", new[] { "EmpleadoId" });
            DropIndex("bow.grupo_informal_empleado", new[] { "GrupoInformalId" });
            DropIndex("bow.grupo_informal", new[] { "SucursalId" });
            DropIndex("bow.grupo_informal", new[] { "EstadoId" });
            DropIndex("bow.grupo_informal", new[] { "PersonaId" });
            DropIndex("bow.beneficio_adicional_plan_exequial", new[] { "BeneficioPlanExequialId" });
            DropIndex("bow.beneficio_adicional_plan_exequial", new[] { "EstadoId" });
            DropIndex("bow.beneficio_adicional_plan_exequial", new[] { "BeneficioId" });
            DropIndex("bow.beneficio_adicional_plan_exequial", new[] { "PlanExequialId" });
            DropIndex("bow.beneficios_gestion_prospecto", new[] { "BeneficioAdicionalPlanExequialId" });
            DropIndex("bow.gestion_prospecto", new[] { "LocalidadId" });
            DropIndex("bow.gestion_prospecto", new[] { "SucursalId" });
            DropIndex("bow.gestion_prospecto", new[] { "GrupoFamiliarId" });
            DropColumn("bow.parentesco", "CoincidirApellidos");
            DropColumn("bow.parentesco", "EdadDiferencia");
            DropColumn("bow.parentesco", "Limite");
            DropColumn("bow.parentesco", "Repeticiones");
            DropColumn("bow.parentesco", "Genero");
            DropColumn("bow.parentesco", "Posicion");
            DropColumn("bow.beneficio_plan_exequial", "Asignables");
            DropColumn("bow.beneficios_gestion_prospecto", "BeneficioAdicionalPlanExequialId");
            DropColumn("bow.gestion_prospecto", "LocalidadId");
            DropColumn("bow.gestion_prospecto", "SucursalId");
            DropColumn("bow.gestion_prospecto", "GrupoFamiliarId");
            DropTable("bow.grupo_informal_empleado");
            DropTable("bow.grupo_informal");
            DropTable("bow.beneficio_adicional_plan_exequial");
            CreateIndex("bow.beneficio_plan_exequial", "TipoId");
            CreateIndex("bow.beneficios_gestion_prospecto", "BeneficioPlanExequialId");
            AddForeignKey("bow.plan_exequial_recaudo_masivo", "PlanExequialId", "bow.plan_exequial", "Id", cascadeDelete: true);
            AddForeignKey("bow.grupo_familiar_parentesco", "GrupoFamiliarId", "bow.grupo_familiar", "Id", cascadeDelete: true);
            AddForeignKey("bow.beneficios_gestion_prospecto", "BeneficioPlanExequialId", "bow.beneficio_plan_exequial", "Id");
            AddForeignKey("bow.beneficio_plan_exequial", "TipoId", "bow.tipo", "Id");
        }
    }
}
