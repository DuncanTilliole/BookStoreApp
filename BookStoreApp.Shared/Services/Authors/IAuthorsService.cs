using BookStoreApp.Shared.Bases;

namespace BookStoreApp.Shared.Services.Authors
{
    public interface IAuthorsService
    {
        Task<Response<List<Author>>> GetAuthors();

        Task<Response<Author>> GetAuthor(int Id);

        Task<Response<int>> CreateAuthor(AuthorCreateDTO author);

        Task<Response<int>> UpdateAuthor(int Id, AuthorUpdateDTO author);

        Task<Response<int>> DeleteAuthor(int Id);
    }
}
