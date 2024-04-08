using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarRentals",
                columns: new[] { "CarRentalId", "Available", "CarModel", "DropOffDate", "MaxPassengers", "PickUpDate", "PickUpLocation", "Price", "RentalCompany", "UserId" },
                values: new object[,]
                {
                    { 1, true, "Cool Car", new DateTime(2024, 6, 20, 8, 17, 24, 445, DateTimeKind.Local).AddTicks(2593), 0, new DateTime(2024, 4, 30, 8, 17, 24, 445, DateTimeKind.Local).AddTicks(2522), "123 str Toronto ON", 350.00m, "Big Company", null },
                    { 2, true, "SUV", new DateTime(2024, 4, 15, 8, 17, 24, 445, DateTimeKind.Local).AddTicks(2600), 0, new DateTime(2024, 4, 10, 8, 17, 24, 445, DateTimeKind.Local).AddTicks(2598), "456 Main St, Vancouver", 500.00m, "Rent-A-Car", null },
                    { 3, true, "Compact", new DateTime(2024, 4, 14, 8, 17, 24, 445, DateTimeKind.Local).AddTicks(2604), 0, new DateTime(2024, 4, 11, 8, 17, 24, 445, DateTimeKind.Local).AddTicks(2603), "789 Elm St, Calgary", 250.00m, "City Cars", null }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Airline", "ArrivalTime", "CurrentPassengers", "DepartureTime", "Destination", "FlightNumber", "MaxPassengers", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, "Flair Airlines", new DateTime(2024, 4, 15, 9, 50, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 6, 30, 0, 0, DateTimeKind.Unspecified), "Fort Lauderdale", "F8 1602", 4, "Toronto", 143.00m },
                    { 2, "Air Canada", new DateTime(2024, 4, 16, 22, 15, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Hanoi", "AC 9", 4, "Toronto", 1693.00m },
                    { 3, "Air Canada", new DateTime(2024, 4, 15, 8, 40, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 7, 45, 0, 0, DateTimeKind.Unspecified), "Montreal", "AC 7952", 2, "Toronto", 434.00m },
                    { 4, "Porter Airlines", new DateTime(2024, 4, 15, 15, 50, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 15, 7, 55, 0, 0, DateTimeKind.Unspecified), "Montreal", "PD 372", 3, "Vancouver", 359.00m },
                    { 5, "Air Canada", new DateTime(2024, 4, 23, 11, 5, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2024, 4, 22, 20, 30, 0, 0, DateTimeKind.Unspecified), "Rome", "AC 890", 4, "Toronto", 866.00m }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "HotelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
