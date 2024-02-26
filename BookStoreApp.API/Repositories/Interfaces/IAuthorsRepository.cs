using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Author;

namespace BookStoreApp.API.Repositories.Interfaces
{
    public interface IAuthorsRepository : IGenericRepository<Author>
    {
        Task<AuthorDetailsDTO> GetAuthorDetailsAsync(int id);
    }
}
