using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class HotelReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotelReviews",
                columns: table => new
                {
                    HotelReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReviews", x => x.HotelReviewId);
                    table.ForeignKey(
                        name: "FK_HotelReviews_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 1,
                columns: new[] { "DropOffDate", "PickUpDate" },
                values: new object[] { new DateTime(2024, 6, 19, 20, 25, 28, 83, DateTimeKind.Local).AddTicks(7754), new DateTime(2024, 4, 29, 20, 25, 28, 83, DateTimeKind.Local).AddTicks(7697) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 2,
                columns: new[] { "DropOffDate", "PickUpDate" },
                values: new object[] { new DateTime(2024, 4, 14, 20, 25, 28, 83, DateTimeKind.Local).AddTicks(7759), new DateTime(2024, 4, 9, 20, 25, 28, 83, DateTimeKind.Local).AddTicks(7758) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 3,
                columns: new[] { "DropOffDate", "PickUpDate" },
                values: new object[] { new DateTime(2024, 4, 13, 20, 25, 28, 83, DateTimeKind.Local).AddTicks(7763), new DateTime(2024, 4, 10, 20, 25, 28, 83, DateTimeKind.Local).AddTicks(7761) });

            migrationBuilder.CreateIndex(
                name: "IX_HotelReviews_HotelId",
                table: "HotelReviews",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelReviews");

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 1,
                columns: new[] { "DropOffDate", "PickUpDate" },
                values: new object[] { new DateTime(2024, 6, 19, 19, 27, 45, 552, DateTimeKind.Local).AddTicks(8084), new DateTime(2024, 4, 29, 19, 27, 45, 552, DateTimeKind.Local).AddTicks(8023) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 2,
                columns: new[] { "DropOffDate", "PickUpDate" },
                values: new object[] { new DateTime(2024, 4, 14, 19, 27, 45, 552, DateTimeKind.Local).AddTicks(8089), new DateTime(2024, 4, 9, 19, 27, 45, 552, DateTimeKind.Local).AddTicks(8088) });

            migrationBuilder.UpdateData(
                table: "CarRentals",
                keyColumn: "CarRentalId",
                keyValue: 3,
                columns: new[] { "DropOffDate", "PickUpDate" },
                values: new object[] { new DateTime(2024, 4, 13, 19, 27, 45, 552, DateTimeKind.Local).AddTicks(8093), new DateTime(2024, 4, 10, 19, 27, 45, 552, DateTimeKind.Local).AddTicks(8092) });
        }
    }
}
