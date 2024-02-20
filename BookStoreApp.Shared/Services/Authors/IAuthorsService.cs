namespace BookStoreApp.Shared.Services.Authors
{
    public interface IAuthorsService
    {
        Task<List<Author>> GetAuthors();
    }
}
