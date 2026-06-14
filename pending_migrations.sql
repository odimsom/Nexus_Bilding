START TRANSACTION;
ALTER TABLE sales.sales_invoice_header DROP COLUMN "Amount";

ALTER TABLE sales.sales_invoice_header DROP COLUMN "AmountIncludingVat";

ALTER TABLE purchasing.vendor ADD currency_code text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD email text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD payment_terms_code text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD phone_no text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD rnc text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260613192225_AddVendorExtendedFields', '10.0.9');

COMMIT;

START TRANSACTION;
ALTER TABLE purchasing.vendor ADD address_2 text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD country text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD credit_limit numeric NOT NULL DEFAULT 0.0;

ALTER TABLE purchasing.vendor ADD payment_method_code text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD phone_no_2 text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD province text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD vendor_type text NOT NULL DEFAULT '';

ALTER TABLE purchasing.vendor ADD web_site text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260613194650_AddVendorFullFields', '10.0.9');

COMMIT;

START TRANSACTION;
ALTER TABLE sales.sales_line ADD hourly_rate numeric;

ALTER TABLE sales.sales_line ADD resource_no text DEFAULT '';

ALTER TABLE sales.sales_line ADD service_billing_type smallint;

ALTER TABLE sales.sales_line ADD service_end_date timestamp without time zone;

ALTER TABLE sales.sales_line ADD service_hours numeric;

ALTER TABLE sales.sales_line ADD service_start_date timestamp without time zone;

ALTER TABLE sales.sales_header ADD observations text DEFAULT '';

ALTER TABLE sales.sales_header ADD quoted_by text DEFAULT '';

ALTER TABLE sales.sales_header ADD valid_until_date timestamp without time zone;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260614025539_AddQuotationFields', '10.0.9');

COMMIT;

START TRANSACTION;
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260614044959_AddMissingSalesHeaderFields', '10.0.9');

COMMIT;

