namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleqid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Equipments", "EqID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "EqID", c => c.String());
        }
    }
}
