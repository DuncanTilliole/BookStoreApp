using BookStoreApp.Shared.Bases;

namespace BookStoreApp.Shared.Services.Books
{
    public interface IBooksService
    {
        Task<Response<List<BookReadOnlyDTO>>> Get();

        Task<Response<BookReadOnlyDTO>> Get(int id);

        Task<Response<int>> Create(BookCreateDTO book);

        Task<Response<int>> Update(int Id, BookUpdateDTO book);

        Task<Response<int>> Delete(int Id);
    }
}
