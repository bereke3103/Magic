﻿// <auto-generated />
using System;
using MagicVilla_VillaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230708200758_VillaNumberToDb")]
    partial class VillaNumberToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaApi.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6213),
                            Details = "Details is good",
                            ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                            Name = "New Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 505,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6226),
                            Details = "Details is good",
                            ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                            Name = "New Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 505,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6228),
                            Details = "Details is good",
                            ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                            Name = "New Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 505,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6230),
                            Details = "Details is good",
                            ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                            Name = "New Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 505,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Amenity = "",
                            CreatedDate = new DateTime(2023, 7, 9, 2, 7, 58, 839, DateTimeKind.Local).AddTicks(6231),
                            Details = "Details is good",
                            ImageUrl = "http://infozakon.kz/uploads/posts/2023-07/1688626858_1688535881_kz3_9824_2.jpg",
                            Name = "New Villa",
                            Occupancy = 5,
                            Rate = 200.0,
                            Sqft = 505,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MagicVilla_VillaApi.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VillaNo");

                    b.ToTable("VillaNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}