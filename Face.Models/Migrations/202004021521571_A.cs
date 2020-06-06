namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class A : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Name", c => c.String());
            AddColumn("dbo.Students", "XueHao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "XueHao");
            DropColumn("dbo.Students", "Name");
        }
    }
}
