using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class zxc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Users_userId",
                table: "CarRentals");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "CarRentals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRentals_userId",
                table: "CarRentals",
                newName: "IX_CarRentals_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Users_UserId",
                table: "CarRentals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Users_UserId",
                table: "CarRentals");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CarRentals",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRentals_UserId",
                table: "CarRentals",
                newName: "IX_CarRentals_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Users_userId",
                table: "CarRentals",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
