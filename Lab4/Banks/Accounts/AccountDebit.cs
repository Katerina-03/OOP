using System.Data.SqlTypes;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Banks.Exception;

namespace Banks.Accounts;

public class AccountDebit : Account
{
    private DateTime _creatingTime;
    private decimal _monthcommission;
    private int _days;
    private decimal _limit = 0;
    public AccountDebit(Bank bank, Client client, decimal balance)
        : base(bank, client)
    {
        _creatingTime = DateTime.Now;
        _days = 0;
        Balance = balance;
    }

    public override void AddSum(decimal money)
    {
        if (money < _limit) throw new BankException("Invalid sum");
        Balance += money;
    }

    public override void Withdraw(decimal money)
    {
        if (money < _limit) throw new BankException("Invalid sum");
        if (money > Balance) throw new BankException("Can't go negative");
        Balance -= money;
    }

    public override void MonthCharges()
    {
        if (_days != 30)
        {
            _days++;
            _monthcommission += Balance * (Bank.BankRate.DebitPercent() / 365);
        }

        Balance += _monthcommission;
        _monthcommission = 0;
    }
}