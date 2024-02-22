using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Data;

public class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }

    [JsonProperty("id")]
    public int Id { get; set; }

    [MaxLength(50)]
    [JsonProperty("firstName")]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    [JsonProperty("lastName")]
    public string? LastName { get; set; }

    [MaxLength(250)]
    [JsonProperty("bio")]
    public string? Bio { get; set; }

    [JsonProperty("books")]
    public virtual ICollection<Book> Books { get; set; }
}
