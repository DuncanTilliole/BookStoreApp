using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTO;
using BookStoreApp.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories.Classes
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public GenericRepository(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _db.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters) where TResult : class
        {
            var totalSize = await _db.Set<T>().CountAsync();
            var items = await _db.Set<T>()
                .Skip(queryParameters.StartIndex)
                .Take(queryParameters.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new VirtualizeResponse<TResult> { Items = items, TotalSize = totalSize };
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }
    }
}
