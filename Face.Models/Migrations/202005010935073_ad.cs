namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Districts", "Cities_Id", c => c.Int());
            CreateIndex("dbo.Districts", "Cities_Id");
            AddForeignKey("dbo.Districts", "Cities_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Districts", "Cities_Id", "dbo.Cities");
            DropIndex("dbo.Districts", new[] { "Cities_Id" });
            DropColumn("dbo.Districts", "Cities_Id");
        }
    }
}
