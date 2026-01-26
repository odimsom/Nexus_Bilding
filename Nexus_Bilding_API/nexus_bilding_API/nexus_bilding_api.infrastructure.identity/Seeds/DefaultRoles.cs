using Microsoft.AspNetCore.Identity;
using nexus_bilding_api.core.domain.Enums;

namespace nexus_bilding_api.infrastructure.identity.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<Entities.AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(UserRole.Owner.ToString()));
        await roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(UserRole.Seller.ToString()));
    }
}
