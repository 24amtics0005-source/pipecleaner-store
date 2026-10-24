using System.ComponentModel.DataAnnotations;

namespace web.net_project.Models;

public class Order
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(250)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string PaymentMethod { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public decimal TotalAmount { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
