using System.ComponentModel.DataAnnotations;

namespace ShoppingPOC.ViewModels
{
    public class PasswordUpdateViewModel
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; }
        
        [Required(ErrorMessage = "Confirmation is required.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirmation { get; set; }

    }
}
