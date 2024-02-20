using Newtonsoft.Json;
using System.Text.Json;

namespace BookStoreApp.Shared.Services.Authors
{
    public class AuthorsService : IAuthorsService
    {
        private readonly HttpClient _httpClient;
        public AuthorsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Author>> GetAuthors()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7003/Authors");
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<Author> authors = JsonConvert.DeserializeObject<IEnumerable<Author>>(responseBody)!;

                    return authors.ToList();
                }
                else
                {
                    return [];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return [];
            }
        }
    }
}
