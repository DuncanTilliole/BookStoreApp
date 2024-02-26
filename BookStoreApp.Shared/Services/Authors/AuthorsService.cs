using Blazored.LocalStorage;
using BookStoreApp.Shared.Bases;
using BookStoreApp.Shared.DTO.Response;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;

namespace BookStoreApp.Shared.Services.Authors
{
    public class AuthorsService : BaseHttpService, IAuthorsService
    {
        private readonly HttpClient _httpClient;

        public AuthorsService(HttpClient httpClient, ILocalStorageService localStoreService) : base (httpClient, localStoreService)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<VirtualizedResponse<AuthorReadOnlyDTO>>> GetAuthors(QueryParameters queryParameters)
        {
            try
            {
                await GetBearerToken();

                // Construct the URL with query parameters
                var uriBuilder = new UriBuilder("https://localhost:7003/Authors");
                if (queryParameters != null)
                {
                    var queryString = queryParameters.ToQueryString();
                    uriBuilder.Query = queryString;
                }

                var response = await _httpClient.GetAsync(uriBuilder.Uri);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    VirtualizedResponse<AuthorReadOnlyDTO> virtualizedResponse = JsonConvert.DeserializeObject<VirtualizedResponse<AuthorReadOnlyDTO>>(responseBody)!;

                    return new Response<VirtualizedResponse<AuthorReadOnlyDTO>>
                    {
                        Datas = virtualizedResponse,
                        Success = true,
                    };
                }
                else return new Response<VirtualizedResponse<AuthorReadOnlyDTO>> { Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<VirtualizedResponse<AuthorReadOnlyDTO>> { Success = false };
            }
        }

        public async Task<Response<Author>> GetAuthor(int Id)
        {
            try
            {
                await GetBearerToken();
                var response = await _httpClient.GetAsync($"https://localhost:7003/Authors/Details/{Id}");
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Author author = JsonConvert.DeserializeObject<Author>(responseBody)!;

                    return new Response<Author>
                    {
                        Datas = author,
                        Success = true,
                    };
                }
                else return new Response<Author> { Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<Author> { Success = false };
            }
        }

        public async Task<Response<int>> CreateAuthor(AuthorCreateDTO author)
        {
            try
            {
                await GetBearerToken();
                var json = JsonConvert.SerializeObject(author);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7003/Authors/Create", content);
                string responseBody = await response.Content.ReadAsStringAsync();
           
                if (response.IsSuccessStatusCode)
                {
                    Author[] responseDeserialized = JsonConvert.DeserializeObject<Author[]>(responseBody)!;

                    // Access the first element if it exists
                    Author authorCreated = responseDeserialized.FirstOrDefault()!;

                    return new Response<int> { Success = true };
                }
                else return new Response<int> { Success = false };
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<int> { Success = false };
            }

        }

        public async Task<Response<int>> UpdateAuthor(int Id, AuthorUpdateDTO author)
        {
            try
            {
                await GetBearerToken();
                var json = JsonConvert.SerializeObject(author);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"https://localhost:7003/Authors/Edit/{Id}", content);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new Response<int> { Success = true };
                }
                else return new Response<int> { Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<int> { Success = false };
            }
        }

        public async Task<Response<int>> DeleteAuthor(int Id)
        {
            try
            {
                await GetBearerToken();
                var response = await _httpClient.DeleteAsync($"https://localhost:7003/Authors/Delete/{Id}");
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new Response<int> { Success = true };
                }
                else return new Response<int> { Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<int> { Success = false };
            }
        }
    }
}
