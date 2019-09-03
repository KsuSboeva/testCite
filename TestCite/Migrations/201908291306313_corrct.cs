namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corrct : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DynamicValues", "OrderId");
            AddForeignKey("dbo.DynamicValues", "OrderId", "dbo.Orders", "orderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DynamicValues", "OrderId", "dbo.Orders");
            DropIndex("dbo.DynamicValues", new[] { "OrderId" });
        }
    }
}
