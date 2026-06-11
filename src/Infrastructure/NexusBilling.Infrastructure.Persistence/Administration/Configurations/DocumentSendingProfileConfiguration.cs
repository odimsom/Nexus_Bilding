using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class DocumentSendingProfileConfiguration : IEntityTypeConfiguration<DocumentSendingProfile>
{
    public void Configure(EntityTypeBuilder<DocumentSendingProfile> builder)
    {
        builder.ToTable("document_sending_profile", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Printer).HasColumnName("printer");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.EMailAttachment).HasColumnName("e_mail_attachment");
        builder.Property(x => x.EMailFormat).HasColumnName("e_mail_format");
        builder.Property(x => x.Disk).HasColumnName("disk");
        builder.Property(x => x.DiskFormat).HasColumnName("disk_format");
        builder.Property(x => x.ElectronicDocument).HasColumnName("electronic_document");
        builder.Property(x => x.ElectronicFormat).HasColumnName("electronic_format");
        builder.Property(x => x.Default).HasColumnName("default");
        builder.Property(x => x.SendTo).HasColumnName("send_to");
        builder.Property(x => x.Usage).HasColumnName("usage");
        builder.Property(x => x.OneRelatedPartySelected).HasColumnName("one_related_party_selected");
    }
}
