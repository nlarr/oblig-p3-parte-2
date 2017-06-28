namespace ObligatorioP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoverTablaUsuarioViewModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UsuarioViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsuarioViewModels",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Email);
            
        }
    }
}
