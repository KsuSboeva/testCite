namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddefaultValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "DedefaultValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "DedefaultValue");
        }
    }
}
