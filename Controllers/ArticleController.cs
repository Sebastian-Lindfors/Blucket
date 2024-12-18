namespace Blocket.Controllers;

using Blocket.Models;
using Microsoft.AspNetCore.Mvc;
using Blocket.ViewModels;

public class ArticleController : Controller
{
    public IActionResult Index()
    {
        var articles = new List<Article>
        {
            new()
            {
                Id = 1,
                Name = "Article 1",
                Description = "Description of Article 1",
                Price = 19.99m,
                Published = DateTime.Now
            },

            new()
            {
                Id = 2,
                Name = "Article 2",
                Description = "Description of Article 2",
                Price = 199.99m,
                Published = DateTime.Now
            }
        };

        var vm = new ArticleIndexVm { Articles = articles };
        return View(vm);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ArticleCreateVm vm)
    {
        if (ModelState.IsValid)
        {
            return Ok($"{vm.Article.Name}, {vm.Article.Description}, {vm.Article.Price}, {vm.Article.Published}");
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
    public IActionResult Edit(ArticleEditVm vm)
    {
        if (ModelState.IsValid)
        {
            return Ok();
        }

        return View(vm);
    }

    public IActionResult Delete(Article article)
    {
        if (article == null)
        {
            return NotFound();
        }

        var vm = new ArticleDeleteVm
        {
            Article = article
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult Delete(ArticleDeleteVm vm)
    {
        if (ModelState.IsValid)
        {
            return Ok();
        }

        return View(vm);
    }
}
