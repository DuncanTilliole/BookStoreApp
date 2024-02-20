using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Auth
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
