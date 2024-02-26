using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Author;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Repositories.Interfaces;

namespace BookStoreApp.API.Repositories.Classes
{
    public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorsRepository(BookStoreDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthorDetailsDTO> GetAuthorDetailsAsync(int id)
        {
            var author = await _context.Authors
                    .Include(author => author.Books)
                    .ProjectTo<AuthorDetailsDTO>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(m => m.Id == id);

            return author;
        }
    }
}
