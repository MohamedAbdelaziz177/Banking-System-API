using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking_system.Migrations
{
    /// <inheritdoc />
    public partial class AddingCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "RefreshTokens",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AppUserID",
                table: "RefreshTokens",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AppUserID",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "RefreshTokens",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_AppUserID",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
