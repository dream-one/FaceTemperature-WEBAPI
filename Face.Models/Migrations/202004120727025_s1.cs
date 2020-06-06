namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.EnterAndLeaves", "InOutType", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.Students", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.EnterAndLeaves", "InOutType", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Students", "Id");
        }
    }
}
