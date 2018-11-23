namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5dd7c697-1e8d-44f1-8c7b-162530f091b1', N'admin@vidly.com', 0, N'AMn126hkuNI4nEvhJ+JkuTu/UhnXRp1URSZsRYWkkCZFgA7E5lDMN+m+BjO6Zo72uQ==', N'dbded298-a53a-4b84-9314-2c3648dd3bfa', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'868667bd-57ad-4e3a-bf1f-be38dff6df18', N'roberto@mail.com', 0, N'ADEm51KOMnBaa3TrK8zrNpbDObHm0B6yzA1QmfaJlO5jE8nffkDaES5cBpPCLLFOmg==', N'085e78ef-027e-4d65-8ad4-481a57a0aa9e', NULL, 0, 0, NULL, 1, 0, N'roberto@mail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e7e53655-2520-4b4d-a84d-28080579e82c', N'guest@vidly.com', 0, N'AFsSAiGjoCSYeL5Gtp3L0fqm/Mt1ZeiknNl8jCJmhUOuy9Sy36jlb6sKlvKOG/uRYQ==', N'da96a485-36f9-46b0-b694-91f9e64db7cd', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c132b9ee-65b3-43dd-97cb-9566b84cfc5d', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5dd7c697-1e8d-44f1-8c7b-162530f091b1', N'c132b9ee-65b3-43dd-97cb-9566b84cfc5d')

");
        }
        
        public override void Down()
        {
        }
    }
}
