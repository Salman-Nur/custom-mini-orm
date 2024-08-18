﻿// <auto-generated />
using System;
using Assignment3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assignment3.TestCase1.Color", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("ProductId");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FeedbackGiverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackGiverId");

                    b.HasIndex("ItemId");

                    b.HasIndex("ProductId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Assignment3.TestCase1.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Vendor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enlisted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendor");
                });

            modelBuilder.Entity("Assignment3.TestCase2.SubClass1", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Property1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Property2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubClass1");
                });

            modelBuilder.Entity("Assignment3.TestCase2.SubClass2", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Property1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Property2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TestClass2Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestClass2Id");

                    b.ToTable("SubClass2");
                });

            modelBuilder.Entity("Assignment3.TestCase2.Temp1", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TestClass1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TestClass2Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestClass1Id");

                    b.HasIndex("TestClass2Id");

                    b.ToTable("Temp1");
                });

            modelBuilder.Entity("Assignment3.TestCase2.Temp2", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Temp1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TempId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Temp1Id");

                    b.HasIndex("TempId");

                    b.ToTable("Temp2");
                });

            modelBuilder.Entity("Assignment3.TestCase2.Temp3", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Temp3");
                });

            modelBuilder.Entity("Assignment3.TestCase2.TestClass1", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Class2Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Class2Id");

                    b.ToTable("TestClass1");
                });

            modelBuilder.Entity("Assignment3.TestCase2.TestClass2", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Class1Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Class1Id");

                    b.ToTable("TestClass2");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Color", b =>
                {
                    b.HasOne("Assignment3.TestCase1.Item", null)
                        .WithMany("Colors")
                        .HasForeignKey("ItemId");

                    b.HasOne("Assignment3.TestCase1.Product", null)
                        .WithMany("Colors")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Feedback", b =>
                {
                    b.HasOne("Assignment3.TestCase1.User", "FeedbackGiver")
                        .WithMany()
                        .HasForeignKey("FeedbackGiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Assignment3.TestCase1.Item", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("ItemId");

                    b.HasOne("Assignment3.TestCase1.Product", null)
                        .WithMany("Feedbacks")
                        .HasForeignKey("ProductId");

                    b.Navigation("FeedbackGiver");
                });

            modelBuilder.Entity("Assignment3.TestCase2.SubClass2", b =>
                {
                    b.HasOne("Assignment3.TestCase2.TestClass2", null)
                        .WithMany("SubClasses")
                        .HasForeignKey("TestClass2Id");
                });

            modelBuilder.Entity("Assignment3.TestCase2.Temp1", b =>
                {
                    b.HasOne("Assignment3.TestCase2.TestClass1", null)
                        .WithMany("Temps")
                        .HasForeignKey("TestClass1Id");

                    b.HasOne("Assignment3.TestCase2.TestClass2", null)
                        .WithMany("Temps")
                        .HasForeignKey("TestClass2Id");
                });

            modelBuilder.Entity("Assignment3.TestCase2.Temp2", b =>
                {
                    b.HasOne("Assignment3.TestCase2.Temp1", null)
                        .WithMany("Temps")
                        .HasForeignKey("Temp1Id");

                    b.HasOne("Assignment3.TestCase2.Temp3", "Temp")
                        .WithMany()
                        .HasForeignKey("TempId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Temp");
                });

            modelBuilder.Entity("Assignment3.TestCase2.TestClass1", b =>
                {
                    b.HasOne("Assignment3.TestCase2.SubClass2", "Class2")
                        .WithMany()
                        .HasForeignKey("Class2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Class2");
                });

            modelBuilder.Entity("Assignment3.TestCase2.TestClass2", b =>
                {
                    b.HasOne("Assignment3.TestCase2.SubClass1", "Class1")
                        .WithMany()
                        .HasForeignKey("Class1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Class1");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Item", b =>
                {
                    b.Navigation("Colors");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Assignment3.TestCase1.Product", b =>
                {
                    b.Navigation("Colors");

                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("Assignment3.TestCase2.Temp1", b =>
                {
                    b.Navigation("Temps");
                });

            modelBuilder.Entity("Assignment3.TestCase2.TestClass1", b =>
                {
                    b.Navigation("Temps");
                });

            modelBuilder.Entity("Assignment3.TestCase2.TestClass2", b =>
                {
                    b.Navigation("SubClasses");

                    b.Navigation("Temps");
                });
#pragma warning restore 612, 618
        }
    }
}
