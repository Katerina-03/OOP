using Banks.Banks;
namespace Banks.Transactions;

public abstract class Transaction
{
    public bool State { get; set; } = false;
    public abstract void Cancel();
}