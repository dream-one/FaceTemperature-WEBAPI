namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Script1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StudentID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "StudentID");
        }
    }
}
