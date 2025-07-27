using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class SpecifiedTransactionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_FamilyId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "Transactions",
                newName: "OwnerFamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OwnerUserId_FamilyId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_OwnerFamilyId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OwnerFamilyId",
                table: "Transactions",
                newName: "FamilyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OwnerUserId_OwnerFamilyId",
                table: "Transactions",
                newName: "IX_Transactions_OwnerUserId_FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_FamilyId",
                table: "Transactions",
                columns: new[] { "OwnerUserId", "FamilyId" },
                principalTable: "UserFamilies",
                principalColumns: new[] { "UserId", "FamilyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
