using Banks.Exception;

namespace Banks.Banks;

public class BankRate
{
    private readonly Dictionary<AccountsSettings, decimal> _bankRate = new ();

    public BankRate(decimal depositPercent, decimal debitPercent, decimal sumForDoubtful, decimal commission)
    {
        if (depositPercent < 0 && depositPercent > 100)
            throw new BankException("Invalid degree for percent");
        if (debitPercent < 0 && debitPercent > 100)
            throw new BankException("Invalid degree for percent");
        if (commission < 0 && commission > 100)
            throw new BankException("Invalid degree for percent");
        if (sumForDoubtful < 0)
            throw new BankException("Invalid degree for percent");
        _bankRate.Add(AccountsSettings.Commission, commission);
        _bankRate.Add(AccountsSettings.DebitPercent, debitPercent);
        _bankRate.Add(AccountsSettings.DepositPercent, depositPercent);
        _bankRate.Add(AccountsSettings.SumForDoubtful, sumForDoubtful);
    }

    public IReadOnlyCollection<decimal> BankRates => _bankRate.Values;

    public decimal DebitPercent()
    {
        return _bankRate[AccountsSettings.DebitPercent];
    }

    public decimal DepositPercent()
    {
        return _bankRate[AccountsSettings.DepositPercent];
    }

    public decimal CommissionPercent()
    {
        return _bankRate[AccountsSettings.Commission];
    }

    public void ChangeRate(decimal newRate, AccountsSettings accountsSettings)
    {
        if (newRate < 0 && newRate > 100)
            throw new BankException("Invalid degree for percent");
        if (accountsSettings != AccountsSettings.SumForDoubtful)
            throw new BankException("Invalid account type");
        _bankRate[accountsSettings] = newRate;
    }

    public void ChangeSum(decimal newSum, AccountsSettings accountsSettings)
    {
        if (newSum < 0)
            throw new BankException("Invalid degree for percent");
        if (accountsSettings == AccountsSettings.SumForDoubtful)
        {
            _bankRate[accountsSettings] = newSum;
        }
        else
        {
            throw new BankException("Invalid account type");
        }
    }
}