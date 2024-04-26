using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gostopolis.Accommodations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccommodationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnershipDocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    HasParking = table.Column<bool>(type: "bit", nullable: false),
                    HasPosTerminal = table.Column<bool>(type: "bit", nullable: false),
                    AcceptOnlinePayments = table.Column<bool>(type: "bit", nullable: false),
                    AcceptPets = table.Column<bool>(type: "bit", nullable: false),
                    SpokenLanguages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodations_AccommodationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccommodationTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Място за настаняване, често предлагащо ресторанти, конферентни зали и други удобства за посетителите.", "Хотел" },
                    { 2, "Частна собственост с отделни помещения за гостите и собственика.", "Къща за гости" },
                    { 3, "Частен дом, предлагащ нощувка и закуска.", "Пансион със закуска (BB)" },
                    { 4, "Споделен дом, в който гостите имат собствена стая, а домакинът живее в останалите помещения. Някои удобства са споделени между домакините и гостите.", "Частна квартира" },
                    { 5, "Място за настаняване за гости с ограничен бюджет, с леглова база предимно от типа туристическа спалня и приятелска атмосфера.", "Хостел" },
                    { 6, "Апартамент с място за самостоятелно приготвяне на храна и някои хотелски удобства, например рецепция.", "Апартхотел" },
                    { 7, "Типичен японски хотел, съдържащ множество крайно малки стаи (капсули), създадени да осигурят единствено нощувка и не предлагащи услугите, които повечето хотели предлагат.", "Хотел капсула (Capsule hotel)" },
                    { 8, "Частен дом с прости условия за настаняване извън града.", "Селска къща" },
                    { 9, "Малко място за настаняване, с основни удобства и рустикален дух.", "Хан (Inn)" },
                    { 10, "Крайпътен хотел с директен достъп до паркинг и ограничени удобства.", "Мотел" },
                    { 11, "Място за почивка с ресторанти, различни възможности за отдих, често с усещане за лукс.", "Хотелски комплекс" },
                    { 12, "Хотел, чието функциониране се подчинява на екологични и природосъобразни принципи, като например пестене на вода, електроенергия, намаляване на отпадъците и пр.", "Зелен хотел (Green hotel)" },
                    { 13, "Традиционно мароканско място за настаняване с двор и усещане за лукс.", "Риад (Riad)" },
                    { 14, "Традиционно японско място за настаняване с възможности за хранене.", "Риокан (Ryokan)" },
                    { 15, "Частно жилище сред природата, например в планината или гората.", "Хижа" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_TypeId",
                table: "Accommodations",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccommodationId",
                table: "Images",
                column: "AccommodationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "AccommodationTypes");
        }
    }
}
