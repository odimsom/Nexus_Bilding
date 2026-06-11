using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ErrorMessageConfiguration : IEntityTypeConfiguration<ErrorMessage>
{
    public void Configure(EntityTypeBuilder<ErrorMessage> builder)
    {
        builder.ToTable("error_message", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.IdNav).HasColumnName("id_nav");
        builder.Property(x => x.RecordId).HasColumnName("record_id");
        builder.Property(x => x.FieldNumber).HasColumnName("field_number");
        builder.Property(x => x.MessageType).HasColumnName("message_type");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.AdditionalInformation).HasColumnName("additional_information");
        builder.Property(x => x.SupportUrl).HasColumnName("support_url");
        builder.Property(x => x.TableNumber).HasColumnName("table_number");
        builder.Property(x => x.ContextRecordId).HasColumnName("context_record_id");
    }
}
