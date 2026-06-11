using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusBilling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MassiveSchemaSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_vendor_tenant_id_No",
                schema: "purchasing",
                table: "vendor");

            migrationBuilder.DropIndex(
                name: "IX_sales_header_tenant_id_DocumentType_No",
                schema: "sales",
                table: "sales_header");

            migrationBuilder.DropIndex(
                name: "IX_purchase_header_tenant_id_DocumentType_No",
                schema: "purchasing",
                table: "purchase_header");

            migrationBuilder.DropIndex(
                name: "IX_location_tenant_id_Code",
                schema: "inventory",
                table: "location");

            migrationBuilder.DropIndex(
                name: "IX_item_unit_of_measure_tenant_id_ItemNo_Code",
                schema: "inventory",
                table: "item_unit_of_measure");

            migrationBuilder.DropIndex(
                name: "IX_customer_tenant_id_No",
                schema: "sales",
                table: "customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_g_l_account",
                schema: "finance",
                table: "g_l_account");

            migrationBuilder.DropIndex(
                name: "IX_g_l_account_tenant_id_No",
                schema: "finance",
                table: "g_l_account");

            migrationBuilder.RenameTable(
                name: "g_l_account",
                schema: "finance",
                newName: "gl_account",
                newSchema: "finance");

            migrationBuilder.RenameColumn(
                name: "No",
                schema: "purchasing",
                table: "vendor",
                newName: "no");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "purchasing",
                table: "vendor",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Contact",
                schema: "purchasing",
                table: "vendor",
                newName: "contact");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "purchasing",
                table: "vendor",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Blocked",
                schema: "purchasing",
                table: "vendor",
                newName: "blocked");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "purchasing",
                table: "vendor",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "security",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "security",
                table: "user",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "security",
                table: "user",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "security",
                table: "user",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "administration",
                table: "tenant",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "administration",
                table: "tenant",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "No",
                schema: "sales",
                table: "sales_header",
                newName: "no");

            migrationBuilder.RenameColumn(
                name: "SellToCustomerNo",
                schema: "sales",
                table: "sales_header",
                newName: "sell_to_customer_no");

            migrationBuilder.RenameColumn(
                name: "PostingDate",
                schema: "sales",
                table: "sales_header",
                newName: "posting_date");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                schema: "sales",
                table: "sales_header",
                newName: "document_type");

            migrationBuilder.RenameColumn(
                name: "BillToName",
                schema: "sales",
                table: "sales_header",
                newName: "bill_to_name");

            migrationBuilder.RenameColumn(
                name: "No",
                schema: "purchasing",
                table: "purchase_header",
                newName: "no");

            migrationBuilder.RenameColumn(
                name: "PostingDate",
                schema: "purchasing",
                table: "purchase_header",
                newName: "posting_date");

            migrationBuilder.RenameColumn(
                name: "PayToName",
                schema: "purchasing",
                table: "purchase_header",
                newName: "pay_to_name");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                schema: "purchasing",
                table: "purchase_header",
                newName: "document_type");

            migrationBuilder.RenameColumn(
                name: "BuyFromVendorNo",
                schema: "purchasing",
                table: "purchase_header",
                newName: "buy_from_vendor_no");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "inventory",
                table: "location",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "inventory",
                table: "location",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "inventory",
                table: "location",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "inventory",
                table: "location",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "inventory",
                table: "item_unit_of_measure",
                newName: "code");

            migrationBuilder.RenameColumn(
                name: "QtyPerUnitOfMeasure",
                schema: "inventory",
                table: "item_unit_of_measure",
                newName: "qty_per_unit_of_measure");

            migrationBuilder.RenameColumn(
                name: "ItemNo",
                schema: "inventory",
                table: "item_unit_of_measure",
                newName: "item_no");

            migrationBuilder.RenameColumn(
                name: "No",
                schema: "inventory",
                table: "item",
                newName: "no");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "inventory",
                table: "item",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Blocked",
                schema: "inventory",
                table: "item",
                newName: "blocked");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "inventory",
                table: "item",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "UnitCost",
                schema: "inventory",
                table: "item",
                newName: "unit_cost");

            migrationBuilder.RenameColumn(
                name: "BaseUnitOfMeasure",
                schema: "inventory",
                table: "item",
                newName: "base_unit_of_measure");

            migrationBuilder.RenameColumn(
                name: "No",
                schema: "sales",
                table: "customer",
                newName: "no");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "sales",
                table: "customer",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Contact",
                schema: "sales",
                table: "customer",
                newName: "contact");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "sales",
                table: "customer",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Blocked",
                schema: "sales",
                table: "customer",
                newName: "blocked");

            migrationBuilder.RenameColumn(
                name: "Address",
                schema: "sales",
                table: "customer",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "No",
                schema: "finance",
                table: "gl_account",
                newName: "no");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "finance",
                table: "gl_account",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Blocked",
                schema: "finance",
                table: "gl_account",
                newName: "blocked");

            migrationBuilder.RenameColumn(
                name: "IncomeBalance",
                schema: "finance",
                table: "gl_account",
                newName: "income_balance");

            migrationBuilder.RenameColumn(
                name: "AccountType",
                schema: "finance",
                table: "gl_account",
                newName: "account_type");

            migrationBuilder.AlterColumn<string>(
                name: "no",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "contact",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "city",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                schema: "purchasing",
                table: "vendor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "security",
                table: "user",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                schema: "security",
                table: "user",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "administration",
                table: "tenant",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "no",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "sell_to_customer_no",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "bill_to_name",
                schema: "sales",
                table: "sales_header",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "no",
                schema: "purchasing",
                table: "purchase_header",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "pay_to_name",
                schema: "purchasing",
                table: "purchase_header",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "buy_from_vendor_no",
                schema: "purchasing",
                table: "purchase_header",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "inventory",
                table: "location",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "inventory",
                table: "location",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "city",
                schema: "inventory",
                table: "location",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                schema: "inventory",
                table: "location",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "inventory",
                table: "item_unit_of_measure",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "qty_per_unit_of_measure",
                schema: "inventory",
                table: "item_unit_of_measure",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldPrecision: 18,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "item_no",
                schema: "inventory",
                table: "item_unit_of_measure",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "no",
                schema: "inventory",
                table: "item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                schema: "inventory",
                table: "item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                schema: "inventory",
                table: "item",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldPrecision: 18,
                oldScale: 5);

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_cost",
                schema: "inventory",
                table: "item",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,5)",
                oldPrecision: 18,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "base_unit_of_measure",
                schema: "inventory",
                table: "item",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "no",
                schema: "sales",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "sales",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "contact",
                schema: "sales",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "city",
                schema: "sales",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                schema: "sales",
                table: "customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "no",
                schema: "finance",
                table: "gl_account",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "finance",
                table: "gl_account",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_gl_account",
                schema: "finance",
                table: "gl_account",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "accounting_period",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    starting_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    new_fiscal_year = table.Column<bool>(type: "boolean", nullable: false),
                    closed = table.Column<bool>(type: "boolean", nullable: false),
                    date_locked = table.Column<bool>(type: "boolean", nullable: false),
                    average_cost_calc_type = table.Column<short>(type: "smallint", nullable: false),
                    average_cost_period = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounting_period", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "business_chart_user_setup",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    object_type = table.Column<short>(type: "smallint", nullable: false),
                    object_id = table.Column<int>(type: "integer", nullable: false),
                    period_length = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_chart_user_setup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "business_unit",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    consolidate = table.Column<bool>(type: "boolean", nullable: false),
                    consolidation = table.Column<decimal>(type: "numeric", nullable: false),
                    starting_date = table.Column<string>(type: "text", nullable: true),
                    ending_date = table.Column<string>(type: "text", nullable: true),
                    income_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    exch_rate_losses_acc = table.Column<string>(type: "text", nullable: false),
                    exch_rate_gains_acc = table.Column<string>(type: "text", nullable: false),
                    residual_account = table.Column<string>(type: "text", nullable: false),
                    last_balance_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    company_name = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    comp_exch_rate_gains_acc = table.Column<string>(type: "text", nullable: false),
                    comp_exch_rate_losses_acc = table.Column<string>(type: "text", nullable: false),
                    equity_exch_rate_gains_acc = table.Column<string>(type: "text", nullable: false),
                    equity_exch_rate_losses_acc = table.Column<string>(type: "text", nullable: false),
                    minority_exch_rate_gains_acc = table.Column<string>(type: "text", nullable: false),
                    minority_exch_rate_losses_acc = table.Column<string>(type: "text", nullable: false),
                    currency_exchange_rate_table = table.Column<short>(type: "smallint", nullable: false),
                    data_source = table.Column<short>(type: "smallint", nullable: false),
                    file_format = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cust_ledger_entry",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_no = table.Column<int>(type: "integer", nullable: false),
                    customer_no = table.Column<string>(type: "text", nullable: true),
                    posting_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    sales_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    profit_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_discount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    sell_to_customer_no = table.Column<string>(type: "text", nullable: true),
                    customer_posting_group = table.Column<string>(type: "text", nullable: false),
                    global_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    global_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    salesperson_code = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    on_hold = table.Column<string>(type: "text", nullable: false),
                    applies_to_doc_type = table.Column<short>(type: "smallint", nullable: false),
                    applies_to_doc_no = table.Column<string>(type: "text", nullable: false),
                    open = table.Column<bool>(type: "boolean", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    pmt_discount_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    original_pmt_disc_possible = table.Column<decimal>(type: "numeric", nullable: false),
                    pmt_disc_given_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    positive = table.Column<bool>(type: "boolean", nullable: false),
                    closed_by_entry_no = table.Column<int>(type: "integer", nullable: false),
                    closed_at_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    closed_by_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    applies_to_id = table.Column<string>(type: "text", nullable: false),
                    journal_batch_name = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    transaction_no = table.Column<int>(type: "integer", nullable: false),
                    closed_by_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    document_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    external_document_no = table.Column<string>(type: "text", nullable: false),
                    calculate_interest = table.Column<bool>(type: "boolean", nullable: false),
                    closing_interest_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    no_series = table.Column<string>(type: "text", nullable: false),
                    closed_by_currency_code = table.Column<string>(type: "text", nullable: true),
                    closed_by_currency_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    adjusted_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    original_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    remaining_pmt_disc_possible = table.Column<decimal>(type: "numeric", nullable: false),
                    pmt_disc_tolerance_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    max_payment_tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    last_issued_reminder_level = table.Column<int>(type: "integer", nullable: false),
                    accepted_payment_tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    accepted_pmt_disc_tolerance = table.Column<bool>(type: "boolean", nullable: false),
                    pmt_tolerance_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_to_apply = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    applying_entry = table.Column<bool>(type: "boolean", nullable: false),
                    reversed = table.Column<bool>(type: "boolean", nullable: false),
                    reversed_by_entry_no = table.Column<int>(type: "integer", nullable: false),
                    reversed_entry_no = table.Column<int>(type: "integer", nullable: false),
                    prepayment = table.Column<bool>(type: "boolean", nullable: false),
                    payment_method_code = table.Column<string>(type: "text", nullable: false),
                    applies_to_ext_doc_no = table.Column<string>(type: "text", nullable: false),
                    recipient_bank_account = table.Column<string>(type: "text", nullable: false),
                    message_to_recipient = table.Column<string>(type: "text", nullable: false),
                    exported_to_payment_file = table.Column<bool>(type: "boolean", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    direct_debit_mandate_id = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cust_ledger_entry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gen_business_posting_group",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    def_vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    auto_insert_default = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_business_posting_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gen_journal_batch",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    journal_template_name = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    no_series = table.Column<string>(type: "text", nullable: false),
                    posting_no_series = table.Column<string>(type: "text", nullable: false),
                    copy_vat_setup_to_jnl_lines = table.Column<bool>(type: "boolean", nullable: false),
                    allow_vat_difference = table.Column<bool>(type: "boolean", nullable: false),
                    allow_payment_export = table.Column<bool>(type: "boolean", nullable: false),
                    bank_statement_import_format = table.Column<string>(type: "text", nullable: false),
                    suggest_balancing_amount = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_journal_batch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gen_journal_line",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    journal_template_name = table.Column<string>(type: "text", nullable: false),
                    line_no = table.Column<int>(type: "integer", nullable: false),
                    account_type = table.Column<short>(type: "smallint", nullable: false),
                    account_no = table.Column<string>(type: "text", nullable: false),
                    posting_date = table.Column<string>(type: "text", nullable: true),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    vat = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    debit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    credit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    sales_purch_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    profit_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_discount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    bill_to_pay_to_no = table.Column<string>(type: "text", nullable: false),
                    posting_group = table.Column<string>(type: "text", nullable: false),
                    shortcut_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    shortcut_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    salespers_purch_code = table.Column<string>(type: "text", nullable: true),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    system_created_entry = table.Column<bool>(type: "boolean", nullable: false),
                    on_hold = table.Column<string>(type: "text", nullable: false),
                    applies_to_doc_type = table.Column<short>(type: "smallint", nullable: false),
                    applies_to_doc_no = table.Column<string>(type: "text", nullable: false),
                    due_date = table.Column<string>(type: "text", nullable: true),
                    pmt_discount_date = table.Column<string>(type: "text", nullable: true),
                    payment_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_no = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_posting = table.Column<short>(type: "smallint", nullable: false),
                    payment_terms_code = table.Column<string>(type: "text", nullable: false),
                    applies_to_id = table.Column<string>(type: "text", nullable: false),
                    business_unit_code = table.Column<string>(type: "text", nullable: false),
                    journal_batch_name = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    recurring_method = table.Column<short>(type: "smallint", nullable: false),
                    expiration_date = table.Column<string>(type: "text", nullable: true),
                    recurring_frequency = table.Column<string>(type: "text", nullable: false),
                    gen_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_calculation_type = table.Column<short>(type: "smallint", nullable: false),
                    eu3_party_trade = table.Column<bool>(type: "boolean", nullable: false),
                    allow_application = table.Column<bool>(type: "boolean", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_gen_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_vat_calculation_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    bank_payment_type = table.Column<short>(type: "smallint", nullable: false),
                    vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    correction = table.Column<bool>(type: "boolean", nullable: false),
                    check_printed = table.Column<bool>(type: "boolean", nullable: false),
                    document_date = table.Column<string>(type: "text", nullable: true),
                    external_document_no = table.Column<string>(type: "text", nullable: false),
                    source_type = table.Column<short>(type: "smallint", nullable: false),
                    source_no = table.Column<string>(type: "text", nullable: false),
                    posting_no_series = table.Column<string>(type: "text", nullable: false),
                    tax_area_code = table.Column<string>(type: "text", nullable: false),
                    tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    tax_group_code = table.Column<string>(type: "text", nullable: false),
                    use_tax = table.Column<bool>(type: "boolean", nullable: false),
                    bal_tax_area_code = table.Column<string>(type: "text", nullable: false),
                    bal_tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    bal_tax_group_code = table.Column<string>(type: "text", nullable: false),
                    bal_use_tax = table.Column<bool>(type: "boolean", nullable: false),
                    vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    additional_currency_posting = table.Column<short>(type: "smallint", nullable: false),
                    fa_add_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    source_currency_code = table.Column<string>(type: "text", nullable: true),
                    source_currency_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    source_curr_vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    source_curr_vat_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_base_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_base_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_base_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    reversing_entry = table.Column<bool>(type: "boolean", nullable: false),
                    allow_zero_amount_posting = table.Column<bool>(type: "boolean", nullable: false),
                    ship_to_order_address_code = table.Column<string>(type: "text", nullable: false),
                    vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    ic_direction = table.Column<short>(type: "smallint", nullable: false),
                    ic_partner_gl_acc_no = table.Column<string>(type: "text", nullable: false),
                    ic_partner_transaction_no = table.Column<int>(type: "integer", nullable: false),
                    sell_to_buy_from_no = table.Column<string>(type: "text", nullable: false),
                    vat_registration_no = table.Column<string>(type: "text", nullable: false),
                    country_region_code = table.Column<string>(type: "text", nullable: true),
                    prepayment = table.Column<bool>(type: "boolean", nullable: false),
                    financial_void = table.Column<bool>(type: "boolean", nullable: false),
                    incoming_document_entry_no = table.Column<int>(type: "integer", nullable: false),
                    creditor_no = table.Column<string>(type: "text", nullable: false),
                    payment_reference = table.Column<string>(type: "text", nullable: false),
                    payment_method_code = table.Column<string>(type: "text", nullable: false),
                    applies_to_ext_doc_no = table.Column<string>(type: "text", nullable: false),
                    recipient_bank_account = table.Column<string>(type: "text", nullable: false),
                    message_to_recipient = table.Column<string>(type: "text", nullable: false),
                    exported_to_payment_file = table.Column<bool>(type: "boolean", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    credit_card_no = table.Column<string>(type: "text", nullable: false),
                    job_task_no = table.Column<string>(type: "text", nullable: false),
                    job_unit_price_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_total_price_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    job_unit_cost_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_disc_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_unit_of_measure_code = table.Column<string>(type: "text", nullable: false),
                    job_line_type = table.Column<short>(type: "smallint", nullable: false),
                    job_unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    job_total_price = table.Column<decimal>(type: "numeric", nullable: false),
                    job_unit_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    job_total_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_total_cost_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    job_currency_code = table.Column<string>(type: "text", nullable: false),
                    job_planning_line_no = table.Column<int>(type: "integer", nullable: false),
                    job_remaining_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    direct_debit_mandate_id = table.Column<string>(type: "text", nullable: false),
                    data_exch_entry_no = table.Column<int>(type: "integer", nullable: false),
                    payer_information = table.Column<string>(type: "text", nullable: false),
                    transaction_information = table.Column<string>(type: "text", nullable: false),
                    data_exch_line_no = table.Column<int>(type: "integer", nullable: false),
                    applied_automatically = table.Column<bool>(type: "boolean", nullable: false),
                    deferral_code = table.Column<string>(type: "text", nullable: false),
                    deferral_line_no = table.Column<int>(type: "integer", nullable: false),
                    campaign_no = table.Column<string>(type: "text", nullable: true),
                    prod_order_no = table.Column<string>(type: "text", nullable: false),
                    fa_posting_date = table.Column<string>(type: "text", nullable: true),
                    fa_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    depreciation_book_code = table.Column<string>(type: "text", nullable: false),
                    salvage_value = table.Column<decimal>(type: "numeric", nullable: false),
                    no_of_depreciation_days = table.Column<int>(type: "integer", nullable: false),
                    depr_until_fa_posting_date = table.Column<bool>(type: "boolean", nullable: false),
                    depr_acquisition_cost = table.Column<bool>(type: "boolean", nullable: false),
                    maintenance_code = table.Column<string>(type: "text", nullable: true),
                    insurance_no = table.Column<string>(type: "text", nullable: true),
                    budgeted_fa_no = table.Column<string>(type: "text", nullable: false),
                    duplicate_in_depreciation_book = table.Column<string>(type: "text", nullable: false),
                    use_duplication_list = table.Column<bool>(type: "boolean", nullable: false),
                    fa_reclassification_entry = table.Column<bool>(type: "boolean", nullable: false),
                    fa_error_entry_no = table.Column<int>(type: "integer", nullable: false),
                    index_entry = table.Column<bool>(type: "boolean", nullable: false),
                    source_line_no = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_journal_line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gen_journal_template",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    test_report_id = table.Column<int>(type: "integer", nullable: false),
                    page_id = table.Column<int>(type: "integer", nullable: false),
                    posting_report_id = table.Column<int>(type: "integer", nullable: false),
                    force_posting_report = table.Column<bool>(type: "boolean", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    recurring = table.Column<bool>(type: "boolean", nullable: false),
                    force_doc_balance = table.Column<bool>(type: "boolean", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    no_series = table.Column<string>(type: "text", nullable: false),
                    posting_no_series = table.Column<string>(type: "text", nullable: false),
                    copy_vat_setup_to_jnl_lines = table.Column<bool>(type: "boolean", nullable: false),
                    allow_vat_difference = table.Column<bool>(type: "boolean", nullable: false),
                    cust_receipt_report_id = table.Column<int>(type: "integer", nullable: false),
                    vendor_receipt_report_id = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_journal_template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gen_product_posting_group",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    def_vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    auto_insert_default = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gen_product_posting_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "general_ledger_setup",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    primary_key = table.Column<string>(type: "text", nullable: false),
                    allow_posting_from = table.Column<string>(type: "text", nullable: true),
                    allow_posting_to = table.Column<string>(type: "text", nullable: true),
                    register_time = table.Column<bool>(type: "boolean", nullable: false),
                    pmt_disc_excl_vat = table.Column<bool>(type: "boolean", nullable: false),
                    unrealized_vat = table.Column<bool>(type: "boolean", nullable: false),
                    adjust_for_payment_disc = table.Column<bool>(type: "boolean", nullable: false),
                    mark_cr_memos_as_corrections = table.Column<bool>(type: "boolean", nullable: false),
                    local_address_format = table.Column<short>(type: "smallint", nullable: false),
                    inv_rounding_precision_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_rounding_type_lcy = table.Column<short>(type: "smallint", nullable: false),
                    local_cont_addr_format = table.Column<short>(type: "smallint", nullable: false),
                    bank_account_nos = table.Column<string>(type: "text", nullable: false),
                    summarize_gl_entries = table.Column<bool>(type: "boolean", nullable: false),
                    amount_decimal_places = table.Column<string>(type: "text", nullable: false),
                    unit_amount_decimal_places = table.Column<string>(type: "text", nullable: false),
                    additional_reporting_currency = table.Column<string>(type: "text", nullable: true),
                    vat_tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    emu_currency = table.Column<bool>(type: "boolean", nullable: false),
                    lcy_code = table.Column<string>(type: "text", nullable: false),
                    vat_exchange_rate_adjustment = table.Column<short>(type: "smallint", nullable: false),
                    amount_rounding_precision = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_amount_rounding_precision = table.Column<decimal>(type: "numeric", nullable: false),
                    appln_rounding_precision = table.Column<decimal>(type: "numeric", nullable: false),
                    global_dimension1_code = table.Column<string>(type: "text", nullable: true),
                    global_dimension2_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension1_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension2_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension3_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension4_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension5_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension6_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension7_code = table.Column<string>(type: "text", nullable: true),
                    shortcut_dimension8_code = table.Column<string>(type: "text", nullable: true),
                    max_vat_difference_allowed = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_rounding_type = table.Column<short>(type: "smallint", nullable: false),
                    pmt_disc_tolerance_posting = table.Column<short>(type: "smallint", nullable: false),
                    payment_discount_grace_period = table.Column<string>(type: "text", nullable: false),
                    payment_tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    max_payment_tolerance_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    adapt_main_menu_to_permissions = table.Column<bool>(type: "boolean", nullable: false),
                    allow_gl_acc_deletion_before = table.Column<string>(type: "text", nullable: true),
                    check_gl_account_usage = table.Column<bool>(type: "boolean", nullable: false),
                    payment_tolerance_posting = table.Column<short>(type: "smallint", nullable: false),
                    pmt_disc_tolerance_warning = table.Column<bool>(type: "boolean", nullable: false),
                    payment_tolerance_warning = table.Column<bool>(type: "boolean", nullable: false),
                    last_ic_transaction_no = table.Column<int>(type: "integer", nullable: false),
                    bill_to_sell_to_vat_calc = table.Column<short>(type: "smallint", nullable: false),
                    acc_sched_for_balance_sheet = table.Column<string>(type: "text", nullable: false),
                    acc_sched_for_income_stmt = table.Column<string>(type: "text", nullable: false),
                    acc_sched_for_cash_flow_stmt = table.Column<string>(type: "text", nullable: false),
                    acc_sched_for_retained_earn = table.Column<string>(type: "text", nullable: false),
                    print_vat_specification_in_lcy = table.Column<bool>(type: "boolean", nullable: false),
                    prepayment_unrealized_vat = table.Column<bool>(type: "boolean", nullable: false),
                    use_legacy_gl_entry_locking = table.Column<bool>(type: "boolean", nullable: false),
                    payroll_trans_import_format = table.Column<string>(type: "text", nullable: false),
                    vat_reg_no_validation_url = table.Column<string>(type: "text", nullable: false),
                    local_currency_symbol = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_ledger_setup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "general_posting_setup",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    sales_account = table.Column<string>(type: "text", nullable: false),
                    sales_line_disc_account = table.Column<string>(type: "text", nullable: false),
                    sales_inv_disc_account = table.Column<string>(type: "text", nullable: false),
                    sales_pmt_disc_debit_acc = table.Column<string>(type: "text", nullable: false),
                    purch_account = table.Column<string>(type: "text", nullable: false),
                    purch_line_disc_account = table.Column<string>(type: "text", nullable: false),
                    purch_inv_disc_account = table.Column<string>(type: "text", nullable: false),
                    purch_pmt_disc_credit_acc = table.Column<string>(type: "text", nullable: false),
                    cogs_account = table.Column<string>(type: "text", nullable: false),
                    inventory_adjmt_account = table.Column<string>(type: "text", nullable: false),
                    sales_credit_memo_account = table.Column<string>(type: "text", nullable: false),
                    purch_credit_memo_account = table.Column<string>(type: "text", nullable: false),
                    sales_pmt_disc_credit_acc = table.Column<string>(type: "text", nullable: false),
                    purch_pmt_disc_debit_acc = table.Column<string>(type: "text", nullable: false),
                    sales_pmt_tol_debit_acc = table.Column<string>(type: "text", nullable: false),
                    sales_pmt_tol_credit_acc = table.Column<string>(type: "text", nullable: false),
                    purch_pmt_tol_debit_acc = table.Column<string>(type: "text", nullable: false),
                    purch_pmt_tol_credit_acc = table.Column<string>(type: "text", nullable: false),
                    sales_prepayments_account = table.Column<string>(type: "text", nullable: false),
                    purch_prepayments_account = table.Column<string>(type: "text", nullable: false),
                    purch_fa_disc_account = table.Column<string>(type: "text", nullable: false),
                    invt_accrual_acc_interim = table.Column<string>(type: "text", nullable: false),
                    cogs_account_interim = table.Column<string>(type: "text", nullable: false),
                    direct_cost_applied_account = table.Column<string>(type: "text", nullable: false),
                    overhead_applied_account = table.Column<string>(type: "text", nullable: false),
                    purchase_variance_account = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_general_posting_setup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gl_account_category",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_no = table.Column<int>(type: "integer", nullable: false),
                    parent_entry_no = table.Column<int>(type: "integer", nullable: false),
                    sibling_sequence_no = table.Column<int>(type: "integer", nullable: false),
                    presentation_order = table.Column<string>(type: "text", nullable: false),
                    indentation = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    account_category = table.Column<short>(type: "smallint", nullable: false),
                    income_balance = table.Column<short>(type: "smallint", nullable: false),
                    additional_report_definition = table.Column<short>(type: "smallint", nullable: false),
                    system_generated = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_account_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gl_budget_name",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    blocked = table.Column<bool>(type: "boolean", nullable: false),
                    budget_dimension1_code = table.Column<string>(type: "text", nullable: true),
                    budget_dimension2_code = table.Column<string>(type: "text", nullable: true),
                    budget_dimension3_code = table.Column<string>(type: "text", nullable: true),
                    budget_dimension4_code = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_budget_name", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gl_entry",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_no = table.Column<int>(type: "integer", nullable: false),
                    gl_account_no = table.Column<string>(type: "text", nullable: false),
                    posting_date = table.Column<string>(type: "text", nullable: true),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    global_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    global_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    system_created_entry = table.Column<bool>(type: "boolean", nullable: false),
                    prior_year_entry = table.Column<bool>(type: "boolean", nullable: false),
                    job_no = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    business_unit_code = table.Column<string>(type: "text", nullable: false),
                    journal_batch_name = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    gen_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    transaction_no = table.Column<int>(type: "integer", nullable: false),
                    debit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    credit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    document_date = table.Column<string>(type: "text", nullable: true),
                    external_document_no = table.Column<string>(type: "text", nullable: false),
                    source_type = table.Column<short>(type: "smallint", nullable: false),
                    source_no = table.Column<string>(type: "text", nullable: false),
                    no_series = table.Column<string>(type: "text", nullable: false),
                    tax_area_code = table.Column<string>(type: "text", nullable: false),
                    tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    tax_group_code = table.Column<string>(type: "text", nullable: false),
                    use_tax = table.Column<bool>(type: "boolean", nullable: false),
                    vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    additional_currency_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    add_currency_debit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    add_currency_credit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    close_income_statement_dim_id = table.Column<int>(type: "integer", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    reversed = table.Column<bool>(type: "boolean", nullable: false),
                    reversed_by_entry_no = table.Column<int>(type: "integer", nullable: false),
                    reversed_entry_no = table.Column<int>(type: "integer", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    prod_order_no = table.Column<string>(type: "text", nullable: false),
                    fa_entry_type = table.Column<short>(type: "smallint", nullable: false),
                    fa_entry_no = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_entry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "gl_register",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    no = table.Column<int>(type: "integer", nullable: false),
                    from_entry_no = table.Column<int>(type: "integer", nullable: false),
                    to_entry_no = table.Column<int>(type: "integer", nullable: false),
                    creation_date = table.Column<string>(type: "text", nullable: true),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    journal_batch_name = table.Column<string>(type: "text", nullable: false),
                    from_vat_entry_no = table.Column<int>(type: "integer", nullable: false),
                    to_vat_entry_no = table.Column<int>(type: "integer", nullable: false),
                    reversed = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gl_register", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "item_ledger_entry",
                schema: "inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_no = table.Column<int>(type: "integer", nullable: false),
                    item_no = table.Column<string>(type: "text", nullable: true),
                    posting_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    entry_type = table.Column<short>(type: "smallint", nullable: false),
                    source_no = table.Column<string>(type: "text", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    location_code = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    remaining_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    invoiced_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    applies_to_entry = table.Column<int>(type: "integer", nullable: false),
                    open = table.Column<bool>(type: "boolean", nullable: false),
                    global_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    global_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    positive = table.Column<bool>(type: "boolean", nullable: false),
                    source_type = table.Column<short>(type: "smallint", nullable: false),
                    drop_shipment = table.Column<bool>(type: "boolean", nullable: false),
                    transaction_type = table.Column<string>(type: "text", nullable: false),
                    transport_method = table.Column<string>(type: "text", nullable: false),
                    country_region_code = table.Column<string>(type: "text", nullable: true),
                    entry_exit_point = table.Column<string>(type: "text", nullable: false),
                    document_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    external_document_no = table.Column<string>(type: "text", nullable: false),
                    area = table.Column<string>(type: "text", nullable: true),
                    transaction_specification = table.Column<string>(type: "text", nullable: false),
                    no_series = table.Column<string>(type: "text", nullable: false),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    document_line_no = table.Column<int>(type: "integer", nullable: false),
                    order_type = table.Column<short>(type: "smallint", nullable: false),
                    order_no = table.Column<string>(type: "text", nullable: false),
                    order_line_no = table.Column<int>(type: "integer", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    assemble_to_order = table.Column<bool>(type: "boolean", nullable: false),
                    job_no = table.Column<string>(type: "text", nullable: true),
                    job_task_no = table.Column<string>(type: "text", nullable: false),
                    job_purchase = table.Column<bool>(type: "boolean", nullable: false),
                    variant_code = table.Column<string>(type: "text", nullable: false),
                    qty_per_unit_of_measure = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_of_measure_code = table.Column<string>(type: "text", nullable: false),
                    derived_from_blanket_order = table.Column<bool>(type: "boolean", nullable: false),
                    cross_reference_no = table.Column<string>(type: "text", nullable: false),
                    originally_ordered_no = table.Column<string>(type: "text", nullable: true),
                    originally_ordered_var_code = table.Column<string>(type: "text", nullable: false),
                    out_of_stock_substitution = table.Column<bool>(type: "boolean", nullable: false),
                    item_category_code = table.Column<string>(type: "text", nullable: false),
                    nonstock = table.Column<bool>(type: "boolean", nullable: false),
                    purchasing_code = table.Column<string>(type: "text", nullable: true),
                    product_group_code = table.Column<string>(type: "text", nullable: false),
                    completely_invoiced = table.Column<bool>(type: "boolean", nullable: false),
                    last_invoice_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    applied_entry_to_adjust = table.Column<bool>(type: "boolean", nullable: false),
                    correction = table.Column<bool>(type: "boolean", nullable: false),
                    shipped_qty_not_returned = table.Column<decimal>(type: "numeric", nullable: false),
                    prod_order_comp_line_no = table.Column<int>(type: "integer", nullable: false),
                    serial_no = table.Column<string>(type: "text", nullable: false),
                    lot_no = table.Column<string>(type: "text", nullable: false),
                    warranty_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    item_tracking = table.Column<short>(type: "smallint", nullable: false),
                    return_reason_code = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_ledger_entry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "my_account",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    account_no = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_my_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "purchase_line",
                schema: "purchasing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    buy_from_vendor_no = table.Column<string>(type: "text", nullable: true),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    line_no = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    no = table.Column<string>(type: "text", nullable: false),
                    location_code = table.Column<string>(type: "text", nullable: false),
                    posting_group = table.Column<string>(type: "text", nullable: false),
                    expected_receipt_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    description2 = table.Column<string>(type: "text", nullable: false),
                    unit_of_measure = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    outstanding_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_to_invoice = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_to_receive = table.Column<decimal>(type: "numeric", nullable: false),
                    direct_unit_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_cost_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    vat = table.Column<decimal>(type: "numeric", nullable: false),
                    line_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    line_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_including_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_price_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    allow_invoice_disc = table.Column<bool>(type: "boolean", nullable: false),
                    gross_weight = table.Column<decimal>(type: "numeric", nullable: false),
                    net_weight = table.Column<decimal>(type: "numeric", nullable: false),
                    units_per_parcel = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_volume = table.Column<decimal>(type: "numeric", nullable: false),
                    appl_to_item_entry = table.Column<int>(type: "integer", nullable: false),
                    shortcut_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    shortcut_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    job_no = table.Column<string>(type: "text", nullable: true),
                    indirect_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    recalculate_invoice_disc = table.Column<bool>(type: "boolean", nullable: false),
                    outstanding_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_rcd_not_invoiced = table.Column<decimal>(type: "numeric", nullable: false),
                    amt_rcd_not_invoiced = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity_received = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity_invoiced = table.Column<decimal>(type: "numeric", nullable: false),
                    receipt_no = table.Column<string>(type: "text", nullable: false),
                    receipt_line_no = table.Column<int>(type: "integer", nullable: false),
                    profit = table.Column<decimal>(type: "numeric", nullable: false),
                    pay_to_vendor_no = table.Column<string>(type: "text", nullable: true),
                    inv_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    vendor_item_no = table.Column<string>(type: "text", nullable: false),
                    sales_order_no = table.Column<string>(type: "text", nullable: false),
                    sales_order_line_no = table.Column<int>(type: "integer", nullable: false),
                    drop_shipment = table.Column<bool>(type: "boolean", nullable: false),
                    gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_calculation_type = table.Column<short>(type: "smallint", nullable: false),
                    transaction_type = table.Column<string>(type: "text", nullable: false),
                    transport_method = table.Column<string>(type: "text", nullable: false),
                    attached_to_line_no = table.Column<int>(type: "integer", nullable: false),
                    entry_point = table.Column<string>(type: "text", nullable: false),
                    area = table.Column<string>(type: "text", nullable: true),
                    transaction_specification = table.Column<string>(type: "text", nullable: false),
                    tax_area_code = table.Column<string>(type: "text", nullable: false),
                    tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    tax_group_code = table.Column<string>(type: "text", nullable: false),
                    use_tax = table.Column<bool>(type: "boolean", nullable: false),
                    vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    outstanding_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    amt_rcd_not_invoiced_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    blanket_order_no = table.Column<string>(type: "text", nullable: false),
                    blanket_order_line_no = table.Column<int>(type: "integer", nullable: false),
                    vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    system_created_entry = table.Column<bool>(type: "boolean", nullable: false),
                    line_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_disc_amount_to_invoice = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_identifier = table.Column<string>(type: "text", nullable: false),
                    ic_partner_ref_type = table.Column<short>(type: "smallint", nullable: false),
                    ic_partner_reference = table.Column<string>(type: "text", nullable: false),
                    prepayment = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_line_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amt_inv = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amt_incl_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_vat_base_amt = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_vat_calc_type = table.Column<short>(type: "smallint", nullable: false),
                    prepayment_vat_identifier = table.Column<string>(type: "text", nullable: false),
                    prepayment_tax_area_code = table.Column<string>(type: "text", nullable: false),
                    prepayment_tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    prepayment_tax_group_code = table.Column<string>(type: "text", nullable: false),
                    prepmt_amt_to_deduct = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amt_deducted = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_line = table.Column<bool>(type: "boolean", nullable: false),
                    prepmt_amount_inv_incl_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amount_inv_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    prepmt_vat_amount_inv_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_vat_diff_to_deduct = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_vat_diff_deducted = table.Column<decimal>(type: "numeric", nullable: false),
                    outstanding_amt_ex_vat_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    a_rcd_not_inv_ex_vat_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    job_task_no = table.Column<string>(type: "text", nullable: false),
                    job_line_type = table.Column<short>(type: "smallint", nullable: false),
                    job_unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    job_total_price = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_unit_price_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_total_price_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_line_disc_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    job_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    job_currency_code = table.Column<string>(type: "text", nullable: false),
                    job_planning_line_no = table.Column<int>(type: "integer", nullable: false),
                    job_remaining_qty = table.Column<decimal>(type: "numeric", nullable: false),
                    job_remaining_qty_base = table.Column<decimal>(type: "numeric", nullable: false),
                    deferral_code = table.Column<string>(type: "text", nullable: false),
                    returns_deferral_start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    prod_order_no = table.Column<string>(type: "text", nullable: false),
                    variant_code = table.Column<string>(type: "text", nullable: false),
                    bin_code = table.Column<string>(type: "text", nullable: false),
                    qty_per_unit_of_measure = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_of_measure_code = table.Column<string>(type: "text", nullable: false),
                    quantity_base = table.Column<decimal>(type: "numeric", nullable: false),
                    outstanding_qty_base = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_to_invoice_base = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_to_receive_base = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_rcd_not_invoiced_base = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_received_base = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_invoiced_base = table.Column<decimal>(type: "numeric", nullable: false),
                    fa_posting_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fa_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    depreciation_book_code = table.Column<string>(type: "text", nullable: false),
                    salvage_value = table.Column<decimal>(type: "numeric", nullable: false),
                    depr_until_fa_posting_date = table.Column<bool>(type: "boolean", nullable: false),
                    depr_acquisition_cost = table.Column<bool>(type: "boolean", nullable: false),
                    maintenance_code = table.Column<string>(type: "text", nullable: true),
                    insurance_no = table.Column<string>(type: "text", nullable: true),
                    budgeted_fa_no = table.Column<string>(type: "text", nullable: false),
                    duplicate_in_depreciation_book = table.Column<string>(type: "text", nullable: false),
                    use_duplication_list = table.Column<bool>(type: "boolean", nullable: false),
                    responsibility_center = table.Column<string>(type: "text", nullable: false),
                    cross_reference_no = table.Column<string>(type: "text", nullable: false),
                    unit_of_measure_cross_ref = table.Column<string>(type: "text", nullable: false),
                    cross_reference_type = table.Column<short>(type: "smallint", nullable: false),
                    cross_reference_type_no = table.Column<string>(type: "text", nullable: false),
                    item_category_code = table.Column<string>(type: "text", nullable: false),
                    nonstock = table.Column<bool>(type: "boolean", nullable: false),
                    purchasing_code = table.Column<string>(type: "text", nullable: true),
                    product_group_code = table.Column<string>(type: "text", nullable: false),
                    special_order = table.Column<bool>(type: "boolean", nullable: false),
                    special_order_sales_no = table.Column<string>(type: "text", nullable: false),
                    special_order_sales_line_no = table.Column<int>(type: "integer", nullable: false),
                    completely_received = table.Column<bool>(type: "boolean", nullable: false),
                    requested_receipt_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    promised_receipt_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    lead_time_calculation = table.Column<string>(type: "text", nullable: false),
                    inbound_whse_handling_time = table.Column<string>(type: "text", nullable: false),
                    planned_receipt_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    allow_item_charge_assignment = table.Column<bool>(type: "boolean", nullable: false),
                    return_qty_to_ship = table.Column<decimal>(type: "numeric", nullable: false),
                    return_qty_to_ship_base = table.Column<decimal>(type: "numeric", nullable: false),
                    return_qty_shipped_not_invd = table.Column<decimal>(type: "numeric", nullable: false),
                    ret_qty_shpd_not_invd_base = table.Column<decimal>(type: "numeric", nullable: false),
                    return_shpd_not_invd = table.Column<decimal>(type: "numeric", nullable: false),
                    return_shpd_not_invd_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    return_qty_shipped = table.Column<decimal>(type: "numeric", nullable: false),
                    return_qty_shipped_base = table.Column<decimal>(type: "numeric", nullable: false),
                    return_shipment_no = table.Column<string>(type: "text", nullable: false),
                    return_shipment_line_no = table.Column<int>(type: "integer", nullable: false),
                    return_reason_code = table.Column<string>(type: "text", nullable: false),
                    routing_no = table.Column<string>(type: "text", nullable: false),
                    operation_no = table.Column<string>(type: "text", nullable: false),
                    work_center_no = table.Column<string>(type: "text", nullable: false),
                    finished = table.Column<bool>(type: "boolean", nullable: false),
                    prod_order_line_no = table.Column<int>(type: "integer", nullable: false),
                    overhead_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    mps_order = table.Column<bool>(type: "boolean", nullable: false),
                    planning_flexibility = table.Column<short>(type: "smallint", nullable: false),
                    safety_lead_time = table.Column<string>(type: "text", nullable: false),
                    routing_reference_no = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rounding_method",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    minimum_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_added_before = table.Column<decimal>(type: "numeric", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    precision = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_added_after = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rounding_method", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sales_line",
                schema: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    sell_to_customer_no = table.Column<string>(type: "text", nullable: true),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    line_no = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    no = table.Column<string>(type: "text", nullable: false),
                    location_code = table.Column<string>(type: "text", nullable: false),
                    posting_group = table.Column<string>(type: "text", nullable: false),
                    shipment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    description2 = table.Column<string>(type: "text", nullable: false),
                    unit_of_measure = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    outstanding_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_to_invoice = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_to_ship = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_cost_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    vat = table.Column<decimal>(type: "numeric", nullable: false),
                    line_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    line_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_including_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    allow_invoice_disc = table.Column<bool>(type: "boolean", nullable: false),
                    gross_weight = table.Column<decimal>(type: "numeric", nullable: false),
                    net_weight = table.Column<decimal>(type: "numeric", nullable: false),
                    units_per_parcel = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_volume = table.Column<decimal>(type: "numeric", nullable: false),
                    appl_to_item_entry = table.Column<int>(type: "integer", nullable: false),
                    shortcut_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    shortcut_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    customer_price_group = table.Column<string>(type: "text", nullable: false),
                    job_no = table.Column<string>(type: "text", nullable: true),
                    work_type_code = table.Column<string>(type: "text", nullable: false),
                    recalculate_invoice_disc = table.Column<bool>(type: "boolean", nullable: false),
                    outstanding_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    qty_shipped_not_invoiced = table.Column<decimal>(type: "numeric", nullable: false),
                    shipped_not_invoiced = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity_shipped = table.Column<decimal>(type: "numeric", nullable: false),
                    quantity_invoiced = table.Column<decimal>(type: "numeric", nullable: false),
                    shipment_no = table.Column<string>(type: "text", nullable: false),
                    shipment_line_no = table.Column<int>(type: "integer", nullable: false),
                    profit = table.Column<decimal>(type: "numeric", nullable: false),
                    bill_to_customer_no = table.Column<string>(type: "text", nullable: true),
                    inv_discount_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    purchase_order_no = table.Column<string>(type: "text", nullable: false),
                    purch_order_line_no = table.Column<int>(type: "integer", nullable: false),
                    drop_shipment = table.Column<bool>(type: "boolean", nullable: false),
                    gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_calculation_type = table.Column<short>(type: "smallint", nullable: false),
                    transaction_type = table.Column<string>(type: "text", nullable: false),
                    transport_method = table.Column<string>(type: "text", nullable: false),
                    attached_to_line_no = table.Column<int>(type: "integer", nullable: false),
                    exit_point = table.Column<string>(type: "text", nullable: false),
                    area = table.Column<string>(type: "text", nullable: true),
                    transaction_specification = table.Column<string>(type: "text", nullable: false),
                    tax_category = table.Column<string>(type: "text", nullable: false),
                    tax_area_code = table.Column<string>(type: "text", nullable: false),
                    tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    tax_group_code = table.Column<string>(type: "text", nullable: false),
                    vat_clause_code = table.Column<string>(type: "text", nullable: false),
                    vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    outstanding_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    shipped_not_invoiced_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    reserve = table.Column<short>(type: "smallint", nullable: false),
                    blanket_order_no = table.Column<string>(type: "text", nullable: false),
                    blanket_order_line_no = table.Column<int>(type: "integer", nullable: false),
                    vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    unit_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    system_created_entry = table.Column<bool>(type: "boolean", nullable: false),
                    line_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_disc_amount_to_invoice = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_identifier = table.Column<string>(type: "text", nullable: false),
                    ic_partner_ref_type = table.Column<short>(type: "smallint", nullable: false),
                    ic_partner_reference = table.Column<string>(type: "text", nullable: false),
                    prepayment = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_line_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amt_inv = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amt_incl_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_vat_base_amt = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_vat_calc_type = table.Column<short>(type: "smallint", nullable: false),
                    prepayment_vat_identifier = table.Column<string>(type: "text", nullable: false),
                    prepayment_tax_area_code = table.Column<string>(type: "text", nullable: false),
                    prepayment_tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    prepayment_tax_group_code = table.Column<string>(type: "text", nullable: false),
                    prepmt_amt_to_deduct = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amt_deducted = table.Column<decimal>(type: "numeric", nullable: false),
                    prepayment_line = table.Column<bool>(type: "boolean", nullable: false),
                    prepmt_amount_inv_incl_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    prepmt_amount_inv_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    prepmt_vat_amount_inv_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "standard_general_journal",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    journal_template_name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_standard_general_journal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "standard_general_journal_line",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    journal_template_name = table.Column<string>(type: "text", nullable: false),
                    line_no = table.Column<int>(type: "integer", nullable: false),
                    account_type = table.Column<short>(type: "smallint", nullable: false),
                    account_no = table.Column<string>(type: "text", nullable: false),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    vat = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    debit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    credit_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    balance_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    sales_purch_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    profit_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_discount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    bill_to_pay_to_no = table.Column<string>(type: "text", nullable: false),
                    posting_group = table.Column<string>(type: "text", nullable: false),
                    shortcut_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    shortcut_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    salespers_purch_code = table.Column<string>(type: "text", nullable: true),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    on_hold = table.Column<string>(type: "text", nullable: false),
                    applies_to_doc_type = table.Column<short>(type: "smallint", nullable: false),
                    payment_discount = table.Column<decimal>(type: "numeric", nullable: false),
                    job_no = table.Column<string>(type: "text", nullable: true),
                    quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    vat_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_terms_code = table.Column<string>(type: "text", nullable: false),
                    business_unit_code = table.Column<string>(type: "text", nullable: false),
                    standard_journal_code = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    gen_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_calculation_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_gen_posting_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_gen_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_gen_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_vat_calculation_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_vat = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    bank_payment_type = table.Column<short>(type: "smallint", nullable: false),
                    vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_base_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    correction = table.Column<bool>(type: "boolean", nullable: false),
                    source_type = table.Column<short>(type: "smallint", nullable: false),
                    source_no = table.Column<string>(type: "text", nullable: false),
                    posting_no_series = table.Column<string>(type: "text", nullable: false),
                    tax_area_code = table.Column<string>(type: "text", nullable: false),
                    tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    tax_group_code = table.Column<string>(type: "text", nullable: false),
                    use_tax = table.Column<bool>(type: "boolean", nullable: false),
                    bal_tax_area_code = table.Column<string>(type: "text", nullable: false),
                    bal_tax_liable = table.Column<bool>(type: "boolean", nullable: false),
                    bal_tax_group_code = table.Column<string>(type: "text", nullable: false),
                    bal_use_tax = table.Column<bool>(type: "boolean", nullable: false),
                    vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_vat_bus_posting_group = table.Column<string>(type: "text", nullable: false),
                    bal_vat_prod_posting_group = table.Column<string>(type: "text", nullable: false),
                    ship_to_order_address_code = table.Column<string>(type: "text", nullable: false),
                    vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    bal_vat_difference = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    ic_partner_gl_acc_no = table.Column<string>(type: "text", nullable: false),
                    sell_to_buy_from_no = table.Column<string>(type: "text", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    campaign_no = table.Column<string>(type: "text", nullable: true),
                    index_entry = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_standard_general_journal_line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trial_balance_setup",
                schema: "finance",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    primary_key = table.Column<string>(type: "text", nullable: false),
                    account_schedule_name = table.Column<string>(type: "text", nullable: false),
                    column_layout_name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trial_balance_setup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vendor_ledger_entry",
                schema: "purchasing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    entry_no = table.Column<int>(type: "integer", nullable: false),
                    vendor_no = table.Column<string>(type: "text", nullable: true),
                    posting_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    document_type = table.Column<short>(type: "smallint", nullable: false),
                    document_no = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: true),
                    purchase_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    inv_discount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    buy_from_vendor_no = table.Column<string>(type: "text", nullable: true),
                    vendor_posting_group = table.Column<string>(type: "text", nullable: false),
                    global_dimension1_code = table.Column<string>(type: "text", nullable: false),
                    global_dimension2_code = table.Column<string>(type: "text", nullable: false),
                    purchaser_code = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    source_code = table.Column<string>(type: "text", nullable: false),
                    on_hold = table.Column<string>(type: "text", nullable: false),
                    applies_to_doc_type = table.Column<short>(type: "smallint", nullable: false),
                    applies_to_doc_no = table.Column<string>(type: "text", nullable: false),
                    open = table.Column<bool>(type: "boolean", nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    pmt_discount_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    original_pmt_disc_possible = table.Column<decimal>(type: "numeric", nullable: false),
                    pmt_disc_rcd_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    positive = table.Column<bool>(type: "boolean", nullable: false),
                    closed_by_entry_no = table.Column<int>(type: "integer", nullable: false),
                    closed_at_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    closed_by_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    applies_to_id = table.Column<string>(type: "text", nullable: false),
                    journal_batch_name = table.Column<string>(type: "text", nullable: false),
                    reason_code = table.Column<string>(type: "text", nullable: false),
                    bal_account_type = table.Column<short>(type: "smallint", nullable: false),
                    bal_account_no = table.Column<string>(type: "text", nullable: false),
                    transaction_no = table.Column<int>(type: "integer", nullable: false),
                    closed_by_amount_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    document_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    external_document_no = table.Column<string>(type: "text", nullable: false),
                    no_series = table.Column<string>(type: "text", nullable: false),
                    closed_by_currency_code = table.Column<string>(type: "text", nullable: true),
                    closed_by_currency_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    adjusted_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    original_currency_factor = table.Column<decimal>(type: "numeric", nullable: false),
                    remaining_pmt_disc_possible = table.Column<decimal>(type: "numeric", nullable: false),
                    pmt_disc_tolerance_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    max_payment_tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    accepted_payment_tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    accepted_pmt_disc_tolerance = table.Column<bool>(type: "boolean", nullable: false),
                    pmt_tolerance_lcy = table.Column<decimal>(type: "numeric", nullable: false),
                    amount_to_apply = table.Column<decimal>(type: "numeric", nullable: false),
                    ic_partner_code = table.Column<string>(type: "text", nullable: false),
                    applying_entry = table.Column<bool>(type: "boolean", nullable: false),
                    reversed = table.Column<bool>(type: "boolean", nullable: false),
                    reversed_by_entry_no = table.Column<int>(type: "integer", nullable: false),
                    reversed_entry_no = table.Column<int>(type: "integer", nullable: false),
                    prepayment = table.Column<bool>(type: "boolean", nullable: false),
                    creditor_no = table.Column<string>(type: "text", nullable: false),
                    payment_reference = table.Column<string>(type: "text", nullable: false),
                    payment_method_code = table.Column<string>(type: "text", nullable: false),
                    applies_to_ext_doc_no = table.Column<string>(type: "text", nullable: false),
                    recipient_bank_account = table.Column<string>(type: "text", nullable: false),
                    message_to_recipient = table.Column<string>(type: "text", nullable: false),
                    exported_to_payment_file = table.Column<bool>(type: "boolean", nullable: false),
                    dimension_set_id = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor_ledger_entry", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounting_period",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "business_chart_user_setup",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "business_unit",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "cust_ledger_entry",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "gen_business_posting_group",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gen_journal_batch",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gen_journal_line",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gen_journal_template",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gen_product_posting_group",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "general_ledger_setup",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "general_posting_setup",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gl_account_category",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gl_budget_name",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gl_entry",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "gl_register",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "item_ledger_entry",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "my_account",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "purchase_line",
                schema: "purchasing");

            migrationBuilder.DropTable(
                name: "rounding_method",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "sales_line",
                schema: "sales");

            migrationBuilder.DropTable(
                name: "standard_general_journal",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "standard_general_journal_line",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "trial_balance_setup",
                schema: "finance");

            migrationBuilder.DropTable(
                name: "vendor_ledger_entry",
                schema: "purchasing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gl_account",
                schema: "finance",
                table: "gl_account");

            migrationBuilder.RenameTable(
                name: "gl_account",
                schema: "finance",
                newName: "g_l_account",
                newSchema: "finance");

            migrationBuilder.RenameColumn(
                name: "no",
                schema: "purchasing",
                table: "vendor",
                newName: "No");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "purchasing",
                table: "vendor",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "contact",
                schema: "purchasing",
                table: "vendor",
                newName: "Contact");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "purchasing",
                table: "vendor",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "blocked",
                schema: "purchasing",
                table: "vendor",
                newName: "Blocked");

            migrationBuilder.RenameColumn(
                name: "address",
                schema: "purchasing",
                table: "vendor",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "security",
                table: "user",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "security",
                table: "user",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "security",
                table: "user",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "security",
                table: "user",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "administration",
                table: "tenant",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "administration",
                table: "tenant",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "no",
                schema: "sales",
                table: "sales_header",
                newName: "No");

            migrationBuilder.RenameColumn(
                name: "sell_to_customer_no",
                schema: "sales",
                table: "sales_header",
                newName: "SellToCustomerNo");

            migrationBuilder.RenameColumn(
                name: "posting_date",
                schema: "sales",
                table: "sales_header",
                newName: "PostingDate");

            migrationBuilder.RenameColumn(
                name: "document_type",
                schema: "sales",
                table: "sales_header",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "bill_to_name",
                schema: "sales",
                table: "sales_header",
                newName: "BillToName");

            migrationBuilder.RenameColumn(
                name: "no",
                schema: "purchasing",
                table: "purchase_header",
                newName: "No");

            migrationBuilder.RenameColumn(
                name: "posting_date",
                schema: "purchasing",
                table: "purchase_header",
                newName: "PostingDate");

            migrationBuilder.RenameColumn(
                name: "pay_to_name",
                schema: "purchasing",
                table: "purchase_header",
                newName: "PayToName");

            migrationBuilder.RenameColumn(
                name: "document_type",
                schema: "purchasing",
                table: "purchase_header",
                newName: "DocumentType");

            migrationBuilder.RenameColumn(
                name: "buy_from_vendor_no",
                schema: "purchasing",
                table: "purchase_header",
                newName: "BuyFromVendorNo");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "inventory",
                table: "location",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "inventory",
                table: "location",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "inventory",
                table: "location",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "address",
                schema: "inventory",
                table: "location",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "code",
                schema: "inventory",
                table: "item_unit_of_measure",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "qty_per_unit_of_measure",
                schema: "inventory",
                table: "item_unit_of_measure",
                newName: "QtyPerUnitOfMeasure");

            migrationBuilder.RenameColumn(
                name: "item_no",
                schema: "inventory",
                table: "item_unit_of_measure",
                newName: "ItemNo");

            migrationBuilder.RenameColumn(
                name: "no",
                schema: "inventory",
                table: "item",
                newName: "No");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "inventory",
                table: "item",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "blocked",
                schema: "inventory",
                table: "item",
                newName: "Blocked");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                schema: "inventory",
                table: "item",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "unit_cost",
                schema: "inventory",
                table: "item",
                newName: "UnitCost");

            migrationBuilder.RenameColumn(
                name: "base_unit_of_measure",
                schema: "inventory",
                table: "item",
                newName: "BaseUnitOfMeasure");

            migrationBuilder.RenameColumn(
                name: "no",
                schema: "sales",
                table: "customer",
                newName: "No");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "sales",
                table: "customer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "contact",
                schema: "sales",
                table: "customer",
                newName: "Contact");

            migrationBuilder.RenameColumn(
                name: "city",
                schema: "sales",
                table: "customer",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "blocked",
                schema: "sales",
                table: "customer",
                newName: "Blocked");

            migrationBuilder.RenameColumn(
                name: "address",
                schema: "sales",
                table: "customer",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "no",
                schema: "finance",
                table: "g_l_account",
                newName: "No");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "finance",
                table: "g_l_account",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "blocked",
                schema: "finance",
                table: "g_l_account",
                newName: "Blocked");

            migrationBuilder.RenameColumn(
                name: "income_balance",
                schema: "finance",
                table: "g_l_account",
                newName: "IncomeBalance");

            migrationBuilder.RenameColumn(
                name: "account_type",
                schema: "finance",
                table: "g_l_account",
                newName: "AccountType");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                schema: "purchasing",
                table: "vendor",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "purchasing",
                table: "vendor",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                schema: "purchasing",
                table: "vendor",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "purchasing",
                table: "vendor",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "purchasing",
                table: "vendor",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "security",
                table: "user",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "security",
                table: "user",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "administration",
                table: "tenant",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                schema: "sales",
                table: "sales_header",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SellToCustomerNo",
                schema: "sales",
                table: "sales_header",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BillToName",
                schema: "sales",
                table: "sales_header",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                schema: "purchasing",
                table: "purchase_header",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PayToName",
                schema: "purchasing",
                table: "purchase_header",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BuyFromVendorNo",
                schema: "purchasing",
                table: "purchase_header",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "inventory",
                table: "location",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "inventory",
                table: "location",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "inventory",
                table: "location",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "inventory",
                table: "location",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "inventory",
                table: "item_unit_of_measure",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "QtyPerUnitOfMeasure",
                schema: "inventory",
                table: "item_unit_of_measure",
                type: "numeric(18,5)",
                precision: 18,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "ItemNo",
                schema: "inventory",
                table: "item_unit_of_measure",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                schema: "inventory",
                table: "item",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "inventory",
                table: "item",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                schema: "inventory",
                table: "item",
                type: "numeric(18,5)",
                precision: 18,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                schema: "inventory",
                table: "item",
                type: "numeric(18,5)",
                precision: 18,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "BaseUnitOfMeasure",
                schema: "inventory",
                table: "item",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                schema: "sales",
                table: "customer",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "sales",
                table: "customer",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Contact",
                schema: "sales",
                table: "customer",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "sales",
                table: "customer",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "sales",
                table: "customer",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "No",
                schema: "finance",
                table: "g_l_account",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "finance",
                table: "g_l_account",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_g_l_account",
                schema: "finance",
                table: "g_l_account",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_vendor_tenant_id_No",
                schema: "purchasing",
                table: "vendor",
                columns: new[] { "tenant_id", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_header_tenant_id_DocumentType_No",
                schema: "sales",
                table: "sales_header",
                columns: new[] { "tenant_id", "DocumentType", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_purchase_header_tenant_id_DocumentType_No",
                schema: "purchasing",
                table: "purchase_header",
                columns: new[] { "tenant_id", "DocumentType", "No" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_tenant_id_Code",
                schema: "inventory",
                table: "location",
                columns: new[] { "tenant_id", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_unit_of_measure_tenant_id_ItemNo_Code",
                schema: "inventory",
                table: "item_unit_of_measure",
                columns: new[] { "tenant_id", "ItemNo", "Code" },
                unique: true);

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
        }
    }
}
