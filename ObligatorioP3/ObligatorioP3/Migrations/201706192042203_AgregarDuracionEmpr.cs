namespace ObligatorioP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarDuracionEmpr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprendimientoes", "Duracion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emprendimientoes", "Duracion");
        }
    }
}
