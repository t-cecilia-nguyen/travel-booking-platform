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
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GBC_Travel_Group_90.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<int?>("CarRentalId")
                        .HasColumnType("int");

                    b.Property<int?>("FlightId")
                        .HasColumnType("int");

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("CarRentalId");

                    b.HasIndex("FlightId");

                    b.HasIndex("HotelId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.CarRental", b =>
                {
                    b.Property<int>("CarRentalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarRentalId"));

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

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CarRentalId");

                    b.HasIndex("UserId");

                    b.ToTable("CarRentals");

                    b.HasData(
                        new
                        {
                            CarRentalId = 1,
                            Available = true,
                            CarModel = "Cool Car",
                            DropOffDate = new DateTime(2024, 5, 7, 18, 13, 38, 991, DateTimeKind.Local).AddTicks(7127),
                            MaxPassengers = 0,
                            PickUpDate = new DateTime(2024, 3, 17, 18, 13, 38, 991, DateTimeKind.Local).AddTicks(7068),
                            PickUpLocation = "123 str Toronto ON",
                            Price = 350.00m,
                            RentalCompany = "Big Company"
                        },
                        new
                        {
                            CarRentalId = 2,
                            Available = true,
                            CarModel = "SUV",
                            DropOffDate = new DateTime(2024, 3, 2, 18, 13, 38, 991, DateTimeKind.Local).AddTicks(7134),
                            MaxPassengers = 0,
                            PickUpDate = new DateTime(2024, 2, 26, 18, 13, 38, 991, DateTimeKind.Local).AddTicks(7132),
                            PickUpLocation = "456 Main St, Vancouver",
                            Price = 500.00m,
                            RentalCompany = "Rent-A-Car"
                        },
                        new
                        {
                            CarRentalId = 3,
                            Available = true,
                            CarModel = "Compact",
                            DropOffDate = new DateTime(2024, 3, 1, 18, 13, 38, 991, DateTimeKind.Local).AddTicks(7137),
                            MaxPassengers = 0,
                            PickUpDate = new DateTime(2024, 2, 27, 18, 13, 38, 991, DateTimeKind.Local).AddTicks(7136),
                            PickUpLocation = "789 Elm St, Calgary",
                            Price = 250.00m,
                            RentalCompany = "City Cars"
                        });
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"));

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentPassengers")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPassengers")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("FlightId");

                    b.ToTable("Flights");

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
                        });
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.Hotel", b =>
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

                    b.ToTable("Hotels");

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

            modelBuilder.Entity("GBC_Travel_Group_90.Models.HotelBooking", b =>
                {
                    b.Property<int>("HotelBookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelBookingId"));

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

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("HotelBookingId");

                    b.HasIndex("HotelId");

                    b.HasIndex("UserId");

                    b.ToTable("HotelBookings");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "admin@example.com",
                            FirstName = "Admin",
                            IsAdmin = true,
                            LastName = "User"
                        });
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.Booking", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Models.CarRental", "CarRental")
                        .WithMany()
                        .HasForeignKey("CarRentalId");

                    b.HasOne("GBC_Travel_Group_90.Models.Flight", "Flight")
                        .WithMany("Bookings")
                        .HasForeignKey("FlightId");

                    b.HasOne("GBC_Travel_Group_90.Models.Hotel", "Hotel")
                        .WithMany("Bookings")
                        .HasForeignKey("HotelId");

                    b.HasOne("GBC_Travel_Group_90.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarRental");

                    b.Navigation("Flight");

                    b.Navigation("Hotel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.CarRental", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.HotelBooking", b =>
                {
                    b.HasOne("GBC_Travel_Group_90.Models.Hotel", "Hotel")
                        .WithMany("HotelBookings")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GBC_Travel_Group_90.Models.User", "User")
                        .WithMany("HotelBookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.Flight", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.Hotel", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("HotelBookings");
                });

            modelBuilder.Entity("GBC_Travel_Group_90.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("HotelBookings");
                });
#pragma warning restore 612, 618
        }
    }
}
