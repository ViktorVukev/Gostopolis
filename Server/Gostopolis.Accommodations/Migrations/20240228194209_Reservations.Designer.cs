﻿// <auto-generated />
using System;
using Gostopolis.Accommodations.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gostopolis.Accommodations.Migrations
{
    [DbContext(typeof(AccommodationsDbContext))]
    [Migration("20240228194209_Reservations")]
    partial class Reservations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Accommodation", b =>
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

                    b.Property<TimeOnly?>("CheckInTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("CheckOutTime")
                        .HasColumnType("time");

                    b.Property<string>("CoverPhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facilities")
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

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.AccommodationType", b =>
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

                    b.ToTable("AccommodationTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Място за настаняване, често предлагащо ресторанти, конферентни зали и други удобства за посетителите.",
                            Name = "Хотел"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Частна собственост с отделни помещения за гостите и собственика.",
                            Name = "Къща за гости"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Частен дом, предлагащ нощувка и закуска.",
                            Name = "Пансион със закуска (BB)"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Споделен дом, в който гостите имат собствена стая, а домакинът живее в останалите помещения. Някои удобства са споделени между домакините и гостите.",
                            Name = "Частна квартира"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Място за настаняване за гости с ограничен бюджет, с леглова база предимно от типа туристическа спалня и приятелска атмосфера.",
                            Name = "Хостел"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Апартамент с място за самостоятелно приготвяне на храна и някои хотелски удобства, например рецепция.",
                            Name = "Апартхотел"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Типичен японски хотел, съдържащ множество крайно малки стаи (капсули), създадени да осигурят единствено нощувка и не предлагащи услугите, които повечето хотели предлагат.",
                            Name = "Хотел капсула (Capsule hotel)"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Частен дом с прости условия за настаняване извън града.",
                            Name = "Селска къща"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Малко място за настаняване, с основни удобства и рустикален дух.",
                            Name = "Хан (Inn)"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Крайпътен хотел с директен достъп до паркинг и ограничени удобства.",
                            Name = "Мотел"
                        },
                        new
                        {
                            Id = 11,
                            Description = "Място за почивка с ресторанти, различни възможности за отдих, често с усещане за лукс.",
                            Name = "Хотелски комплекс"
                        },
                        new
                        {
                            Id = 12,
                            Description = "Хотел, чието функциониране се подчинява на екологични и природосъобразни принципи, като например пестене на вода, електроенергия, намаляване на отпадъците и пр.",
                            Name = "Зелен хотел (Green hotel)"
                        },
                        new
                        {
                            Id = 13,
                            Description = "Традиционно мароканско място за настаняване с двор и усещане за лукс.",
                            Name = "Риад (Riad)"
                        },
                        new
                        {
                            Id = 14,
                            Description = "Традиционно японско място за настаняване с възможности за хранене.",
                            Name = "Риокан (Ryokan)"
                        },
                        new
                        {
                            Id = 15,
                            Description = "Частно жилище сред природата, например в планината или гората.",
                            Name = "Хижа"
                        });
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccommodationId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccommodationId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccommodationId")
                        .HasColumnType("int");

                    b.Property<string>("AdditionalInformation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("Guests")
                        .HasColumnType("int");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.ReservationRoom", b =>
                {
                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId", "RoomId");

                    b.HasIndex("RoomId");

                    b.ToTable("ReservationRooms");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccommodationId")
                        .HasColumnType("int");

                    b.Property<string>("BathroomAmenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Beds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Floor")
                        .HasColumnType("int");

                    b.Property<bool>("HasPrivateBathroom")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomAmenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Rooms");
                });

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

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Accommodation", b =>
                {
                    b.HasOne("Gostopolis.Accommodations.Data.Models.AccommodationType", "Type")
                        .WithMany("Accommodations")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Image", b =>
                {
                    b.HasOne("Gostopolis.Accommodations.Data.Models.Accommodation", "Accommodation")
                        .WithMany("Images")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Meal", b =>
                {
                    b.HasOne("Gostopolis.Accommodations.Data.Models.Accommodation", "Accommodation")
                        .WithMany("Meals")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Reservation", b =>
                {
                    b.HasOne("Gostopolis.Accommodations.Data.Models.Accommodation", "Accommodation")
                        .WithMany("Reservations")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.ReservationRoom", b =>
                {
                    b.HasOne("Gostopolis.Accommodations.Data.Models.Reservation", "Reservation")
                        .WithMany("ReservationRooms")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gostopolis.Accommodations.Data.Models.Room", "Room")
                        .WithMany("ReservationRooms")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Room", b =>
                {
                    b.HasOne("Gostopolis.Accommodations.Data.Models.Accommodation", "Accommodation")
                        .WithMany("Rooms")
                        .HasForeignKey("AccommodationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accommodation");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Accommodation", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Meals");

                    b.Navigation("Reservations");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.AccommodationType", b =>
                {
                    b.Navigation("Accommodations");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Reservation", b =>
                {
                    b.Navigation("ReservationRooms");
                });

            modelBuilder.Entity("Gostopolis.Accommodations.Data.Models.Room", b =>
                {
                    b.Navigation("ReservationRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
