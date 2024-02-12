using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreApp.API.Data;

public partial class Author
{
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
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
