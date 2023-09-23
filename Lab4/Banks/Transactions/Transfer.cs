using Banks.Accounts;
using Banks.Exception;

namespace Banks.Transactions;

public class Transfer : Transaction
{
    private Account _toAccount;
    private Account _fromAccount;
    private decimal _money;
    public Transfer(Account toAccount, Account fromAccount, decimal money)
    {
        _toAccount = toAccount;
        _fromAccount = fromAccount;
        _money = money;
        fromAccount.Withdraw(_money);
        toAccount.AddSum(_money);
    }

    public override void Cancel()
    {
        if (State == true)
            throw new BankException("Transaction already canceled");
        if (State == false)
        {
            _toAccount.Withdraw(_money);
            _fromAccount.AddSum(_money);
            State = true;
        }
    }
}