using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreadCore.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BroodType",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    BakProgramma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroodType", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Filiaal",
                columns: table => new
                {
                    FiliaalNummer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiliaalNaam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filiaal", x => x.FiliaalNummer);
                });

            migrationBuilder.CreateTable(
                name: "Medewerker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedienerNr = table.Column<int>(type: "int", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiliaalNummer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerker_Filiaal_FiliaalNummer",
                        column: x => x.FiliaalNummer,
                        principalTable: "Filiaal",
                        principalColumn: "FiliaalNummer");
                });

            migrationBuilder.CreateTable(
                name: "Brood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GebakkenBroodType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GebakkenFiliaalFiliaalNummer = table.Column<int>(type: "int", nullable: false),
                    TijdGebakken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoeveelheidGebakken = table.Column<int>(type: "int", nullable: false),
                    HoeveelheidDerving = table.Column<int>(type: "int", nullable: false),
                    BakkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brood_BroodType_GebakkenBroodType",
                        column: x => x.GebakkenBroodType,
                        principalTable: "BroodType",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brood_Filiaal_GebakkenFiliaalFiliaalNummer",
                        column: x => x.GebakkenFiliaalFiliaalNummer,
                        principalTable: "Filiaal",
                        principalColumn: "FiliaalNummer",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brood_Medewerker_BakkerId",
                        column: x => x.BakkerId,
                        principalTable: "Medewerker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brood_BakkerId",
                table: "Brood",
                column: "BakkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Brood_GebakkenBroodType",
                table: "Brood",
                column: "GebakkenBroodType");

            migrationBuilder.CreateIndex(
                name: "IX_Brood_GebakkenFiliaalFiliaalNummer",
                table: "Brood",
                column: "GebakkenFiliaalFiliaalNummer");

            migrationBuilder.CreateIndex(
                name: "IX_Medewerker_FiliaalNummer",
                table: "Medewerker",
                column: "FiliaalNummer");
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
                name: "Filiaal");
        }
    }
}
