namespace Blocket.Services;

using System.Security.Claims;
using Blocket.Models;
using Microsoft.AspNetCore.Identity;

public class UserService(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
{
    public async Task<bool> IsCurrentUserAdministratorAsync()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return false;
        }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        return await userManager.IsInRoleAsync(user, RoleConstants.Administrator);
    }

    public async Task<bool> IsUserAsync()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
        {
            return false;
        }

        return await Task.FromResult(user?.Identity?.IsAuthenticated ?? false);
    }
}