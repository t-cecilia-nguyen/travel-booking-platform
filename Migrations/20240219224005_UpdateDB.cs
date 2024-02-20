using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_CarRentals_CarRentalId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Flights_FlightId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotel_HotelId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Users_UserId",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_UserId",
                table: "Bookings",
                newName: "IX_Bookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_HotelId",
                table: "Bookings",
                newName: "IX_Bookings_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_FlightId",
                table: "Bookings",
                newName: "IX_Bookings_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_CarRentalId",
                table: "Bookings",
                newName: "IX_Bookings_CarRentalId");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarRentalId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CarRentals_CarRentalId",
                table: "Bookings",
                column: "CarRentalId",
                principalTable: "CarRentals",
                principalColumn: "CarRentalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Hotel_HotelId",
                table: "Bookings",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CarRentals_CarRentalId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Flights_FlightId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Hotel_HotelId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId",
                table: "Booking",
                newName: "IX_Booking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_HotelId",
                table: "Booking",
                newName: "IX_Booking_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_FlightId",
                table: "Booking",
                newName: "IX_Booking_FlightId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CarRentalId",
                table: "Booking",
                newName: "IX_Booking_CarRentalId");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarRentalId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_CarRentals_CarRentalId",
                table: "Booking",
                column: "CarRentalId",
                principalTable: "CarRentals",
                principalColumn: "CarRentalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Flights_FlightId",
                table: "Booking",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotel_HotelId",
                table: "Booking",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Users_UserId",
                table: "Booking",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
