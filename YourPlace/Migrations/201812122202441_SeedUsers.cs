namespace YourPlace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6aeeca89-374c-48f9-9cf5-4d1024e7d5ef', N'admin@email.com', 0, N'AJiA/1oEi6sutvFBgjoI/d2n2KpWwilE66CtT2+BiolK/YmizJy52pdgec8bGQR4Dg==', N'4b43c2a3-4e17-4ef1-a87b-b2e2c8d64179', NULL, 0, 0, NULL, 1, 0, N'admin@email.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b68a6ee8-ed43-478b-a87a-c0a911a4c8dc', N'guest@email.com', 0, N'AA+n3Mq/g+7QhyFzwQGqDrAViqnud091MvHNhPuteCbLOCgwzA0jKNZvrehz+ZDkpg==', N'61831f26-e7d9-4803-b597-87f894f10d66', NULL, 0, 0, NULL, 1, 0, N'guest@email.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'26f6a014-880e-41da-b9e1-7842de733e8b', N'Admin')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6aeeca89-374c-48f9-9cf5-4d1024e7d5ef', N'26f6a014-880e-41da-b9e1-7842de733e8b')

");
        }
        
        public override void Down()
        {
        }
    }
}
