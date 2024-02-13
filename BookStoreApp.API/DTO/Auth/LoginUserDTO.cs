using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Auth
{
    public class LoginUserDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
