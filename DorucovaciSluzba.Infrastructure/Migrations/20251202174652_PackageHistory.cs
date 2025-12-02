using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DorucovaciSluzba.Migrations
{
    /// <inheritdoc />
    public partial class PackageHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageHistoryItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ZasilkaId = table.Column<int>(type: "int", nullable: false),
                    StavId = table.Column<int>(type: "int", nullable: false),
                    DatumZmeny = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageHistoryItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageHistoryItem_StavZasilka_StavId",
                        column: x => x.StavId,
                        principalTable: "StavZasilka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PackageHistoryItem_StavId",
                table: "PackageHistoryItem",
                column: "StavId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageHistoryItem");
        }
    }
}
