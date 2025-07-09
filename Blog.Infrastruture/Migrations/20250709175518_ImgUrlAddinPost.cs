using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class ImgUrlAddinPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 6,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 7,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 8,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 9,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 10,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 11,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 12,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 13,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 14,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 15,
                column: "ImageUrl",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");
        }
    }
}
