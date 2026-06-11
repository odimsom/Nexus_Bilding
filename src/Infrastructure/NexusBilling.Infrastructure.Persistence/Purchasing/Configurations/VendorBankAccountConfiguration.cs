using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Purchasing.Entities;

namespace NexusBilling.Infrastructure.Persistence.Purchasing.Configurations;

public class VendorBankAccountConfiguration : IEntityTypeConfiguration<VendorBankAccount>
{
    public void Configure(EntityTypeBuilder<VendorBankAccount> builder)
    {
        builder.ToTable("vendor_bank_account", "purchasing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.VendorNo).HasColumnName("vendor_no");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.Address2).HasColumnName("address_2");
        builder.Property(x => x.City).HasColumnName("city");
        builder.Property(x => x.PostCode).HasColumnName("post_code");
        builder.Property(x => x.Contact).HasColumnName("contact");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.TelexNo).HasColumnName("telex_no");
        builder.Property(x => x.BankBranchNo).HasColumnName("bank_branch_no");
        builder.Property(x => x.BankAccountNo).HasColumnName("bank_account_no");
        builder.Property(x => x.TransitNo).HasColumnName("transit_no");
        builder.Property(x => x.CurrencyCode).HasColumnName("currency_code");
        builder.Property(x => x.CountryRegionCode).HasColumnName("country_region_code");
        builder.Property(x => x.County).HasColumnName("county");
        builder.Property(x => x.FaxNo).HasColumnName("fax_no");
        builder.Property(x => x.TelexAnswerBack).HasColumnName("telex_answer_back");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.HomePage).HasColumnName("home_page");
        builder.Property(x => x.Iban).HasColumnName("iban");
        builder.Property(x => x.SwiftCode).HasColumnName("swift_code");
        builder.Property(x => x.BankClearingCode).HasColumnName("bank_clearing_code");
        builder.Property(x => x.BankClearingStandard).HasColumnName("bank_clearing_standard");
    }
}
