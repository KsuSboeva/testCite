namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Templates", "Name", c => c.String());
            DropColumn("dbo.Templates", "Style");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Templates", "Style", c => c.String());
            DropColumn("dbo.Templates", "Name");
        }
    }
}
