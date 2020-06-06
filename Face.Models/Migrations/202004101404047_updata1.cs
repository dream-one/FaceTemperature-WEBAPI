namespace Face.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updata1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EnterAndLeaves", "Temperature", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EnterAndLeaves", "Temperature", c => c.Single(nullable: false));
        }
    }
}
