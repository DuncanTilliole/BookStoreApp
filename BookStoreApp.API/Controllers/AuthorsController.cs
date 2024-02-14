using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BookStoreApp.API.DTO.Author;
using AutoMapper;
using BookStoreApp.API.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly BookStoreDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AuthorsController(BookStoreDbContext context, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Authors
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _context.Authors.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing in {nameof(Index)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: Authors/Details/5
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var author = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id);
                if (author == null)
                {
                    return NotFound();
                }

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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] AuthorCreateDTO authorDTO)
        {
            try
            {
                var author = _mapper.Map<Author>(authorDTO);
                if (ModelState.IsValid)
                {
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
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
        [Authorize]
        public async Task<IActionResult> Edit(int id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            // Retrieve the existing author from the database using FindAsync
            var existingAuthor = await _context.Authors.FindAsync(id);
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
                    // Save the changes to the database
                    await _context.SaveChangesAsync();
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);

                await _context.SaveChangesAsync();
                return Ok(Messages.Delete200Message);
            }
            return NotFound();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
