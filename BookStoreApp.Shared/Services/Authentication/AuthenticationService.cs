using System.Text.Json;
using System.Text;
using Blazored.LocalStorage;
using BookStoreApp.Shared.DTO.Response;
using Microsoft.AspNetCore.Components.Authorization;
using BookStoreApp.Shared.Providers;
using BookStoreApp.Shared.Bases;

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
        public async Task<Response<string>> AuthenticateAsync(UserToLoginDTO loginModel)
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

                    return new Response<string> { Datas = responseBody, Success = true };
                }
                else
                {
                    return new Response<string> { Success = false };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<string> { Success = false };
            }
        }

        public async Task<Response<string>> RegisterAsync(UserToRegisterDTO registerModel)
        {
            try
            {
                var json = JsonSerializer.Serialize(registerModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("https://localhost:7003/Auth/Register", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new Response<string> { Datas = responseBody, Success = true };
                }
                else
                {
                    return new Response<string> { Success = false };
                }
            }
            catch
            {
                return new Response<string> { Message = "Error Server, try again later.", Success = false };
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
