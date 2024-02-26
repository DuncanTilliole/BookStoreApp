using BookStoreApp.Shared.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Web.Client.Pages.Users
{
    public partial class Logout
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
        [Inject] protected IAuthenticationService AuthService { get; set; } = null!;

        private string RegistrationError = null!;

        public async void LogoutApp()
        {
            bool response = await AuthService.Logout();

            if (response)
                NavigationManager.NavigateTo("/login");
            else
                RegistrationError = "Impossible to logout";
        }
    }
}
