﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_90.Migrations
{
    /// <inheritdoc />
    public partial class Hotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInDate",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "CheckOutDate",
                table: "Hotel");

            migrationBuilder.RenameColumn(
                name: "HotelName",
                table: "Hotel",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "StarRate",
                table: "Hotel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarRate",
                table: "Hotel");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Hotel",
                newName: "HotelName");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDate",
                table: "Hotel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDate",
                table: "Hotel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
