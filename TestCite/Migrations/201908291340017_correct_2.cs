namespace TestCite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correct_2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "nameValue1");
            DropColumn("dbo.Orders", "nameValue2");
            DropColumn("dbo.Orders", "nameValue3");
            DropColumn("dbo.Orders", "nameValue4");
            DropColumn("dbo.Orders", "nameValue5");
            DropColumn("dbo.Orders", "nameValue6");
            DropColumn("dbo.Orders", "nameValue7");
            DropColumn("dbo.Orders", "nameValue8");
            DropColumn("dbo.Orders", "nameValue9");
            DropColumn("dbo.Orders", "nameValue10");
            DropColumn("dbo.Orders", "nameValue11");
            DropColumn("dbo.Orders", "value1");
            DropColumn("dbo.Orders", "value2");
            DropColumn("dbo.Orders", "value3");
            DropColumn("dbo.Orders", "value4");
            DropColumn("dbo.Orders", "value5");
            DropColumn("dbo.Orders", "value6");
            DropColumn("dbo.Orders", "value7");
            DropColumn("dbo.Orders", "value8");
            DropColumn("dbo.Orders", "value9");
            DropColumn("dbo.Orders", "value10");
            DropColumn("dbo.Orders", "value11");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "value11", c => c.String());
            AddColumn("dbo.Orders", "value10", c => c.String());
            AddColumn("dbo.Orders", "value9", c => c.String());
            AddColumn("dbo.Orders", "value8", c => c.String());
            AddColumn("dbo.Orders", "value7", c => c.String());
            AddColumn("dbo.Orders", "value6", c => c.String());
            AddColumn("dbo.Orders", "value5", c => c.String());
            AddColumn("dbo.Orders", "value4", c => c.String());
            AddColumn("dbo.Orders", "value3", c => c.String());
            AddColumn("dbo.Orders", "value2", c => c.String());
            AddColumn("dbo.Orders", "value1", c => c.String());
            AddColumn("dbo.Orders", "nameValue11", c => c.String());
            AddColumn("dbo.Orders", "nameValue10", c => c.String());
            AddColumn("dbo.Orders", "nameValue9", c => c.String());
            AddColumn("dbo.Orders", "nameValue8", c => c.String());
            AddColumn("dbo.Orders", "nameValue7", c => c.String());
            AddColumn("dbo.Orders", "nameValue6", c => c.String());
            AddColumn("dbo.Orders", "nameValue5", c => c.String());
            AddColumn("dbo.Orders", "nameValue4", c => c.String());
            AddColumn("dbo.Orders", "nameValue3", c => c.String());
            AddColumn("dbo.Orders", "nameValue2", c => c.String());
            AddColumn("dbo.Orders", "nameValue1", c => c.String());
        }
    }
}
