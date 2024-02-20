

using System.Text.Json.Serialization;

namespace BookStoreApp.Shared.DTO.Response
{
    public class AuthResponse
    {
        [JsonPropertyName("userId")]
        public required string UserId { get; set; }

        [JsonPropertyName("token")]
        public required string Token { get; set; }

        [JsonPropertyName("email")]
        public required string Email { get; set; }
    }
}
