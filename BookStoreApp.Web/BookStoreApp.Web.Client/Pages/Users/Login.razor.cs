using Microsoft.AspNetCore.Components;
using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared;
using BookStoreApp.Shared.Services.Authentication;

namespace BookStoreApp.Web.Client.Pages.Users
{
    public partial class Login
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
        [Inject] protected IAuthenticationService AuthService { get; set; } = null!;

        private string RegistrationError = null!;
        private UserToLoginDTO LoginModel { get; set; } = new UserToLoginDTO();

        private async Task HandleLogin()
        {
            Response<string> response = await AuthService.AuthenticateAsync(LoginModel);

            if (response.Success)
                NavigationManager.NavigateTo("");
            else
                RegistrationError = response.Message;
        }
    }
}
