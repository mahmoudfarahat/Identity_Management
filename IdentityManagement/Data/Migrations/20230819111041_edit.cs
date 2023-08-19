using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityManagement.Data.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "Secuitry",
                newName: "UserTokens",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Secuitry",
                newName: "Users",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Secuitry",
                newName: "UserRoles",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Secuitry",
                newName: "UserLogins",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Secuitry",
                newName: "UserClaims",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Secuitry",
                newName: "Roles",
                newSchema: "Security");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "Secuitry",
                newName: "RoleClaims",
                newSchema: "Security");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Secuitry");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "Security",
                newName: "UserTokens",
                newSchema: "Secuitry");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Security",
                newName: "Users",
                newSchema: "Secuitry");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Security",
                newName: "UserRoles",
                newSchema: "Secuitry");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "Security",
                newName: "UserLogins",
                newSchema: "Secuitry");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "Security",
                newName: "UserClaims",
                newSchema: "Secuitry");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Security",
                newName: "Roles",
                newSchema: "Secuitry");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "Security",
                newName: "RoleClaims",
                newSchema: "Secuitry");
        }
    }
}
