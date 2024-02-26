using BookStoreApp.Shared.DTO.Response;

namespace BookStoreApp.Shared.Services.Authors
{
    public interface IAuthorsService
    {
        Task<Response<VirtualizedResponse<AuthorReadOnlyDTO>>> GetAuthors(QueryParameters queryParameters);

        Task<Response<Author>> GetAuthor(int id);

        Task<Response<int>> CreateAuthor(AuthorCreateDTO author);

        Task<Response<int>> UpdateAuthor(int id, AuthorUpdateDTO author);

        Task<Response<int>> DeleteAuthor(int id);
    }
}
