using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_system.Migrations
{
    /// <inheritdoc />
    public partial class sdad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_FromAccountId",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_ToAccountId",
                table: "transactions");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_FromAccountId",
                table: "transactions",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_ToAccountId",
                table: "transactions",
                column: "ToAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_FromAccountId",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_ToAccountId",
                table: "transactions");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_FromAccountId",
                table: "transactions",
                column: "FromAccountId",
                unique: true,
                filter: "[FromAccountId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_ToAccountId",
                table: "transactions",
                column: "ToAccountId",
                unique: true,
                filter: "[ToAccountId] IS NOT NULL");
        }
    }
}
