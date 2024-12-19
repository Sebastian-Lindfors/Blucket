using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blocket.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Blocket.ViewModels;

namespace Blocket.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var articles = await _context.Articles.Where(a => a.UserId == userId).ToListAsync();

        var vm = new HomeIndexVm { Articles = articles };

        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
