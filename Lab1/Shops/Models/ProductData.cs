using Shops.Entities;

namespace Shops.Models;

public class ProductData
{
    public ProductData(int amount, decimal price, Product product)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price));
        Amount = amount;
        Price = price;
        Product = product;
    }

    public int Amount { get; set; }

    public decimal Price { get; set; }

    public Product Product { get; }
}