namespace Gostopolis.Restaurants.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class Reservations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Reservations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                Guests = table.Column<int>(type: "int", nullable: false),
                TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                RestaurantId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reservations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reservations_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ReservationTables",
            columns: table => new
            {
                ReservationId = table.Column<int>(type: "int", nullable: false),
                TableId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReservationTables", x => new { x.ReservationId, x.TableId });
                table.ForeignKey(
                    name: "FK_ReservationTables_Reservations_ReservationId",
                    column: x => x.ReservationId,
                    principalTable: "Reservations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ReservationTables_Tables_TableId",
                    column: x => x.TableId,
                    principalTable: "Tables",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Reservations_RestaurantId",
            table: "Reservations",
            column: "RestaurantId");

        migrationBuilder.CreateIndex(
            name: "IX_ReservationTables_TableId",
            table: "ReservationTables",
            column: "TableId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ReservationTables");

        migrationBuilder.DropTable(
            name: "Reservations");
    }
}