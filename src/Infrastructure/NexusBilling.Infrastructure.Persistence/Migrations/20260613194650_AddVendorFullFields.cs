using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusBilling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVendorFullFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address_2",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "credit_limit",
                schema: "purchasing",
                table: "vendor",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "payment_method_code",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_no_2",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "province",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "vendor_type",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "web_site",
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
                name: "address_2",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "credit_limit",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "payment_method_code",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "phone_no_2",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "province",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "vendor_type",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropColumn(
                name: "web_site",
                schema: "purchasing",
                table: "vendor");
        }
    }
}
