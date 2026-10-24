using Microsoft.AspNetCore.Mvc.RazorPages;
using web.net_project.Models;
using web.net_project.Services;

namespace web.net_project.Pages.Products;

public class IndexModel(StoreService storeService) : PageModel
{
    public IReadOnlyList<Product> Products { get; private set; } = [];

    public async Task OnGetAsync()
    {
        Products = await storeService.GetAllProductsAsync();
    }
}
