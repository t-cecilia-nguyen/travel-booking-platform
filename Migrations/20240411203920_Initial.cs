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
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "CarRentals",
                schema: "Identity",
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
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.CarRentalId);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                schema: "Identity",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
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
                schema: "Identity",
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
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarRentalReviews",
                schema: "Identity",
                columns: table => new
                {
                    CarRentalReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarRentalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentalReviews", x => x.CarRentalReviewId);
                    table.ForeignKey(
                        name: "FK_CarRentalReviews_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalSchema: "Identity",
                        principalTable: "CarRentals",
                        principalColumn: "CarRentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelReviews",
                schema: "Identity",
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
                        principalSchema: "Identity",
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "Identity",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "Identity",
                        principalTable: "Flights",
                        principalColumn: "FlightId");
                    table.ForeignKey(
                        name: "FK_Bookings_User_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarSuccess",
                schema: "Identity",
                columns: table => new
                {
                    CarSuccessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CarRentalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarSuccess", x => x.CarSuccessId);
                    table.ForeignKey(
                        name: "FK_CarSuccess_CarRentals_CarRentalId",
                        column: x => x.CarRentalId,
                        principalSchema: "Identity",
                        principalTable: "CarRentals",
                        principalColumn: "CarRentalId");
                    table.ForeignKey(
                        name: "FK_CarSuccess_User_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HotelBookings",
                schema: "Identity",
                columns: table => new
                {
                    HotelBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumOfRoomsToBook = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelBookings", x => x.HotelBookingId);
                    table.ForeignKey(
                        name: "FK_HotelBookings_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "Identity",
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelBookings_User_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "CarRentals",
                columns: new[] { "CarRentalId", "ApplicationUserId", "Available", "CarModel", "DropOffDate", "MaxPassengers", "PickUpDate", "PickUpLocation", "Price", "RentalCompany" },
                values: new object[,]
                {
                    { 1, null, true, "Cool Car", new DateTime(2024, 6, 23, 16, 39, 19, 989, DateTimeKind.Local).AddTicks(8224), 4, new DateTime(2024, 5, 3, 16, 39, 19, 989, DateTimeKind.Local).AddTicks(8161), "123 str Toronto ON", 350.00m, "Big Company" },
                    { 2, null, true, "SUV", new DateTime(2024, 4, 18, 16, 39, 19, 989, DateTimeKind.Local).AddTicks(8230), 5, new DateTime(2024, 4, 13, 16, 39, 19, 989, DateTimeKind.Local).AddTicks(8229), "456 Main St, Vancouver", 500.00m, "Rent-A-Car" },
                    { 3, null, true, "Compact", new DateTime(2024, 4, 17, 16, 39, 19, 989, DateTimeKind.Local).AddTicks(8234), 4, new DateTime(2024, 4, 14, 16, 39, 19, 989, DateTimeKind.Local).AddTicks(8232), "789 Elm St, Calgary", 250.00m, "City Cars" }
                });

            migrationBuilder.InsertData(
                schema: "Identity",
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
                schema: "Identity",
                table: "Hotels",
                columns: new[] { "HotelId", "Location", "Name", "NumberOfRooms", "Price", "StarRate" },
                values: new object[,]
                {
                    { 1, "123 str Toronto ON", "Marriot", 50, 350.00m, 4 },
                    { 2, "234 str Calgary AB", "Holiday Inn", 100, 250.00m, 3 },
                    { 3, "999 str Vancouver BC", "Happy Hotel", 20, 150.00m, 3 },
                    { 4, "012 str Norway", "Angry Hotel", 15, 1000.00m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ApplicationUserId",
                schema: "Identity",
                table: "Bookings",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                schema: "Identity",
                table: "Bookings",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentalReviews_CarRentalId",
                schema: "Identity",
                table: "CarRentalReviews",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_CarSuccess_ApplicationUserId",
                schema: "Identity",
                table: "CarSuccess",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CarSuccess_CarRentalId",
                schema: "Identity",
                table: "CarSuccess",
                column: "CarRentalId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_ApplicationUserId",
                schema: "Identity",
                table: "HotelBookings",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_HotelId",
                schema: "Identity",
                table: "HotelBookings",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReviews_HotelId",
                schema: "Identity",
                table: "HotelReviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Identity",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CarRentalReviews",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CarSuccess",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "HotelBookings",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "HotelReviews",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Flights",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CarRentals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Hotels",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}
