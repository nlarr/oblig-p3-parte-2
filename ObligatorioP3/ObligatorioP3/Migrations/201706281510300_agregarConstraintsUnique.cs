namespace ObligatorioP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarConstraintsUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emprendimientoes", "Titulo", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AlterColumn("dbo.Usuarios", "Email", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            CreateIndex("dbo.Emprendimientoes", "Titulo", unique: true);
            CreateIndex("dbo.Usuarios", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuarios", new[] { "Email" });
            DropIndex("dbo.Emprendimientoes", new[] { "Titulo" });
            AlterColumn("dbo.Usuarios", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Emprendimientoes", "Titulo", c => c.String(nullable: false));
        }
    }
}
