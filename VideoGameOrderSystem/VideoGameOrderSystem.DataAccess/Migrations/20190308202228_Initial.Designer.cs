﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoGameOrderSystem.DataAccess;

namespace VideoGameOrderSystem.DataAccess.Migrations
{
    [DbContext(typeof(OrderSystemContext))]
    [Migration("20190308202228_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("StoreId")
                        .HasColumnName("StoreID");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("Customer","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.IbundleItems", b =>
                {
                    b.Property<int>("BundleId")
                        .HasColumnName("BundleID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("BundleId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("IBundleItems","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Ibundles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("IBundles","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Inventory", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnName("StoreID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("StoreId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Inventory","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Iproduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.ToTable("IProduct","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.ObundleItems", b =>
                {
                    b.Property<int>("BundleId")
                        .HasColumnName("BundleID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("BundleId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OBundleItems","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Obundles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("OBundles","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Oproduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.ToTable("OProduct","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.OrderItems", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnName("CustomerID");

                    b.Property<int>("StoreId")
                        .HasColumnName("StoreID");

                    b.Property<DateTime>("TimePlaced");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StoreId");

                    b.ToTable("Orders","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Store","OS");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Customer", b =>
                {
                    b.HasOne("VideoGameOrderSystem.DataAccess.Store", "Store")
                        .WithMany("Customer")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Customer_To_Store");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.IbundleItems", b =>
                {
                    b.HasOne("VideoGameOrderSystem.DataAccess.Ibundles", "Bundle")
                        .WithMany("IbundleItems")
                        .HasForeignKey("BundleId")
                        .HasConstraintName("FK_BundleItems_To_IBundles");

                    b.HasOne("VideoGameOrderSystem.DataAccess.Iproduct", "Product")
                        .WithMany("IbundleItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_BundleItems_To_IProduct");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Inventory", b =>
                {
                    b.HasOne("VideoGameOrderSystem.DataAccess.Iproduct", "Product")
                        .WithMany("Inventory")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Inventory_To_Product");

                    b.HasOne("VideoGameOrderSystem.DataAccess.Store", "Store")
                        .WithMany("Inventory")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Inventory_To_Store");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.ObundleItems", b =>
                {
                    b.HasOne("VideoGameOrderSystem.DataAccess.Obundles", "Bundle")
                        .WithMany("ObundleItems")
                        .HasForeignKey("BundleId")
                        .HasConstraintName("FK_BundleItems_To_OBundles");

                    b.HasOne("VideoGameOrderSystem.DataAccess.Oproduct", "Product")
                        .WithMany("ObundleItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_BundleItems_To_OProduct");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.OrderItems", b =>
                {
                    b.HasOne("VideoGameOrderSystem.DataAccess.Orders", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderItems_To_Order");

                    b.HasOne("VideoGameOrderSystem.DataAccess.Oproduct", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_OrderItems_To_Product");
                });

            modelBuilder.Entity("VideoGameOrderSystem.DataAccess.Orders", b =>
                {
                    b.HasOne("VideoGameOrderSystem.DataAccess.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_To_Customer");

                    b.HasOne("VideoGameOrderSystem.DataAccess.Store", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_Order_To_Store");
                });
#pragma warning restore 612, 618
        }
    }
}
