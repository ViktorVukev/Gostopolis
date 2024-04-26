using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gostopolis.Restaurants.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationId",
                table: "ReservationTables");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationId",
                table: "ReservationTables",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationId",
                table: "ReservationTables");

            migrationBuilder.DropForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Reservations_ReservationId",
                table: "ReservationTables",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationTables_Tables_TableId",
                table: "ReservationTables",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
