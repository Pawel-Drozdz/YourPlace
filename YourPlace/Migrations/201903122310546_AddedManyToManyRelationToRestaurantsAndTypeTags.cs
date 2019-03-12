namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedManyToManyRelationToRestaurantsAndTypeTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantTypeTags",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false),
                        TypeTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RestaurantId, t.TypeTagId })
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .ForeignKey("dbo.TypeTags", t => t.TypeTagId, cascadeDelete: true)
                .Index(t => t.RestaurantId)
                .Index(t => t.TypeTagId);
            
            CreateTable(
                "dbo.TypeTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantTypeTags", "TypeTagId", "dbo.TypeTags");
            DropForeignKey("dbo.RestaurantTypeTags", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.RestaurantTypeTags", new[] { "TypeTagId" });
            DropIndex("dbo.RestaurantTypeTags", new[] { "RestaurantId" });
            DropTable("dbo.TypeTags");
            DropTable("dbo.RestaurantTypeTags");
        }
    }
}
