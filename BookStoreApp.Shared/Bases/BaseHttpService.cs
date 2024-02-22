using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BookStoreApp.Shared.Bases
{
    public class BaseHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public BaseHttpService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        protected async Task GetBearerToken()
        {
            var token = await _localStorageService.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
