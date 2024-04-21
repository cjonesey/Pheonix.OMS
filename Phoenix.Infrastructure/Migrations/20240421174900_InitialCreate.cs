using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ISOCode2 = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPostingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPostingGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPriceGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPriceGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SellPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CostPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VATProductId = table.Column<int>(type: "int", nullable: false),
                    VATCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    LastNumber = table.Column<int>(type: "int", nullable: false),
                    Increment = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    MaxNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PaymentDats = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesDeliveryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDeliveries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillToName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BillToStreet1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BillToStreet2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BillToCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BillToCounty = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BillToPostcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BillToCountryId = table.Column<int>(type: "int", nullable: false),
                    BillToCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ShipToName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShipToStreet1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShipToStreet2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShipToCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShipToCounty = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShipToPostcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShipToCountryId = table.Column<int>(type: "int", nullable: false),
                    ShipToCountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    OrderStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderType = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderTax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderShipping = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderDiscount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderSubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderGrandTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OrderPaymentMethod = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderPaymentStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    OrderPaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    County = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VATPostingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VATPostingGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    County = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDeliveryLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesDeliveryId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ItemNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityShipped = table.Column<int>(type: "int", nullable: false),
                    QuantityBackOrdered = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDeliveryLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDeliveryLines_SalesDeliveries_SalesDeliveryId",
                        column: x => x.SalesDeliveryId,
                        principalTable: "SalesDeliveries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Street1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    County = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VATRegistrationNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GLN = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CompanyRegistrationNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EORIRegistrationNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    VATPostingGroupID = table.Column<int>(type: "int", nullable: false),
                    CustomerPostingGroupID = table.Column<int>(type: "int", nullable: false),
                    CustomerPriceGroupID = table.Column<int>(type: "int", nullable: true),
                    ConsolidateInvoices = table.Column<bool>(type: "bit", nullable: false),
                    PrepaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CashApplicationMethod = table.Column<short>(type: "smallint", nullable: false),
                    PaymentTermsID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    ReserveStock = table.Column<short>(type: "smallint", nullable: false),
                    ShipComplete = table.Column<short>(type: "smallint", nullable: false),
                    ShowPricesOnSalesDocuments = table.Column<short>(type: "smallint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerPostingGroups_CustomerPostingGroupID",
                        column: x => x.CustomerPostingGroupID,
                        principalTable: "CustomerPostingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerPriceGroups_CustomerPriceGroupID",
                        column: x => x.CustomerPriceGroupID,
                        principalTable: "CustomerPriceGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Customers_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_PaymentTerms_PaymentTermsID",
                        column: x => x.PaymentTermsID,
                        principalTable: "PaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_VATPostingGroups_VATPostingGroupID",
                        column: x => x.VATPostingGroupID,
                        principalTable: "VATPostingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    LocationCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Length = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesOrderId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityAllocated = table.Column<int>(type: "int", nullable: false),
                    QuantityShipped = table.Column<int>(type: "int", nullable: false),
                    QuantityBackOrdered = table.Column<int>(type: "int", nullable: false),
                    QuantityReturned = table.Column<int>(type: "int", nullable: false),
                    QuantityInvoiced = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderLines_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    AddressCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BillToAddressId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Street2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    County = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerItemReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    CustomerItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerItemReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerItemReferences_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerItemReferences_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeCheck = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemWarehouses_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemWarehouses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemWarehouses_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerItemReferences_CustomerId",
                table: "CustomerItemReferences",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerItemReferences_ItemId",
                table: "CustomerItemReferences",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerPostingGroupID",
                table: "Customers",
                column: "CustomerPostingGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerPriceGroupID",
                table: "Customers",
                column: "CustomerPriceGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentMethodID",
                table: "Customers",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentTermsID",
                table: "Customers",
                column: "PaymentTermsID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_VATPostingGroupID",
                table: "Customers",
                column: "VATPostingGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouses_ItemId",
                table: "ItemWarehouses",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouses_LocationId",
                table: "ItemWarehouses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouses_SupplierId",
                table: "ItemWarehouses",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemWarehouses_WarehouseId",
                table: "ItemWarehouses",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_WarehouseId",
                table: "Locations",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDeliveryLines_SalesDeliveryId",
                table: "SalesDeliveryLines",
                column: "SalesDeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_CustomerId",
                table: "SalesInvoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_ItemId",
                table: "SalesOrderLines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_SalesOrderId",
                table: "SalesOrderLines",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLines_WarehouseId",
                table: "SalesOrderLines",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CountryId",
                table: "Warehouses",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "CustomerItemReferences");

            migrationBuilder.DropTable(
                name: "ItemWarehouses");

            migrationBuilder.DropTable(
                name: "NumberSeries");

            migrationBuilder.DropTable(
                name: "PurchaseParameters");

            migrationBuilder.DropTable(
                name: "SalesDeliveryLines");

            migrationBuilder.DropTable(
                name: "SalesInvoiceLines");

            migrationBuilder.DropTable(
                name: "SalesInvoices");

            migrationBuilder.DropTable(
                name: "SalesOrderLines");

            migrationBuilder.DropTable(
                name: "SalesParameters");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "SalesDeliveries");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "CustomerPostingGroups");

            migrationBuilder.DropTable(
                name: "CustomerPriceGroups");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentTerms");

            migrationBuilder.DropTable(
                name: "VATPostingGroups");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
