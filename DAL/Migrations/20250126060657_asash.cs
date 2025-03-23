using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_system.Migrations
{
    /// <inheritdoc />
    public partial class asash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_AspNetUsers_customerId",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_cards_AspNetUsers_customerId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_loans_AspNetUsers_customerId",
                table: "loans");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_accounts_FromAccountId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_accounts_ToAccountId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transactions",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_loans",
                table: "loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cards",
                table: "cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.RenameTable(
                name: "transactions",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "loans",
                newName: "Loans");

            migrationBuilder.RenameTable(
                name: "cards",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_ToAccountId",
                table: "Transactions",
                newName: "IX_Transactions_ToAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_FromAccountId",
                table: "Transactions",
                newName: "IX_Transactions_FromAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_loans_customerId",
                table: "Loans",
                newName: "IX_Loans_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_cards_customerId",
                table: "Cards",
                newName: "IX_Cards_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_customerId",
                table: "Accounts",
                newName: "IX_Accounts_customerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_customerId",
                table: "Accounts",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_AspNetUsers_customerId",
                table: "Cards",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_AspNetUsers_customerId",
                table: "Loans",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions",
                column: "FromAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions",
                column: "ToAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_customerId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_AspNetUsers_customerId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_AspNetUsers_customerId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_FromAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_ToAccountId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "transactions");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "loans");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "cards");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ToAccountId",
                table: "transactions",
                newName: "IX_transactions_ToAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_FromAccountId",
                table: "transactions",
                newName: "IX_transactions_FromAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_customerId",
                table: "loans",
                newName: "IX_loans_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_customerId",
                table: "cards",
                newName: "IX_cards_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_customerId",
                table: "accounts",
                newName: "IX_accounts_customerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactions",
                table: "transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_loans",
                table: "loans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cards",
                table: "cards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_AspNetUsers_customerId",
                table: "accounts",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cards_AspNetUsers_customerId",
                table: "cards",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_loans_AspNetUsers_customerId",
                table: "loans",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_accounts_FromAccountId",
                table: "transactions",
                column: "FromAccountId",
                principalTable: "accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_accounts_ToAccountId",
                table: "transactions",
                column: "ToAccountId",
                principalTable: "accounts",
                principalColumn: "Id");
        }
    }
}
