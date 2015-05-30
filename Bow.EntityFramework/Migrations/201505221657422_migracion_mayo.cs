namespace Bow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion_mayo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("bow.prospecto", "DireccionId", "bow.direccion");
            DropForeignKey("bow.prospecto", "TelefonoId", "bow.telefono");
            DropIndex("bow.prospecto", new[] { "DireccionId" });
            DropIndex("bow.prospecto", new[] { "TelefonoId" });
            AlterColumn("bow.prospecto", "DireccionId", c => c.Int());
            AlterColumn("bow.prospecto", "TelefonoId", c => c.Int());
            CreateIndex("bow.prospecto", "DireccionId");
            CreateIndex("bow.prospecto", "TelefonoId");
            AddForeignKey("bow.prospecto", "DireccionId", "bow.direccion", "Id");
            AddForeignKey("bow.prospecto", "TelefonoId", "bow.telefono", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("bow.prospecto", "TelefonoId", "bow.telefono");
            DropForeignKey("bow.prospecto", "DireccionId", "bow.direccion");
            DropIndex("bow.prospecto", new[] { "TelefonoId" });
            DropIndex("bow.prospecto", new[] { "DireccionId" });
            AlterColumn("bow.prospecto", "TelefonoId", c => c.Int(nullable: false));
            AlterColumn("bow.prospecto", "DireccionId", c => c.Int(nullable: false));
            CreateIndex("bow.prospecto", "TelefonoId");
            CreateIndex("bow.prospecto", "DireccionId");
            AddForeignKey("bow.prospecto", "TelefonoId", "bow.telefono", "Id", cascadeDelete: true);
            AddForeignKey("bow.prospecto", "DireccionId", "bow.direccion", "Id", cascadeDelete: true);
        }
    }
}
