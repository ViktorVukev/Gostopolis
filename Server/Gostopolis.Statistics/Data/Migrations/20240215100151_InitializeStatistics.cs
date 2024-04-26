using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gostopolis.Statistics.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitializeStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalRestaurants = table.Column<int>(type: "int", nullable: false),
                    TotalAccommodations = table.Column<int>(type: "int", nullable: false),
                    TotalReservations = table.Column<int>(type: "int", nullable: false),
                    TotalPartners = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
