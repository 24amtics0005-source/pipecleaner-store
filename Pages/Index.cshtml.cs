using Microsoft.AspNetCore.Mvc.RazorPages;
using web.net_project.Models;
using web.net_project.Services;

namespace web.net_project.Pages;

public class IndexModel(StoreService storeService) : PageModel
{
    public IReadOnlyList<Product> FeaturedProducts { get; private set; } = [];

    public async Task OnGetAsync()
    {
        FeaturedProducts = await storeService.GetFeaturedProductsAsync();
    }
}
