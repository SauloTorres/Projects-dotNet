namespace EnvioEmail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ativo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoas", "Ativo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pessoas", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Pessoas", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoas", "Email", c => c.String());
            AlterColumn("dbo.Pessoas", "Nome", c => c.String());
            DropColumn("dbo.Pessoas", "Ativo");
        }
    }
}
