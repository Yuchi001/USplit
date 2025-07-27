using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTransactionsFromFamilyAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Families_FamilyEntityId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserEntityId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FamilyEntityId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserEntityId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FamilyEntityId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyEntityId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FamilyEntityId",
                table: "Transactions",
                column: "FamilyEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserEntityId",
                table: "Transactions",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Families_FamilyEntityId",
                table: "Transactions",
                column: "FamilyEntityId",
                principalTable: "Families",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserEntityId",
                table: "Transactions",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
