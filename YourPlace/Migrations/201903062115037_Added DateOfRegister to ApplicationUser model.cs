namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateOfRegistertoApplicationUsermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DateOfRegister", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DateOfRegister");
        }
    }
}
