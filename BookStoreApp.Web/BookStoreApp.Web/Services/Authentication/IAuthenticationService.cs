using BookStoreApp.Shared;

namespace BookStoreApp.Web.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(string user);
    }
}