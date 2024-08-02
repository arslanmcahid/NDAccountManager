using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDAccountManager.Repository.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Accounts",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                newName: "IX_Accounts_OwnerId");

            migrationBuilder.CreateTable(
                name: "SharedAccounts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    IsUnlimited = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedAccounts", x => new { x.UserId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_SharedAccounts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SharedAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedAccounts_AccountId",
                table: "SharedAccounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_OwnerId",
                table: "Accounts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_OwnerId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "SharedAccounts");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Accounts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                newName: "IX_Accounts_UserId");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Accounts",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
