using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MaxPassengers = table.Column<int>(type: "int", nullable: false),
                    CurrentPassengers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StarRate = table.Column<int>(type: "int", nullable: false),
                    NumberOfRooms = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    CarRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentalCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DropOffDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPassengers = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.CarRentalId);
                    table.ForeignKey(
                        name: "FK_CarRentals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "HotelBookings",
                columns: table => new
                {
                    HotelBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumOfRoomsToBook = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelBookings", x => x.HotelBookingId);
                    table.ForeignKey(
                        name: "FK_HotelBookings_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelBookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    CarRentalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalTable: "CarRentals",
                        principalColumn: "CarRentalId");
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId");
                    table.ForeignKey(
                        name: "FK_Bookings_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId");
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "CarRentalId", "Available", "CarModel", "DropOffDate", "MaxPassengers", "PickUpDate", "PickUpLocation", "Price", "RentalCompany", "UserId" },
                values: new object[,]
                {
                    { 1, true, "Cool Car", new DateTime(2024, 6, 15, 19, 2, 55, 677, DateTimeKind.Local).AddTicks(1300), 0, new DateTime(2024, 4, 25, 19, 2, 55, 677, DateTimeKind.Local).AddTicks(1241), "123 str Toronto ON", 350.00m, "Big Company", null },
                    { 2, true, "SUV", new DateTime(2024, 4, 10, 19, 2, 55, 677, DateTimeKind.Local).AddTicks(1307), 0, new DateTime(2024, 4, 5, 19, 2, 55, 677, DateTimeKind.Local).AddTicks(1305), "456 Main St, Vancouver", 500.00m, "Rent-A-Car", null },
                    { 3, true, "Compact", new DateTime(2024, 4, 9, 19, 2, 55, 677, DateTimeKind.Local).AddTicks(1311), 0, new DateTime(2024, 4, 6, 19, 2, 55, 677, DateTimeKind.Local).AddTicks(1309), "789 Elm St, Calgary", 250.00m, "City Cars", null }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Airline", "ArrivalTime", "CurrentPassengers", "DepartureTime", "Destination", "FlightNumber", "MaxPassengers", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, "Flair Airlines", new DateTime(2024, 4, 15, 9, 50, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 6, 30, 0, 0, DateTimeKind.Unspecified), "Fort Lauderdale", "F8 1602", 4, "Toronto", 143.00m },
                    { 2, "Air Canada", new DateTime(2024, 4, 16, 22, 15, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Hanoi", "AC 9", 4, "Toronto", 1693.00m },
                    { 3, "Air Canada", new DateTime(2024, 4, 15, 8, 40, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 7, 45, 0, 0, DateTimeKind.Unspecified), "Montreal", "AC 7952", 2, "Toronto", 434.00m },
                    { 4, "Porter Airlines", new DateTime(2024, 4, 15, 15, 50, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 7, 55, 0, 0, DateTimeKind.Unspecified), "Montreal", "PD 372", 3, "Vancouver", 359.00m }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "HotelId", "Location", "Name", "NumberOfRooms", "Price", "StarRate" },
                values: new object[,]
                {
                    { 1, "123 str Toronto ON", "Marriot", 50, 350.00m, 4 },
                    { 2, "234 str Calgary AB", "Holiday Inn", 100, 250.00m, 3 },
                    { 3, "999 str Vancouver BC", "Happy Hotel", 20, 150.00m, 3 },
                    { 4, "012 str Norway", "Angry Hotel", 15, 1000.00m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "IsAdmin", "LastName" },
                values: new object[] { 1, "admin@example.com", "Admin", true, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CarRentalId",
                table: "Bookings",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_HotelId",
                table: "Bookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_UserId",
                table: "CarRentals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_HotelId",
                table: "HotelBookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_UserId",
                table: "HotelBookings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "HotelBookings");

            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
