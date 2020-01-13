﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyRental.Models;

namespace MyRental.Migrations
{
    [DbContext(typeof(MyRentalDbContext))]
    [Migration("20200108221624_change_item_column_name")]
    partial class change_item_column_name
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("myrental.Models.ItemModels.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("varchar(1000) CHARACTER SET utf8mb4")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("ItemID");

                    b.ToTable("items");

                    b.HasData(
                        new
                        {
                            ItemID = 1,
                            Detail = "details1",
                            ExpireTime = new DateTime(2020, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ItemName = "item1",
                            PostTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Price = 200
                        },
                        new
                        {
                            ItemID = 2,
                            Detail = "details2",
                            ExpireTime = new DateTime(2020, 7, 1, 6, 0, 0, 0, DateTimeKind.Unspecified),
                            ItemName = "item2",
                            PostTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Price = 100
                        });
                });
#pragma warning restore 612, 618
        }
    }
}