using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreadCore.Migrations
{
    public partial class ThirdCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerker_Filiaal_FiliaalNummer",
                table: "Medewerker");

            migrationBuilder.DropIndex(
                name: "IX_Medewerker_FiliaalNummer",
                table: "Medewerker");

            migrationBuilder.AlterColumn<int>(
                name: "Wachtwoord",
                table: "Medewerker",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "FiliaalNummer",
                table: "Medewerker",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Wachtwoord",
                table: "Medewerker",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FiliaalNummer",
                table: "Medewerker",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
