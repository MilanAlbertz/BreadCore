using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreadCore.Migrations
{
    public partial class WerkendFiliaal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FiliaalNummer",
                table: "Medewerker",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "WerkendFiliaal",
                table: "Medewerker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medewerker_FiliaalNummer",
                table: "Medewerker",
                column: "FiliaalNummer");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerker_Filiaal_FiliaalNummer",
                table: "Medewerker",
                column: "FiliaalNummer",
                principalTable: "Filiaal",
                principalColumn: "FiliaalNummer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WerkendFiliaal",
                table: "Medewerker");
        }
    }
}
