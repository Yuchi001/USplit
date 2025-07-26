using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTransactionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Families_FamilyId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FamilyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "OwnerUserId");

            migrationBuilder.AddColumn<int>(
                name: "FamilyEntityId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SplitType",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Debts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionEntityUserEntity",
                columns: table => new
                {
                    ParticipantsId = table.Column<int>(type: "integer", nullable: false),
                    TransactionEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionEntityUserEntity", x => new { x.ParticipantsId, x.TransactionEntityId });
                    table.ForeignKey(
                        name: "FK_TransactionEntityUserEntity_Transactions_TransactionEntityId",
                        column: x => x.TransactionEntityId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionEntityUserEntity_Users_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FamilyEntityId",
                table: "Transactions",
                column: "FamilyEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OwnerUserId_FamilyId",
                table: "Transactions",
                columns: new[] { "OwnerUserId", "FamilyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Debts_TransactionId",
                table: "Debts",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntityUserEntity_TransactionEntityId",
                table: "TransactionEntityUserEntity",
                column: "TransactionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Transactions_TransactionId",
                table: "Debts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Families_FamilyEntityId",
                table: "Transactions",
                column: "FamilyEntityId",
                principalTable: "Families",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_FamilyId",
                table: "Transactions",
                columns: new[] { "OwnerUserId", "FamilyId" },
                principalTable: "UserFamilies",
                principalColumns: new[] { "UserId", "FamilyId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Transactions_TransactionId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Families_FamilyEntityId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserFamilies_OwnerUserId_FamilyId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionEntityUserEntity");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FamilyEntityId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OwnerUserId_FamilyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Debts_TransactionId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "FamilyEntityId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SplitType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Debts");

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                table: "Transactions",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FamilyId",
                table: "Transactions",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Families_FamilyId",
                table: "Transactions",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
