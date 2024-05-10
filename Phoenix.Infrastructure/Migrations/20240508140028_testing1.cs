using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testing1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NullablDate",
                table: "PaymentTerms",
                newName: "NullableDate");

            migrationBuilder.AlterColumn<int>(
                name: "NullableInt",
                table: "PaymentTerms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "NullableDecimal",
                table: "PaymentTerms",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NullableDecimal",
                table: "PaymentTerms");

            migrationBuilder.RenameColumn(
                name: "NullableDate",
                table: "PaymentTerms",
                newName: "NullablDate");

            migrationBuilder.AlterColumn<int>(
                name: "NullableInt",
                table: "PaymentTerms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
