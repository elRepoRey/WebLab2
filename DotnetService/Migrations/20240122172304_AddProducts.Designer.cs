﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheFlowerplace.Data;

#nullable disable

namespace TheFlowerplace.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240122172304_AddProducts")]
    partial class AddProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TheFlowerplace.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "A classic red rose, perfect for all occasions.",
                            Image = "images/rose.jpg",
                            Name = "Rose",
                            Price = 15.99m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Colorful tulips to brighten your day.",
                            Image = "images/tulip.jpg",
                            Name = "Tulip",
                            Price = 9.99m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Cheerful daisies with a fresh look.",
                            Image = "images/daisy.jpg",
                            Name = "Daisy",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Bright and beautiful sunflowers.",
                            Image = "images/sunflower.jpg",
                            Name = "Sunflower",
                            Price = 12.50m
                        },
                        new
                        {
                            Id = 5,
                            Description = "Elegant and exotic orchids.",
                            Image = "images/orchid.jpg",
                            Name = "Orchid",
                            Price = 22.99m
                        },
                        new
                        {
                            Id = 6,
                            Description = "Stunning lilies for a beautiful bouquet.",
                            Image = "images/lily.jpg",
                            Name = "Lily",
                            Price = 18.99m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
