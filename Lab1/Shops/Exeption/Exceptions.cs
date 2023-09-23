namespace Shops.Exeption;

public class ProductException : Exception
{
    public ProductException()
        : base("This product doesn't exist")
    {
    }
}

public class ShopException : Exception
{
    public ShopException()
        : base("This shop doesn't exist")
    {
    }
}

public class ShopIdException : Exception
{
    public ShopIdException()
        : base("This shop already exist")
    {
    }
}

public class MoneyException : Exception
{
    public MoneyException()
        : base("Not enough money for basket")
    {
    }
}

public class AmountException : Exception
{
    public AmountException()
        : base("Lack of amount")
    {
    }
}