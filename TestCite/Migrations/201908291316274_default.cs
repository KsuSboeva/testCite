namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _default : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "DefaultValue", c => c.String());
            DropColumn("dbo.Settings", "DedefaultValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "DedefaultValue", c => c.String());
            DropColumn("dbo.Settings", "DefaultValue");
        }
    }
}
