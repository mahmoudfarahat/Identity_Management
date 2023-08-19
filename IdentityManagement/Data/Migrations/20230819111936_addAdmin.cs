using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityManagement.Data.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LasttName], [ProfilePicture]) VALUES (N'b1ccf7b6-1a8a-4619-af4d-024c4ccdecd2', N'mahmoud.ped', N'MAHMOUD.PED', N'mahmoud.ped@gmail.com', N'MAHMOUD.PED@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEHLEv9DkqXHXCy4GLeUJS1q4cEbxdQwgjrxRQt6OEEzVC4kCsrnu/ull/JPJ7FIbjA==', N'ABZIAII4LQ5QEUH6OKCGHSCJNJNDLHGH', N'72835a10-440d-477d-b72e-67948b1be677', N'01090449567', 0, NULL, 1, 0, N'Mahmoud', N'Farahat', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Users] WHERE Id ='b1ccf7b6-1a8a-4619-af4d-024c4ccdecd2' ");
        }
    }
}
