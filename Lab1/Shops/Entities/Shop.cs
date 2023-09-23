using Shops.Exeption;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private Dictionary<string, ProductData> _listofProdutcs = new ();

    public Shop(string name, int id, string adress)
    {
        Name = name;
        Id = id;
        Adress = adress;
    }

    public string Name { get; }
    public int Id { get; }
    public string Adress { get; }

    public Basket Basket { get; set; }

    public Product AddProduct(Product product, int amount, decimal price)
    {
        ArgumentNullException.ThrowIfNull(nameof(product));
        ArgumentNullException.ThrowIfNull(nameof(amount));
        ArgumentNullException.ThrowIfNull(nameof(price));
        if (_listofProdutcs.ContainsKey(product.Name))
        {
            _listofProdutcs[product.Name].Amount += amount;
            _listofProdutcs[product.Name].Price = price;
        }
        else
        {
            ProductData productData = new ProductData(amount, price, product);
            _listofProdutcs.Add(product.Name, productData);
        }

        return product;
    }

    public bool FindProduct(Product product)
    {
        if (!_listofProdutcs.ContainsKey(product.Name))
            throw new ProductException();
        return _listofProdutcs.ContainsKey(product.Name);
    }

    public decimal GetPrice(Product product)
    {
        if (!_listofProdutcs.ContainsKey(product.Name))
            throw new ProductException();
        return _listofProdutcs[product.Name].Price;
    }

    public decimal ChangePrice(Product product, decimal newprice)
    {
        if (!_listofProdutcs.ContainsKey(product.Name))
            throw new ProductException();
        _listofProdutcs[product.Name].Price = newprice;
        return _listofProdutcs[product.Name].Price;
    }

    public void SingleBuy(Person person, Basket basket)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (basket == null) throw new ArgumentNullException(nameof(basket));
        if (!EnoughMoneyForBasket(person, basket))
            throw new MoneyException();
        person.Balance -= GetPriceOfOneProduct(basket);
        RemoveProduct(basket);
    }

    public void ManyBuy(Person person, ICollection<Basket> baskets)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (baskets == null) throw new ArgumentNullException(nameof(baskets));
        if (!EnoughMoneyForBaskets(person, baskets))
            throw new MoneyException();
        person.Balance -= GetPriceOfProducts(baskets);
        RemoveProducts(baskets);
    }

    public decimal GetPriceOfOneProduct(Basket basket)
    {
        if (basket == null) throw new ArgumentNullException(nameof(basket));
        if (!_listofProdutcs.ContainsKey(basket.Product.Name))
            throw new ProductException();
        return basket.Amount * _listofProdutcs[basket.Product.Name].Price;
    }

    public decimal GetPriceOfProducts(ICollection<Basket> baskets)
    {
        decimal price = 0;
        foreach (Basket basket in baskets)
            price += GetPriceOfOneProduct(basket);
        return price;
    }

    private void RemoveProduct(Basket basket)
    {
        if (basket == null) throw new ArgumentNullException(nameof(basket));
        if (basket.Amount > _listofProdutcs[basket.Product.Name].Amount)
            throw new AmountException();
        if (basket.Amount == _listofProdutcs[basket.Product.Name].Amount)
            _listofProdutcs.Remove(basket.Product.Name);
        else if (basket.Amount < _listofProdutcs[basket.Product.Name].Amount)
            _listofProdutcs[basket.Product.Name].Amount -= basket.Amount;
    }

    private void RemoveProducts(ICollection<Basket> baskets)
    {
        if (baskets == null) throw new ArgumentNullException(nameof(baskets));
        foreach (Basket basket in baskets)
            RemoveProduct(basket);
    }

    private bool EnoughMoneyForBasket(Person person, Basket basket)
    {
        return person.Balance >= GetPriceOfOneProduct(basket);
    }

    private bool EnoughMoneyForBaskets(Person person, ICollection<Basket> baskets)
    {
        return person.Balance >= GetPriceOfProducts(baskets);
    }
}