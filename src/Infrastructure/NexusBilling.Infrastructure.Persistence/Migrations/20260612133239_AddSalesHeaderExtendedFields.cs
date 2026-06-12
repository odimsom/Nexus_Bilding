using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusBilling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesHeaderExtendedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                schema: "sales",
                table: "sales_header",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "amount_including_vat",
                schema: "sales",
                table: "sales_header",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "currency_code",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "due_date",
                schema: "sales",
                table: "sales_header",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "external_document_no",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payment_method_code",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payment_terms_code",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "salesperson_code",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sell_to_customer_name",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                defaultValue: "Open");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "amount",                schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "amount_including_vat",  schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "currency_code",          schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "due_date",               schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "external_document_no",   schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "payment_method_code",    schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "payment_terms_code",     schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "salesperson_code",       schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "sell_to_customer_name",  schema: "sales", table: "sales_header");
            migrationBuilder.DropColumn(name: "status",                 schema: "sales", table: "sales_header");
        }
    }
}
