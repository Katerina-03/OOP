namespace Shops.Entities;

public class Product
{
    public Product(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name { get; }
}