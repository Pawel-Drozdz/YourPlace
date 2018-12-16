namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserPropertiesToCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AuthorId", c => c.Guid(nullable: false));
            AddColumn("dbo.Comments", "AuthorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "AuthorName");
            DropColumn("dbo.Comments", "AuthorId");
        }
    }
}
