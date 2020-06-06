namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsGraduation = c.Boolean(nullable: false),
                        GradeID = c.Int(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grades", t => t.GradeID, cascadeDelete: true)
                .Index(t => t.GradeID);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EnterAndLeaves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaceId = c.String(maxLength: 128),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faces", t => t.FaceId)
                .Index(t => t.FaceId);
            
            CreateTable(
                "dbo.Faces",
                c => new
                    {
                        FaceId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        IsStudent = c.Boolean(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FaceId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        EquipmentNum = c.String(nullable: false, maxLength: 128),
                        EquipmentIP = c.String(),
                        EqID = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EquipmentNum);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        FaceId = c.String(nullable: false, maxLength: 128),
                        ClassId = c.Int(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FaceId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.EnterAndLeaves", "FaceId", "dbo.Faces");
            DropForeignKey("dbo.Classes", "GradeID", "dbo.Grades");
            DropIndex("dbo.Students", new[] { "ClassId" });
            DropIndex("dbo.EnterAndLeaves", new[] { "FaceId" });
            DropIndex("dbo.Classes", new[] { "GradeID" });
            DropTable("dbo.Students");
            DropTable("dbo.Schools");
            DropTable("dbo.Equipments");
            DropTable("dbo.Faces");
            DropTable("dbo.EnterAndLeaves");
            DropTable("dbo.Grades");
            DropTable("dbo.Classes");
        }
    }
}
