namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCOMMENTentitycreateoneotmanyrelationwithRESTAURANTentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Comments", new[] { "RestaurantId" });
            DropTable("dbo.Comments");
        }
    }
}
