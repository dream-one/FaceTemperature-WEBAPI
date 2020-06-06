namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Script : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Grades", "SchoolID", "dbo.Schools");
            DropIndex("dbo.Grades", new[] { "SchoolID" });
            AddColumn("dbo.Classes", "SchoolID", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Grades", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Schools", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Districts", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.EnterAndLeaves", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Equipments", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDelete", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Classes", "SchoolID");
            AddForeignKey("dbo.Classes", "SchoolID", "dbo.Schools", "Id", cascadeDelete: true);
            DropColumn("dbo.Grades", "SchoolID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Grades", "SchoolID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Classes", "SchoolID", "dbo.Schools");
            DropIndex("dbo.Classes", new[] { "SchoolID" });
            DropColumn("dbo.Users", "IsDelete");
            DropColumn("dbo.Students", "IsDelete");
            DropColumn("dbo.Equipments", "IsDelete");
            DropColumn("dbo.EnterAndLeaves", "IsDelete");
            DropColumn("dbo.Districts", "IsDelete");
            DropColumn("dbo.Schools", "IsDelete");
            DropColumn("dbo.Grades", "IsDelete");
            DropColumn("dbo.Classes", "IsDelete");
            DropColumn("dbo.Classes", "SchoolID");
            CreateIndex("dbo.Grades", "SchoolID");
            AddForeignKey("dbo.Grades", "SchoolID", "dbo.Schools", "Id", cascadeDelete: true);
        }
    }
}
