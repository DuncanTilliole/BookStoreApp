using BookStoreApp.Shared.Bases;

namespace BookStoreApp.Shared.Services.Authors
{
    public interface IAuthorsService
    {
        Task<Response<List<Author>>> GetAuthors();

        Task<Response<Author>> GetAuthor(int id);

        Task<Response<int>> CreateAuthor(AuthorCreateDTO author);

        Task<Response<int>> UpdateAuthor(int id, AuthorUpdateDTO author);

        Task<Response<int>> DeleteAuthor(int id);
    }
}
