﻿// <auto-generated />
using System;
using BreadCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BreadCore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BreadCore.Models.Brood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BakkerId")
                        .HasColumnType("int");

                    b.Property<string>("GebakkenBroodType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GebakkenFiliaalFiliaalNummer")
                        .HasColumnType("int");

                    b.Property<int>("HoeveelheidDerving")
                        .HasColumnType("int");

                    b.Property<int>("HoeveelheidGebakken")
                        .HasColumnType("int");

                    b.Property<DateTime>("TijdGebakken")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BakkerId");

                    b.HasIndex("GebakkenBroodType");

                    b.HasIndex("GebakkenFiliaalFiliaalNummer");

                    b.ToTable("Brood");
                });

            modelBuilder.Entity("BreadCore.Models.BroodType", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BakProgramma")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.HasKey("Type");

                    b.ToTable("BroodType");
                });

            modelBuilder.Entity("BreadCore.Models.Filiaal", b =>
                {
                    b.Property<int>("FiliaalNummer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FiliaalNummer"), 1L, 1);

                    b.Property<string>("FiliaalNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FiliaalNummer");

                    b.ToTable("Filiaal");
                });

            modelBuilder.Entity("BreadCore.Models.Medewerker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BedienerNr")
                        .HasColumnType("int");

                    b.Property<int?>("FiliaalNummer")
                        .HasColumnType("int");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FiliaalNummer");

                    b.ToTable("Medewerker");
                });

            modelBuilder.Entity("BreadCore.Models.Brood", b =>
                {
                    b.HasOne("BreadCore.Models.Medewerker", "Bakker")
                        .WithMany()
                        .HasForeignKey("BakkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreadCore.Models.BroodType", "GebakkenBrood")
                        .WithMany()
                        .HasForeignKey("GebakkenBroodType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreadCore.Models.Filiaal", "GebakkenFiliaal")
                        .WithMany()
                        .HasForeignKey("GebakkenFiliaalFiliaalNummer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bakker");

                    b.Navigation("GebakkenBrood");

                    b.Navigation("GebakkenFiliaal");
                });

            modelBuilder.Entity("BreadCore.Models.Medewerker", b =>
                {
                    b.HasOne("BreadCore.Models.Filiaal", null)
                        .WithMany("Medewerkers")
                        .HasForeignKey("FiliaalNummer");
                });

            modelBuilder.Entity("BreadCore.Models.Orderline", b =>
                {
                    b.HasOne("BreadCore.Models.Order", null)
                        .WithMany("Orderlines")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("BreadCore.Models.Filiaal", b =>
                {
                    b.Navigation("Medewerkers");
                });

            modelBuilder.Entity("BreadCore.Models.Order", b =>
                {
                    b.Navigation("Orderlines");
                });
#pragma warning restore 612, 618
        }
    }
}
