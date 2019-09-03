namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctconnection : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DynamicValues", "settingId");
            CreateIndex("dbo.DynamicValues", "templateID");
            AddForeignKey("dbo.DynamicValues", "templateID", "dbo.Templates", "Id");
            AddForeignKey("dbo.DynamicValues", "settingId", "dbo.Settings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DynamicValues", "settingId", "dbo.Settings");
            DropForeignKey("dbo.DynamicValues", "templateID", "dbo.Templates");
            DropIndex("dbo.DynamicValues", new[] { "templateID" });
            DropIndex("dbo.DynamicValues", new[] { "settingId" });
        }
    }
}
