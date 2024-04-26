using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gostopolis.Accommodations.Migrations
{
    /// <inheritdoc />
    public partial class IsTemporaryClosed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOpen",
                table: "Accommodations",
                newName: "IsTemporaryClosed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTemporaryClosed",
                table: "Accommodations",
                newName: "IsOpen");
        }
    }
}
