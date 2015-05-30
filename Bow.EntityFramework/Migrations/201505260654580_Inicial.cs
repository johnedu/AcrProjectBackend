namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AcrProject.AbpAuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        ServiceName = c.String(maxLength: 256),
                        MethodName = c.String(maxLength: 256),
                        Parameters = c.String(maxLength: 1024),
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 128),
                        BrowserInfo = c.String(maxLength: 256),
                        Exception = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AcrProject.AbpPermissions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsGranted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        RoleId = c.Int(),
                        UserId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("AcrProject.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "AcrProject.AbpRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(nullable: false, maxLength: 64),
                        IsStatic = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("AcrProject.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("AcrProject.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("AcrProject.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "AcrProject.AbpUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 32),
                        Surname = c.String(nullable: false, maxLength: 32),
                        UserName = c.String(nullable: false, maxLength: 32),
                        Password = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(nullable: false, maxLength: 256),
                        IsEmailConfirmed = c.Boolean(nullable: false),
                        EmailConfirmationCode = c.String(maxLength: 128),
                        PasswordResetCode = c.String(maxLength: 128),
                        LastLoginTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("AcrProject.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("AcrProject.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("AcrProject.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "AcrProject.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "AcrProject.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "AcrProject.AbpSettings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.UserId)
                .ForeignKey("AcrProject.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "AcrProject.AbpTenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizacionId = c.Int(),
                        TenancyName = c.String(nullable: false, maxLength: 64),
                        Name = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("AcrProject.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("AcrProject.AbpUsers", t => t.LastModifierUserId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "AcrProject.pregunta_frecuente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pregunta = c.String(nullable: false),
                        Respuesta = c.String(nullable: false),
                        EstadoActiva = c.Boolean(nullable: false),
                        FechaCreacion = c.String(),
                        UsuarioIdCreacion = c.Int(nullable: false),
                        FechaModificacion = c.String(),
                        UsuarioIdModificacion = c.Int(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PreguntaFrecuente_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AcrProject.juego",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Juego_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AcrProject.pregunta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(nullable: false),
                        JuegoId = c.Int(nullable: false),
                        DimensionId = c.Int(nullable: false),
                        Nivel = c.String(nullable: false, maxLength: 1),
                        Pista = c.String(nullable: false),
                        EstadoActiva = c.Boolean(nullable: false),
                        FechaCreacion = c.String(),
                        UsuarioIdCreacion = c.Int(nullable: false),
                        FechaModificacion = c.String(),
                        UsuarioIdModificacion = c.Int(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Pregunta_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.dimension", t => t.DimensionId)
                .ForeignKey("AcrProject.juego", t => t.JuegoId)
                .Index(t => t.JuegoId)
                .Index(t => t.DimensionId);
            
            CreateTable(
                "AcrProject.dimension",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Dimension_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AcrProject.entidad",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        Descripcion = c.String(nullable: false),
                        DimensionId = c.Int(nullable: false),
                        EstadoActiva = c.Boolean(nullable: false),
                        FechaCreacion = c.String(),
                        UsuarioIdCreacion = c.Int(nullable: false),
                        FechaModificacion = c.String(),
                        UsuarioIdModificacion = c.Int(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Entidad_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.dimension", t => t.DimensionId)
                .Index(t => t.DimensionId);
            
            CreateTable(
                "AcrProject.puntaje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        PreguntaId = c.Int(nullable: false),
                        PuntajeValor = c.Int(nullable: false),
                        Respuesta = c.String(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Puntaje_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.usuario", t => t.UsuarioId)
                .ForeignKey("AcrProject.pregunta", t => t.PreguntaId)
                .Index(t => t.UsuarioId)
                .Index(t => t.PreguntaId);
            
            CreateTable(
                "AcrProject.usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Coda = c.String(nullable: false, maxLength: 100),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        TipoId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Usuario_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.tipo", t => t.TipoId)
                .Index(t => t.TipoId);
            
            CreateTable(
                "AcrProject.mensaje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioEmisorId = c.Int(nullable: false),
                        UsuarioReceptorId = c.Int(nullable: false),
                        Titulo = c.String(nullable: false, maxLength: 512),
                        Contenido = c.String(nullable: false),
                        FueLeido = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Mensaje_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.usuario", t => t.UsuarioEmisorId)
                .ForeignKey("AcrProject.usuario", t => t.UsuarioReceptorId)
                .Index(t => t.UsuarioEmisorId)
                .Index(t => t.UsuarioReceptorId);
            
            CreateTable(
                "AcrProject.tipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 512),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tipo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "AcrProject.respuesta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(nullable: false),
                        Comodin50_50 = c.Boolean(nullable: false),
                        RespuestaVerdadera = c.Boolean(nullable: false),
                        PreguntaId = c.Int(nullable: false),
                        EstadoActiva = c.Boolean(nullable: false),
                        FechaCreacion = c.String(),
                        UsuarioIdCreacion = c.Int(nullable: false),
                        FechaModificacion = c.String(),
                        UsuarioIdModificacion = c.Int(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Respuesta_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("AcrProject.pregunta", t => t.PreguntaId)
                .Index(t => t.PreguntaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("AcrProject.pregunta", "JuegoId", "AcrProject.juego");
            DropForeignKey("AcrProject.respuesta", "PreguntaId", "AcrProject.pregunta");
            DropForeignKey("AcrProject.puntaje", "PreguntaId", "AcrProject.pregunta");
            DropForeignKey("AcrProject.usuario", "TipoId", "AcrProject.tipo");
            DropForeignKey("AcrProject.puntaje", "UsuarioId", "AcrProject.usuario");
            DropForeignKey("AcrProject.mensaje", "UsuarioReceptorId", "AcrProject.usuario");
            DropForeignKey("AcrProject.mensaje", "UsuarioEmisorId", "AcrProject.usuario");
            DropForeignKey("AcrProject.pregunta", "DimensionId", "AcrProject.dimension");
            DropForeignKey("AcrProject.entidad", "DimensionId", "AcrProject.dimension");
            DropForeignKey("AcrProject.AbpRoles", "TenantId", "AcrProject.AbpTenants");
            DropForeignKey("AcrProject.AbpPermissions", "RoleId", "AcrProject.AbpRoles");
            DropForeignKey("AcrProject.AbpRoles", "LastModifierUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpRoles", "DeleterUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpRoles", "CreatorUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpUsers", "TenantId", "AcrProject.AbpTenants");
            DropForeignKey("AcrProject.AbpSettings", "TenantId", "AcrProject.AbpTenants");
            DropForeignKey("AcrProject.AbpTenants", "LastModifierUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpTenants", "DeleterUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpTenants", "CreatorUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpSettings", "UserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpUserRoles", "UserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpPermissions", "UserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpUserLogins", "UserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpUsers", "LastModifierUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpUsers", "DeleterUserId", "AcrProject.AbpUsers");
            DropForeignKey("AcrProject.AbpUsers", "CreatorUserId", "AcrProject.AbpUsers");
            DropIndex("AcrProject.respuesta", new[] { "PreguntaId" });
            DropIndex("AcrProject.mensaje", new[] { "UsuarioReceptorId" });
            DropIndex("AcrProject.mensaje", new[] { "UsuarioEmisorId" });
            DropIndex("AcrProject.usuario", new[] { "TipoId" });
            DropIndex("AcrProject.puntaje", new[] { "PreguntaId" });
            DropIndex("AcrProject.puntaje", new[] { "UsuarioId" });
            DropIndex("AcrProject.entidad", new[] { "DimensionId" });
            DropIndex("AcrProject.pregunta", new[] { "DimensionId" });
            DropIndex("AcrProject.pregunta", new[] { "JuegoId" });
            DropIndex("AcrProject.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("AcrProject.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("AcrProject.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("AcrProject.AbpSettings", new[] { "UserId" });
            DropIndex("AcrProject.AbpSettings", new[] { "TenantId" });
            DropIndex("AcrProject.AbpUserRoles", new[] { "UserId" });
            DropIndex("AcrProject.AbpUserLogins", new[] { "UserId" });
            DropIndex("AcrProject.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("AcrProject.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("AcrProject.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("AcrProject.AbpUsers", new[] { "TenantId" });
            DropIndex("AcrProject.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("AcrProject.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("AcrProject.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("AcrProject.AbpRoles", new[] { "TenantId" });
            DropIndex("AcrProject.AbpPermissions", new[] { "UserId" });
            DropIndex("AcrProject.AbpPermissions", new[] { "RoleId" });
            DropTable("AcrProject.respuesta",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Respuesta_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.tipo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tipo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.mensaje",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Mensaje_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.usuario",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Usuario_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.puntaje",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Puntaje_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.entidad",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Entidad_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.dimension",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Dimension_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.pregunta",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Pregunta_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.juego",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Juego_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.pregunta_frecuente",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PreguntaFrecuente_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.AbpSettings");
            DropTable("AcrProject.AbpUserRoles");
            DropTable("AcrProject.AbpUserLogins");
            DropTable("AcrProject.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("AcrProject.AbpPermissions");
            DropTable("AcrProject.AbpAuditLogs");
        }
    }
}
