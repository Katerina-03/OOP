using Shops.Entities;
using Shops.Exeption;
using Shops.Models;

namespace Shops.Services;

public class Shopmanager
{
    private Dictionary<string, Shop> _shops = new ();

    public Shop BuildShop(string shopName, int id, string adress)
    {
        ArgumentNullException.ThrowIfNull(shopName);
        if (_shops.ContainsKey(shopName))
            throw new ShopIdException();
        Shop shop = new Shop(shopName, id, adress);
        _shops.Add(shopName, shop);
        return shop;
    }

    public Shop FindShop(string shopName)
    {
        ArgumentNullException.ThrowIfNull(nameof(shopName));
        if (!_shops.ContainsKey(shopName))
            throw new ShopException();
        return _shops[shopName];
    }

    public Shop BuyByTheLowestPrice(Basket basket)
    {
        if (basket == null) throw new ArgumentNullException(nameof(basket));
        Shop priorityShop = _shops.Values.First();
        decimal price = priorityShop.GetPriceOfOneProduct(basket);
        if (_shops.Count < 0)
            throw new ShopException();
        if (_shops.Count == 1)
            return priorityShop;
        foreach (Shop shop in _shops.Values)
        {
            decimal newPrice = shop.GetPriceOfOneProduct(basket);
            if (newPrice < price)
            {
                price = newPrice;
                priorityShop = shop;
            }
        }

        return priorityShop;
    }

    public Shop BuyByTheLowestPriceManyProducts(ICollection<Basket> baskets)
    {
        if (baskets == null) throw new ArgumentNullException(nameof(baskets));
        Shop priorityShop = _shops.Values.First();
        decimal price = priorityShop.GetPriceOfProducts(baskets);
        if (_shops.Count < 0)
            throw new ShopException();
        if (_shops.Count == 1)
            return priorityShop;
        foreach (Shop shop in _shops.Values)
        {
            decimal newPrice = shop.GetPriceOfProducts(baskets);
            if (newPrice < price)
            {
                price = newPrice;
                priorityShop = shop;
            }
        }

        return priorityShop;
    }
}