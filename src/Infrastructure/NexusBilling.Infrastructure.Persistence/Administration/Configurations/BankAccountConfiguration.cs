using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("bank_account", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.No).HasColumnName("no");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.SearchName).HasColumnName("search_name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.Contact).HasColumnName("contact");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.TelexNo).HasColumnName("telex_no");
        builder.Property(x => x.BankAccountNo).HasColumnName("bank_account_no");
        builder.Property(x => x.TransitNo).HasColumnName("transit_no");
        builder.Property(x => x.TerritoryCode).HasColumnName("territory_code");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.ChainName).HasColumnName("chain_name");
        builder.Property(x => x.MinBalance).HasColumnName("min_balance").HasPrecision(18, 5);
        builder.Property(x => x.BankAccPostingGroup).HasColumnName("bank_acc_posting_group");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.StatisticsGroup).HasColumnName("statistics_group");
        builder.Property(x => x.OurContactCode).HasColumnName("our_contact_code");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.Amount).HasColumnName("amount").HasPrecision(18, 5);
        builder.Property(x => x.Blocked).HasColumnName("blocked");
        builder.Property(x => x.LastStatementNo).HasColumnName("last_statement_no");
        builder.Property(x => x.LastPaymentStatementNo).HasColumnName("last_payment_statement_no");
        builder.Property(x => x.LastDateModified).HasColumnName("last_date_modified");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.TelexAnswerBack).HasColumnName("telex_answer_back");
        builder.Property(x => x.Picture).HasColumnName("picture");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.LastCheckNo).HasColumnName("last_check_no");
        builder.Property(x => x.BalanceLastStatement).HasColumnName("balance_last_statement").HasPrecision(18, 5);
        builder.Property(x => x.BankBranchNo).HasColumnName("bank_branch_no");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.HomePage).HasColumnName("home_page");
        builder.Property(x => x.NoSeries).HasColumnName("no_series");
        builder.Property(x => x.CheckReportId).HasColumnName("check_report_id");
        builder.Property(x => x.Iban).HasColumnName("iban");
        builder.Property(x => x.SwiftCode).HasColumnName("swift_code");
        builder.Property(x => x.BankStatementImportFormat).HasColumnName("bank_statement_import_format");
        builder.Property(x => x.CreditTransferMsgNos).HasColumnName("credit_transfer_msg_nos");
        builder.Property(x => x.DirectDebitMsgNos).HasColumnName("direct_debit_msg_nos");
        builder.Property(x => x.SepaDirectDebitExpFormat).HasColumnName("sepa_direct_debit_exp_format");
        builder.Property(x => x.BankStmtServiceRecordId).HasColumnName("bank_stmt_service_record_id");
        builder.Property(x => x.TransactionImportTimespan).HasColumnName("transaction_import_timespan");
        builder.Property(x => x.AutomaticStmtImportEnabled).HasColumnName("automatic_stmt_import_enabled");
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.CreditorNo).HasColumnName("creditor_no");
        builder.Property(x => x.PaymentExportFormat).HasColumnName("payment_export_format");
        builder.Property(x => x.BankClearingCode).HasColumnName("bank_clearing_code");
        builder.Property(x => x.BankClearingStandard).HasColumnName("bank_clearing_standard");
        builder.Property(x => x.BankNameDataConversion).HasColumnName("bank_name_data_conversion");
        builder.Property(x => x.MatchToleranceType).HasColumnName("match_tolerance_type");
        builder.Property(x => x.MatchToleranceValue).HasColumnName("match_tolerance_value").HasPrecision(18, 5);
        builder.Property(x => x.PositivePayExportCode).HasColumnName("positive_pay_export_code");
    }
}
