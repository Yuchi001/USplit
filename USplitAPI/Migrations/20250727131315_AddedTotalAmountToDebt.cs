using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalAmountToDebt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Debts");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Debts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "Debts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Debts");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Debts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Debts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
