using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Auth
{
    public class AuthResponse
    {
        public required string UserId { get; set; }

        public required string Token { get; set; }

        public required string Email { get; set; }
    }
}
