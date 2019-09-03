namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorder : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TemplateSettings", newName: "SettingTemplates");
            DropPrimaryKey("dbo.SettingTemplates");
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Int(nullable: false, identity: true),
                        templateID = c.Int(),
                    })
                .PrimaryKey(t => t.orderId)
                .ForeignKey("dbo.Templates", t => t.templateID)
                .Index(t => t.templateID);
            
            AddPrimaryKey("dbo.SettingTemplates", new[] { "Setting_Id", "Template_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "templateID", "dbo.Templates");
            DropIndex("dbo.Orders", new[] { "templateID" });
            DropPrimaryKey("dbo.SettingTemplates");
            DropTable("dbo.Orders");
            AddPrimaryKey("dbo.SettingTemplates", new[] { "Template_Id", "Setting_Id" });
            RenameTable(name: "dbo.SettingTemplates", newName: "TemplateSettings");
        }
    }
}
