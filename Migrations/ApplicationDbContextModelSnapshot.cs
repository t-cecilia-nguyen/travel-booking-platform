﻿// <auto-generated />
using System;
using GBC_Travel_Group_90.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GBC_Travel_Group_90.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", "Identity");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("FlightId");

                    b.HasIndex("HotelId");

                    b.ToTable("Bookings", "Identity");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarRental", b =>
                {
                    b.Property<int>("CarRentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarRentalId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DropOffDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxPassengers")
                        .HasColumnType("int");

                    b.Property<DateTime>("PickUpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PickUpLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RentalCompany")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarRentalId");

                    b.ToTable("CarRentals", "Identity");

                    b.HasData(
                        new
                        {
                            CarRentalId = 1,
                            Available = true,
                            CarModel = "Cool Car",
                            DropOffDate = new DateTime(2024, 6, 21, 16, 15, 59, 182, DateTimeKind.Local).AddTicks(5970),
                            MaxPassengers = 0,
                            PickUpDate = new DateTime(2024, 5, 1, 16, 15, 59, 182, DateTimeKind.Local).AddTicks(5908),
                            PickUpLocation = "123 str Toronto ON",
                            Price = 350.00m,
                            RentalCompany = "Big Company"
                        },
                        new
                        {
                            CarRentalId = 2,
                            Available = true,
                            CarModel = "SUV",
                            DropOffDate = new DateTime(2024, 4, 16, 16, 15, 59, 182, DateTimeKind.Local).AddTicks(5977),
                            MaxPassengers = 0,
                            PickUpDate = new DateTime(2024, 4, 11, 16, 15, 59, 182, DateTimeKind.Local).AddTicks(5975),
                            PickUpLocation = "456 Main St, Vancouver",
                            Price = 500.00m,
                            RentalCompany = "Rent-A-Car"
                        },
                        new
                        {
                            CarRentalId = 3,
                            Available = true,
                            CarModel = "Compact",
                            DropOffDate = new DateTime(2024, 4, 15, 16, 15, 59, 182, DateTimeKind.Local).AddTicks(5981),
                            MaxPassengers = 0,
                            PickUpDate = new DateTime(2024, 4, 12, 16, 15, 59, 182, DateTimeKind.Local).AddTicks(5979),
                            PickUpLocation = "789 Elm St, Calgary",
                            Price = 250.00m,
                            RentalCompany = "City Cars"
                        });
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarRentalReview", b =>
                {
                    b.Property<int>("CarRentalReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarRentalReviewId"));

                    b.Property<int>("CarRentalId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CarRentalReviewId");

                    b.HasIndex("CarRentalId");

                    b.ToTable("CarRentalReviews", "Identity");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarSuccess", b =>
                {
                    b.Property<int>("CarSuccessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarSuccessId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CarRentalId")
                        .HasColumnType("int");

                    b.HasKey("CarSuccessId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CarRentalId");

                    b.ToTable("CarSuccess", "Identity");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"));

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentPassengers")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("MaxPassengers")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FlightId");

                    b.ToTable("Flights", "Identity");

                    b.HasData(
                        new
                        {
                            FlightId = 1,
                            Airline = "Flair Airlines",
                            ArrivalTime = new DateTime(2024, 4, 15, 9, 50, 0, 0, DateTimeKind.Unspecified),
                            CurrentPassengers = 0,
                            DepartureTime = new DateTime(2024, 4, 15, 6, 30, 0, 0, DateTimeKind.Unspecified),
                            Destination = "Fort Lauderdale",
                            FlightNumber = "F8 1602",
                            MaxPassengers = 4,
                            Origin = "Toronto",
                            Price = 143.00m
                        },
                        new
                        {
                            FlightId = 2,
                            Airline = "Air Canada",
                            ArrivalTime = new DateTime(2024, 4, 16, 22, 15, 0, 0, DateTimeKind.Unspecified),
                            CurrentPassengers = 0,
                            DepartureTime = new DateTime(2024, 4, 15, 13, 30, 0, 0, DateTimeKind.Unspecified),
                            Destination = "Hanoi",
                            FlightNumber = "AC 9",
                            MaxPassengers = 4,
                            Origin = "Toronto",
                            Price = 1693.00m
                        },
                        new
                        {
                            FlightId = 3,
                            Airline = "Air Canada",
                            ArrivalTime = new DateTime(2024, 4, 15, 8, 40, 0, 0, DateTimeKind.Unspecified),
                            CurrentPassengers = 0,
                            DepartureTime = new DateTime(2024, 4, 15, 7, 45, 0, 0, DateTimeKind.Unspecified),
                            Destination = "Montreal",
                            FlightNumber = "AC 7952",
                            MaxPassengers = 2,
                            Origin = "Toronto",
                            Price = 434.00m
                        },
                        new
                        {
                            FlightId = 4,
                            Airline = "Porter Airlines",
                            ArrivalTime = new DateTime(2024, 4, 15, 15, 50, 0, 0, DateTimeKind.Unspecified),
                            CurrentPassengers = 0,
                            DepartureTime = new DateTime(2024, 4, 15, 7, 55, 0, 0, DateTimeKind.Unspecified),
                            Destination = "Montreal",
                            FlightNumber = "PD 372",
                            MaxPassengers = 3,
                            Origin = "Vancouver",
                            Price = 359.00m
                        },
                        new
                        {
                            FlightId = 5,
                            Airline = "Air Canada",
                            ArrivalTime = new DateTime(2024, 4, 23, 11, 5, 0, 0, DateTimeKind.Unspecified),
                            CurrentPassengers = 0,
                            DepartureTime = new DateTime(2024, 4, 22, 20, 30, 0, 0, DateTimeKind.Unspecified),
                            Destination = "Rome",
                            FlightNumber = "AC 890",
                            MaxPassengers = 4,
                            Origin = "Toronto",
                            Price = 866.00m
                        });
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.Hotel", b =>
                {
                    b.Property<int>("HotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StarRate")
                        .HasColumnType("int");

                    b.HasKey("HotelId");

                    b.ToTable("Hotels", "Identity");

                    b.HasData(
                        new
                        {
                            HotelId = 1,
                            Location = "123 str Toronto ON",
                            Name = "Marriot",
                            NumberOfRooms = 50,
                            Price = 350.00m,
                            StarRate = 4
                        },
                        new
                        {
                            HotelId = 2,
                            Location = "234 str Calgary AB",
                            Name = "Holiday Inn",
                            NumberOfRooms = 100,
                            Price = 250.00m,
                            StarRate = 3
                        },
                        new
                        {
                            HotelId = 3,
                            Location = "999 str Vancouver BC",
                            Name = "Happy Hotel",
                            NumberOfRooms = 20,
                            Price = 150.00m,
                            StarRate = 3
                        },
                        new
                        {
                            HotelId = 4,
                            Location = "012 str Norway",
                            Name = "Angry Hotel",
                            NumberOfRooms = 15,
                            Price = 1000.00m,
                            StarRate = 5
                        });
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.HotelBooking", b =>
                {
                    b.Property<int>("HotelBookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelBookingId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("NumOfRoomsToBook")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("HotelBookingId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelBookings", "Identity");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.HotelReview", b =>
                {
                    b.Property<int>("HotelReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelReviewId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("HotelReviewId");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelReviews", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "Identity");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.Booking", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.Flight", "Flight")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightId");

                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.Hotel", null)
                        .WithMany("Bookings")
                        .HasForeignKey("HotelId");

                    b.Navigation("Flight");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarRentalReview", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarRental", "CarRental")
                        .WithMany()
                        .HasForeignKey("CarRentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarRental");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarSuccess", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", "User")
                        .WithMany("CarSuccesses")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.CarRental", "CarRental")
                        .WithMany()
                        .HasForeignKey("CarRentalId");

                    b.Navigation("CarRental");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.HotelBooking", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", "User")
                        .WithMany("HotelBookings")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.Hotel", "Hotel")
                        .WithMany("HotelBookings")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.HotelReview", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.ApplicationUser", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("CarSuccesses");

                    b.Navigation("HotelBookings");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.Flight", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Areas.TravelManagement.Models.Hotel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("HotelBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
