using Banks.Clients;
using Banks.Exception;

namespace Banks.Banks;

public class CentralBank
{
    private static CentralBank _centralBank;
    private readonly List<Bank> _banks;

    public CentralBank()
    {
        _banks = new List<Bank>();
    }

    public IReadOnlyCollection<Bank> Banks => _banks;

    public static CentralBank GetInstance()
    {
        return _centralBank ?? (_centralBank = new CentralBank());
    }

    public Bank FindBank(string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _banks.FirstOrDefault(x => x.BankName == name);
    }

    public void AddBank(Bank bank)
    {
        if (bank is null)
            throw new BankException("Invalid bank");
        if (CheckBank(bank))
            _banks.Add(bank);
    }

    public void DeleteBank(Bank bank, string bankName)
    {
        if (bank is null)
            throw new BankException("Invalid bank");
        if (!CheckBank(bank))
            throw new BankException("Bank doesn't exist");
        _banks.Remove(bank);
    }

    private bool CheckBank(Bank bank)
    {
        return _banks.Any(x => x.BankName == bank.BankName);
    }
}
