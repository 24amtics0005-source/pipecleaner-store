using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.net_project.Models;
using web.net_project.Services;

namespace web.net_project.Pages.Products;

public class ProductDetailModel(StoreService storeService) : PageModel
{
    public Product? Product { get; private set; }

    public async Task OnGetAsync(int id)
    {
        Product = await storeService.GetProductByIdAsync(id);
    }

    public async Task<IActionResult> OnPostAddToCartAsync(int productId)
    {
        var product = await storeService.GetProductByIdAsync(productId);
        if (product is null)
        {
            return RedirectToPage("/Products/Index");
        }

        CartService.AddItem(HttpContext.Session, product);
        return RedirectToPage("/Cart");
    }
}
