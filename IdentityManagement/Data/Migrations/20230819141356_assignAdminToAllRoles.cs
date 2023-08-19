using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityManagement.Data.Migrations
{
    public partial class assignAdminToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into [Security].[UserRoles] (UserId , RoleId) SElECT 'b1ccf7b6-1a8a-4619-af4d-024c4ccdecd2' , Id  From [Security].[Roles]");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRoles] WHERE UserId ='b1ccf7b6-1a8a-4619-af4d-024c4ccdecd2' ");

        }
    }
}
