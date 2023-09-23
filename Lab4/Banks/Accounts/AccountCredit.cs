using Banks.Banks;
using Banks.Clients;
using Banks.Exception;

namespace Banks.Accounts;

public class AccountCredit : Account
{
    private DateTime _creatingTime;
    private decimal _limit = 0;

    public AccountCredit(Bank bank, Client client, decimal balance)
        : base(bank, client)
    {
        _creatingTime = DateTime.Now;
        Balance = balance;
    }

    public override void AddSum(decimal money)
    {
        if (money < _limit)
        {
            Balance += money - Bank.BankRate.CommissionPercent();
        }

        Balance += money;
    }

    public override void Withdraw(decimal money)
    {
        if (Balance < _limit)
        {
            Balance -= money - Bank.BankRate.CommissionPercent();
        }
        else
        {
            Balance -= money;
        }
    }

    public override void MonthCharges()
    {
        throw new BankException("Month charges for credit account doesn't exist");
    }
}