namespace ObligatorioP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cosaRara : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.UsuarioViewModels");
        }
    }
}
