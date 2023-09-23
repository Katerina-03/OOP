using Shops.Entities;

namespace Shops.Models;

public class Basket
{
    public Basket(Product product, int amount)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        Amount = amount;
    }

    public int Amount { get; }
    public Product Product { get; }
}