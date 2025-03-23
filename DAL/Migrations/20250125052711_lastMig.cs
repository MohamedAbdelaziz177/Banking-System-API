using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_system.Migrations
{
    /// <inheritdoc />
    public partial class lastMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_AspNetUsers_customerId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_loans_AspNetUsers_customerId",
                table: "loans");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_AspNetUsers_customerId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_loans_AspNetUsers_customerId",
                table: "loans");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_AspNetUsers_customerId",
                table: "cards",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loans_AspNetUsers_customerId",
                table: "loans",
                column: "customerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
