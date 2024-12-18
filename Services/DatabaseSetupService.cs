namespace Blocket.Services;
using Microsoft.AspNetCore.Identity;
using Blocket.Models;

public class DatabaseSetupService(
    RoleManager<IdentityRole> roleManager,
    UserManager<IdentityUser> userManager)
{
    public async Task InitializeAsync()
    {
        // Create Administrator role if it doesn't exist.
        if (!await roleManager.RoleExistsAsync(RoleConstants.Administrator))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(RoleConstants.Administrator));
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create the administrator role.");
            }
        }

        // Create admin user if one doesn't exist.
        var adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create admin user.");
            }
            await userManager.AddToRoleAsync(adminUser, RoleConstants.Administrator);
        }
    }
}