using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusBilling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddQuotationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "hourly_rate",
                schema: "sales",
                table: "sales_line",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "resource_no",
                schema: "sales",
                table: "sales_line",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "service_billing_type",
                schema: "sales",
                table: "sales_line",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_end_date",
                schema: "sales",
                table: "sales_line",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "service_hours",
                schema: "sales",
                table: "sales_line",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_start_date",
                schema: "sales",
                table: "sales_line",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "observations",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "quoted_by",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "valid_until_date",
                schema: "sales",
                table: "sales_header",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hourly_rate",
                schema: "sales",
                table: "sales_line");

            migrationBuilder.DropColumn(
                name: "resource_no",
                schema: "sales",
                table: "sales_line");

            migrationBuilder.DropColumn(
                name: "service_billing_type",
                schema: "sales",
                table: "sales_line");

            migrationBuilder.DropColumn(
                name: "service_end_date",
                schema: "sales",
                table: "sales_line");

            migrationBuilder.DropColumn(
                name: "service_hours",
                schema: "sales",
                table: "sales_line");

            migrationBuilder.DropColumn(
                name: "service_start_date",
                schema: "sales",
                table: "sales_line");

            migrationBuilder.DropColumn(
                name: "observations",
                schema: "sales",
                table: "sales_header");

            migrationBuilder.DropColumn(
                name: "quoted_by",
                schema: "sales",
                table: "sales_header");

            migrationBuilder.DropColumn(
                name: "valid_until_date",
                schema: "sales",
                table: "sales_header");
        }
    }
}
