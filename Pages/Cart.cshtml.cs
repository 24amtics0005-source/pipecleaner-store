using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.net_project.Models;
using web.net_project.Services;

namespace web.net_project.Pages;

public class CartModel(StoreService storeService) : PageModel
{
    public List<CartItem> Items { get; private set; } = [];
    public decimal Subtotal { get; private set; }

    public void OnGet()
    {
        Items = CartService.GetCart(HttpContext.Session);
        Subtotal = CartService.GetCartTotal(Items);
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        var product = await storeService.GetProductByIdAsync(productId);
        if (product is null)
        {
            return RedirectToPage("/Products/Index");
        }

        CartService.AddItem(HttpContext.Session, product);
        return RedirectToPage();
    }

    public IActionResult OnPostUpdateQuantity(int productId, int quantity)
    {
        CartService.UpdateQuantity(HttpContext.Session, productId, quantity);
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveItem(int productId)
    {
        CartService.RemoveItem(HttpContext.Session, productId);
        return RedirectToPage();
    }
}
