using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NullablDate",
                table: "PaymentTerms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NullableInt",
                table: "PaymentTerms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NullablDate",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "NullableInt",
                table: "PaymentTerms");
        }
    }
}
