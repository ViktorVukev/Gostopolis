﻿// <auto-generated />
using System;
using Gostopolis.Restaurants.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gostopolis.Restaurants.Data.Migrations
{
    [DbContext(typeof(RestaurantsDbContext))]
    [Migration("20240425051709_ReservationIsCancelled")]
    partial class ReservationIsCancelled
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gostopolis.Data.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("serializedData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.ProductIngredient", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("ProductIngredients");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Guests")
                        .HasColumnType("int");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.ReservationTable", b =>
                {
                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId", "TableId");

                    b.HasIndex("TableId");

                    b.ToTable("ReservationTables");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AcceptOnlinePayments")
                        .HasColumnType("bit");

                    b.Property<bool>("AcceptPets")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasParking")
                        .HasColumnType("bit");

                    b.Property<bool>("HasPosTerminal")
                        .HasColumnType("bit");

                    b.Property<string>("IdentificationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTemporaryClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("LocationUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnershipDocumentUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpokenLanguages")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.RestaurantType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RestaurantTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Предлага широк и разнообразен асортимент от висококачествена кухненска продукция, сладкарска продукция, десерти, плодове, специалитети, ястия, тестени изделия, безалкохолни и алкохолни напитки и създава условия за хранене и развлечения.",
                            Name = "Класически ресторант"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Асортиментът се базира на даден основен продукт. Обслужва туристи с определени интереси.",
                            Name = "Специализиран ресторант"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Асортиментът се базира на съответните национални кухни и напитки.",
                            Name = "Ресторант с национална кухня "
                        },
                        new
                        {
                            Id = 4,
                            Description = "С характерен архитектурен вътрешен и външен облик, с подходящ асортимент от ястия, напитки и/или артистично-музикална програма (шатри, кошари, колиби, фрегати, пикник и др.).",
                            Name = "Атракционно-тематичен ресторант "
                        },
                        new
                        {
                            Id = 5,
                            Description = "Предлага асортимент от ястия и специалитети, приготвени на скара или плоча, аламинути, салати, супи, бульони; готови сладкарски изделия, сладолед; топли напитки, алкохолни и безалкохолни напитки, бира (пиво). Кухненската продукция се приготвя пред клиента и се предлага от бар-плот.",
                            Name = "Снек-бар"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Кухненската продукция в заведението е ограничена до: студени и топли предястия, скара и аламинути, салати, супи, десерти. Липсва предварителният процес на подготовка на храната, т.е. използват се предварително подготвени храни – полуфабрикати. Карт-менюто включва топли напитки, безалкохолни и алкохолни напитки и бира.",
                            Name = "Бистро"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Предлага сандвичи, различни видове бургери, пържени картофи, скара от месни полуфабрикати, сосове, салати, готови тестени изделия, сладкарски изделия, захарни и шоколадови изделия, сладолед, топли напитки, безалкохолни напитки и бира.",
                            Name = "Фаст-фууд"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Предлага различни видове пици, спагети, лазаня, макарони, салати, десерти и др., както и алкохолни и безалкохолни напитки.",
                            Name = "Пицария"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Предлага богат асортимент от алкохолни и безалкохолни напитки; ограничен асортимент кулинарна продукция – салати, студени мезета, пържени картофи, месни полуфабрикати на скара, сандвичи, бургери, захарни и шоколадови изделия, ядки, сладолед.",
                            Name = "Кафе-аперитив"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Предлага преобладаващо богат асортимент от наливни и бутилирани вина, други алкохолни и безалкохолни напитки, подходяща кухненска продукция и мезета. Може да има условия за дегустация.",
                            Name = "Винарна"
                        },
                        new
                        {
                            Id = 11,
                            Description = "Предлага разливни и бутилирани алкохолни и безалкохолни напитки, топли напитки, салати, студени мезета, ядки, захарни и шоколадови изделия.",
                            Name = "Пивница (кръчма)"
                        },
                        new
                        {
                            Id = 12,
                            Description = "Предлага разнообразен асортимент от наливна и бутилирана бира, салати, студени мезета, пържени картофи, месни полуфабрикати на скара, топли напитки, безалкохолни и алкохолни напитки.",
                            Name = "Бирария"
                        },
                        new
                        {
                            Id = 13,
                            Description = "Предлага малотрайни и трайни сладкарски изделия, захарни и шоколадови изделия, тестени закуски, сандвичи, сладолед, топли напитки и безалкохолни напитки.",
                            Name = "Сладкарница"
                        },
                        new
                        {
                            Id = 14,
                            Description = "Заведение за клиенти с определени интереси (интернет, изкуство, игри и др.), в което се предлагат топли напитки, безалкохолни напитки, алкохолни напитки, захарни и шоколадови изделия, ядки.",
                            Name = "Кафе-клуб"
                        },
                        new
                        {
                            Id = 15,
                            Description = "Предлага разнообразни топли напитки, безалкохолни напитки, закуски, тестени, захарни и шоколадови изделия.",
                            Name = "Кафетерия"
                        },
                        new
                        {
                            Id = 16,
                            Description = "Предлага богат асортимент предимно от алкохолни и безалкохолни коктейли и напитки, ядки, сладкарска продукция и захарни изделия.",
                            Name = "Коктейл-бар"
                        },
                        new
                        {
                            Id = 17,
                            Description = "Предлага различни видове кафе, безалкохолни и алкохолни напитки, плодове и др.",
                            Name = "Кафе-бар"
                        },
                        new
                        {
                            Id = 18,
                            Description = "Предлага алкохолни и безалкохолни напитки, коктейли, закуски, десерти, ядки и др. Разположен е в непосредствена близост до фоайето на средства за подслон или места за настаняване.",
                            Name = "Бар-фоайе (Lobby bar)"
                        },
                        new
                        {
                            Id = 19,
                            Description = "Заведение предимно за танцуване с дансинг и плотове в търговската зала и ограничен брой места за сядане. Предлага алкохолни и безалкохолни напитки, коктейли, ядки, топли напитки, сандвичи, захарни и шоколадови изделия.",
                            Name = "Дискотека"
                        },
                        new
                        {
                            Id = 20,
                            Description = "Заведение с музикално-артистична програма за клиенти с определени интереси, предлагащо алкохолни и безалкохолни напитки, кухненска и сладкарска продукция и др.",
                            Name = "Бар-клуб"
                        },
                        new
                        {
                            Id = 21,
                            Description = "Заведение с тихо музициране и асортимент, съответстващ на асортимента на баровете.",
                            Name = "Пиано-бар"
                        },
                        new
                        {
                            Id = 22,
                            Description = "Предлага асортимент за барове и е с нощен режим на работа.",
                            Name = "Нощен бар"
                        });
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<bool>("IsOutdoor")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSmokingAllowed")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.WorkingTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly?>("FridayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("FridayOpenTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("MondayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("MondayOpenTime")
                        .HasColumnType("time");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<TimeOnly?>("SaturdayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("SaturdayOpenTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("SundayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("SundayOpenTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("ThursdayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("ThursdayOpenTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("TuesdayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("TuesdayOpenTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("WednesdayCloseTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("WednesdayOpenTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId")
                        .IsUnique();

                    b.ToTable("WorkingTimes");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Image", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Images")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Menu", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Product", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Menu", "Menu")
                        .WithMany("Products")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gostopolis.Restaurants.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Products")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.ProductIngredient", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Ingredient", "Ingredient")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gostopolis.Restaurants.Data.Models.Product", "Product")
                        .WithMany("ProductIngredients")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Reservation", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.ReservationTable", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Reservation", "Reservation")
                        .WithMany("ReservationTables")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gostopolis.Restaurants.Data.Models.Table", "Table")
                        .WithMany("ReservationTables")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Restaurant", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.RestaurantType", "Type")
                        .WithMany("Restaurants")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Table", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.WorkingTime", b =>
                {
                    b.HasOne("Gostopolis.Restaurants.Data.Models.Restaurant", null)
                        .WithOne("WorkingTime")
                        .HasForeignKey("Gostopolis.Restaurants.Data.Models.WorkingTime", "RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Ingredient", b =>
                {
                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Menu", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Product", b =>
                {
                    b.Navigation("ProductIngredients");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Reservation", b =>
                {
                    b.Navigation("ReservationTables");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Restaurant", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Menus");

                    b.Navigation("Products");

                    b.Navigation("Reservations");

                    b.Navigation("Tables");

                    b.Navigation("WorkingTime");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.RestaurantType", b =>
                {
                    b.Navigation("Restaurants");
                });

            modelBuilder.Entity("Gostopolis.Restaurants.Data.Models.Table", b =>
                {
                    b.Navigation("ReservationTables");
                });
#pragma warning restore 612, 618
        }
    }
}
