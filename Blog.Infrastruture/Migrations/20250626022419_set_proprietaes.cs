using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class set_proprietaes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash", "Username" },
                values: new object[] { 1, "teste@teste.com", "AHIjjUiOw1IVwAau3589yIYYrlMf6mjnvu98HDhs36Kx7ZwEqYnCw72xklLO4yZ1gw==", "admin" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Content", "CreatedAt", "Title", "UpdatedAt", "UserId" },
                values: new object[] { 1, "Este é o conteúdo do primeiro post.", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Primeiro Post", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Coments",
                columns: new[] { "ComentId", "Content", "CreatedAt", "PostId", "UserId" },
                values: new object[] { 1, "Este é o primeiro comentário.", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
