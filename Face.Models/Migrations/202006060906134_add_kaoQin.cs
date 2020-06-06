namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_kaoQin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutTime = c.DateTime(nullable: false),
                        InTime = c.DateTime(nullable: false),
                        SchoolID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schools", t => t.SchoolID, cascadeDelete: true)
                .Index(t => t.SchoolID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "SchoolID", "dbo.Schools");
            DropIndex("dbo.Attendances", new[] { "SchoolID" });
            DropTable("dbo.Attendances");
        }
    }
}
