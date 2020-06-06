namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnterAndLeaves", "DeviceSerial", c => c.String(nullable: false));
            DropColumn("dbo.EnterAndLeaves", "DeviceIld");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnterAndLeaves", "DeviceIld", c => c.String(nullable: false));
            DropColumn("dbo.EnterAndLeaves", "DeviceSerial");
        }
    }
}
