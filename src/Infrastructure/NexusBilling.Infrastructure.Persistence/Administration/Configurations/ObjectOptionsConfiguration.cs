using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class ObjectOptionsConfiguration : IEntityTypeConfiguration<ObjectOptions>
{
    public void Configure(EntityTypeBuilder<ObjectOptions> builder)
    {
        builder.ToTable("object_options", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.ParameterName).HasColumnName("parameter_name");
        builder.Property(x => x.ObjectId).HasColumnName("object_id");
        builder.Property(x => x.ObjectType).HasColumnName("object_type");
        builder.Property(x => x.CompanyName).HasColumnName("company_name");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.OptionData).HasColumnName("option_data");
        builder.Property(x => x.PublicVisible).HasColumnName("public_visible");
        builder.Property(x => x.Temporary).HasColumnName("temporary");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by");
    }
}
