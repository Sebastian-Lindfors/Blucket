namespace Blocket.ViewModels;
using Blocket.Models;

public class ArticleDeleteVm
{
    public required Article Article { get; set; }
    public string Previous { get; set; } = "";
}