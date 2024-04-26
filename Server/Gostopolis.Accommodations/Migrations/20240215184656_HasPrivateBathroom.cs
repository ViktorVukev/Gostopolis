using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gostopolis.Accommodations.Migrations
{
    /// <inheritdoc />
    public partial class HasPrivateBathroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPrivateBathroom",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPrivateBathroom",
                table: "Rooms");
        }
    }
}
