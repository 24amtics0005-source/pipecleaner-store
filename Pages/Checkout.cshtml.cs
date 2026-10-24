using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.net_project.Data;
using web.net_project.Models;
using web.net_project.Services;

namespace web.net_project.Pages;

public class CheckoutFormInput
{
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(250)]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required.")]
    [StringLength(20)]
    public string PostalCode { get; set; } = string.Empty;
}

public class CheckoutModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public CheckoutModel(ApplicationDbContext db)
    {
        _db = db;
    }

    [BindProperty]
    public CheckoutFormInput Customer { get; set; } = new();

    [BindProperty]
    public string PaymentMethod { get; set; } = "Cash on Delivery";

    public List<CartItem> Items { get; private set; } = [];
    public decimal Subtotal { get; private set; }

    public void OnGet()
    {
        Items = CartService.GetCart(HttpContext.Session);
        Subtotal = CartService.GetCartTotal(Items);
        PaymentMethod ??= "Cash on Delivery";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Items = CartService.GetCart(HttpContext.Session);
        Subtotal = CartService.GetCartTotal(Items);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (string.IsNullOrWhiteSpace(PaymentMethod))
        {
            PaymentMethod = "Cash on Delivery";
        }

        // Persist the order to the database
        var order = new Order
        {
            CustomerName = $"{Customer.FirstName} {Customer.LastName}",
            Email = Customer.Email,
            Address = Customer.Address,
            City = Customer.City,
            PostalCode = Customer.PostalCode,
            PaymentMethod = PaymentMethod,
            TotalAmount = Subtotal,
            CreatedAt = DateTime.UtcNow,
            Items = Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.Price
            }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        TempData["OrderId"] = order.Id;
        TempData["SelectedPaymentMethod"] = PaymentMethod;
        TempData["CustomerName"] = order.CustomerName;
        HttpContext.Session.Clear();
        return RedirectToPage("/Checkout/Success");
    }
}
