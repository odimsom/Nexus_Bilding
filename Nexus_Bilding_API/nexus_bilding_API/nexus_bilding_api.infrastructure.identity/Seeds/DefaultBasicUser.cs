using Microsoft.AspNetCore.Identity;
using nexus_bilding_api.core.domain.Enums;
using nexus_bilding_api.infrastructure.identity.Entities;

namespace nexus_bilding_api.infrastructure.identity.Seeds;

public static class DefaultBasicUser
{
    public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var defaultUser = new AppUser
        {
            UserName = "basicuser",
            Email = "basic@nexus.com",
            FirstName = "Jane",
            LastName = "Doe",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, UserRole.Seller.ToString());
            }
        }
    }
}
