using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.DTO.Book;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Statics;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public BooksController(BookStoreDbContext context, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Books
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookStoreDbContext =await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookReadOnlyDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            // var booksDTO = _mapper.Map<IEnumerable<BookReadOnlyDTO>>(bookStoreDbContext); // The projectTo function makes this line
            return Ok(bookStoreDbContext);
        }

        // GET: Books/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .ProjectTo<BookReadOnlyDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: Books/Create
        [HttpPost("Create")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookDTO)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDTO);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Create)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BookUpdateDTO bookDTO)
        {
            if (id != bookDTO.Id)
            {
                return BadRequest();
            }

            // Retrieve the existing book from the database using FindAsync
            var existingBook = await _context.Books.FindAsync(id);   

            // Check if the author exists
            if (existingBook == null)
            {
                // If the author does not exist, return a NotFound response
                return NotFound();
            }

            _mapper.Map(bookDTO, existingBook);
            _context.Entry(existingBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        // POST: Books/Delete/5
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return Ok(Messages.Delete200Message);
            }
            return NotFound();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
