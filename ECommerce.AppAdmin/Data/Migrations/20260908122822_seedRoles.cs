using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.AppAdmin.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table:"Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values:new object[]{Guid.NewGuid().ToString(),"User","User",Guid.NewGuid().ToString() },
                schema: "Security"

                );
            migrationBuilder.InsertData(
               table: "Roles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin", Guid.NewGuid().ToString() },
               schema: "Security"

               );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Roles]");
        }
    }
}
