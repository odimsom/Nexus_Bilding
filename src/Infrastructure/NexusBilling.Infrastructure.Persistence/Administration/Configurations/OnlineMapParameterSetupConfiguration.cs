using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Administration.Entities;

namespace NexusBilling.Infrastructure.Persistence.Administration.Configurations;

public class OnlineMapParameterSetupConfiguration : IEntityTypeConfiguration<OnlineMapParameterSetup>
{
    public void Configure(EntityTypeBuilder<OnlineMapParameterSetup> builder)
    {
        builder.ToTable("online_map_parameter_setup", "administration");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TenantId).HasConversion(v => v.Value, v => TenantIdentifier.Create(v)).HasColumnName("tenant_id").IsRequired();
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.MapService).HasColumnName("map_service");
        builder.Property(x => x.DirectionsService).HasColumnName("directions_service");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.UrlEncodeNonAsciiChars).HasColumnName("url_encode_non_ascii_chars");
        builder.Property(x => x.MilesKilometersOptionList).HasColumnName("miles_kilometers_option_list");
        builder.Property(x => x.QuickestShortestOptionList).HasColumnName("quickest_shortest_option_list");
        builder.Property(x => x.DirectionsFromLocationServ).HasColumnName("directions_from_location_serv");
    }
}
