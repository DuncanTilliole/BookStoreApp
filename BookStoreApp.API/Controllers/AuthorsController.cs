using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.DTO.Author;
using AutoMapper;
using BookStoreApp.API.Statics;
using Microsoft.AspNetCore.Authorization;
using BookStoreApp.API.DTO.Book;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Repositories.Interfaces;
using BookStoreApp.API.DTO;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsController(ILogger<AuthorsController> logger, IMapper mapper, IAuthorsRepository authorsRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _authorsRepository = authorsRepository;
        }

        // GET: Authors
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<AuthorReadOnlyDTO>>> Index([FromQuery]QueryParameters queryParameters)
        {
            try
            {
                var authors = await _authorsRepository.GetAllAsync<AuthorReadOnlyDTO>(queryParameters);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Index)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: Authors/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<ActionResult<AuthorDetailsDTO>> Details(int id)
        {
            try
            {
                var author = await _authorsRepository.GetAuthorDetailsAsync(id);

                if (author == null) return NotFound();

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Details)}");
                return StatusCode(500, Messages.Error500Message);
            } 
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDTO);
                if (ModelState.IsValid)
                {
                    await _authorsRepository.AddAsync(author);

                    return RedirectToAction(nameof(Index));
                }
                return StatusCode(500, Messages.Error500Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Create)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // PUT: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut("Edit/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            // Retrieve the existing author from the database using FindAsync
            var existingAuthor = await _authorsRepository.GetAsync(id);
            // Check if the author exists
            if (existingAuthor == null)
            {
                // If the author does not exist, return a NotFound response
                return NotFound();
            }

            // Update the properties of the existing author with the values from the request
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.Bio = author.Bio;
            existingAuthor.Books = author.Books;

            if (ModelState.IsValid)
            {
                try
                {
                    await _authorsRepository.UpdateAsync(existingAuthor);

                    return Ok(existingAuthor);
                }
                catch (Exception ex)
                {
                    // Handle concurrency conflicts if necessary
                    _logger.LogError(ex, $"Error performing in {nameof(Edit)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }
            // If the ModelState is not valid, return the updated author
            return StatusCode(500, Messages.Error500Message);
        }


        // DELETE: Authors/Delete/5
        [HttpDelete("Delete/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _authorsRepository.GetAsync(id);
            if (author != null)
            {
                await _authorsRepository.DeleteAsync(id);

                return Ok(Messages.Delete200Message);
            }
            return NotFound();
        }
    }
}
