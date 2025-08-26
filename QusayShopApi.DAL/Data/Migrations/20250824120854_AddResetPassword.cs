using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QusayShopApi.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Categories",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Brands",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeExpiredDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeExpiredDate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Categories",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Brands",
                newName: "status");
        }
    }
}
