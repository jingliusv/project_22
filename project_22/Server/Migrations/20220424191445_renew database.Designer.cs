﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_22.Server.Data;

#nullable disable

namespace project_22.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220424191445_renew database")]
    partial class renewdatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("project_22.Shared.CartItem", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("project_22.Shared.Entity.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kamera"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Kamera Väska"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Hörlurar"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Högtalare"
                        });
                });

            modelBuilder.Entity("project_22.Shared.Entity.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ArticleNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ArticleNumber")
                        .IsUnique();

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArticleNumber = "VBA420AE",
                            CategoryId = 1,
                            Description = "Nikon D750 är en digital systemkamera som ger dig frihet att lyckas fånga alla motiv.",
                            ImageUrl = "https://images.unsplash.com/photo-1621520291095-aa6c7137f048?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                            Price = 11900m,
                            ProductName = "Nikon D750"
                        },
                        new
                        {
                            Id = 2,
                            ArticleNumber = "20945",
                            CategoryId = 1,
                            Description = "Polaroid Now är en klassisk direktfilmskamera som plåtar och framkallar polaroidbilder med riktig retrokänsla. ",
                            ImageUrl = "https://www.kjell.com/globalassets/productimages/839191_20945.tif?ref=D5872CA259&format=jpg&w=960&h=960&mode=pad",
                            Price = 2100m,
                            ProductName = "Polaroid Now"
                        },
                        new
                        {
                            Id = 3,
                            ArticleNumber = "VINTA145",
                            CategoryId = 2,
                            Description = "This bag is perfect for a lifestyle of adventure, trekking to and from the studio, the office or the mountains.",
                            ImageUrl = "https://images.unsplash.com/photo-1547949003-9792a18a2601?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
                            Price = 1800m,
                            ProductName = "VINTA TYPE-II"
                        },
                        new
                        {
                            Id = 4,
                            ArticleNumber = "1000XM4",
                            CategoryId = 3,
                            Description = "WH-1000XM4-hörlurarna kombinerar elegant design och komfort på hög nivå. Supermjuka, tryckavlastande kuddar i uretanskum ger jämnt tryck och ökad kontakt med öronen för god passform. Den lägre vikten gör att du knappt kommer känna att du har dem på dig.",
                            ImageUrl = "https://images.unsplash.com/photo-1546435770-a3e426bf472b?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1165&q=80",
                            Price = 1699m,
                            ProductName = "Sony WH-1000XM4"
                        },
                        new
                        {
                            Id = 5,
                            ArticleNumber = "JBLGO3NHJ",
                            CategoryId = 4,
                            Description = "JBL GO 3 är den perfekta högtalaren att ta med dig på språng! Med färgladd tyg och uttrycksfulla detaljer som är inspi- rerade av street style, är detta tillbehör en nödvändighet på din nästa utflykt. GO 3 är vatten- och dammtät enligt IP67 och har en integrerade ögla som gör att du kan bära med den vart som helst.",
                            ImageUrl = "https://images.unsplash.com/photo-1608223652565-63abae6cf733?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                            Price = 365m,
                            ProductName = "JBL GO 3"
                        });
                });

            modelBuilder.Entity("project_22.Shared.Entity.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("project_22.Shared.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("project_22.Shared.OrderItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("project_22.Shared.Entity.ProductEntity", b =>
                {
                    b.HasOne("project_22.Shared.Entity.CategoryEntity", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("project_22.Shared.OrderItem", b =>
                {
                    b.HasOne("project_22.Shared.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("project_22.Shared.Entity.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("project_22.Shared.Entity.CategoryEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("project_22.Shared.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
