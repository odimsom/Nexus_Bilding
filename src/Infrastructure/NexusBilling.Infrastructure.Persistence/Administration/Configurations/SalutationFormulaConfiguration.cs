using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class SalutationFormulaConfiguration : IEntityTypeConfiguration<SalutationFormula>
{
    public void Configure(EntityTypeBuilder<SalutationFormula> builder)
    {
        builder.ToTable("salutation_formula", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.SalutationCode).HasColumnName("salutation_code");
        builder.Property(x => x.LanguageCode).HasColumnName("language_code");
        builder.Property(x => x.SalutationType).HasColumnName("salutation_type");
        builder.Property(x => x.Salutation).HasColumnName("salutation");
        builder.Property(x => x.Name1).HasColumnName("name_1");
        builder.Property(x => x.Name2).HasColumnName("name_2");
        builder.Property(x => x.Name3).HasColumnName("name_3");
        builder.Property(x => x.Name4).HasColumnName("name_4");
        builder.Property(x => x.Name5).HasColumnName("name_5");
    }
}
