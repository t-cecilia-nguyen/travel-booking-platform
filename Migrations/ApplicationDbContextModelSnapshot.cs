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

				b.ToTable("CarRentals");
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
			});

			modelBuilder.Entity("GBC_Travel_Group_90.Models.Hotel", b =>
			{
				b.Property<int>("HotelId")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HotelId"));

				b.Property<DateTime>("CheckInDate")
					.HasColumnType("datetime2");

				b.Property<DateTime>("CheckOutDate")
					.HasColumnType("datetime2");

				b.Property<string>("HotelName")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("Location")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<int>("NumberOfRooms")
					.HasColumnType("int");

				b.Property<decimal>("Price")
					.HasColumnType("decimal(18,2)");

				b.HasKey("HotelId");

				b.ToTable("Hotel");
			});

			modelBuilder.Entity("GBC_Travel_Group_90.Models.User", b =>
			{
				b.Property<int>("UserId")
					.ValueGeneratedOnAdd()
					.HasColumnType("int");

				SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

				b.Property<bool>("Admin")
					.HasColumnType("bit");

				b.Property<string>("Email")
					.IsRequired()
					.HasColumnType("nvarchar(max)");

				b.Property<string>("FirstName")
					.IsRequired()
					.HasMaxLength(100)
					.HasColumnType("nvarchar(100)");

				b.Property<string>("LastName")
					.IsRequired()
					.HasMaxLength(100)
					.HasColumnType("nvarchar(100)");

				b.HasKey("UserId");

				b.ToTable("Users");
			});

			modelBuilder.Entity("GBC_Travel_Group_90.Models.Booking", b =>
			{
				b.HasOne("GBC_Travel_Group_90.Models.CarRental", "CarRental")
					.WithMany("Bookings")
					.HasForeignKey("CarRentalId");

				b.HasOne("GBC_Travel_Group_90.Models.Flight", "Flight")
					.WithMany("Bookings")
					.HasForeignKey("FlightId");

				b.HasOne("GBC_Travel_Group_90.Models.Hotel", "Hotel")
					.WithMany("Bookings")
					.HasForeignKey("HotelId");

				b.HasOne("GBC_Travel_Group_90.Models.User", "User")
					.WithMany("Booking")
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
				b.Navigation("Bookings");
			});

			modelBuilder.Entity("GBC_Travel_Group_90.Models.Flight", b =>
			{
				b.Navigation("Bookings");
			});

			modelBuilder.Entity("GBC_Travel_Group_90.Models.Hotel", b =>
			{
				b.Navigation("Bookings");
			});

			modelBuilder.Entity("GBC_Travel_Group_90.Models.User", b =>
			{
				b.Navigation("Booking");
			});
#pragma warning restore 612, 618
		}
	}
}
