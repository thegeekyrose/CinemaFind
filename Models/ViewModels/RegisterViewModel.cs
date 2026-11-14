
using System.ComponentModel.DataAnnotations;

namespace CinemaFind.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your display name.")]
        [StringLength(60, MinimumLength = 2,
            ErrorMessage = "Display name must contain between 2 and 60 characters.")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8,
            ErrorMessage = "Password must contain at least 8 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),
            ErrorMessage = "The passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
