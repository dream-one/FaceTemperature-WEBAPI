namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnterAndLeaves", "SchoolID", c => c.Int(nullable: false));
            CreateIndex("dbo.EnterAndLeaves", "SchoolID");
            AddForeignKey("dbo.EnterAndLeaves", "SchoolID", "dbo.Schools", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnterAndLeaves", "SchoolID", "dbo.Schools");
            DropIndex("dbo.EnterAndLeaves", new[] { "SchoolID" });
            DropColumn("dbo.EnterAndLeaves", "SchoolID");
        }
    }
}
