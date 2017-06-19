namespace ObligatorioP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionBD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emprendimientoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tiempo = c.Int(nullable: false),
                        PuntajeTotal = c.Int(nullable: false),
                        Financiador_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Financiadores", t => t.Financiador_Id)
                .Index(t => t.Financiador_Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Rol = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Financiadores",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Organizacion = c.String(nullable: false),
                        MontoMax = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Financiadores", "Id", "dbo.Usuarios");
            DropForeignKey("dbo.Emprendimientoes", "Financiador_Id", "dbo.Financiadores");
            DropIndex("dbo.Financiadores", new[] { "Id" });
            DropIndex("dbo.Emprendimientoes", new[] { "Financiador_Id" });
            DropTable("dbo.Financiadores");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Emprendimientoes");
        }
    }
}
