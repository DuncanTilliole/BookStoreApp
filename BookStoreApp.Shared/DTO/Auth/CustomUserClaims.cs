namespace BookStoreApp.Shared.DTO.Auth
{
    public record CustomUserClaims
    (
        string Name = null!,

        string Email = null!
    );
}
