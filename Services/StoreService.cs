using Microsoft.EntityFrameworkCore;
using web.net_project.Data;
using web.net_project.Models;

namespace web.net_project.Services;

public class StoreService
{
    private readonly ApplicationDbContext _db;

    public StoreService(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Seeds the 6 default products if the Products table is empty.
    /// Should be called once at application startup.
    /// </summary>
    public async Task SeedIfEmptyAsync()
    {
        if (await _db.Products.AnyAsync())
            return;

        var defaultProducts = new List<Product>
        {
            new()
            {
                Name = "Pro Max Degreaser",
                Category = "Kitchen",
                Tag = "Best seller",
                Price = 1499.00m,
                Description = "Heavy-duty formula for stovetops, sinks, and stubborn kitchen grease.",
                ImageClass = "image-one",
                Rating = 5,
                StockQuantity = 20,
                IsFeatured = true
            },
            new()
            {
                Name = "Fresh Flow Cleaner",
                Category = "Bathroom",
                Tag = "Popular",
                Price = 1299.00m,
                Description = "Removes soap scum and everyday buildup while leaving a fresh, clean scent.",
                ImageClass = "image-two",
                Rating = 4,
                StockQuantity = 16,
                IsFeatured = true
            },
            new()
            {
                Name = "Glass & Surface Kit",
                Category = "Office",
                Tag = "New",
                Price = 1899.00m,
                Description = "Perfect for counters, desks, and office surfaces that need quick attention.",
                ImageClass = "image-three",
                Rating = 5,
                StockQuantity = 18,
                IsFeatured = true
            },
            new()
            {
                Name = "Bathroom Bright",
                Category = "Bathroom",
                Tag = "Eco-safe",
                Price = 999.00m,
                Description = "A low-foam cleaner designed for tub, tile, and sink maintenance.",
                ImageClass = "image-four",
                Rating = 4,
                StockQuantity = 14,
                IsFeatured = false
            },
            new()
            {
                Name = "Floor Shine Pro",
                Category = "Floorcare",
                Tag = "Premium",
                Price = 2199.00m,
                Description = "Restores shine to hard floors without streaking or residue.",
                ImageClass = "image-five",
                Rating = 5,
                StockQuantity = 12,
                IsFeatured = false
            },
            new()
            {
                Name = "Kitchen Spark Pack",
                Category = "Kitchen",
                Tag = "Bundle",
                Price = 2799.00m,
                Description = "A value bundle for complete kitchen cleaning and maintenance tasks.",
                ImageClass = "image-six",
                Rating = 5,
                StockQuantity = 9,
                IsFeatured = false
            }
        };

        await _db.Products.AddRangeAsync(defaultProducts);
        await _db.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
    {
        return await _db.Products.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<IReadOnlyList<Product>> GetFeaturedProductsAsync()
    {
        return await _db.Products.Where(p => p.IsFeatured).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _db.Products.FindAsync(productId);
    }

    public async Task AddProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.ImageClass))
            product.ImageClass = "image-one";

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var existing = await _db.Products.FindAsync(product.Id);
        if (existing is null)
            return;

        existing.Name = product.Name;
        existing.Category = product.Category;
        existing.Tag = product.Tag;
        existing.Price = product.Price;
        existing.Description = product.Description;
        existing.ImageClass = string.IsNullOrWhiteSpace(product.ImageClass)
            ? existing.ImageClass
            : product.ImageClass;
        existing.Rating = product.Rating;
        existing.StockQuantity = product.StockQuantity;
        existing.IsFeatured = product.IsFeatured;

        await _db.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product is not null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}
