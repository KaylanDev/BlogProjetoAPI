using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class Add_new_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash", "Username" },
                values: new object[] { 2, "teste2@teste.com", "AHIjjUiOw1IVwAau3589yIYYrlMf6mjnvu98HDhs36Kx7ZwEqYnCw72xklLO4yZ1gw==", "admin2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);
        }
    }
}
