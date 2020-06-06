namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nozizeng : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Teachers");
            AlterColumn("dbo.Teachers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Teachers", "Id");
        }
    }
}
