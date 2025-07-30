using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Infrastruture.Migrations
{
    /// <inheritdoc />
    public partial class add_refreshTOken_teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.InsertData(
                table: "RefreshToken",
                columns: new[] { "Id", "Expiration", "Token", "UserId" },
                values: new object[] { 1, new DateTime(2025, 7, 29, 12, 0, 0, 0, DateTimeKind.Utc), "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidW5pcXVlX25hbWUiOiJhZG1pbiIsImVtYWlsIjoidGVzdGVAdGVzdGUuY29tIiwiZXhwIjoxNzUzODM4MDg4LCJpc3MiOiJUZXN0SXNzdWVyIiwiYXVkIjoiVGVzdEF1ZGllbmNlIn0.Xv3JXQ9s1QjvPTe_AmnFldqdrzTGkd1UKnvfUU5jRfU", 1 });

     
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
     

            migrationBuilder.DeleteData(
                table: "RefreshToken",
                keyColumn: "Id",
                keyValue: 1);

       
        }
    }
}
