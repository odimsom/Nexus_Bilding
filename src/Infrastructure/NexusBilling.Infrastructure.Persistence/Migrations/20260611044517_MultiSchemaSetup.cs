using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusBilling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MultiSchemaSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sales");

            migrationBuilder.EnsureSchema(
                name: "finance");

            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.EnsureSchema(
                name: "purchasing");

            migrationBuilder.EnsureSchema(
                name: "administration");

            migrationBuilder.EnsureSchema(
                name: "security");

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "g_l_account",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AccountType = table.Column<short>(type: "smallint", nullable: false),
                    IncomeBalance = table.Column<short>(type: "smallint", nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_g_l_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "item",
                schema: "inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BaseUnitOfMeasure = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "item_unit_of_measure",
                schema: "inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    QtyPerUnitOfMeasure = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_unit_of_measure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "location",
                schema: "inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_header",
                schema: "purchasing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BuyFromVendorNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PayToName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_header", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sales_header",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentType = table.Column<string>(type: "text", nullable: false),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    SellToCustomerNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BillToName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PostingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_header", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tenant",
                schema: "administration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "security",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendor",
                schema: "purchasing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    No = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Contact = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Blocked = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customer_tenant_id_No",
                schema: "sales",
                table: "customer",
                columns: new[] { "tenant_id", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_g_l_account_tenant_id_No",
                schema: "finance",
                table: "g_l_account",
                columns: new[] { "tenant_id", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_unit_of_measure_tenant_id_ItemNo_Code",
                schema: "inventory",
                table: "item_unit_of_measure",
                columns: new[] { "tenant_id", "ItemNo", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_tenant_id_Code",
                schema: "inventory",
                table: "location",
                columns: new[] { "tenant_id", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_tenant_id_DocumentType_No",
                schema: "purchasing",
                table: "purchase_header",
                columns: new[] { "tenant_id", "DocumentType", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_header_tenant_id_DocumentType_No",
                schema: "sales",
                table: "sales_header",
                columns: new[] { "tenant_id", "DocumentType", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vendor_tenant_id_No",
                schema: "purchasing",
                table: "vendor",
                columns: new[] { "tenant_id", "No" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "g_l_account",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "item",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "item_unit_of_measure",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "location",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "purchase_header",
                schema: "purchasing");

            migrationBuilder.DropTable(
                name: "sales_header",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "tenant",
                schema: "administration");

            migrationBuilder.DropTable(
                name: "user",
                schema: "security");

            migrationBuilder.DropTable(
                name: "vendor",
                schema: "purchasing");
        }
    }
}
