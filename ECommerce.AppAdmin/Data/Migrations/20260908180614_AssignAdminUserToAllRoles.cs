using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.AppAdmin.Data.Migrations
{
    /// <inheritdoc />
    public partial class AssignAdminUserToAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[UserRoles] (UserId,RoleId) (SELECT 'bd3502ba-488a-4222-90d0-8ae185887b23' , Id FROM [Security].[Roles])");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRoles] WHERE UserId='bd3502ba-488a-4222-90d0-8ae185887b23' ");

        }
    }
}
