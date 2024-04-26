using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gostopolis.Accommodations.Migrations
{
    /// <inheritdoc />
    public partial class AccommodationDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "CheckInTime",
                table: "Accommodations",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CheckOutTime",
                table: "Accommodations",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facilities",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInTime",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "CheckOutTime",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Facilities",
                table: "Accommodations");
        }
    }
}
