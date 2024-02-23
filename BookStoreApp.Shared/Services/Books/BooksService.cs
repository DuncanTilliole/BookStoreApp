using Blazored.LocalStorage;
using BookStoreApp.Shared.Bases;
using Newtonsoft.Json;
using System.Text;

namespace BookStoreApp.Shared.Services.Books
{
    public class BooksService : BaseHttpService, IBooksService
    {
        private readonly HttpClient _httpClient;

        public BooksService(HttpClient httpClient, ILocalStorageService localStoreService) : base(httpClient, localStoreService)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<List<BookReadOnlyDTO>>> Get()
        {
            try
            {
                await GetBearerToken();
                var response = await _httpClient.GetAsync("https://localhost:7003/Books");
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<BookReadOnlyDTO> books = JsonConvert.DeserializeObject<IEnumerable<BookReadOnlyDTO>>(responseBody)!;

                    return new Response<List<BookReadOnlyDTO>>
                    {
                        Datas = books.ToList(),
                        Success = true,
                    };
                }
                else return new Response<List<BookReadOnlyDTO>> { Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<List<BookReadOnlyDTO>> { Success = false };
            }
        }

        public async Task<Response<BookReadOnlyDTO>> Get(int Id)
        {
            try
            {
                await GetBearerToken();
                var response = await _httpClient.GetAsync($"https://localhost:7003/Books/Details/{Id}");
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    BookReadOnlyDTO book = JsonConvert.DeserializeObject<BookReadOnlyDTO>(responseBody)!;

                    return new Response<BookReadOnlyDTO>
                    {
                        Datas = book,
                        Success = true,
                    };
                }
                else return new Response<BookReadOnlyDTO> { Success = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR WRITING : {ex}");
                return new Response<BookReadOnlyDTO> { Success = false };
            }
        }

        public async Task<Response<int>> Create(BookCreateDTO book)
        {
            try
            {
                await GetBearerToken();
                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7003/Books/Create", content);
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

        public async Task<Response<int>> Update(int Id, BookUpdateDTO book)
        {
            try
            {
                await GetBearerToken();
                var json = JsonConvert.SerializeObject(book);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"https://localhost:7003/Books/Edit/{Id}", content);
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

        public async Task<Response<int>> Delete(int Id)
        {
            try
            {
                await GetBearerToken();
                var response = await _httpClient.DeleteAsync($"https://localhost:7003/Books/Delete/{Id}");
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
