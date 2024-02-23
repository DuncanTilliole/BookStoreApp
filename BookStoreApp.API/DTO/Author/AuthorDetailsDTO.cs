using BookStoreApp.API.DTO.Book;

namespace BookStoreApp.API.DTO.Author
{
    public class AuthorDetailsDTO : AuthorReadOnlyDTO
    {
        public List<BookReadOnlyDTO> Books { get; set; }
    }
}
