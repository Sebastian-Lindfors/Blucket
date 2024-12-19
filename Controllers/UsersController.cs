namespace Blocket.Controllers;

using Blocket.Models;
using Microsoft.AspNetCore.Mvc;
using Blocket.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class UserController(
    ApplicationDbContext context,
    UserManager<IdentityUser> userManager) : Controller
{


    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Index()
    {
        var users = await context.Users.ToListAsync();

        var vm = new UsersIndexVm { Users = users };
        return View(vm);
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Create(UsersCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (string.IsNullOrWhiteSpace(vm.Email) || string.IsNullOrWhiteSpace(vm.PlainPassword))
        {
            return View(vm);
        }

        var user = new IdentityUser
        {
            UserName = vm.Email,
            Email = vm.Email
        };

        var existingUser = await userManager.FindByEmailAsync(user.Email);
        if (existingUser == null)
        {
            var result = await userManager.CreateAsync(user, vm.PlainPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Could not create user");
            }
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }


    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Delete(User user)
    {
        var usr = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (usr == null)
        {
            return NotFound();
        }

        var vm = new UsersDeleteVm
        {
            User = usr
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.Administrator)]
    public async Task<IActionResult> Delete(string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        context.Remove(user);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}