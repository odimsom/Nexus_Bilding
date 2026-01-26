using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using nexus_bilding_api.infrastructure.identity.Entities;

namespace nexus_bilding_api.infrastructure.identity.Context;

public class IdentityContext : IdentityDbContext<AppUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable("Identity_Users");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.AvatarUrl).HasMaxLength(500);
        });

        builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>(entity =>
        {
            entity.ToTable("Identity_Roles");
        });

        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("Identity_UserRoles");
        });

        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("Identity_UserClaims");
        });

        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("Identity_UserLogins");
        });

        builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("Identity_RoleClaims");
        });

        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("Identity_UserTokens");
        });
    }
}
