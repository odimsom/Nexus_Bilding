using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Finance.Entities;

namespace NexusBilling.Infrastructure.Persistence.Finance.Configurations;

public class CurrExchRateUpdateSetupConfiguration : IEntityTypeConfiguration<CurrExchRateUpdateSetup>
{
    public void Configure(EntityTypeBuilder<CurrExchRateUpdateSetup> builder)
    {
        builder.ToTable("curr_exch_rate_update_setup", "finance");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.WebServiceUrl).HasColumnName("web_service_url");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.ServiceProvider).HasColumnName("service_provider");
        builder.Property(x => x.TermsOfService).HasColumnName("terms_of_service");
        builder.Property(x => x.DataExchDefCode).HasColumnName("data_exch_def_code");
        builder.Property(x => x.LogWebRequests).HasColumnName("log_web_requests");
    }
}
