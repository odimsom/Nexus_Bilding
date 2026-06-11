using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BankExportImportSetupConfiguration : IEntityTypeConfiguration<BankExportImportSetup>
{
    public void Configure(EntityTypeBuilder<BankExportImportSetup> builder)
    {
        builder.ToTable("bank_export_import_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Direction).HasColumnName("direction");
        builder.Property(x => x.ProcessingCodeunitId).HasColumnName("processing_codeunit_id");
        builder.Property(x => x.ProcessingXmlportId).HasColumnName("processing_xmlport_id");
        builder.Property(x => x.DataExchDefCode).HasColumnName("data_exch_def_code");
        builder.Property(x => x.PreserveNonLatinCharacters).HasColumnName("preserve_non_latin_characters");
        builder.Property(x => x.CheckExportCodeunit).HasColumnName("check_export_codeunit");
    }
}
