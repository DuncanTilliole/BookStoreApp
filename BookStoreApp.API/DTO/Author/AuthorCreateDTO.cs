using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.DTO.Author
{
    public class AuthorCreateDTO
    {
        [Required]
        [StringLength(50)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public required string Bio { get; set; }
    }
}
