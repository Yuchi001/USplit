using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedFamilyOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "Families",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Families_OwnerUserId",
                table: "Families",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Users_OwnerUserId",
                table: "Families",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_Users_OwnerUserId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Families_OwnerUserId",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Families");
        }
    }
}
