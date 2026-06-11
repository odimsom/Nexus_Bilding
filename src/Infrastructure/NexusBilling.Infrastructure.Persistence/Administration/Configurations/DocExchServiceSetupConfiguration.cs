using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DocExchServiceSetupConfiguration : IEntityTypeConfiguration<DocExchServiceSetup>
{
    public void Configure(EntityTypeBuilder<DocExchServiceSetup> builder)
    {
        builder.ToTable("doc_exch_service_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.PrimaryKey).HasColumnName("primary_key");
        builder.Property(x => x.SignUpUrl).HasColumnName("sign_up_url");
        builder.Property(x => x.ServiceUrl).HasColumnName("service_url");
        builder.Property(x => x.SignInUrl).HasColumnName("sign_in_url");
        builder.Property(x => x.ConsumerKey).HasColumnName("consumer_key");
        builder.Property(x => x.ConsumerSecret).HasColumnName("consumer_secret");
        builder.Property(x => x.Token).HasColumnName("token");
        builder.Property(x => x.TokenSecret).HasColumnName("token_secret");
        builder.Property(x => x.DocExchTenantId).HasColumnName("doc_exch_tenant_id");
        builder.Property(x => x.UserAgent).HasColumnName("user_agent");
        builder.Property(x => x.Enabled).HasColumnName("enabled");
        builder.Property(x => x.LogWebRequests).HasColumnName("log_web_requests");
    }
}
