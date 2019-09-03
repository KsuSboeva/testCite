namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNmeatr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DynamicValues", "settingName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DynamicValues", "settingName");
        }
    }
}
