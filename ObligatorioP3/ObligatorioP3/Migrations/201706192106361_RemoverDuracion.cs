namespace ObligatorioP3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoverDuracion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Emprendimientoes", "Duracion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emprendimientoes", "Duracion", c => c.Int(nullable: false));
        }
    }
}
