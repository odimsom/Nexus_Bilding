using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Sales.Entities;

namespace NexusBilling.Infrastructure.Persistence.Sales.Configurations;

public class SalespersonPurchaserConfiguration : IEntityTypeConfiguration<SalespersonPurchaser>
{
    public void Configure(EntityTypeBuilder<SalespersonPurchaser> builder)
    {
        builder.ToTable("salesperson_purchaser", "sales");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Commission).HasColumnName("commission").HasPrecision(18, 5);
        builder.Property(x => x.Image).HasColumnName("image");
        builder.Property(x => x.GlobalDimension1Code).HasColumnName("global_dimension_1_code");
        builder.Property(x => x.GlobalDimension2Code).HasColumnName("global_dimension_2_code");
        builder.Property(x => x.EMail).HasColumnName("e_mail");
        builder.Property(x => x.PhoneNo).HasColumnName("phone_no");
        builder.Property(x => x.JobTitle).HasColumnName("job_title");
        builder.Property(x => x.SearchEMail).HasColumnName("search_e_mail");
        builder.Property(x => x.EMail2).HasColumnName("e_mail_2");
    }
}
