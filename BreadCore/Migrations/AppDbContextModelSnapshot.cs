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

            modelBuilder.Entity("BreadCore.Models.Bakprogramma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bakprogramma");
                });

            modelBuilder.Entity("BreadCore.Models.Brood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Bakprogramma")
                        .HasColumnType("int");

                    b.Property<int?>("BroodTypeID")
                        .HasColumnType("int");

                    b.Property<int?>("GebakkenFiliaalId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("HoeveelheidDerving")
                        .HasColumnType("int");

                    b.Property<int?>("HoeveelheidGebakken")
                        .HasColumnType("int");

                    b.Property<int?>("MedewerkerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TijdGebakken")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BroodTypeID");

                    b.HasIndex("GebakkenFiliaalId");

                    b.HasIndex("MedewerkerId");

                    b.ToTable("Brood");
                });

            modelBuilder.Entity("BreadCore.Models.BroodType", b =>
                {
                    b.Property<int>("BroodTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BroodTypeID"), 1L, 1);

                    b.Property<int>("BakprogrammaId")
                        .HasColumnType("int");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BroodTypeID");

                    b.HasIndex("BakprogrammaId");

                    b.ToTable("BroodType");
                });

            modelBuilder.Entity("BreadCore.Models.Filiaal", b =>
                {
                    b.Property<int>("FiliaalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FiliaalId"), 1L, 1);

                    b.Property<string>("FiliaalNaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FiliaalId");

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

                    b.Property<int?>("FiliaalId")
                        .HasColumnType("int");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wachtwoord")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FiliaalId");

                    b.ToTable("Medewerker");
                });

            modelBuilder.Entity("BreadCore.Models.Brood", b =>
                {
                    b.HasOne("BreadCore.Models.BroodType", "BroodType")
                        .WithMany()
                        .HasForeignKey("BroodTypeID");

                    b.HasOne("BreadCore.Models.Filiaal", "GebakkenFiliaal")
                        .WithMany()
                        .HasForeignKey("GebakkenFiliaalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BreadCore.Models.Medewerker", "Medewerker")
                        .WithMany()
                        .HasForeignKey("MedewerkerId");

                    b.Navigation("BroodType");

                    b.Navigation("GebakkenFiliaal");

                    b.Navigation("Medewerker");
                });

            modelBuilder.Entity("BreadCore.Models.BroodType", b =>
                {
                    b.HasOne("BreadCore.Models.Bakprogramma", "Bakprogramma")
                        .WithMany("BroodTypes")
                        .HasForeignKey("BakprogrammaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bakprogramma");
                });

            modelBuilder.Entity("BreadCore.Models.Medewerker", b =>
                {
                    b.HasOne("BreadCore.Models.Filiaal", "Filiaal")
                        .WithMany("Medewerkers")
                        .HasForeignKey("FiliaalId");

                    b.Navigation("Filiaal");
                });

            modelBuilder.Entity("BreadCore.Models.Bakprogramma", b =>
                {
                    b.Navigation("BroodTypes");
                });

            modelBuilder.Entity("BreadCore.Models.Filiaal", b =>
                {
                    b.Navigation("Medewerkers");
                });
#pragma warning restore 612, 618
        }
    }
}
