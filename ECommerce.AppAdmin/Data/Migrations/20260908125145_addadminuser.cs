using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.AppAdmin.Data.Migrations
{
    /// <inheritdoc />
    public partial class addadminuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES ('bd3502ba-488a-4222-90d0-8ae185887b23', 'admin', 'ADMIN', 'admin@dd.cc', 'ADMIN@DD.CC', 0, 'AQAAAAIAAYagAAAAEHDmXinqS6QlkUE8GQLVuBJ+mxCvh5gnRLQ1cXlawqDJMQ56r5x76MxN51PC9N06ug==', 'CCTNTS2V6BHMGP6KN2RHVFNWKHYZYELR', '7c57d973-0773-4ecf-b1cb-eb490e72d285', NULL, 0, 0, NULL, 1, 0, 'admin', 'admin', NULL);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Users] WHERE Id='bd3502ba-488a-4222-90d0-8ae185887b23' ");
        }
    }
}
