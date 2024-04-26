using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gostopolis.Accommodations.Migrations
{
    /// <inheritdoc />
    public partial class AddressDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Accommodations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LocationUrl",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Accommodations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "LocationUrl",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Accommodations");
        }
    }
}
