using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeCheck",
                table: "VATPostingGroups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VATPostingGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "VATPostingGroups");

            migrationBuilder.DropColumn(
                name: "ChangeCheck",
                table: "CustomerPostingGroups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CustomerPostingGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CustomerPostingGroups");

            migrationBuilder.RenameColumn(
                name: "PaymentDays",
                table: "PaymentTerms",
                newName: "PaymentDayCalculation");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "PaymentTerms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PaymentDay",
                table: "PaymentTerms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "PaymentDay",
                table: "PaymentTerms");

            migrationBuilder.RenameColumn(
                name: "PaymentDayCalculation",
                table: "PaymentTerms",
                newName: "PaymentDays");

            migrationBuilder.AddColumn<byte[]>(
                name: "ChangeCheck",
                table: "VATPostingGroups",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "VATPostingGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "VATPostingGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "ChangeCheck",
                table: "CustomerPostingGroups",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CustomerPostingGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CustomerPostingGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
