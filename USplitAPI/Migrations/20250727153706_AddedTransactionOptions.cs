using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransactionOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_OwnerFamilyId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                table: "Transactions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OwnerFamilyId",
                table: "Transactions",
                newName: "FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OwnerUserId_OwnerFamilyId",
                table: "Transactions",
                newName: "IX_Transactions_UserId_FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserFamilies_UserId_FamilyId",
                table: "Transactions",
                columns: new[] { "UserId", "FamilyId" },
                principalTable: "UserFamilies",
                principalColumns: new[] { "UserId", "FamilyId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserFamilies_UserId_FamilyId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "OwnerUserId");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "Transactions",
                newName: "OwnerFamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_UserId_FamilyId",
                table: "Transactions",
                newName: "IX_Transactions_OwnerUserId_OwnerFamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_OwnerFamilyId",
                table: "Transactions",
                columns: new[] { "OwnerUserId", "OwnerFamilyId" },
                principalTable: "UserFamilies",
                principalColumns: new[] { "UserId", "FamilyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
