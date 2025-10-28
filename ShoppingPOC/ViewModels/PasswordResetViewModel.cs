using System.ComponentModel.DataAnnotations;

namespace ShoppingPOC.ViewModels
{
    public class PasswordResetViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;
        public int ValidationCode { get; set; }
    }
}
