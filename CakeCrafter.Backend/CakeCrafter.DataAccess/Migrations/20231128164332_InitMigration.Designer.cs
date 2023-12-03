﻿// <auto-generated />
using System;
using CakeCrafter.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CakeCrafter.DataAccess.Migrations
{
    [DbContext(typeof(CakeCrafterDbContext))]
    [Migration("20231128164332_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CakeCrafter.Core.Models.Cake", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("CookTime")
                        .HasColumnType("time");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TasteId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TasteId");

                    b.ToTable("Cake");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.CakesIngredients", b =>
                {
                    b.Property<int>("CakeId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("IngredientId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int?>("CakeEntityId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientQuantity")
                        .HasColumnType("int");

                    b.HasKey("CakeId", "IngredientId");

                    b.HasIndex("CakeEntityId");

                    b.HasIndex("IngredientId");

                    b.ToTable("CakesIngredients");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IngredientCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("MeasureUnitId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("IngredientCategoryId");

                    b.HasIndex("MeasureUnitId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.IngredientCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("IngredientCategories");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.MeasureUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("MeasureUnits");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Taste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tastes");
                });

            modelBuilder.Entity("CakeCrafter.DataAccess.Entites.CakeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("CookTime")
                        .HasColumnType("time");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TasteId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TasteId");

                    b.ToTable("Cakes");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Cake", b =>
                {
                    b.HasOne("CakeCrafter.Core.Models.Category", null)
                        .WithMany("Cakes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CakeCrafter.Core.Models.Taste", null)
                        .WithMany("Cakes")
                        .HasForeignKey("TasteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.CakesIngredients", b =>
                {
                    b.HasOne("CakeCrafter.DataAccess.Entites.CakeEntity", null)
                        .WithMany("CakeIngredients")
                        .HasForeignKey("CakeEntityId");

                    b.HasOne("CakeCrafter.Core.Models.Cake", "Cake")
                        .WithMany("CakeIngredients")
                        .HasForeignKey("CakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CakeCrafter.Core.Models.Ingredient", "Ingredient")
                        .WithMany("CakesIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cake");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Ingredient", b =>
                {
                    b.HasOne("CakeCrafter.Core.Models.IngredientCategory", "IngredientCategory")
                        .WithMany("Ingredients")
                        .HasForeignKey("IngredientCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CakeCrafter.Core.Models.MeasureUnit", "MeasureUnit")
                        .WithMany("Ingredients")
                        .HasForeignKey("MeasureUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngredientCategory");

                    b.Navigation("MeasureUnit");
                });

            modelBuilder.Entity("CakeCrafter.DataAccess.Entites.CakeEntity", b =>
                {
                    b.HasOne("CakeCrafter.Core.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CakeCrafter.Core.Models.Taste", "Taste")
                        .WithMany()
                        .HasForeignKey("TasteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Taste");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Cake", b =>
                {
                    b.Navigation("CakeIngredients");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Category", b =>
                {
                    b.Navigation("Cakes");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Ingredient", b =>
                {
                    b.Navigation("CakesIngredients");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.IngredientCategory", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.MeasureUnit", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("CakeCrafter.Core.Models.Taste", b =>
                {
                    b.Navigation("Cakes");
                });

            modelBuilder.Entity("CakeCrafter.DataAccess.Entites.CakeEntity", b =>
                {
                    b.Navigation("CakeIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}