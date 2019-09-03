namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDynamicValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DynamicValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        settingId = c.Int(),
                        orderId = c.Int(),
                        templateID = c.Int(),
                        dynamicValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DynamicValues");
        }
    }
}
