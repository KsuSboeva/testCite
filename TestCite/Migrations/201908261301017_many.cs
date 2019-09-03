namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class many : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Settings", "TemplateId", "dbo.Templates");
            DropIndex("dbo.Settings", new[] { "TemplateId" });
            CreateTable(
                "dbo.TemplateSettings",
                c => new
                    {
                        Template_Id = c.Int(nullable: false),
                        Setting_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Template_Id, t.Setting_Id })
                .ForeignKey("dbo.Templates", t => t.Template_Id, cascadeDelete: true)
                .ForeignKey("dbo.Settings", t => t.Setting_Id, cascadeDelete: true)
                .Index(t => t.Template_Id)
                .Index(t => t.Setting_Id);
            
            DropColumn("dbo.Settings", "TemplateId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "TemplateId", c => c.Int());
            DropForeignKey("dbo.TemplateSettings", "Setting_Id", "dbo.Settings");
            DropForeignKey("dbo.TemplateSettings", "Template_Id", "dbo.Templates");
            DropIndex("dbo.TemplateSettings", new[] { "Setting_Id" });
            DropIndex("dbo.TemplateSettings", new[] { "Template_Id" });
            DropTable("dbo.TemplateSettings");
            CreateIndex("dbo.Settings", "TemplateId");
            AddForeignKey("dbo.Settings", "TemplateId", "dbo.Templates", "Id");
        }
    }
}
