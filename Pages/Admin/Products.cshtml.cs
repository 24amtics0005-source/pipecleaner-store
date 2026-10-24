using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.net_project.Models;
using web.net_project.Services;

namespace web.net_project.Pages.Admin;

public class ProductsModel : PageModel
{
    private readonly StoreService _storeService;

    public ProductsModel(StoreService storeService)
    {
        _storeService = storeService;
    }

    public IList<Product> Products { get; private set; } = new List<Product>();
    public string? StatusMessage { get; private set; }
    public bool IsEditing { get; private set; }
    public Product? ProductToEdit { get; private set; }

    [BindProperty]
    public Product ProductForm { get; set; } = new();

    public async Task OnGetAsync(int? id = null)
    {
        Products = (await _storeService.GetAllProductsAsync()).ToList();

        if (id.HasValue)
        {
            ProductToEdit = await _storeService.GetProductByIdAsync(id.Value);
            IsEditing = ProductToEdit is not null;
            if (ProductToEdit is not null)
            {
                ProductForm = ProductToEdit;
            }
        }
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        if (!ModelState.IsValid)
        {
            Products = (await _storeService.GetAllProductsAsync()).ToList();
            return Page();
        }

        if (ProductForm.Id > 0)
        {
            var existing = await _storeService.GetProductByIdAsync(ProductForm.Id);
            if (existing is null)
            {
                return RedirectToPage();
            }

            existing.Name = ProductForm.Name;
            existing.Category = ProductForm.Category;
            existing.Tag = ProductForm.Tag;
            existing.Price = ProductForm.Price;
            existing.Description = ProductForm.Description;
            existing.Rating = ProductForm.Rating;
            existing.StockQuantity = ProductForm.StockQuantity;
            existing.IsFeatured = ProductForm.IsFeatured;
            await _storeService.UpdateProductAsync(existing);
        }
        else
        {
            await _storeService.AddProductAsync(new Product
            {
                Name = ProductForm.Name,
                Category = ProductForm.Category,
                Tag = ProductForm.Tag,
                Price = ProductForm.Price,
                Description = ProductForm.Description,
                ImageClass = "image-one",
                Rating = ProductForm.Rating,
                StockQuantity = ProductForm.StockQuantity,
                IsFeatured = ProductForm.IsFeatured
            });
        }

        StatusMessage = ProductForm.Id > 0 ? "Product updated successfully." : "Product created successfully.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _storeService.DeleteProductAsync(id);
        StatusMessage = "Product deleted successfully.";
        return RedirectToPage();
    }
}
