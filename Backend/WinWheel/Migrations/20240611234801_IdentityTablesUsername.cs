using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinWheel.Migrations
{
    /// <inheritdoc />
    public partial class IdentityTablesUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { "b2fcf741-4583-43cf-b2a2-e1e57b85ac49", "Tom Cruise" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { "bcb3104b-34b8-4776-8560-4e81110327bb", "Keanu Reeves" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                columns: new[] { "ConcurrencyStamp", "Name", "UserName" },
                values: new object[] { "aa4acc20-79a0-4996-b322-6ca3b296ad2d", "Tom Cruise", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "PlayerId",
                keyValue: new Guid("b7e9e3a1-3e29-4e6f-8e1a-2f3b6e0b6e1f"),
                columns: new[] { "ConcurrencyStamp", "Name", "UserName" },
                values: new object[] { "ed9cea62-4619-43e4-9038-d1c55f020ec8", "Keanu Reeves", null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name",
                unique: true);
        }
    }
}
