namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ad1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Districts", "Cities_Id", "dbo.Cities");
            DropIndex("dbo.Districts", new[] { "Cities_Id" });
            RenameColumn(table: "dbo.Districts", name: "Cities_Id", newName: "CityID");
            AlterColumn("dbo.Districts", "CityID", c => c.Int(nullable: false));
            CreateIndex("dbo.Districts", "CityID");
            AddForeignKey("dbo.Districts", "CityID", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Districts", "CityID", "dbo.Cities");
            DropIndex("dbo.Districts", new[] { "CityID" });
            AlterColumn("dbo.Districts", "CityID", c => c.Int());
            RenameColumn(table: "dbo.Districts", name: "CityID", newName: "Cities_Id");
            CreateIndex("dbo.Districts", "Cities_Id");
            AddForeignKey("dbo.Districts", "Cities_Id", "dbo.Cities", "Id");
        }
    }
}
