using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DorucovaciSluzba.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRolesAndBetterSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Kuryr");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Uzivatel");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "admin@kuryr.cz", "Admin", "Admin", "ADMIN@KURYR.CZ", "AQAAAAIAAYagAAAAEP88Q6YXU7RQG6LcDoHvZpXtdmv1Kh0hyoBkWJtFrTAYVz+OyRD1ZvzowRpa/sI4xg==", "VGWVSB6MVQ7NB5QYDEG52OEBGMYC3CP5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "SecurityStamp", "Telefon", "UserName" },
                values: new object[] { "cee2a6bf-19a7-4e22-8d24-1d13c8c50045", "mikydosh@kuryr.cz", "Mikydosh", "Mikydosh", "MIKYDOSH@KURYR.CZ", "MIKYDOSH", "AQAAAAIAAYagAAAAEB0xgwR38FFdOSyzuCtsLKEgylMVI5QJJtX7SD4TNt+plq1SOFJIXSPV2HL2XZVzUw==", "777 777 777", "3TUBCJIIU3M7B4O2CBRAFMUJQ2LMEBID", null, "Mikydosh" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "SecurityStamp", "Telefon", "UserName" },
                values: new object[] { "038db544-65bc-4eb8-babd-2b62292f550f", "support@kuryr.cz", "Support", "Support", "SUPPORT@KURYR.CZ", "SUPPORT", "AQAAAAIAAYagAAAAEIK3vTeRO8jtSHQeu22rjXmOAcOrnj5TTrUyLrC2PO/CLutEmOinp2XbeeZ8JzeztQ==", "777 888 999", "LZ6JM3OG3F3SHIPBSX5CXCHLIZOBAO7N", "777 888 999", "support" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "SecurityStamp", "Telefon", "UserName" },
                values: new object[] { "0f5ced05-1c9c-4ed4-bf45-560a91558b19", "petr.svoboda@email.cz", "Petr", "Svoboda", "PETR.SVOBODA@EMAIL.CZ", "PETR.SVOBODA", "AQAAAAIAAYagAAAAEIwBb49OKla+4yeCCS1R94+d1xcujKitJWWBDa41PgsTXnvmJJfVoxkUZ2/+fVXjNA==", "700 123 456", "5LIJBOB6YT3NYVGTHLD3WFTPCHTTFEKQ", "700 123 456", "petr.svoboda" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { null, "d20c95cg-ehg5-6gg9-d0g8-hdfhb21hg90e", "martin_vesely@seznam.cz", "Martin", "Veselý", null, "MARTIN_VESELY@SEZNAM.CZ", "MARTIN_VESELY", "AQAAAAIAAYagAAAAEIwBb49OKla+4yeCCS1R94+d1xcujKitJWWBDa41PgsTXnvmJJfVoxkUZ2/+fVXjNA==", "702 456 789", null, "UNK27SYDWN7W5R2YEOCUUFERKXCP4ITS", "702 456 789", null, "martin_vesely" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { null, "d20c95cg-ehg5-6gg9-d0g8-hdfhb21hg90e", "lukas.cerny@gmail.com", "Lukáš", "Černý", null, "LUKAS.CERNY@GMAIL.COM", "LUKAS.CERNY", "AQAAAAIAAYagAAAAEIwBb49OKla+4yeCCS1R94+d1xcujKitJWWBDa41PgsTXnvmJJfVoxkUZ2/+fVXjNA==", "702 456 789", null, "A3LA2H6W6F4PZIAB2UHWRER4FVJJIVUM", "739 556 789", null, "lukas.cerny" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { "123", "38882ce7-1365-4e87-b881-11156cb75c5a", "karel.prochazka@email.cz", "Karel", "Procházka", "Praha", "KAREL.PROCHAZKA@EMAIL.CZ", "KAREL.PROCHAZKA", "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==", "603 111 222", "110 00", "56RHAYBYWWYNQZ3N53XH76OVIE6QGTPQ", "603 111 222", "Hlavní", "karel.prochazka" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { "456", "6eee7fcd-c43b-42eb-acdb-909a007f85ee", "eva.malkova@email.cz", "Eva", "Málková", "Brno", "EVA.MALKOVA@EMAIL.CZ", "EVA.MALKOVA", "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==", "604 333 444", "602 00", "B3FEHEGUWPYSIMP6TIN7X7XUBGLJTF2T", "604 333 444", "Nádražní", "eva.malkova" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CP", "ConcurrencyStamp", "DatumNarozeni", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Psc", "SecurityStamp", "Telefon", "TwoFactorEnabled", "Ulice", "UserName" },
                values: new object[,]
                {
                    { 9, 0, "789", "2a59d5f9-9062-4722-b9f6-370de85ec0c7", null, "jana_horakova@email.cz", true, "Jana", "Horáková", true, null, "Ostrava", "JANA_HORAKOVA@EMAIL.CZ", "JANA_HORAKOVA", "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==", "605 555 666", false, "700 30", "XNCJIJ2L3J6HVXENA6UKUV5ND4QGLJCA", "605 555 666", false, "Zahradní", "jana_horakova" },
                    { 10, 0, "321", "0f5ced05-1c9c-4ed4-bf45-560a91558b19", null, "paveldobry@gmail.com", true, "Pavel", "Dobrý", true, null, "Plzeň", "PAVELDOBRY@GMAILL.COM", "PAVELDOBRY", "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==", "606 777 888", false, "301 00", "IAHFGXHAVOV3PM7P2TG7TNTHNJ4GBJD5", "606 777 888", false, "Školní", "paveldobry" },
                    { 11, 0, "321", "62667ef0-5589-4d42-9d78-575134d4442f", null, "katerina.dobra@gmail.com", true, "Kateřina", "Dobrá", true, null, "Plzeň", "KATERINA.DOBRA@GMAILL.COM", "KATERINADOBRA", "AQAAAAIAAYagAAAAEOwdhKmr4UhpvNhElz/3Ed+laOZriqxPV8u5bk7WW3l0iZTV6jnyElweL1GXNZJkzQ==", "609 654 888", false, "301 00", "B4PZVLZDNK7MKANXOJL4RRETNS5EHLFJ", "609 654 888", false, "Školní", "katerina.dobra" }
                });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 4, 7, 8 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 4, 7, 9 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OdesilatelId", "PrijemceId" },
                values: new object[] { 9, 11 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 4, 10, 11 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 5, 8, 10 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId", "StavId" },
                values: new object[] { 5, 11, 7, 2 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 4, 9 },
                    { 4, 10 },
                    { 4, 11 }
                });

            migrationBuilder.InsertData(
                table: "Zasilka",
                columns: new[] { "Id", "Cislo", "DatumOdeslani", "DestinaceCP", "DestinaceMesto", "DestinacePsc", "DestinaceUlice", "KuryrId", "OdesilatelId", "PrijemceId", "StavId", "UserId" },
                values: new object[] { 7, "789-01-23", new DateTime(2025, 11, 15, 8, 30, 0, 0, DateTimeKind.Unspecified), "300", "Zlín", "760 01", "Školní", 6, 7, 10, 2, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 9 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 11 });

            migrationBuilder.DeleteData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Kurýr");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Uživatel");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 3, 3 },
                    { 2, 4 },
                    { 4, 5 },
                    { 4, 6 }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FirstName", "LastName", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "admin@dorucovacisluzba.cz", "Jan", "Novák", "ADMIN@DORUCOVACISLUZBA.CZ", "AQAAAAEAACcQAAAAEM9O98Suoh2o2JOK1ZOJScgOfQ21odn/k6EYUpGWnrbevCaBFFXrNL7JZxHNczhh/w==", "SEJEPXC646ZBNCDYSM3H5FRK5RWP2TN6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "SecurityStamp", "Telefon", "UserName" },
                values: new object[] { "7a8d96fd-5918-441b-b800-cbafa99de97b", "kuryr@dorucovacisluzba.cz", "Petr", "Svoboda", "KURYR@DORUCOVACISLUZBA.CZ", "KURYR", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "700 123 456", "MAJXOSATJKOEM4YFF32Y5G2XPR5OFEL6", "700 123 456", "kuryr" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "SecurityStamp", "Telefon", "UserName" },
                values: new object[] { "d20c95cg-ehg5-6gg9-d0g8-hdfhb21hg90e", "kuryr2@dorucovacisluzba.cz", "Lukáš", "Černý", "KURYR2@DORUCOVACISLUZBA.CZ", "KURYR2", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "702 456 789", "KURYR2XC646ZBNCDYSM3H5FRK5RWP2TN6", "702 456 789", "kuryr2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "SecurityStamp", "Telefon", "UserName" },
                values: new object[] { "c19b94bf-dgf4-5ff8-c9f7-gcfga10gf89d", "podpora@dorucovacisluzba.cz", "Marie", "Dvořáková", "PODPORA@DORUCOVACISLUZBA.CZ", "PODPORA", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "777 888 999", "PODPORAXC646ZBNCDYSM3H5FRK5RWP2TN6", "777 888 999", "podpora" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { "123", "e31d05dh-fih6-7hh0-e1h9-ieiic32ih01f", "karel.prochazka@email.cz", "Karel", "Procházka", "Praha", "KAREL.PROCHAZKA@EMAIL.CZ", "KAREL.PROCHAZKA", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "603 111 222", "110 00", "UZIV1XC646ZBNCDYSM3H5FRK5RWP2TN6", "603 111 222", "Hlavní", "karel.prochazka" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { "456", "f42e16ei-gji7-8ii1-f2i0-jfjjd43ji12g", "eva.malkova@email.cz", "Eva", "Málková", "Brno", "EVA.MALKOVA@EMAIL.CZ", "EVA.MALKOVA", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "604 333 444", "602 00", "UZIV2XC646ZBNCDYSM3H5FRK5RWP2TN6", "604 333 444", "Nádražní", "eva.malkova" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { "789", "g53f27fj-hkj8-9jj2-g3j1-kgkkf54kj23h", "tomas.vesely@email.cz", "Tomáš", "Veselý", "Ostrava", "TOMAS.VESELY@EMAIL.CZ", "TOMAS.VESELY", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "605 555 666", "700 30", "UZIV3XC646ZBNCDYSM3H5FRK5RWP2TN6", "605 555 666", "Zahradní", "tomas.vesely" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CP", "ConcurrencyStamp", "Email", "FirstName", "LastName", "Mesto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "Psc", "SecurityStamp", "Telefon", "Ulice", "UserName" },
                values: new object[] { "321", "h64g38gk-ilk9-0kk3-h4k2-lhllg65lk34i", "jana.horakova@email.cz", "Jana", "Horáková", "Plzeň", "JANA.HORAKOVA@EMAIL.CZ", "JANA.HORAKOVA", "AQAAAAEAACcQAAAAEOzeajp5etRMZn7TWj9lhDMJ2GSNTtljLWVIWivadWXNMz8hj6mZ9iDR+alfEUHEMQ==", "606 777 888", "301 00", "UZIV4XC646ZBNCDYSM3H5FRK5RWP2TN6", "606 777 888", "Školní", "jana.horakova" });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 2, 5, 6 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 3, 6, 7 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "OdesilatelId", "PrijemceId" },
                values: new object[] { 7, 8 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 2, 8, 5 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId" },
                values: new object[] { 3, 5, 7 });

            migrationBuilder.UpdateData(
                table: "Zasilka",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "KuryrId", "OdesilatelId", "PrijemceId", "StavId" },
                values: new object[] { 2, 6, 5, 4 });
        }
    }
}
