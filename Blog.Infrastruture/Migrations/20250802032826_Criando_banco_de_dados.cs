using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class Criando_banco_de_dados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coments",
                columns: table => new
                {
                    ComentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coments", x => x.ComentId);
                    table.ForeignKey(
                        name: "FK_Coments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Coments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "teste@teste.com", "AHIjjUiOw1IVwAau3589yIYYrlMf6mjnvu98HDhs36Kx7ZwEqYnCw72xklLO4yZ1gw==", "admin" },
                    { 2, "teste2@teste.com", "AHIjjUiOw1IVwAau3589yIYYrlMf6mjnvu98HDhs36Kx7ZwEqYnCw72xklLO4yZ1gw==", "admin2" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Content", "CreatedAt", "ImageUrl", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Este é o conteúdo do primeiro post.", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Primeiro Post", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Conteúdo do post 2", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 2", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "Conteúdo do post 3", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 3", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "Conteúdo do post 4", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 4", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "Conteúdo do post 5", new DateTime(2025, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 5", new DateTime(2025, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, "Conteúdo do post 6", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 6", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, "Conteúdo do post 7", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 7", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, "Conteúdo do post 8", new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 8", new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, "Conteúdo do post 9", new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 9", new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, "Conteúdo do post 10", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 10", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, "Conteúdo do post 11", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 11", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, "Conteúdo do post 12", new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 12", new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, "Conteúdo do post 13", new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 13", new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 14, "Conteúdo do post 14", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 14", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 15, "Conteúdo do post 15", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Post 15", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "RefreshToken",
                columns: new[] { "Id", "Expiration", "Token", "UserId" },
                values: new object[] { 1, new DateTime(2025, 7, 29, 12, 0, 0, 0, DateTimeKind.Utc), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidW5pcXVlX25hbWUiOiJhZG1pbiIsImVtYWlsIjoidGVzdGVAdGVzdGUuY29tIiwiZXhwIjoxNzUzODM4MDg4LCJpc3MiOiJUZXN0SXNzdWVyIiwiYXVkIjoiVGVzdEF1ZGllbmNlIn0.Xv3JXQ9s1QjvPTe_AmnFldqdrzTGkd1UKnvfUU5jRfU", 1 });

            migrationBuilder.InsertData(
                table: "Coments",
                columns: new[] { "ComentId", "Content", "CreatedAt", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "Este é o primeiro comentário.", new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "Comentário 2", new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, "Comentário 3", new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 4, "Comentário 4", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1 },
                    { 5, "Comentário 5", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 6, "Comentário 6", new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1 },
                    { 7, "Comentário 7", new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1 },
                    { 8, "Comentário 8", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1 },
                    { 9, "Comentário 9", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1 },
                    { 10, "Comentário 10", new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1 },
                    { 11, "Comentário 11", new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 1 },
                    { 12, "Comentário 12", new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 1 },
                    { 13, "Comentário 13", new DateTime(2025, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13, 1 },
                    { 14, "Comentário 14", new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 14, 1 },
                    { 15, "Comentário 15", new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coments_PostId",
                table: "Coments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Coments_UserId",
                table: "Coments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coments");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
