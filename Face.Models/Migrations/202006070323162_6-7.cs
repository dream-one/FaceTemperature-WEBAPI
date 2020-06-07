namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _67 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "CreatTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Attendances", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attendances", "IsDelete");
            DropColumn("dbo.Attendances", "CreatTime");
        }
    }
}
