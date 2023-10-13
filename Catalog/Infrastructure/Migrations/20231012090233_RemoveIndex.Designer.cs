﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231012090233_RemoveIndex")]
    partial class RemoveIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.CategoryAggregate.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId")
                        .IsUnique()
                        .HasFilter("[ParentId] IS NOT NULL");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Domain.CategoryAggregate.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Domain.CategoryAggregate.Category", b =>
                {
                    b.HasOne("Domain.CategoryAggregate.Category", "Parent")
                        .WithOne()
                        .HasForeignKey("Domain.CategoryAggregate.Category", "ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("Domain.CategoryAggregate.Image", "Image", b1 =>
                        {
                            b1.Property<int>("CategoryId")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Category");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Image");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Domain.CategoryAggregate.Item", b =>
                {
                    b.HasOne("Domain.CategoryAggregate.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.CategoryAggregate.Description", "Description", b1 =>
                        {
                            b1.Property<int>("ItemId")
                                .HasColumnType("int");

                            b1.Property<bool>("IsHtml")
                                .HasColumnType("bit");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ItemId");

                            b1.ToTable("Item");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.OwnsOne("Domain.CategoryAggregate.Image", "Image", b1 =>
                        {
                            b1.Property<int>("ItemId")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ItemId");

                            b1.ToTable("Item");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Description");

                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
