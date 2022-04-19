﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_22.Server.Data;

#nullable disable

namespace project_22.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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
                        });
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

            modelBuilder.Entity("project_22.Shared.Entity.CategoryEntity", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
