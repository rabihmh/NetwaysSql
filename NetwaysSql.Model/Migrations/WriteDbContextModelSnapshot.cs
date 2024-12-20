﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetwaysSql.Model;

#nullable disable

namespace NetwaysSql.Model.Migrations
{
    [DbContext(typeof(WriteDbContext))]
    partial class WriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetwaysSql.Model.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NetwaysSql.Model.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("NetwaysSql.Model.ProductTag", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags");
                });

            modelBuilder.Entity("NetwaysSql.Model.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8011bc5-f5ef-496e-a4b5-3310701e913f"),
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = new Guid("13711902-594b-4174-b2f2-90ce83f9230b"),
                            Name = "Home Appliances"
                        },
                        new
                        {
                            Id = new Guid("f847b4bf-3e60-408d-a494-93c118501e7c"),
                            Name = "Fashion"
                        },
                        new
                        {
                            Id = new Guid("db1c84a4-003f-4b6f-af67-ae2dcde31568"),
                            Name = "Toys"
                        },
                        new
                        {
                            Id = new Guid("ae3f7f4f-c8d4-4cb5-ad8c-976f0df5828c"),
                            Name = "Books"
                        },
                        new
                        {
                            Id = new Guid("250c8a57-a911-40ff-9cf8-77ec4a39e191"),
                            Name = "For Kids"
                        },
                        new
                        {
                            Id = new Guid("f6ac494a-0a3d-473b-b22b-875a9680a092"),
                            Name = "Sport"
                        },
                        new
                        {
                            Id = new Guid("f0b308a1-07ff-40fc-96db-5be6e2e4047b"),
                            Name = "Health"
                        });
                });

            modelBuilder.Entity("NetwaysSql.Model.Product", b =>
                {
                    b.HasOne("NetwaysSql.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("NetwaysSql.Model.ProductTag", b =>
                {
                    b.HasOne("NetwaysSql.Model.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetwaysSql.Model.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("NetwaysSql.Model.Product", b =>
                {
                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("NetwaysSql.Model.Tag", b =>
                {
                    b.Navigation("ProductTags");
                });
#pragma warning restore 612, 618
        }
    }
}
