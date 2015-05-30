namespace Bow.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ModuleZero : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "bow.AbpAuditLogs",
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
                "bow.AbpPermissions",
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
                .ForeignKey("bow.AbpUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("bow.AbpRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "bow.AbpRoles",
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
                .ForeignKey("bow.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("bow.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("bow.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("bow.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "bow.AbpUsers",
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
                .ForeignKey("bow.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("bow.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("bow.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("bow.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "bow.AbpUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "bow.AbpUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("bow.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "bow.AbpSettings",
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
                .ForeignKey("bow.AbpUsers", t => t.UserId)
                .ForeignKey("bow.AbpTenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.UserId);
            
            CreateTable(
                "bow.AbpTenants",
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
                .ForeignKey("bow.AbpUsers", t => t.CreatorUserId)
                .ForeignKey("bow.AbpUsers", t => t.DeleterUserId)
                .ForeignKey("bow.AbpUsers", t => t.LastModifierUserId)
                .ForeignKey("bow.organizacion", t => t.OrganizacionId)
                .Index(t => t.OrganizacionId)
                .Index(t => t.DeleterUserId)
                .Index(t => t.LastModifierUserId)
                .Index(t => t.CreatorUserId);
            
            AlterTableAnnotations(
                "bow.pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Indicativo = c.String(nullable: false, maxLength: 4),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Pais_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
        
        public override void Down()
        {
            DropForeignKey("bow.AbpRoles", "TenantId", "bow.AbpTenants");
            DropForeignKey("bow.AbpPermissions", "RoleId", "bow.AbpRoles");
            DropForeignKey("bow.AbpRoles", "LastModifierUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpRoles", "DeleterUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpRoles", "CreatorUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpUsers", "TenantId", "bow.AbpTenants");
            DropForeignKey("bow.AbpSettings", "TenantId", "bow.AbpTenants");
            DropForeignKey("bow.AbpTenants", "OrganizacionId", "bow.organizacion");
            DropForeignKey("bow.AbpTenants", "LastModifierUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpTenants", "DeleterUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpTenants", "CreatorUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpSettings", "UserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpUserRoles", "UserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpPermissions", "UserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpUserLogins", "UserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpUsers", "LastModifierUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpUsers", "DeleterUserId", "bow.AbpUsers");
            DropForeignKey("bow.AbpUsers", "CreatorUserId", "bow.AbpUsers");
            DropIndex("bow.AbpTenants", new[] { "CreatorUserId" });
            DropIndex("bow.AbpTenants", new[] { "LastModifierUserId" });
            DropIndex("bow.AbpTenants", new[] { "DeleterUserId" });
            DropIndex("bow.AbpTenants", new[] { "OrganizacionId" });
            DropIndex("bow.AbpSettings", new[] { "UserId" });
            DropIndex("bow.AbpSettings", new[] { "TenantId" });
            DropIndex("bow.AbpUserRoles", new[] { "UserId" });
            DropIndex("bow.AbpUserLogins", new[] { "UserId" });
            DropIndex("bow.AbpUsers", new[] { "CreatorUserId" });
            DropIndex("bow.AbpUsers", new[] { "LastModifierUserId" });
            DropIndex("bow.AbpUsers", new[] { "DeleterUserId" });
            DropIndex("bow.AbpUsers", new[] { "TenantId" });
            DropIndex("bow.AbpRoles", new[] { "CreatorUserId" });
            DropIndex("bow.AbpRoles", new[] { "LastModifierUserId" });
            DropIndex("bow.AbpRoles", new[] { "DeleterUserId" });
            DropIndex("bow.AbpRoles", new[] { "TenantId" });
            DropIndex("bow.AbpPermissions", new[] { "UserId" });
            DropIndex("bow.AbpPermissions", new[] { "RoleId" });
            AlterTableAnnotations(
                "bow.pais",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 25),
                        Indicativo = c.String(nullable: false, maxLength: 4),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Pais_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropTable("bow.AbpTenants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Tenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("bow.AbpSettings");
            DropTable("bow.AbpUserRoles");
            DropTable("bow.AbpUserLogins");
            DropTable("bow.AbpUsers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_User_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_User_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("bow.AbpRoles",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Role_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Role_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("bow.AbpPermissions");
            DropTable("bow.AbpAuditLogs");
        }
    }
}
