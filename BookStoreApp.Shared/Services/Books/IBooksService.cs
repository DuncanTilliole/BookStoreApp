using BookStoreApp.Shared.DTO.Response;

namespace BookStoreApp.Shared.Services.Books
{
    public interface IBooksService
    {
        Task<Response<VirtualizedResponse<BookReadOnlyDTO>>> Get(QueryParameters queryParameters);

        Task<Response<BookReadOnlyDTO>> Get(int id);

        Task<Response<int>> Create(BookCreateDTO book);

        Task<Response<int>> Update(int Id, BookUpdateDTO book);

        Task<Response<int>> Delete(int Id);
    }
}
