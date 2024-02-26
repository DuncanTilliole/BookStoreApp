using AutoMapper;
using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Repositories.Interfaces;
using BookStoreApp.API.DTO.Book;
using BookStoreApp.API.DTO;

namespace BookStoreApp.API.Repositories.Classes
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BooksRepository(BookStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookReadOnlyDTO>> GetAllBooksDetailsAsync()
        {
            var books = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookReadOnlyDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return books;
        }
    }
}
