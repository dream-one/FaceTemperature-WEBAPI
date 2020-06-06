namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "SchoolId", c => c.Int(nullable: false));
            CreateIndex("dbo.Equipments", "SchoolId");
            AddForeignKey("dbo.Equipments", "SchoolId", "dbo.Schools", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "SchoolId", "dbo.Schools");
            DropIndex("dbo.Equipments", new[] { "SchoolId" });
            DropColumn("dbo.Equipments", "SchoolId");
        }
    }
}
