using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pheonix.OMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class warehouse1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CountryId",
                table: "Warehouses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Countries_CountryId",
                table: "Warehouses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Countries_CountryId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_CountryId",
                table: "Warehouses");
        }
    }
}
