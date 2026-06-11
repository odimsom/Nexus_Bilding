using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class BankPmtApplRuleConfiguration : IEntityTypeConfiguration<BankPmtApplRule>
{
    public void Configure(EntityTypeBuilder<BankPmtApplRule> builder)
    {
        builder.ToTable("bank_pmt_appl_rule", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.MatchConfidence).HasColumnName("match_confidence");
        builder.Property(x => x.Priority).HasColumnName("priority");
        builder.Property(x => x.RelatedPartyMatched).HasColumnName("related_party_matched");
        builder.Property(x => x.DocNoExtDocNoMatched).HasColumnName("doc_no_ext_doc_no_matched");
        builder.Property(x => x.AmountInclToleranceMatched).HasColumnName("amount_incl_tolerance_matched");
        builder.Property(x => x.Score).HasColumnName("score");
    }
}
