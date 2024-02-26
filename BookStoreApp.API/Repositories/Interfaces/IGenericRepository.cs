using BookStoreApp.API.DTO;

namespace BookStoreApp.API.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);

        Task<List<T>> GetAllAsync();

        Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters) where TResult : class;

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

        Task<bool> Exists(int id);
    }
}
