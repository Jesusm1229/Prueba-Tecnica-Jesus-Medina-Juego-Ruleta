using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWheel.Migrations
{
    /// <inheritdoc />
    public partial class IdentityTablesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                column: "ConcurrencyStamp",
                value: "aa4acc20-79a0-4996-b322-6ca3b296ad2d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
                column: "ConcurrencyStamp",
                value: "ed9cea62-4619-43e4-9038-d1c55f020ec8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                column: "ConcurrencyStamp",
                value: "7577b37d-2bd3-4966-9edf-49c5bc57655a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
                column: "ConcurrencyStamp",
                value: "c217c443-07b8-44a1-abd4-823403f66266");
        }
    }
}
