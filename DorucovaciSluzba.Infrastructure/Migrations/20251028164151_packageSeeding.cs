using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DorucovaciSluzba.Migrations
{
    /// <inheritdoc />
    public partial class packageSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DestinaceCP",
                table: "Zasilka",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CP",
                table: "Uzivatel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StavZasilka",
                columns: new[] { "Id", "Stav" },
                values: new object[,]
                {
                    { 1, "Objednávka vytvořena" },
                    { 2, "Zásilka je v přepravě" },
                    { 3, "Doručeno" },
                    { 4, "Reklamováno" }
                });

            migrationBuilder.UpdateData(
                table: "Uzivatel",
                keyColumn: "Id",
                keyValue: 1,
                column: "CP",
                value: null);

            migrationBuilder.InsertData(
                table: "Uzivatel",
                columns: new[] { "Id", "CP", "DatumNarozeni", "Email", "Heslo", "Jmeno", "Mesto", "Prijmeni", "Psc", "Telefon", "TypId", "Ulice" },
                values: new object[,]
                {
                    { 2, "4653", new DateTime(1995, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "novak@web.cz", "test", "Petr", "Zlín", "Novák", "760 01", "700 600 500", 2, "Kvítková" },
                    { 3, "4872", new DateTime(1998, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "buben@web.cz", "test", "Prokop", "Zlín", "Buben", "760 01", "700 600 400", 2, "Družstevní" },
                    { 4, null, null, "novotny@web.cz", "test", "Petr", null, "Novotný", null, "300 600 500", 3, null }
                });

            migrationBuilder.InsertData(
                table: "Zasilka",
                columns: new[] { "Id", "Cislo", "DestinaceCP", "DestinaceMesto", "DestinacePsc", "DestinaceUlice", "KuryrId", "OdesilatelId", "PrijemceId", "StavId" },
                values: new object[] { 1, "532-65-33", "4653", "Zlín", "760 01", "Kvítková", 4, 3, 2, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StavZasilka",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StavZasilka",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StavZasilka",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StavZasilka",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Uzivatel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Uzivatel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Uzivatel",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "DestinaceCP",
                table: "Zasilka");

            migrationBuilder.DropColumn(
                name: "CP",
                table: "Uzivatel");
        }
    }
}
