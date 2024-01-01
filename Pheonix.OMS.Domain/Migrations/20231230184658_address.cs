using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pheonix.OMS.Domain.Migrations
{
    /// <inheritdoc />
    public partial class address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Warehouses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Warehouses",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Warehouses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Warehouses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street1",
                table: "Warehouses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "Warehouses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Suppliers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Suppliers",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Suppliers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Suppliers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Suppliers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street1",
                table: "Suppliers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "Suppliers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillToCity",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillToCountryCode",
                table: "SalesOrders",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BillToCountryId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BillToCounty",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillToName",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillToPostcode",
                table: "SalesOrders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillToStreet1",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillToStreet2",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShipToCity",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToCountryCode",
                table: "SalesOrders",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShipToCountryId",
                table: "SalesOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShipToCounty",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToName",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToPostcode",
                table: "SalesOrders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToStreet1",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShipToStreet2",
                table: "SalesOrders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Customers",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "Customers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street1",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CustomerAddresses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "CustomerAddresses",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "CustomerAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "CustomerAddresses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CustomerAddresses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "CustomerAddresses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street1",
                table: "CustomerAddresses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street2",
                table: "CustomerAddresses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Street1",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "Street2",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Street1",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "Street2",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "BillToCity",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToCountryCode",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToCountryId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToCounty",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToName",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToPostcode",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToStreet1",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "BillToStreet2",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToCity",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToCountryCode",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToCountryId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToCounty",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToName",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToPostcode",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToStreet1",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "ShipToStreet2",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "County",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Street1",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Street2",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "County",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "Street1",
                table: "CustomerAddresses");

            migrationBuilder.DropColumn(
                name: "Street2",
                table: "CustomerAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCode",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
