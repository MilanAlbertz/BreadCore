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
                name: "BroodType",
                columns: table => new
                {
                    BroodTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    BakProgramma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroodType", x => x.BroodTypeID);
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
                name: "Brood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BroodTypeID = table.Column<int>(type: "int", nullable: false),
                    GebakkenFiliaalFiliaalId = table.Column<int>(type: "int", nullable: false),
                    TijdGebakken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoeveelheidGebakken = table.Column<int>(type: "int", nullable: false),
                    HoeveelheidDerving = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brood_BroodType_BroodTypeID",
                        column: x => x.BroodTypeID,
                        principalTable: "BroodType",
                        principalColumn: "BroodTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Brood_Filiaal_GebakkenFiliaalFiliaalId",
                        column: x => x.GebakkenFiliaalFiliaalId,
                        principalTable: "Filiaal",
                        principalColumn: "FiliaalId",
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
                    FiliaalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerker_Filiaal_FiliaalId",
                        column: x => x.FiliaalId,
                        principalTable: "Filiaal",
                        principalColumn: "FiliaalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brood_BroodTypeID",
                table: "Brood",
                column: "BroodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Brood_GebakkenFiliaalFiliaalId",
                table: "Brood",
                column: "GebakkenFiliaalFiliaalId");

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
                name: "Medewerker");

            migrationBuilder.DropTable(
                name: "BroodType");

            migrationBuilder.DropTable(
                name: "Filiaal");
        }
    }
}
