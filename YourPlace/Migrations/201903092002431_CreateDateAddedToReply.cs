namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDateAddedToReply : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Replies", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Replies", "CreateDate");
        }
    }
}
