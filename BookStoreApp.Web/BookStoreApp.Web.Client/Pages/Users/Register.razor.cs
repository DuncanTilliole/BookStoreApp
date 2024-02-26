using BookStoreApp.Shared.DTO.Response;
using BookStoreApp.Shared;
using Microsoft.AspNetCore.Components;
using BookStoreApp.Shared.Services.Authentication;

namespace BookStoreApp.Web.Client.Pages.Users
{
    public partial class Register
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
        [Inject] protected IAuthenticationService AuthService { get; set; } = null!;

        private string registrationError = string.Empty;
        private UserToRegisterDTO RegistrationModel { get; set; } = new UserToRegisterDTO();

        /**
         * Registration of a new user
        */
        private async Task HandleRegistration()
        {
            RegistrationModel.UserName = RegistrationModel.Email;
            RegistrationModel.Role = "User";

            Response<string> response = await AuthService.RegisterAsync(RegistrationModel);

            if (response.Success)
                NavigationManager.NavigateTo("/login");
            else
                registrationError = response.Message;
        }
    }
}
