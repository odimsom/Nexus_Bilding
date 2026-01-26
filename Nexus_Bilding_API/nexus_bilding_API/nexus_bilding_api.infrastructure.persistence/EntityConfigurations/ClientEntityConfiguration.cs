// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using nexus_bilding_api.core.domain.Entities;
//
// namespace nexus_bilding_api.infrastructure.persistence.EntityConfigurations;
//
// public class ClientEntityConfiguration : IEntityTypeConfiguration<Client>
// {
//     public void Configure(EntityTypeBuilder<Client> builder)
//     {
//         builder.ToTable("Clients");
//
//         builder.HasKey(c => c.Id);
//
//         builder.Property(c => c.Name)
//             .IsRequired()
//             .HasMaxLength(200);
//
//         builder.Property(c => c.Email)
//             .HasMaxLength(200);
//
//         builder.Property(c => c.Phone)
//             .HasMaxLength(20);
//
//         builder.Property(c => c.TaxId)
//             .IsRequired()
//             .HasMaxLength(20);
//
//         builder.Property(c => c.Address)
//             .HasMaxLength(500);
//
//         builder.Property(c => c.City)
//             .HasMaxLength(100);
//
//         builder.Property(c => c.Status)
//             .HasConversion<string>();
//
//         builder.Property(c => c.Notes)
//             .HasMaxLength(1000);
//
//         // Relationships
//         builder.HasMany(c => c.FiscalDocuments)
//             .WithOne(fd => fd.Client)
//             .HasForeignKey(fd => fd.ClientId)
//             .OnDelete(DeleteBehavior.Restrict);
//     }
// }