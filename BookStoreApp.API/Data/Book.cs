using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Data;

public partial class Book
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [MaxLength(50)]
    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("year")]
    public int? Year { get; set; }

    [MaxLength(250)]
    [JsonProperty("isbn")]
    public string Isbn { get; set; } = null!;

    [MaxLength(250)]
    [JsonProperty("summary")]
    public string? Summary { get; set; }

    [MaxLength(50)]
    [JsonProperty("image")]
    public string? Image { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }
}
