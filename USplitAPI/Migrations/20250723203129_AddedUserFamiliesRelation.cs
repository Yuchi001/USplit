using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserFamiliesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_UserFamilyJoinedEntity_OwnerUserId_OwnerFamilyId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFamilyJoinedEntity_Families_FamilyId",
                table: "UserFamilyJoinedEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFamilyJoinedEntity_Users_UserId",
                table: "UserFamilyJoinedEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFamilyJoinedEntity",
                table: "UserFamilyJoinedEntity");

            migrationBuilder.RenameTable(
                name: "UserFamilyJoinedEntity",
                newName: "UserFamilies");

            migrationBuilder.RenameIndex(
                name: "IX_UserFamilyJoinedEntity_FamilyId",
                table: "UserFamilies",
                newName: "IX_UserFamilies_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFamilies",
                table: "UserFamilies",
                columns: new[] { "UserId", "FamilyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_UserFamilies_OwnerUserId_OwnerFamilyId",
                table: "Debts",
                columns: new[] { "OwnerUserId", "OwnerFamilyId" },
                principalTable: "UserFamilies",
                principalColumns: new[] { "UserId", "FamilyId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFamilies_Families_FamilyId",
                table: "UserFamilies",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFamilies_Users_UserId",
                table: "UserFamilies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_UserFamilies_OwnerUserId_OwnerFamilyId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFamilies_Families_FamilyId",
                table: "UserFamilies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFamilies_Users_UserId",
                table: "UserFamilies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFamilies",
                table: "UserFamilies");

            migrationBuilder.RenameTable(
                name: "UserFamilies",
                newName: "UserFamilyJoinedEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserFamilies_FamilyId",
                table: "UserFamilyJoinedEntity",
                newName: "IX_UserFamilyJoinedEntity_FamilyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFamilyJoinedEntity",
                table: "UserFamilyJoinedEntity",
                columns: new[] { "UserId", "FamilyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_UserFamilyJoinedEntity_OwnerUserId_OwnerFamilyId",
                table: "Debts",
                columns: new[] { "OwnerUserId", "OwnerFamilyId" },
                principalTable: "UserFamilyJoinedEntity",
                principalColumns: new[] { "UserId", "FamilyId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFamilyJoinedEntity_Families_FamilyId",
                table: "UserFamilyJoinedEntity",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFamilyJoinedEntity_Users_UserId",
                table: "UserFamilyJoinedEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
