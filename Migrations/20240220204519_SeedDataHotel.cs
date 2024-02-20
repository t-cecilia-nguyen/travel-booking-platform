using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Hotels");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Hotels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
