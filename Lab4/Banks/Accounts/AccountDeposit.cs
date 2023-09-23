using Banks.Banks;
using Banks.Clients;
using Banks.Exception;

namespace Banks.Accounts;

public class AccountDeposit : Account
{
    private DateTime _creatingTime;
    private decimal _monthCommission;
    private int _days;
    private DateTime _term;
    private decimal _limit = 0;
    public AccountDeposit(Bank bank, Client client, decimal balance, DateTime finnish)
        : base(bank, client)
    {
        _creatingTime = DateTime.Now;
        Balance = balance;
        _days = 0;
        _term = finnish;
    }

    public override void AddSum(decimal money)
    {
        if (money < _limit) throw new BankException("Invalid sum");
        Balance += money;
    }

    public override void Withdraw(decimal money)
    {
        if (money < _limit) throw new BankException("Invalid sum");
        if (_term != DateTime.Today) throw new BankException("Deposit account hasn't expired");
        Balance -= money;
    }

    public override void MonthCharges()
    {
        if (_days != 30)
        {
            _days++;
            _monthCommission += Balance * (Bank.BankRate.DepositPercent() / 365);
        }

        Balance += _monthCommission;
        _monthCommission = 0;
    }

    // transfer bitween accounts
}