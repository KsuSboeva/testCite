namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ValueDatas", "OrderId", "dbo.Orders");
            DropIndex("dbo.ValueDatas", new[] { "OrderId" });
            DropTable("dbo.ValueDatas");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ValueDatas", "OrderId");
            AddForeignKey("dbo.ValueDatas", "OrderId", "dbo.Orders", "orderId");
        }
    }
}
