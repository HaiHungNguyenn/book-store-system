using System.ComponentModel.DataAnnotations;

namespace BMS.Services.BookAPI.Models;

public class Book
{
    [Key]
    public int BookId { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Author { get; set; }
    [Required]
    public int PublishYear { get; set; }
    [Required]
    public float Price { get; set; }
    public string Description { get; set; } = string.Empty;
    [Required]
    public required string Genre { get; set; }
}