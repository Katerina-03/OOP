using Banks.Accounts;
using Banks.Exception;

namespace Banks.Transactions;

public class Withdraw : Transaction
{
    private Account _account;
    private decimal _money;
    public Withdraw(Account account, decimal money)
    {
        _account = account;
        _money = money;
        account.Withdraw(_money);
    }

    public override void Cancel()
    {
        if (State == true)
            throw new BankException("Transaction already canceled");
        if (State == false)
        {
            _account.AddSum(_money);
            State = true;
        }
    }
}