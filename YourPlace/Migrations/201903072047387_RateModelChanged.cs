namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RateModelChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rates", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rates", "Rating", c => c.Byte(nullable: false));
        }
    }
}
