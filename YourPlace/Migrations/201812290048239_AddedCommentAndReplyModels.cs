namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentAndReplyModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentReplyId = c.Int(nullable: false),
                        Body = c.String(),
                        AuthorName = c.String(),
                        CommentId = c.Int(nullable: false),
                        Reply_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Replies", t => t.Reply_Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.CommentId)
                .Index(t => t.Reply_Id);
            
            AddColumn("dbo.Comments", "AuthorId", c => c.Guid(nullable: false));
            AddColumn("dbo.Comments", "AuthorName", c => c.String());
            AddColumn("dbo.Comments", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Replies", "Reply_Id", "dbo.Replies");
            DropIndex("dbo.Replies", new[] { "Reply_Id" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropColumn("dbo.Comments", "DateTime");
            DropColumn("dbo.Comments", "AuthorName");
            DropColumn("dbo.Comments", "AuthorId");
            DropTable("dbo.Replies");
        }
    }
}
