namespace Face.Models.Migrations {
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EnterAndLeaves", "FaceId", "dbo.Faces");
            DropIndex("dbo.EnterAndLeaves", new[] { "FaceId" });
            DropPrimaryKey("dbo.Equipments");
            DropPrimaryKey("dbo.Students");
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Grades", "SchoolID", c => c.Int(nullable: false));
            AddColumn("dbo.EnterAndLeaves", "DeviceIld", c => c.String(nullable: false));
            AddColumn("dbo.EnterAndLeaves", "DeviceId", c => c.String());
            AddColumn("dbo.EnterAndLeaves", "FaceName", c => c.String(nullable: false));
            AddColumn("dbo.EnterAndLeaves", "AuthType", c => c.String(nullable: false));
            AddColumn("dbo.EnterAndLeaves", "InOutType", c => c.Int(nullable: false));
            AddColumn("dbo.EnterAndLeaves", "SnapshotUrl", c => c.String());
            AddColumn("dbo.EnterAndLeaves", "SnapshotContent", c => c.String());
            AddColumn("dbo.EnterAndLeaves", "Temperature", c => c.String());
            AddColumn("dbo.EnterAndLeaves", "Time", c => c.String(nullable: false));
            AddColumn("dbo.Equipments", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Equipments", "Local", c => c.String());
            AddColumn("dbo.Schools", "DistrictID", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Students", "PhoneNum", c => c.String());
            AddColumn("dbo.Students", "Image", c => c.String());
            AddColumn("dbo.Users", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "organizationID", c => c.Int(nullable: false));
            AlterColumn("dbo.EnterAndLeaves", "FaceId", c => c.String(nullable: false));
            AlterColumn("dbo.Equipments", "EquipmentNum", c => c.String());
            AddPrimaryKey("dbo.Equipments", "Id");
            AddPrimaryKey("dbo.Students", "Id");
            CreateIndex("dbo.Grades", "SchoolID");
            CreateIndex("dbo.Schools", "DistrictID");
            AddForeignKey("dbo.Schools", "DistrictID", "dbo.Districts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Grades", "SchoolID", "dbo.Schools", "Id", cascadeDelete: true);
            DropColumn("dbo.Students", "FaceId");
            DropColumn("dbo.Students", "XueHao");
            DropTable("dbo.Faces");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Students", "XueHao", c => c.String());
            AddColumn("dbo.Students", "FaceId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Grades", "SchoolID", "dbo.Schools");
            DropForeignKey("dbo.Schools", "DistrictID", "dbo.Districts");
            DropIndex("dbo.Schools", new[] { "DistrictID" });
            DropIndex("dbo.Grades", new[] { "SchoolID" });
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.Equipments");
            AlterColumn("dbo.Equipments", "EquipmentNum", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.EnterAndLeaves", "FaceId", c => c.String(maxLength: 128));
            DropColumn("dbo.Users", "organizationID");
            DropColumn("dbo.Users", "Level");
            DropColumn("dbo.Students", "Image");
            DropColumn("dbo.Students", "PhoneNum");
            DropColumn("dbo.Students", "Id");
            DropColumn("dbo.Schools", "DistrictID");
            DropColumn("dbo.Equipments", "Local");
            DropColumn("dbo.Equipments", "Id");
            DropColumn("dbo.EnterAndLeaves", "Time");
            DropColumn("dbo.EnterAndLeaves", "Temperature");
            DropColumn("dbo.EnterAndLeaves", "SnapshotContent");
            DropColumn("dbo.EnterAndLeaves", "SnapshotUrl");
            DropColumn("dbo.EnterAndLeaves", "InOutType");
            DropColumn("dbo.EnterAndLeaves", "AuthType");
            DropColumn("dbo.EnterAndLeaves", "FaceName");
            DropColumn("dbo.EnterAndLeaves", "DeviceId");
            DropColumn("dbo.EnterAndLeaves", "DeviceIld");
            DropColumn("dbo.Grades", "SchoolID");
            DropTable("dbo.Districts");
            AddPrimaryKey("dbo.Students", "FaceId");
            AddPrimaryKey("dbo.Equipments", "EquipmentNum");
            CreateIndex("dbo.EnterAndLeaves", "FaceId");
            AddForeignKey("dbo.EnterAndLeaves", "FaceId", "dbo.Faces", "FaceId");
        }
    }
}
