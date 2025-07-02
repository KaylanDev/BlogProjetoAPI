using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class addPostsEComentsTestes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Content", "CreatedAt", "Title", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 2, "Conteúdo do post 2", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 2", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, "Conteúdo do post 3", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 3", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, "Conteúdo do post 4", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 4", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, "Conteúdo do post 5", new DateTime(2025, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 5", new DateTime(2025, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, "Conteúdo do post 6", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 6", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, "Conteúdo do post 7", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 7", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, "Conteúdo do post 8", new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 8", new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 9, "Conteúdo do post 9", new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 9", new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, "Conteúdo do post 10", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 10", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, "Conteúdo do post 11", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 11", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, "Conteúdo do post 12", new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 12", new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, "Conteúdo do post 13", new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 13", new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 14, "Conteúdo do post 14", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 14", new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 15, "Conteúdo do post 15", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post 15", new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "Coments",
                columns: new[] { "ComentId", "Content", "CreatedAt", "PostId", "UserId" },
                values: new object[,]
                {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Coments",
                keyColumn: "ComentId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 15);
        }
    }
}
