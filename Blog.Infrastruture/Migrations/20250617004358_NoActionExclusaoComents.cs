using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class NoActionExclusaoComents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coment_AspNetUsers_UserId",
                table: "Coment");

            migrationBuilder.DropForeignKey(
                name: "FK_Coment_Post_PostId",
                table: "Coment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_UserId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coment",
                table: "Coment");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Coment",
                newName: "Coments");

            migrationBuilder.RenameIndex(
                name: "IX_Post_UserId",
                table: "Posts",
                newName: "IX_Posts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coment_UserId",
                table: "Coments",
                newName: "IX_Coments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coment_PostId",
                table: "Coments",
                newName: "IX_Coments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coments",
                table: "Coments",
                column: "ComentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coments_AspNetUsers_UserId",
                table: "Coments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coments_Posts_PostId",
                table: "Coments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coments_AspNetUsers_UserId",
                table: "Coments");

            migrationBuilder.DropForeignKey(
                name: "FK_Coments_Posts_PostId",
                table: "Coments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coments",
                table: "Coments");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Coments",
                newName: "Coment");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_UserId",
                table: "Post",
                newName: "IX_Post_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coments_UserId",
                table: "Coment",
                newName: "IX_Coment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Coments_PostId",
                table: "Coment",
                newName: "IX_Coment_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coment",
                table: "Coment",
                column: "ComentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coment_AspNetUsers_UserId",
                table: "Coment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coment_Post_PostId",
                table: "Coment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_UserId",
                table: "Post",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
