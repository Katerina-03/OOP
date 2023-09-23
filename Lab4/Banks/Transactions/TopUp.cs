using Banks.Accounts;
using Banks.Banks;
using Banks.Exception;

namespace Banks.Transactions;

public class TopUp : Transaction
{
    private Account _account;
    private decimal _money;
    public TopUp(Account account, decimal money)
    {
        _account = account;
        _money = money;
        account.AddSum(_money);
    }

    public override void Cancel()
    {
        if (State == true)
            throw new BankException("Transaction already canceled");
        if (State == false)
        {
            _account.Withdraw(_money);
            State = true;
        }
    }
}
