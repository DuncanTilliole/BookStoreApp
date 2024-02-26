using BookStoreApp.Shared.DTO.Response;

namespace BookStoreApp.Shared.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Response<string>> AuthenticateAsync(UserToLoginDTO user);

        Task<Response<string>> RegisterAsync(UserToRegisterDTO user);

        Task<bool> Logout();
    }
}