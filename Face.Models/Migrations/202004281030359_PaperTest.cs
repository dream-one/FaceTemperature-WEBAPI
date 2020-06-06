namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaperTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ClassName", c => c.String());
            AddColumn("dbo.Students", "GradeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "GradeName");
            DropColumn("dbo.Students", "ClassName");
        }
    }
}
