using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentDats",
                table: "PaymentTerms",
                newName: "PaymentDays");

            migrationBuilder.AddColumn<byte[]>(
                name: "ChangeCheck",
                table: "PaymentTerms",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentTerms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PaymentTerms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeCheck",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PaymentTerms");

            migrationBuilder.RenameColumn(
                name: "PaymentDays",
                table: "PaymentTerms",
                newName: "PaymentDats");
        }
    }
}
