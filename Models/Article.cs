using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Blocket.Models;

public class Article
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters")]
    public string Name { get; set; } = "";

    [Required]
    [MaxLength(200, ErrorMessage = "The description cannot be longer than 200 characters")]
    public string Description { get; set; } = "";

    [Required]
    public decimal Price { get; set; }

    [Required]
    public DateTime? Published { get; set; }

    // Foreign key for the user.
    public string? UserId { get; set; }

    // Navigation property(?)
    public IdentityUser? User { get; set; }
}