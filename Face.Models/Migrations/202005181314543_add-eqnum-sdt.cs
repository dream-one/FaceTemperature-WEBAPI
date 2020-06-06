namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeqnumsdt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "DeviceSerial", c => c.String());
            AddColumn("dbo.Teachers", "DeviceSerial", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "DeviceSerial");
            DropColumn("dbo.Students", "DeviceSerial");
        }
    }
}
