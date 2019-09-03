namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvalues2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValueDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValueDatas", "OrderId", "dbo.Orders");
            DropIndex("dbo.ValueDatas", new[] { "OrderId" });
            DropTable("dbo.ValueDatas");
        }
    }
}
