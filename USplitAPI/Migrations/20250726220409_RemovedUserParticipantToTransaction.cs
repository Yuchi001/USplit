using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace USplitAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUserParticipantToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionEntityUserEntity");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserEntityId",
                table: "Transactions",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserEntityId",
                table: "Transactions",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserEntityId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserEntityId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Transactions");

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
                name: "IX_TransactionEntityUserEntity_TransactionEntityId",
                table: "TransactionEntityUserEntity",
                column: "TransactionEntityId");
        }
    }
}
