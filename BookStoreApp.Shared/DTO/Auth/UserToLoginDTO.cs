using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Shared
{
    public class UserToLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)(?!.*\s).{6,}$",
    ErrorMessage = "Password must be at least 6 characters long and contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }
}
