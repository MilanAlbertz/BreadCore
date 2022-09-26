using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreadCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bakprogramma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakprogramma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filiaal",
                columns: table => new
                {
                    FiliaalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiliaalNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiaal", x => x.FiliaalId);
                });

            migrationBuilder.CreateTable(
                name: "BroodType",
                columns: table => new
                {
                    BroodTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    BakprogrammaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroodType", x => x.BroodTypeID);
                    table.ForeignKey(
                        name: "FK_BroodType_Bakprogramma_BakprogrammaId",
                        column: x => x.BakprogrammaId,
                        principalTable: "Bakprogramma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medewerker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedienerNr = table.Column<int>(type: "int", nullable: false),
                    Wachtwoord = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiliaalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerker_Filiaal_FiliaalId",
                        column: x => x.FiliaalId,
                        principalTable: "Filiaal",
                        principalColumn: "FiliaalId");
                });

            migrationBuilder.CreateTable(
                name: "Brood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BroodTypeID = table.Column<int>(type: "int", nullable: true),
                    GebakkenFiliaalId = table.Column<int>(type: "int", nullable: false),
                    TijdGebakken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoeveelheidGebakken = table.Column<int>(type: "int", nullable: true),
                    HoeveelheidDerving = table.Column<int>(type: "int", nullable: true),
                    MedewerkerId = table.Column<int>(type: "int", nullable: true),
                    Bakprogramma = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brood_BroodType_BroodTypeID",
                        column: x => x.BroodTypeID,
                        principalTable: "BroodType",
                        principalColumn: "BroodTypeID");
                    table.ForeignKey(
                        name: "FK_Brood_Filiaal_GebakkenFiliaalId",
                        column: x => x.GebakkenFiliaalId,
                        principalTable: "Filiaal",
                        principalColumn: "FiliaalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brood_Medewerker_MedewerkerId",
                        column: x => x.MedewerkerId,
                        principalTable: "Medewerker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brood_BroodTypeID",
                table: "Brood",
                column: "BroodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Brood_GebakkenFiliaalId",
                table: "Brood",
                column: "GebakkenFiliaalId");

            migrationBuilder.CreateIndex(
                name: "IX_Brood_MedewerkerId",
                table: "Brood",
                column: "MedewerkerId");

            migrationBuilder.CreateIndex(
                name: "IX_BroodType_BakprogrammaId",
                table: "BroodType",
                column: "BakprogrammaId");

            migrationBuilder.CreateIndex(
                name: "IX_Medewerker_FiliaalId",
                table: "Medewerker",
                column: "FiliaalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brood");

            migrationBuilder.DropTable(
                name: "BroodType");

            migrationBuilder.DropTable(
                name: "Medewerker");

            migrationBuilder.DropTable(
                name: "Bakprogramma");

            migrationBuilder.DropTable(
                name: "Filiaal");
        }
    }
}
