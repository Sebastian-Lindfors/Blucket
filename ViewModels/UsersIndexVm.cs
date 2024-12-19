namespace Blocket.ViewModels;
using Blocket.Models;
using Microsoft.AspNetCore.Identity;

public class UsersIndexVm
{
    public List<IdentityUser> Users { get; set; } = [];
}