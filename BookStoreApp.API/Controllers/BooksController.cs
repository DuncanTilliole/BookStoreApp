using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using BookStoreApp.API.DTO.Book;
using BookStoreApp.API.Statics;
using Microsoft.AspNetCore.Authorization;
using BookStoreApp.API.Repositories.Interfaces;
using BookStoreApp.API.DTO.Author;
using BookStoreApp.API.DTO;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBooksRepository _booksRepository;

        public BooksController(ILogger<AuthorsController> logger, IMapper mapper, IWebHostEnvironment webHostEnvironment, IBooksRepository booksRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _booksRepository = booksRepository;
        }

        // GET: Books
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<BookReadOnlyDTO>>> Index([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var books = await _booksRepository.GetAllAsync<BookReadOnlyDTO>(queryParameters);

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Index)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: Books/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var book = await _booksRepository.GetAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Details)}");
                return StatusCode(500, Messages.Error500Message);
            }
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

                await _booksRepository.AddAsync(book);

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
            var existingBook = await _booksRepository.GetAsync(id);   

            // Check if the author exists
            if (existingBook == null)
            {
                // If the author does not exist, return a NotFound response
                return NotFound();
            }

            if (string.IsNullOrEmpty(bookDTO.ImageData) == false && string.IsNullOrEmpty(bookDTO.OriginalImageName) == false)
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

            try
            {
                await _booksRepository.UpdateAsync(existingBook);
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
            var book = await _booksRepository.GetAsync(id);
            if (book != null)
            {
                await _booksRepository.DeleteAsync(id);

                return Ok(Messages.Delete200Message);
            }
            return NotFound();
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
