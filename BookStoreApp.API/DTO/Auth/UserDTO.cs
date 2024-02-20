using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Auth
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}
