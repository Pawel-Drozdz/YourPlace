namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJoinTableRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Rating = c.Byte(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RestaurantId, t.UserId })
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.RestaurantId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rates", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rates", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Rates", new[] { "User_Id" });
            DropIndex("dbo.Rates", new[] { "RestaurantId" });
            DropTable("dbo.Rates");
        }
    }
}
