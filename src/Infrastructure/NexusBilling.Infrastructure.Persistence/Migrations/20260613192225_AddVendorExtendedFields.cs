using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusBilling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVendorExtendedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "sales",
                table: "sales_invoice_header");

            migrationBuilder.DropColumn(
                name: "AmountIncludingVat",
                schema: "sales",
                table: "sales_invoice_header");

            migrationBuilder.AddColumn<string>(
                name: "currency_code",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payment_terms_code",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_no",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "rnc",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currency_code",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "email",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "payment_terms_code",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "phone_no",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "rnc",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "sales",
                table: "sales_invoice_header",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountIncludingVat",
                schema: "sales",
                table: "sales_invoice_header",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
