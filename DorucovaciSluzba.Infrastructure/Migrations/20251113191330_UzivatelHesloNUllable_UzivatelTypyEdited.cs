using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DorucovaciSluzba.Migrations
{
    /// <inheritdoc />
    public partial class UzivatelHesloNUllable_UzivatelTypyEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Heslo",
                table: "Uzivatel",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 1,
                column: "Typ",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 2,
                column: "Typ",
                value: "Běžný uživatel");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 3,
                column: "Typ",
                value: "Kurýr");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 4,
                column: "Typ",
                value: "Podpora");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Uzivatel",
                keyColumn: "Heslo",
                keyValue: null,
                column: "Heslo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Heslo",
                table: "Uzivatel",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 1,
                column: "Typ",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 2,
                column: "Typ",
                value: "bezny uzivatel");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 3,
                column: "Typ",
                value: "kuryr");

            migrationBuilder.UpdateData(
                table: "TypUzivatel",
                keyColumn: "Id",
                keyValue: 4,
                column: "Typ",
                value: "podpora");
        }
    }
}
