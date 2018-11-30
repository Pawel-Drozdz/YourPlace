namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationForRestaurant : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Restaurants", "RestaurantType", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Localisation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Localisation", c => c.String());
            AlterColumn("dbo.Restaurants", "RestaurantType", c => c.String());
            AlterColumn("dbo.Restaurants", "Name", c => c.String());
        }
    }
}
