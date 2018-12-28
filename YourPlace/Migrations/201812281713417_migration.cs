namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AuthorId", c => c.Guid(nullable: false));
            AddColumn("dbo.Comments", "AuthorName", c => c.String());
            AddColumn("dbo.Comments", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "DateTime");
            DropColumn("dbo.Comments", "AuthorName");
            DropColumn("dbo.Comments", "AuthorId");
        }
    }
}
