using Banks.Banks;
using Banks.Clients;

namespace Banks.Accounts;

public abstract class Account
{
    private Client _client;
    public Account(Bank bank, Client client)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(client);
        Bank = bank;
        _client = client;
        AccountId = Guid.NewGuid();
        Balance = 0;
    }

    public Guid AccountId { get; }
    public decimal Balance { get; protected set; }
    protected Bank Bank { get; }
    public abstract void AddSum(decimal money);
    public abstract void Withdraw(decimal money);
    public abstract void MonthCharges();
}