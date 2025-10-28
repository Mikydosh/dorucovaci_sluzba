using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DorucovaciSluzba.Migrations
{
    /// <inheritdoc />
    public partial class userSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TypUzivatel",
                columns: new[] { "Id", "Typ" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "bezny uzivatel" },
                    { 3, "kuryr" },
                    { 4, "podpora" }
                });

            migrationBuilder.InsertData(
                table: "Uzivatel",
                columns: new[] { "Id", "DatumNarozeni", "Email", "Heslo", "Jmeno", "Mesto", "Prijmeni", "Psc", "Telefon", "TypId", "Ulice" },
                values: new object[] { 1, null, "admin@web.cz", "admin", "Web", null, "Admin", null, null, 1, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Uzivatel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
