using Microsoft.AspNetCore.Identity;

namespace Blocket.ViewModels;

public class UsersUpdateVm
{
    public IdentityUser? User { get; set; }
}