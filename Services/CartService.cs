using System.Text.Json;
using web.net_project.Models;

namespace web.net_project.Services;

public static class CartService
{
    private const string CartKey = "shopping_cart";

    public static List<CartItem> GetCart(ISession session)
    {
        var cartJson = session.GetString(CartKey);
        if (string.IsNullOrWhiteSpace(cartJson))
        {
            return new List<CartItem>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }
        catch
        {
            return new List<CartItem>();
        }
    }

    public static void SaveCart(ISession session, IEnumerable<CartItem> items)
    {
        session.SetString(CartKey, JsonSerializer.Serialize(items));
    }

    public static void AddItem(ISession session, Product product, int quantity = 1)
    {
        var cart = GetCart(session);
        var item = cart.FirstOrDefault(x => x.ProductId == product.Id);

        if (item is null)
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                ImageClass = product.ImageClass,
                Category = product.Category,
                Price = product.Price,
                Quantity = quantity
            });
        }
        else
        {
            item.Quantity += quantity;
        }

        SaveCart(session, cart);
    }

    public static void RemoveItem(ISession session, int productId)
    {
        var cart = GetCart(session);
        var updated = cart.Where(item => item.ProductId != productId).ToList();
        SaveCart(session, updated);
    }

    public static void UpdateQuantity(ISession session, int productId, int quantity)
    {
        if (quantity <= 0)
        {
            RemoveItem(session, productId);
            return;
        }

        var cart = GetCart(session);
        var item = cart.FirstOrDefault(x => x.ProductId == productId);
        if (item is not null)
        {
            item.Quantity = quantity;
            SaveCart(session, cart);
        }
    }

    public static decimal GetCartTotal(IEnumerable<CartItem> items) => items.Sum(item => item.Price * item.Quantity);
}
