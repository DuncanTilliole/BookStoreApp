using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Book
{
    public class BookReadOnlyDTO : BaseDTO
    {
        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public required decimal Price { get; set; }

        [Required]
        public required int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public required string AuthorName { get; set; }

        public string? Image { get; set; }
    }
}