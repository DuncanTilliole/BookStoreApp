using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.DTO.Book;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Statics;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(BookStoreDbContext context, ILogger<AuthorsController> logger, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Books
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookStoreDbContext =await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookReadOnlyDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: Books/Create
        [HttpPost("Create")]
        [Authorize]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookDTO)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDTO);

                // Insert the image in the project
                if (bookDTO.ImageData != null && bookDTO.OriginalImageName != null)
                    book.Image = CreateFile(bookDTO.ImageData, bookDTO.OriginalImageName);

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
        [Authorize]
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

            if (string.IsNullOrEmpty(bookDTO.ImageData) == false)
            {
                bookDTO.Image = CreateFile(bookDTO.ImageData, bookDTO.OriginalImageName);

                var picName = Path.GetFileName(existingBook.Image);
                var path = $"{_webHostEnvironment.ContentRootPath}\\Publics\\Images\\{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
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
        [Authorize]
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

        private string CreateFile(string imageBase64, string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}{ext}";

            // Specify the directory where images are stored
            var directory = Path.Combine(_webHostEnvironment.ContentRootPath, "Publics", "Images");
            Directory.CreateDirectory(directory); // Ensure the directory exists

            var path = Path.Combine(directory, fileName);

            byte[] image = Convert.FromBase64String(imageBase64);

            using (var fileStream = System.IO.File.Create(path))
            {
                fileStream.Write(image, 0, image.Length);
            }

            return $"https://{url}/Publics/Images/{fileName}";
        }
    }
}
