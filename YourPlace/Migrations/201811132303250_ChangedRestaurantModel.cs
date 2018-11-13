namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRestaurantModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "RestaurantType", c => c.String());
            AddColumn("dbo.Restaurants", "Localisation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "Localisation");
            DropColumn("dbo.Restaurants", "RestaurantType");
        }
    }
}
