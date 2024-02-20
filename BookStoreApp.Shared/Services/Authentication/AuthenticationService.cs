using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using BookStoreApp.Shared.DTO.Response;
using Microsoft.AspNetCore.Components.Authorization;
using BookStoreApp.Shared.Providers;

namespace BookStoreApp.Shared.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _stateProvider;
        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorage;
            _stateProvider = authenticationStateProvider;
        }
        public async Task<ResponseAPI> AuthenticateAsync(UserToLoginDTO loginModel)
        {
           try
           {
                var json = JsonSerializer.Serialize(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7003/Auth/Login", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    AuthResponse authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody)!;

                    await _localStorageService.SetItemAsync("accessToken", authResponse.Token);
                    
                    ((ApiAuthenticationStateProvider)_stateProvider).UpdateAuthenticationState(authResponse.Token);

                    var res = ((ApiAuthenticationStateProvider)_stateProvider).GetAuthenticationStateAsync();

                    return new ResponseAPI { ResponseBody = responseBody, IsValidate = true };
                }
                else
                {
                    return new ResponseAPI { ResponseBody = responseBody, IsValidate = false };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new ResponseAPI { ResponseBody = "Error Server, try again later.", IsValidate = false };
            }
        }

        public async Task<ResponseAPI> RegisterAsync(UserToRegisterDTO registerModel)
        {
            try
            {
                var json = JsonSerializer.Serialize(registerModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7003/Auth/Register", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ResponseAPI { ResponseBody = responseBody, IsValidate = true };
                }
                else
                {
                    return new ResponseAPI { ResponseBody = responseBody, IsValidate = false };
                }
            }
            catch (Exception ex)
            {
                return new ResponseAPI { ResponseBody = "Error Server, try again later.", IsValidate = false };
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await ((ApiAuthenticationStateProvider)_stateProvider).UpdateAuthenticationState(null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
