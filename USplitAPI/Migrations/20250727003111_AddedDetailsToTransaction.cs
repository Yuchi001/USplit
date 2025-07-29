using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDetailsToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Transactions");
        }
    }
}
