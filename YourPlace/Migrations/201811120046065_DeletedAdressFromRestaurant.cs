namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedAdressFromRestaurant : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Restaurants", "Adress_Id", "dbo.Adresses");
            DropIndex("dbo.Restaurants", new[] { "Adress_Id" });
            DropColumn("dbo.Restaurants", "Adress_Id");
            DropTable("dbo.Adresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        NameOfTheStreet = c.String(),
                        BuildingNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Restaurants", "Adress_Id", c => c.Int());
            CreateIndex("dbo.Restaurants", "Adress_Id");
            AddForeignKey("dbo.Restaurants", "Adress_Id", "dbo.Adresses", "Id");
        }
    }
}
