namespace Shops.Entities;

public class Person
{
    public Person(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }

    public string Name { get; }
    public decimal Balance { get; set; }
}