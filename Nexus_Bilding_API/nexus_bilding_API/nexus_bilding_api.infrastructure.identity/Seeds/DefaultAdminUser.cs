using Microsoft.AspNetCore.Identity;
using nexus_bilding_api.core.domain.Enums;
using nexus_bilding_api.infrastructure.identity.Entities;

namespace nexus_bilding_api.infrastructure.identity.Seeds;

public static class DefaultAdminUser
{
    public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var defaultUser = new AppUser
        {
            UserName = "adminuser",
            Email = "admin@nexus.com",
            FirstName = "John",
            LastName = "Doe",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                defaultUser.IsActive = true;
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, UserRole.Admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, UserRole.Owner.ToString());
            }
            else
            {
                if (!user.IsActive)
                {
                    user.IsActive = true;
                    await userManager.UpdateAsync(user);
                }
            }
        }
    }
}
