using System.ComponentModel.DataAnnotations;

namespace web.net_project.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [StringLength(30)]
    public string Tag { get; set; } = string.Empty;

    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string ImageClass { get; set; } = "image-one";

    [Range(1, 5)]
    public int Rating { get; set; } = 4;

    [Range(1, 500)]
    public int StockQuantity { get; set; } = 10;

    public bool IsFeatured { get; set; }
}
