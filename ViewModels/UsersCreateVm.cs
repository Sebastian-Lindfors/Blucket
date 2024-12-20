using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace Blocket.ViewModels;

public class UsersCreateVm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [StrongPassword]
    public string PlainPassword { get; set; } = "";
}

public class StrongPasswordAttribute : ValidationAttribute
{
    private const string DefaultErrorMessage = "Password must contain at least one uppercase letter, one number, one special character, and be at least 6 characters long.";

    public StrongPasswordAttribute() : base(DefaultErrorMessage) { }

    public override bool IsValid(object? value)
    {
        if (value == null) return false;

        var password = value.ToString();

        // Ensure the password meets the required criteria
        bool hasUppercase = Regex.IsMatch(password!, @"[A-Z]");
        bool hasLowercase = Regex.IsMatch(password!, @"[a-z]");
        bool hasDigit = Regex.IsMatch(password!, @"\d");
        bool hasSpecialChar = Regex.IsMatch(password!, @"[\W_]");
        bool isValidLength = password!.Length >= 6;

        return hasUppercase && hasLowercase && hasDigit && hasSpecialChar && isValidLength;
    }
}