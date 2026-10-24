namespace web.net_project.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageClass { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = string.Empty;
}
