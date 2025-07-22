using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserDebtRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Users_UserEntityId",
                table: "Debts");

            migrationBuilder.DropIndex(
                name: "IX_Debts_UserEntityId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Debts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Debts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Debts_UserEntityId",
                table: "Debts",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Users_UserEntityId",
                table: "Debts",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
