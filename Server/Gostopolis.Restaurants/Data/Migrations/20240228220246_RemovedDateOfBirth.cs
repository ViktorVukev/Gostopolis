namespace Gostopolis.Restaurants.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class RemovedDateOfBirth : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateOfBirth",
            table: "Reservations");

        migrationBuilder.DropColumn(
            name: "EndDate",
            table: "Reservations");

        migrationBuilder.DropColumn(
            name: "TotalPrice",
            table: "Reservations");

        migrationBuilder.AlterColumn<DateTime>(
            name: "StartDate",
            table: "Reservations",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateOnly),
            oldType: "date");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateOnly>(
            name: "StartDate",
            table: "Reservations",
            type: "date",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<DateOnly>(
            name: "DateOfBirth",
            table: "Reservations",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1));

        migrationBuilder.AddColumn<DateOnly>(
            name: "EndDate",
            table: "Reservations",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1));

        migrationBuilder.AddColumn<decimal>(
            name: "TotalPrice",
            table: "Reservations",
            type: "decimal(18,2)",
            nullable: false,
            defaultValue: 0m);
    }
}