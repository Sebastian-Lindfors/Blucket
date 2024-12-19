namespace Blocket.Controllers;

using Blocket.Models;
using Microsoft.AspNetCore.Mvc;
using Blocket.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class ArticleController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var articles = await context.Articles.ToListAsync();

        var vm = new ArticleIndexVm { Articles = articles };
        return View(vm);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ArticleCreateVm vm)
    {
        vm.Article.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (ModelState.IsValid)
        {
            if (vm.Article.Published.HasValue)
            {
                vm.Article.Published = vm.Article.Published.Value.ToUniversalTime();
            }

            context.Add(vm.Article);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }

    public IActionResult Edit(Article article)
    {
        if (article == null)
        {
            return NotFound();
        }

        var vm = new ArticleEditVm
        {
            Article = article
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ArticleEditVm vm)
    {
        var article = vm.Article;

        if (ModelState.IsValid)
        {
            if (!context.Articles.Any(e => e.Id == article.Id))
            {
                return NotFound();
            }

            if (article.Published.HasValue)
            {
                article.Published = vm.Article.Published!.Value.ToUniversalTime();
            }

            context.Update(vm.Article);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }

    public async Task<IActionResult> Delete(Article article)
    {
        var _article = await context.Articles.FirstOrDefaultAsync(a => a.Id == article.Id);

        if (_article == null)
        {
            return NotFound();
        }

        var vm = new ArticleDeleteVm
        {
            Article = _article
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var article = await context.Articles.FirstOrDefaultAsync(a => a.Id == id);

        if (article == null)
        {
            return NotFound();
        }

        if (!IsAuthorized(article))
        {
            // If the user is not authorized...
            return Forbid();
        }

        context.Remove(article);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }


    private bool IsAuthorized(Article article)
    {
        var isAdmin = User.IsInRole(RoleConstants.Administrator);
        var isUser = User.FindFirstValue(ClaimTypes.NameIdentifier) == article.UserId;

        if (isAdmin || isUser)
        {
            return true;
        }

        return false;
    }
}
