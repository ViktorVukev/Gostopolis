namespace Gostopolis.Restaurants.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Ingredients",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ingredients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Messages",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Published = table.Column<bool>(type: "bit", nullable: false),
                serializedData = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Messages", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RestaurantTypes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RestaurantTypes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Restaurants",
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
                Latitude = table.Column<double>(type: "float", nullable: false),
                Longitude = table.Column<double>(type: "float", nullable: false),
                Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                LocationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                table.PrimaryKey("PK_Restaurants", x => x.Id);
                table.ForeignKey(
                    name: "FK_Restaurants_RestaurantTypes_TypeId",
                    column: x => x.TypeId,
                    principalTable: "RestaurantTypes",
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
                RestaurantId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Images", x => x.Id);
                table.ForeignKey(
                    name: "FK_Images_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Menus",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RestaurantId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Menus", x => x.Id);
                table.ForeignKey(
                    name: "FK_Menus_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Tables",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Capacity = table.Column<int>(type: "int", nullable: false),
                IsSmokingAllowed = table.Column<bool>(type: "bit", nullable: false),
                IsOutdoor = table.Column<bool>(type: "bit", nullable: false),
                RestaurantId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tables", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tables_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WorkingTimes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RestaurantId = table.Column<int>(type: "int", nullable: false),
                MondayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                MondayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                TuesdayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                TuesdayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                WednesdayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                WednesdayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                ThursdayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                ThursdayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                FridayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                FridayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                SaturdayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                SaturdayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                SundayOpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                SundayCloseTime = table.Column<TimeOnly>(type: "time", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WorkingTimes", x => x.Id);
                table.ForeignKey(
                    name: "FK_WorkingTimes_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Weight = table.Column<double>(type: "float", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                RestaurantId = table.Column<int>(type: "int", nullable: false),
                MenuId = table.Column<int>(type: "int", nullable: true),
                Type = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    name: "FK_Products_Menus_MenuId",
                    column: x => x.MenuId,
                    principalTable: "Menus",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Products_Restaurants_RestaurantId",
                    column: x => x.RestaurantId,
                    principalTable: "Restaurants",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ProductIngredients",
            columns: table => new
            {
                ProductId = table.Column<int>(type: "int", nullable: false),
                IngredientId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductIngredients", x => new { x.ProductId, x.IngredientId });
                table.ForeignKey(
                    name: "FK_ProductIngredients_Ingredients_IngredientId",
                    column: x => x.IngredientId,
                    principalTable: "Ingredients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ProductIngredients_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "RestaurantTypes",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[,]
            {
                { 1, "Предлага широк и разнообразен асортимент от висококачествена кухненска продукция, сладкарска продукция, десерти, плодове, специалитети, ястия, тестени изделия, безалкохолни и алкохолни напитки и създава условия за хранене и развлечения.", "Класически ресторант" },
                { 2, "Асортиментът се базира на даден основен продукт. Обслужва туристи с определени интереси.", "Специализиран ресторант" },
                { 3, "Асортиментът се базира на съответните национални кухни и напитки.", "Ресторант с национална кухня " },
                { 4, "С характерен архитектурен вътрешен и външен облик, с подходящ асортимент от ястия, напитки и/или артистично-музикална програма (шатри, кошари, колиби, фрегати, пикник и др.).", "Атракционно-тематичен ресторант " },
                { 5, "Предлага асортимент от ястия и специалитети, приготвени на скара или плоча, аламинути, салати, супи, бульони; готови сладкарски изделия, сладолед; топли напитки, алкохолни и безалкохолни напитки, бира (пиво). Кухненската продукция се приготвя пред клиента и се предлага от бар-плот.", "Снек-бар" },
                { 6, "Кухненската продукция в заведението е ограничена до: студени и топли предястия, скара и аламинути, салати, супи, десерти. Липсва предварителният процес на подготовка на храната, т.е. използват се предварително подготвени храни – полуфабрикати. Карт-менюто включва топли напитки, безалкохолни и алкохолни напитки и бира.", "Бистро" },
                { 7, "Предлага сандвичи, различни видове бургери, пържени картофи, скара от месни полуфабрикати, сосове, салати, готови тестени изделия, сладкарски изделия, захарни и шоколадови изделия, сладолед, топли напитки, безалкохолни напитки и бира.", "Фаст-фууд" },
                { 8, "Предлага различни видове пици, спагети, лазаня, макарони, салати, десерти и др., както и алкохолни и безалкохолни напитки.", "Пицария" },
                { 9, "Предлага богат асортимент от алкохолни и безалкохолни напитки; ограничен асортимент кулинарна продукция – салати, студени мезета, пържени картофи, месни полуфабрикати на скара, сандвичи, бургери, захарни и шоколадови изделия, ядки, сладолед.", "Кафе-аперитив" },
                { 10, "Предлага преобладаващо богат асортимент от наливни и бутилирани вина, други алкохолни и безалкохолни напитки, подходяща кухненска продукция и мезета. Може да има условия за дегустация.", "Винарна" },
                { 11, "Предлага разливни и бутилирани алкохолни и безалкохолни напитки, топли напитки, салати, студени мезета, ядки, захарни и шоколадови изделия.", "Пивница (кръчма)" },
                { 12, "Предлага разнообразен асортимент от наливна и бутилирана бира, салати, студени мезета, пържени картофи, месни полуфабрикати на скара, топли напитки, безалкохолни и алкохолни напитки.", "Бирария" },
                { 13, "Предлага малотрайни и трайни сладкарски изделия, захарни и шоколадови изделия, тестени закуски, сандвичи, сладолед, топли напитки и безалкохолни напитки.", "Сладкарница" },
                { 14, "Заведение за клиенти с определени интереси (интернет, изкуство, игри и др.), в което се предлагат топли напитки, безалкохолни напитки, алкохолни напитки, захарни и шоколадови изделия, ядки.", "Кафе-клуб" },
                { 15, "Предлага разнообразни топли напитки, безалкохолни напитки, закуски, тестени, захарни и шоколадови изделия.", "Кафетерия" },
                { 16, "Предлага богат асортимент предимно от алкохолни и безалкохолни коктейли и напитки, ядки, сладкарска продукция и захарни изделия.", "Коктейл-бар" },
                { 17, "Предлага различни видове кафе, безалкохолни и алкохолни напитки, плодове и др.", "Кафе-бар" },
                { 18, "Предлага алкохолни и безалкохолни напитки, коктейли, закуски, десерти, ядки и др. Разположен е в непосредствена близост до фоайето на средства за подслон или места за настаняване.", "Бар-фоайе (Lobby bar)" },
                { 19, "Заведение предимно за танцуване с дансинг и плотове в търговската зала и ограничен брой места за сядане. Предлага алкохолни и безалкохолни напитки, коктейли, ядки, топли напитки, сандвичи, захарни и шоколадови изделия.", "Дискотека" },
                { 20, "Заведение с музикално-артистична програма за клиенти с определени интереси, предлагащо алкохолни и безалкохолни напитки, кухненска и сладкарска продукция и др.", "Бар-клуб" },
                { 21, "Заведение с тихо музициране и асортимент, съответстващ на асортимента на баровете.", "Пиано-бар" },
                { 22, "Предлага асортимент за барове и е с нощен режим на работа.", "Нощен бар" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Images_RestaurantId",
            table: "Images",
            column: "RestaurantId");

        migrationBuilder.CreateIndex(
            name: "IX_Menus_RestaurantId",
            table: "Menus",
            column: "RestaurantId");

        migrationBuilder.CreateIndex(
            name: "IX_ProductIngredients_IngredientId",
            table: "ProductIngredients",
            column: "IngredientId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_MenuId",
            table: "Products",
            column: "MenuId");

        migrationBuilder.CreateIndex(
            name: "IX_Products_RestaurantId",
            table: "Products",
            column: "RestaurantId");

        migrationBuilder.CreateIndex(
            name: "IX_Restaurants_TypeId",
            table: "Restaurants",
            column: "TypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Tables_RestaurantId",
            table: "Tables",
            column: "RestaurantId");

        migrationBuilder.CreateIndex(
            name: "IX_WorkingTimes_RestaurantId",
            table: "WorkingTimes",
            column: "RestaurantId",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Images");

        migrationBuilder.DropTable(
            name: "Messages");

        migrationBuilder.DropTable(
            name: "ProductIngredients");

        migrationBuilder.DropTable(
            name: "Tables");

        migrationBuilder.DropTable(
            name: "WorkingTimes");

        migrationBuilder.DropTable(
            name: "Ingredients");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "Menus");

        migrationBuilder.DropTable(
            name: "Restaurants");

        migrationBuilder.DropTable(
            name: "RestaurantTypes");
    }
}