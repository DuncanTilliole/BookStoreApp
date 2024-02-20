namespace BookStoreApp.Shared.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ResponseAPI> AuthenticateAsync(UserToLoginDTO user);

        Task<ResponseAPI> RegisterAsync(UserToRegisterDTO user);

        Task<bool> Logout();
    }
}