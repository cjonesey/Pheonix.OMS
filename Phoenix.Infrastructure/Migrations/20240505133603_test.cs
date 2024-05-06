using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "PaymentMethods",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PaymentMethods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "ChangeCheck",
                table: "CustomerPriceGroups",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CustomerPriceGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CustomerPriceGroups",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "ChangeCheck",
                table: "CustomerPriceGroups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CustomerPriceGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CustomerPriceGroups");

            migrationBuilder.DropColumn(
                name: "ChangeCheck",
                table: "CustomerPostingGroups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CustomerPostingGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CustomerPostingGroups");
        }
    }
}
