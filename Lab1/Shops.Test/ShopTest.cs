using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class Shop_Test
{
    private const decimal Money = 300;
    private Shopmanager _test = new Shopmanager();
    private int _lastId;
    private Product _product1 = new Product("Tomatoes");
    private Product _product2 = new Product("Chocolate");
    private Person _person = new ("Oleg", Money);

    [Fact]
    public void AddProductInShop()
    {
        Shop shop = _test.BuildShop("Alice", _lastId++, "Радужная,30");
        Product product = shop.AddProduct(new Product("Apples"), 23, 57);
        Assert.True(shop.FindProduct(product));
    }

    [Fact]
    public void ChangeProductPrice()
    {
        Shop shop = _test.BuildShop("Ni", _lastId++, "Парфёнова,50");
        int curAmount = 3;
        decimal curPrice = 14;
        decimal newPrice = 20;
        Product product = shop.AddProduct(new Product("Tomatoes"), curAmount, curPrice);
        shop.ChangePrice(product, newPrice);
        Assert.Equal(newPrice, shop.GetPrice(product));
    }

    [Fact]
    public void BuyLowPriceProduct()
    {
        Shop shop1 = _test.BuildShop("Anna", _lastId++, "Бронницка, 37");
        Shop shop2 = _test.BuildShop("Maria", _lastId++, "Уральска, 6");
        int curAmount = 15;
        decimal curPrice = 20;
        shop1.AddProduct(_product1, curAmount, curPrice);
        shop2.AddProduct(_product1, curAmount, curPrice - 10);
        Basket basket = new Basket(_product1, 12);
        Shop shop = _test.BuyByTheLowestPrice(basket);
        shop.SingleBuy(_person, basket);
        Assert.Equal(Money - (12 * 10), _person.Balance);
    }

    [Fact]
    public void BuyLowPriceManyProducts()
    {
        Shop shop1 = _test.BuildShop("Anna", _lastId++, "Бронницка, 37");
        Shop shop2 = _test.BuildShop("Maria", _lastId++, "Уральска, 6");

        shop1.AddProduct(_product1, 1, 10);
        shop1.AddProduct(_product2, 1, 5);

        shop2.AddProduct(_product1, 1, 11);
        shop2.AddProduct(_product2, 1, 2);
        List<Basket> baskets = new List<Basket>();
        baskets.Add(new Basket(_product1, 1));
        baskets.Add(new Basket(_product2, 1));

        Shop shop = _test.BuyByTheLowestPriceManyProducts(baskets);
        shop.ManyBuy(_person, baskets);
        Assert.Equal(Money - (11 + 2), _person.Balance);
    }
}