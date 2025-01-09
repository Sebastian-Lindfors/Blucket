namespace Blocket.ViewModels;

using System.ComponentModel.DataAnnotations;
using Blocket.Models;

public class ArticleEditVm
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters")]
    public string Name { get; set; } = "";

    [Required]
    [MaxLength(200, ErrorMessage = "The description cannot be longer than 200 characters")]
    public string Description { get; set; } = "";

    public string? ImageUrl { get; set; }

    [Required]
    public decimal Price { get; set; }
}