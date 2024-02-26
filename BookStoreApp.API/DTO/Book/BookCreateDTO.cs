using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Book
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        [Range(1800, int.MaxValue)]
        public required int Year { get; set; }

        [Required]
        public required string Isbn { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public required string Summary { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public required decimal Price { get; set; }

        [Required]
        public required decimal AuthorId { get; set; }

        public string? ImageData { get; set; }

        public string? OriginalImageName { get; set; }
    }
}
