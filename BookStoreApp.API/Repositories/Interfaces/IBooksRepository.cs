using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Book;

namespace BookStoreApp.API.Repositories.Interfaces
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<List<BookReadOnlyDTO>> GetAllBooksDetailsAsync();
    }
}
